// <author>TakoMan02</author>
// <summary>Tools.cs is the tools that belong to no specific class or are not related to EE.</summary>

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using PlayerIOClient;
using Skylight.Physics;


namespace Skylight
{
    /// <summary>
    ///     Tools that are available to the core of the program (converting a player id or name into
    ///     a player object) and internal methods are mostly stored here.
    /// </summary>

    public static class RoomAccessor
    {
        public static int Width { get; set; }
        public static int Height { get; set; }
    
}
    public static class Tools
    {

        
        /// <summary>
        ///     Delegate ProgramEvent
        /// </summary>
        /// <param name="message">The message.</param>
        public delegate void ProgramEvent(string message);

        /// <summary>
        ///     The guest client
        /// </summary>
        internal static readonly Lazy<Client> GuestClient =
            new Lazy<Client>(() => PlayerIO.QuickConnect.SimpleConnect(Config.PlayerioGameId, "guest", "guest"));

        /// <summary>
        ///     Occurs when a program message is sent.
        /// </summary>
        public static event ProgramEvent ProgramMessage = delegate { };

        /// <summary>
        ///     Gets the winners.
        /// </summary>
        /// <param name="r">The room.</param>
        /// <returns>A list of Players who have touched the trophy</returns>
        public static List<Player> GetWinners(Room r)
        {
            return r.OnlinePlayers.Where(p => p.HasSilverCrown).ToList();
        }

        /// <summary>
        ///     Gets the crown holder.
        /// </summary>
        /// <param name="r">The room.</param>
        /// <returns>The Player who holds the crown (if there is one).</returns>
        public static Player GetCrownHolder(Room r)
        {
            foreach (var p in r.OnlinePlayers.Where(p => p.HasCrown))
            {
                return p;
            }

            SkylightMessage("Could not find crown holder.");
            return null;
        }

        /// <summary>
        ///     Gets the player by identifier.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <param name="r">The room.</param>
        /// <param name="onlyReturnBots">if set to <c>true</c> [only return bots].</param>
        /// <returns>Player.</returns>
        public static Player GetPlayerById(int id, Room r, bool onlyReturnBots = false)
        {
            foreach (var p in r.OnlinePlayers.Where(p => p.Id == id).Where(p => !onlyReturnBots || p.IsBot))
            {
                return p;
            }

            SkylightMessage("Could not find player " + id + " in " + r.Name);
            return null;
        }

        /// <summary>
        ///     Gets the name of the player by.
        /// </summary>
        /// <param name="name">The name.</param>
        /// <param name="r">The room.</param>
        /// <param name="onlyReturnBots">if set to <c>true</c> [only return bots].</param>
        /// <returns>Player.</returns>
        public static Player GetPlayerByName(string name, Room r, bool onlyReturnBots = false)
        {
            foreach (var p in r.OnlinePlayers.Where(p => p.Name == name).Where(p => !onlyReturnBots || p.IsBot))
            {
                return p;
            }

            SkylightMessage("Could not find player " + name + " in " + r.Name);
            return null;
        }

        /// <summary>
        ///     Gets the room.
        /// </summary>
        /// <param name="name">The name.</param>
        /// <returns>Room.</returns>
        public static Room GetRoom(string name)
        {
            foreach (var r in Room.JoinedRooms.Where(r => r.Name == name))
            {
                return r;
            }

            SkylightMessage("Could not find room \"" + name + "\"");
            return null;
        }


        /// <summary>
        ///     Main logging method.
        /// </summary>
        /// <param name="m">The message.</param>
        internal static void SkylightMessage(string m)
        {
            ProgramMessage(m);
        }

        /// <summary>
        ///     Derots the specified world key.
        /// </summary>
        /// <param name="worldKey">The world key.</param>
        /// <returns>Derotted world key</returns>
        internal static string Derot(string worldKey)
        {
            var array = worldKey.ToCharArray();

            for (var i = 0; i < array.Length; i++)
            {
                int number = array[i];

                if (number >= 'a' && number <= 'z')
                {
                    if (number > 'm')
                    {
                        number -= 13;
                    }
                    else
                    {
                        number += 13;
                    }
                }
                else if (number >= 'A' && number <= 'Z')
                {
                    if (number > 'M')
                    {
                        number -= 13;
                    }
                    else
                    {
                        number += 13;
                    }
                }

                array[i] = (char) number;
            }

            return new string(array);
        }

        /// <summary>
        ///     Parses the URL.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <returns>A parsed room id</returns>
        internal static string ParseUrl(string id)
        {
            // If it matches any type of URL and has 13 characters at the end, return the last 13 characters.
            // Supports haphazard copy/pasting.
            if (Regex.IsMatch(id, "[htp:/w.evrybodis.comga]{0,36}[a-zA-Z0-9_-]{13}"))
            {
                var parsedUrl = id.Substring(id.ToCharArray().Length - 13, 13);
                return parsedUrl;
            }

            return id;
        }

        /// <summary>
        ///     Deserializes the initialize.
        /// </summary>
        /// <param name="m">The message</param>
        /// <param name="start">The start.</param>
        /// <param name="r">The room.</param>
        /// <returns>A list of blocks which is the room.</returns>
        internal static List<Block> DeserializeInit(Message m, uint start, Room r)
        {
            var list = new List<Block>();
            try
            {
                //// FULL CREDIT TO BASS5098 FOR THE FOLLOWING CODE
                //// I wrote it in my own way in C# but Bass5098 made the original.
                //// Without it I would not know how to do this.
                //// https://github.com/kevin-brown/ee-level-editor/blob/master/LevelEditor/WorldConverter.vb
                //// Lines 438-507
                //// Praise him. (this is mainly due to my laziness)

                // First, fill the entire map with blank blocks (so that you don't get null exceptions).

                for (var x = 0; x <= RoomAccessor.Width; x++)
                {
                    for (var y = 0; y <= RoomAccessor.Height; y++)
                    {
                        for (var z = 0; z < 2; z++)
                        {
                            r.Map[x, y, z] = new Block(0, x, y, z);
                        }
                    }
                }

                // And now replace empty blocks with the ones that already exist.
                var messageIndex = start;

                // Iterate through each internal set of messages.
                while (messageIndex < m.Count)
                {
                    // If it is a string, exit.
                    if (m[messageIndex] is string)
                    {
                        break;
                    }

                    // The ID is first.
                    var blockId = m.GetInteger(messageIndex);
                    messageIndex++;

                    // Then the z.
                    var z = m.GetInteger(messageIndex);
                    messageIndex++;

                    // Then the list of all X coordinates of given block
                    var xa = m.GetByteArray(messageIndex);
                    messageIndex++;

                    // Then the list of all Y coordinates of given block
                    var ya = m.GetByteArray(messageIndex);
                    messageIndex++;

                    int rotation = 0, note = 0, type = 0, portalId = 0, destination = 0, coins = 0;
                    bool isVisible = false, isGate = false;
                    var roomDestination = "";

                    // Get the variables that are unique to the current block
                    switch (blockId)
                    {
                        case BlockIds.Action.Portals.Invisible:
                        case BlockIds.Action.Portals.Normal:
                            rotation = m.GetInteger(messageIndex);
                            messageIndex++;
                            portalId = m.GetInteger(messageIndex);
                            messageIndex++;
                            destination = m.GetInteger(messageIndex);
                            messageIndex++;
                            isVisible = (blockId != BlockIds.Action.Portals.Invisible);
                            break;
                        case BlockIds.Action.Portals.World:
                            roomDestination = m.GetString(messageIndex);
                            messageIndex++;
                            break;
                        case BlockIds.Action.Gates.Coin:
                        case BlockIds.Action.Doors.Coin:
                            coins = m.GetInteger(messageIndex);
                            messageIndex++;
                            isGate = blockId == BlockIds.Action.Gates.Coin;
                            break;
                        case BlockIds.Action.Music.Percussion:
                            type = m.GetInteger(messageIndex);
                            messageIndex++;
                            break;
                        case BlockIds.Action.Music.Piano:
                            note = m.GetInteger(messageIndex);
                            messageIndex++;
                            break;
                        case BlockIds.Action.Hazards.Spike:
                        case BlockIds.Decorative.SciFi2013.Orangestraight:
                        case BlockIds.Decorative.SciFi2013.Orangebend:
                        case BlockIds.Decorative.SciFi2013.Greenstraight:
                        case BlockIds.Decorative.SciFi2013.Greenbend:
                        case BlockIds.Decorative.SciFi2013.Bluestraight:
                        case BlockIds.Decorative.SciFi2013.Bluebend:
                            rotation = m.GetInteger(messageIndex);
                            messageIndex++;
                            break;
                    }


                    // Some variables to simplify things.

                    for (var pos = 0; pos < ya.Length; pos += 2)
                    {
                        // Extract the X and Y positions from the array.
                        var x = (xa[pos]*256) + xa[pos + 1];
                        var y = (ya[pos]*256) + ya[pos + 1];

                        // Ascertain the block from the ID.
                        // Add block accordingly.
                        switch (blockId)
                        {
                            case BlockIds.Action.Portals.Invisible:
                            case BlockIds.Action.Portals.Normal:
                                list.Add(new PortalBlock(
                                    x,
                                    y,
                                    rotation,
                                    portalId,
                                    destination,
                                    isVisible));
                                break;
                            case BlockIds.Action.Portals.World:
                                list.Add(new RoomPortalBlock(
                                    x,
                                    y,
                                    roomDestination));
                                break;
                            case BlockIds.Action.Gates.Coin:
                            case BlockIds.Action.Doors.Coin:
                                list.Add(new CoinBlock(
                                    x,
                                    y,
                                    coins,
                                    isGate));
                                break;
                            case BlockIds.Action.Music.Percussion:
                                list.Add(new PercussionBlock(
                                    x,
                                    y,
                                    type));
                                break;
                            case BlockIds.Action.Music.Piano:
                                list.Add(new PianoBlock(
                                    x,
                                    y,
                                    note));
                                break;
                            default:
                                list.Add(new Block(
                                    blockId,
                                    x,
                                    y,
                                    z,
                                    rotation));
                                break;
                        }
                    }
                }
            }
            catch (Exception e)
            {
                SkylightMessage("Error loading existing blocks:\n" + e);
            }

            SkylightMessage("Done loading blocks");
            return list;
        }

        // Return the correct portal ID based on whether or not the portal is visible or invisible.
        /// <summary>
        ///     Portals the identifier by visible.
        /// </summary>
        /// <param name="visible">if set to <c>true</c> [visible].</param>
        /// <returns>System.Int32.</returns>
        internal static int PortalIdByVisible(bool visible)
        {
            return visible ? BlockIds.Action.Portals.Normal : BlockIds.Action.Portals.Invisible;
        }

        // Return the correct coin ID based based on whether or not the block is gate or door
        /// <summary>
        ///     Coins the identifier by gate.
        /// </summary>
        /// <param name="isGate">if set to <c>true</c> [is gate].</param>
        /// <returns>System.Int32.</returns>
        internal static int CoinIdByGate(bool isGate)
        {
            return isGate ? BlockIds.Action.Gates.Coin : BlockIds.Action.Doors.Coin;
        }
    }
}