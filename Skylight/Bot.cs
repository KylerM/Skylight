﻿namespace Skylight
{
    using PlayerIOClient;
    using System;
    using System.Collections.Generic;
    using System.Threading;
    public partial class Bot : Player
    {
        private bool
            isConnected,
            joined;

        private Client client;
        private Client vers_Client;

        // In milliseconds.
        private int
            blockDelay = 10,
            speechDelay = 1000;

        private string chatPrefix = "";

        private readonly string
            emailOrToken,
            passwordOrToken;

        private readonly AccountType accType;

        private Connection connection = null;

        private Out push = new Out();

        private Room r = new Room(null);

        public static Room currentRoom { get; set; }
        /// <param name="password">Make this field null if it isn't needed for your log-in method.</param>
        public Bot(Room r,
                   string emailOrToken = Utilities.GuestEmail,
                   string passwordOrToken = Utilities.GuestPassword,
                   AccountType accType = AccountType.Regular)
            : base(r, 0, "", 0, 0.0, 0.0, false, false, true, 0, false, false, 0)
        {
            this.emailOrToken = emailOrToken;
            this.passwordOrToken = passwordOrToken;
            this.R = r;
            currentRoom = r;
            this.accType = accType;
            this.ShouldTick = true;
        }

        public bool IsConnected { get; internal set; }

        public bool Joined { get; internal set; }

        public bool ShouldTick { get; set; }

        public Client Client { get; internal set; }

        public string gameVersion { get; internal set; }

        public int BlockDelay
        {
            get { return this.BlockDelay; }

            set
            {
                if (this.blockDelay > 0)
                {
                    this.blockDelay = value;
                }
            }
        }

        public int SpeechDelay
        {
            get { return this.SpeechDelay; }

            set
            {
                if (this.speechDelay > 0)
                {
                    this.speechDelay = value;
                }
            }
        }

        public string ChatPrefix { get; set; }

        public Out Push { get; internal set; }

        public Room R { get; internal set; }

        static public In i = new In();

        public Connection Connection { get; internal set; }

        // Public methods
        public void LogIn()
        {
            if (this.isConnected)
            {
                Logging.SkylightMessage("You are already connected. Please run Bot.Disconnect() to change the connection.");
                return;
            }
            try
            {
                switch (this.accType)
                {
                    case AccountType.Regular:
                        if (this.emailOrToken == Utilities.GuestEmail && this.passwordOrToken == Utilities.GuestPassword)
                        {
                            this.Client = Utilities.GuestClient.Value;
                        }
                        else
                        {
                            this.Client = PlayerIO.QuickConnect.SimpleConnect(Utilities.GameID, this.emailOrToken, this.passwordOrToken);
                        }
                        break;

                    case AccountType.Facebook:
                        this.Client = PlayerIO.QuickConnect.FacebookOAuthConnect(Utilities.GameID, this.emailOrToken, null);
                        break;

                    case AccountType.Kongregate:
                        this.Client = PlayerIO.QuickConnect.KongregateConnect(Utilities.GameID, this.emailOrToken, this.passwordOrToken);
                        break;

                    case AccountType.ArmorGames:
                        ArmorGamesConnect();
                        break;
                    default:
                        Logging.SkylightMessage("UNKNOWN ACCOUNT TYPE");
                        break;
                }
                updateGameVersion();
            }
            catch (PlayerIOError e)
            {
                Logging.SkylightMessage("Cannot log in: " + e.Message);
                this.IsConnected = false;
                return;
            }

            this.IsConnected = true;
        }

        private void updateGameVersion()
        {
            this.gameVersion = (Utilities.NormalRoom + Convert.ToString(this.Client.BigDB.Load("config", "config")["version"])).ToString();
        }

        private void ArmorGamesConnect()
        {
            var c = Utilities.GuestClient.Value.Multiplayer.JoinRoom("", null);
            c.OnMessage += (sender, message) =>
            {
                if (message.Type != "auth") return;

                if (message.Count == 0)
                    Logging.SkylightMessage("Cannot log in using ArmorGames. The response from the auth server is wrong.");
                else
                {
                    this.Client = PlayerIOClient.PlayerIO.Connect(Utilities.GameID, "secure",
                                                                  message.GetString(0), message.GetString(1),
                                                                  "armorgames");
                }

                c.Disconnect();
            };

            c.Send("auth", this.emailOrToken, this.passwordOrToken);
        }

        public void Join(bool createRoom = true)
        {

            if (!this.IsConnected)
            {
                // Log in
                this.LogIn();

                // If you didn't connect, it must have failed.
                if (!this.IsConnected)
                {
                    Logging.SkylightMessage("Error: connection failed!");
                    return;
                }
            }

            // Parse the level ID (because some people like to put full URLs in).
            this.R.Id = Utilities.ParseURL(this.R.Id);

            try
            {
                if (createRoom)
                {
                    // Join room
                    this.Connection = this.Client.Multiplayer.CreateJoinRoom(
                        this.R.Id,                         // RoomId   (URL)
                        this.gameVersion,                // RoomType (Server)
                        true,                              // Visible
                        new Dictionary<string, string>(),  // RoomData
                        new Dictionary<string, string>()); // JoinData
                }
                else
                {
                    this.Connection = this.Client.Multiplayer.JoinRoom(
                        this.R.Id,
                        new Dictionary<string, string>()
                        );
                }
                // Update room data
                Room.JoinedRooms.Add(this.R);

                Logging.SkylightMessage("CONNECTING VALUE: " + Convert.ToString(this.Connection));
                // Everyone gets a connection.
                this.R.Connections.Add(this.Connection);

                // The following section deals with filtering messages from the client.
                // Every bot receives info from the room, because some of it is exclusive to the bot.
                // We call those "personal" pulls.
                // They are exactly the same as the main pull, except In.IsPersonal = true.
                
                i.IsPersonal = true;
                i.Source = this.R;
                i.Bot = this;
                this.Connection.OnMessage += i.OnMessage;
                this.R.Pulls.Add(i);

                // However, everything else only needs one bot to handle. Things like chat and movement.
                // We don't need five bots firing an event every time someone chats.
                // except when they are located in different rooms, which would be an exception.
                if (!this.R.HasPull)
                {
                    this.R.HasPull = true;

                    this.R.Receiver = this;

                    this.Connection.OnMessage += this.R.Pull.OnMessage;
                    this.R.Pull.IsPersonal = false;
                    this.R.Pull.Bot = this;
                    this.R.Pull.Source = this.R;
                }

                // Once everything is internal settled, send the inits.
                this.Connection.Send("init");
                this.Connection.Send("init2");

                // this.connection is null so... hmm...?
                this.R.OnlinePlayers.Add(this);

                this.Joined = true;

                while (!this.R.BlocksLoaded)
                {
                    Thread.Sleep(50); //http://stackoverflow.com/questions/11809277/

                }
            }
            catch (Exception e)
            {
                Logging.SkylightMessage("Unable to join room \"" + this.R.Id + "\": " + e.Message);
                Logging.SkylightMessage("R ID: " + Convert.ToString(i.Source));
                return;
            }
        }

        public void Disconnect()
        {
            // Basically undo everything you already did.
            this.Connection.Disconnect();

            this.Client = null;
            this.Connection = null;
            this.Push = null;
            this.IsConnected = false;
            this.Joined = false;
        }




        public enum AccountType : sbyte
        {
            Regular = 0,
            Facebook = 1,
            Kongregate = 2,
            ArmorGames = 3
        }
    }
}
