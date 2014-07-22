﻿// <author>TakoMan02</author>
// <summary>Player.cs describes a singular player in an EE world.</summary>

using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using Skylight.Blocks;

namespace Skylight
{
    /// <summary>
    ///     Class Player.
    /// </summary>
    public class Player
    {
        // Private instance fields
        /// <summary>
        ///     The maximum thrust
        /// </summary>
        private const double MaxThrust = 0.2;

        /// <summary>
        ///     The thrust burn off duration
        /// </summary>
        private const double ThrustBurnOff = 0.01;

        /// <summary>
        ///     The gravity multiplier
        /// </summary>
        private const double GravityMultiplier = 1;

        /*
        /// <summary>
        ///     The jump boost
        /// </summary>
 *      Before this is re-initialized the potion id that concerns this has
 *      to be looked up. Only then can JumpBoost be true.
        private const bool JumpBoost = false;
*/

        /// <summary>
        ///     The admins. This is never used.
        /// </summary>
        public static readonly List<string> Admins = new List<string>
        {
            "benjaminsen",
            "cyclone",
            "toby",
            "rpgmaster2000",
            "mrshoe",
            "mrvoid"
        };

        /// <summary>
        ///     The boost
        /// </summary>
        private readonly double Boost;

        /// <summary>
        ///     The gravity
        /// </summary>
        private readonly double Gravity;

        /// <summary>
        ///     The mud buoyancy
        /// </summary>
        private readonly double MudBuoyancy;

        /// <summary>
        ///     The mud drag
        /// </summary>
        private readonly double MudDrag;

        /// <summary>
        ///     The no modifier drag x
        /// </summary>
        private readonly double NoModifierDragX;

        /// <summary>
        ///     The no modifier drag y
        /// </summary>
        private readonly double NoModifierDragY;

        /// <summary>
        ///     The size of the block
        /// </summary>
        protected double Boost;
        private readonly int Size;

        /// <summary>
        ///     The switch opened
        /// </summary>
        private readonly bool SwitchOpened;

        /// <summary>
        ///     The water buoyancy
        /// </summary>
        private readonly double WaterBuoyancy;

        /// <summary>
        ///     The water drag
        /// </summary>
        private readonly double WaterDrag;

        /// <summary>
        ///     Whether or not the player has died
        /// </summary>
        private readonly bool _isDead;

        /// <summary>
        ///     Whether or not the player is invulnerable
        /// </summary>
        private readonly bool _isInvulnerable;

        /// <summary>
        ///     Whether or not the player is thrusting
        /// </summary>
        private bool Isclubmember = false;
        private readonly bool _isThrusting;

        /// <summary>
        ///     The player is a zombie
        /// </summary>
        private readonly bool _isZombie;

        /// <summary>
        ///     The multiplyer.
        /// </summary>
        protected double ModifierY = 0;
        private readonly double _mult;

        /// <summary>
        ///     The main event queue.
        /// </summary>
        protected double MudBuoyancy;
        private readonly Queue<int> _queue = new Queue<int>();

        /// <summary>
        ///     The checkpoint at the x coordinate
        /// </summary>
        protected double MudDrag;
        public int CheckpointX = -1;

        /// <summary>
        ///     The checkpoint at the y coordinate
        /// </summary>
        protected double NoModifierDragX;
        public int CheckpointY = -1;

        /// <summary>
        ///     The current block identifier
        /// </summary>
        protected double NoModifierDragY;
        private int CurrentBlockId;

        /// <summary>
        ///     The horizontal position
        /// </summary>
        protected int Size;
        internal int Horizontal = 0;

        /// <summary>
        ///     Whether the player is a club member or not
        /// </summary>
        protected double SpeedX = 0;
        private bool Isclubmember = false;

        /// <summary>
        ///     The modifier x
        /// </summary>
        protected double SpeedY = 0;
        private double ModifierX;

        /// <summary>
        ///     The modifier y
        /// </summary>
        public bool SwitchOpened = false;
        private double ModifierY;

        /// <summary>
        ///     The speed x
        /// </summary>
        private double SpeedX;

        /// <summary>
        ///     The speed y
        /// </summary>
        protected double WaterBuoyancy;
        private double SpeedY;

        /// <summary>
        ///     The vertical
        /// </summary>
        protected double WaterDrag;
        internal int Vertical = 0;

        /// <summary>
        ///     The x position
        /// </summary>
        public double X { get; internal set; }

        /// <summary>
        ///     The y position
        /// </summary>
        public double Y { get; internal set; }

        /// <summary>
        ///     The _animoffset
        /// </summary>
        private double _animoffset;

        /// <summary>
        ///     The _cluboffset
        /// </summary>
        private double _cluboffset;

        /// <summary>
        ///     The _current sx
        /// </summary>
        private double _currentSx;

        /// <summary>
        ///     The _current sy
        /// </summary>
        private double _currentSy;

        /// <summary>
        ///     The _current thrust
        /// </summary>
        private double _currentThrust;

        /// <summary>
        ///     The _CX
        /// </summary>
        private int _cx;

        /// <summary>
        ///     The _cy
        /// </summary>
        private int _cy;

        /// <summary>
        ///     The _deadoffset
        /// </summary>
        private double _deadoffset;

        /// <summary>
        ///     The _donex
        /// </summary>
        private bool _donex;

        /// <summary>
        ///     The _doney
        /// </summary>
        private bool _doney;

        /// <summary>
        ///     The horizontal acceleration
        /// </summary>
        private double _horizontalAcceleration;

        /*
        private bool injump = false;
*/
        /*
        /// <summary>
        /// The _is cursed
        /// </summary>
        private bool _isCursed;
*/

        /// <summary>
        ///     The player has god mode
        /// </summary>
        private bool _isgodmod;

        /// <summary>
        ///     The coordinate of the player's last occurance with a portal
        /// </summary>
        private Point _lastPortal;

        /*
        /// <summary>
        /// The _last respawn
        /// </summary>
        private double _lastRespawn;
*/
        /*
        private int mod = 0;
*/

        /// <summary>
        ///     The _modoffset
        /// </summary>
        private double _modoffset;

        /// <summary>
        ///     The _morx
        /// </summary>
        private int _morx;

        /// <summary>
        ///     The _mory
        /// </summary>
        private int _mory;

        /// <summary>
        ///     The _name
        /// </summary>
        private string _name;

        /// <summary>
        ///     The old horizontal acceleration
        /// </summary>
        private double _oldHorizontalAcceleration;

        /// <summary>
        ///     The old vertical acceleration
        /// </summary>
        private double _oldVerticalAcceleration;

        /// <summary>
        ///     The _osx
        /// </summary>
        private double _osx;

        /// <summary>
        ///     The _osy
        /// </summary>
        private double _osy;

        /// <summary>
        ///     The _overlapy
        /// </summary>
        private int _overlapy;

        /// <summary>
        ///     The _ox
        /// </summary>
        private double _ox;

        /// <summary>
        ///     The _oy
        /// </summary>
        private double _oy;

        /// <summary>
        ///     The _reminder x
        /// </summary>
        private double _reminderX;

        /// <summary>
        ///     The _reminder y
        /// </summary>
        private double _reminderY;

        /// <summary>
        ///     The_spacejustdown
        /// </summary>
        private bool _spacejustdown;

        /// <summary>
        ///     The _TX
        /// </summary>
        private double _tx;

        /// <summary>
        ///     The _ty
        /// </summary>
        private double _ty;

        /// <summary>
        ///     The _vertical acceleration
        /// </summary>
        private double _verticalAcceleration;

        /// <summary>
        ///     Initializes a new instance of the <see cref="Player" /> class.
        /// </summary>
        /// <param name="room">The room.</param>
        /// <param name="id">The identifier.</param>
        /// <param name="name">The name.</param>
        /// <param name="smiley">The smiley.</param>
        /// <param name="xPos">The x position.</param>
        /// <param name="yPos">The y position.</param>
        /// <param name="isGod">if set to <c>true</c> [is god].</param>
        /// <param name="isMod">if set to <c>true</c> [is mod].</param>
        /// <param name="hasChat">if set to <c>true</c> [has chat].</param>
        /// <param name="coins">The coins.</param>
        /// <param name="switchOpened">if set to <c>true</c> [purple].</param>
        /// <param name="isFriend">if set to <c>true</c> [is friend].</param>
        /// <param name="level">The level.</param>
        /// <param name="hasClub">If the player has builder's club or not.</param>
        /// <param name="isInvulnerable">The player can die.</param>
        /// <param name="isThrusting">Player is using boost potion</param>
        /// <param name="isZombie">Player is a zombie</param>
        /// <param name="isDead">Player is dead</param>
        /// <param name="levitation">Player has levitation potion</param>
        public Player(Room room, int id, string name, int smiley, double xPos, double yPos, bool isGod, bool isMod,
            bool hasChat, int coins, bool switchOpened, bool isFriend, int level, bool hasClub, bool isInvulnerable,
            bool isThrusting, bool isZombie, bool isDead, bool levitation)
        {
            PotionEffects = new List<int>();
            PlayingIn = room;
            Smiley = smiley;
            IsGod = isGod;
            IsMod = isMod;
            HasChat = hasChat;
            Id = id;
            Coins = coins;
            SwitchOpened = switchOpened;
            IsFriend = isFriend;
            Level = level;
            HasClub = hasClub;
            _isInvulnerable = isInvulnerable;
            _isThrusting = isThrusting;
            _isZombie = isZombie;
            _isDead = isDead;
            Levitation = levitation;
            _queue = new Queue<int>(Config.PhysicsQueueLength);
            _lastPortal = new Point();

            _currentThrust = MaxThrust;
            X = xPos;
            Y = yPos;
            Name = name;
            Size = 16;
            NoModifierDragX = Config.PhysicsNoModifierDrag;
            NoModifierDragY = Config.PhysicsNoModifierDrag;
            WaterDrag = Config.PhysicsWaterDrag;
            WaterBuoyancy = Config.PhysicsWaterBuoyancy;
            MudDrag = Config.PhysicsMudDrag;
            MudBuoyancy = Config.PhysicsMudBuoyancy;
            Boost = Config.PhysicsBoost;
            Gravity = Config.PhysicsGravity;
            _mult = Config.PhysicsVariableMultiplyer;

            ShouldTick = true;
        }

        /// <summary>
        ///     Creates a copy of a player object.
        /// </summary>
        /// <param name="p">The player object to be copied.</param>
        public Player(Player p)
        {
            PotionEffects = new List<int>();
            PlayingIn = p.PlayingIn;
            Smiley = p.Smiley;
            IsGod = p.IsGod;
            IsMod = p.IsMod;
            HasChat = p.HasChat;
            Id = p.Id;
            Coins = p.Coins;
            SwitchOpened = p.SwitchOpened;
            IsFriend = p.IsFriend;
            Level = p.Level;
            HasClub = p.HasClub;
            _isInvulnerable = p._isInvulnerable;
            _isThrusting = p._isThrusting;
            _isZombie = p._isZombie;
            _isDead = p._isDead;
            Levitation = p.Levitation;
            _queue = new Queue<int>(Config.PhysicsQueueLength);
            _lastPortal = new Point();

            _currentThrust = MaxThrust;
            X = p.X;
            Y = p.Y;
            Name = p.Name;
            Size = 16;
            NoModifierDragX = Config.PhysicsNoModifierDrag;
            NoModifierDragY = Config.PhysicsNoModifierDrag;
            WaterDrag = Config.PhysicsWaterDrag;
            WaterBuoyancy = Config.PhysicsWaterBuoyancy;
            MudDrag = Config.PhysicsMudDrag;
            MudBuoyancy = Config.PhysicsMudBuoyancy;
            Boost = Config.PhysicsBoost;
            Gravity = Config.PhysicsGravity;
            _mult = Config.PhysicsVariableMultiplyer;

            ShouldTick = true;
        }

        // Public instance properties.
        /// <summary>
        ///     Gets or sets value indicating whether this player should have accurate player coordinates.
        /// </summary>
        public bool ShouldTick { get; set; }

        /// <summary>
        ///     Gets a value indicating whether this instance has access.
        /// </summary>
        /// <value><c>true</c> if this instance has access; otherwise, <c>false</c>.</value>
        public bool HasAccess { get; internal set; }

        /// <summary>
        ///     Gets a value indicating whether this instance has boost.
        /// </summary>
        /// <value><c>true</c> if this instance has boost; otherwise, <c>false</c>.</value>
        public bool HasBoost { get; internal set; }

        /// <summary>
        ///     Gets a value indicating whether this instance has club.
        /// </summary>
        /// <value><c>true</c> if this instance has club; otherwise, <c>false</c>.</value>
        private bool HasClub { get; set; }

        /// <summary>
        ///     Gets a value indicating whether this instance has command access.
        /// </summary>
        /// <value><c>true</c> if this instance has command access; otherwise, <c>false</c>.</value>
        public bool HasCommandAccess { get; internal set; }

        /// <summary>
        ///     Gets a value indicating whether this instance has crown.
        /// </summary>
        /// <value><c>true</c> if this instance has crown; otherwise, <c>false</c>.</value>
        public bool HasCrown { get; internal set; }

        /// <summary>
        ///     Gets a value indicating whether this instance has gravity modifier.
        /// </summary>
        /// <value><c>true</c> if this instance has gravity modifier; otherwise, <c>false</c>.</value>
        public bool HasGravityModifier { get; internal set; }

        /// <summary>
        ///     Gets a value indicating whether this instance has silver crown.
        /// </summary>
        /// <value><c>true</c> if this instance has silver crown; otherwise, <c>false</c>.</value>
        public bool HasSilverCrown { get; internal set; }

        /// <summary>
        ///     Gets a value indicating whether this instance is bot.
        /// </summary>
        /// <value><c>true</c> if this instance is bot; otherwise, <c>false</c>.</value>
        public bool IsBot
        {
            get { return PlayingIn != null && PlayingIn.OnlineBots.Any(bt => bt.Id == Id); }
        }

        /// <summary>
        ///     Gets a value indicating whether this instance is friend.
        /// </summary>
        /// <value><c>true</c> if this instance is friend; otherwise, <c>false</c>.</value>
        private bool IsFriend { get; set; }

        /// <summary>
        ///     The level of the player (in terms of xp).
        /// </summary>
        /// <value>The level.</value>
        private int Level { get; set; }

        /// <summary>
        ///     Gets a value indicating whether this instance is god.
        /// </summary>
        /// <value><c>true</c> if this instance is god; otherwise, <c>false</c>.</value>
        public bool IsGod { get; internal set; }

        /// <summary>
        ///     Gets a value indicating whether this instance is mod.
        /// </summary>
        /// <value><c>true</c> if this instance is mod; otherwise, <c>false</c>.</value>
        public bool IsMod { get; internal set; }

        /// <summary>
        ///     Whether or not the player is able to use the chat box for
        ///     free form chat messages.
        /// </summary>
        /// <value><c>true</c> if this instance has chat; otherwise, <c>false</c>.</value>
        private bool HasChat { get; set; }

        /// <summary>
        ///     Gets a value indicating whether this instance is holding left.
        /// </summary>
        /// <value><c>true</c> if this instance is holding left; otherwise, <c>false</c>.</value>
        public bool IsHoldingLeft { get; internal set; }

        /// <summary>
        ///     Gets a value indicating whether this instance is holding right.
        /// </summary>
        /// <value><c>true</c> if this instance is holding right; otherwise, <c>false</c>.</value>
        public bool IsHoldingRight { get; internal set; }

        /// <summary>
        ///     Gets a value indicating whether this instance is holding up.
        /// </summary>
        /// <value><c>true</c> if this instance is holding up; otherwise, <c>false</c>.</value>
        public bool IsHoldingUp { get; internal set; }

        /// <summary>
        ///     Gets a value indicating whether this instance is holding down.
        /// </summary>
        /// <value><c>true</c> if this instance is holding down; otherwise, <c>false</c>.</value>
        public bool IsHoldingDown { get; internal set; }

        /// <summary>
        ///     Gets a value indicating whether this instance is holding space.
        /// </summary>
        /// <value><c>true</c> if this instance is holding space; otherwise, <c>false</c>.</value>
        public bool IsHoldingSpace { get; internal set; }

        /// <summary>
        ///     Gets a value indicating whether this instance is owner.
        /// </summary>
        /// <value><c>true</c> if this instance is owner; otherwise, <c>false</c>.</value>
        public bool IsOwner
        {
            get { return PlayingIn.Owner == this; }

            set { throw new NotImplementedException(); }
        }

        /// <summary>
        ///     Gets the coins.
        /// </summary>
        /// <value>The coins.</value>
        public int Coins { get; internal set; }

        /// <summary>
        ///     Gets the blue coins.
        /// </summary>
        /// <value>The blue coins.</value>
        public int BlueCoins { get; internal set; }

        /// <summary>
        ///     Gets the collected magic.
        /// </summary>
        /// <value>The collected magic.</value>
        public int CollectedMagic { get; internal set; }

        /// <summary>
        ///     Gets the death count.
        /// </summary>
        /// <value>The death count.</value>
        public int DeathCount { get; internal set; }

        /// <summary>
        ///     Gets the identifier.
        /// </summary>
        /// <value>The identifier.</value>
        public int Id { get; internal set; }

        /// <summary>
        ///     Gets the smiley.
        /// </summary>
        /// <value>The smiley.</value>
        public int Smiley { get; internal set; }

        /// <summary>
        ///     Gets the potion effects.
        /// </summary>
        /// <value>The potion effects.</value>
        public List<int> PotionEffects { get; internal set; }

        /// <summary>
        ///     Gets the playing in.
        /// </summary>
        /// <value>The playing in.</value>
        public Room PlayingIn { get; internal set; }

        /// <summary>
        ///     Gets the name.
        /// </summary>
        /// <value>The name.</value>
        public string Name
        {
            get { return _name; }
            internal set { _name = value.ToLower(); }
        }

        /// <summary>
        ///     Gets the block x.
        /// </summary>
        /// <value>The block x.</value>
        /// <exception cref="System.NotImplementedException"></exception>
        private int blockX
        {
            get { return (int) Math.Round(X/16.0); }
        }

        /// <summary>
        ///     Gets the block y.
        /// </summary>
        /// <value>The block y.</value>
        /// <exception cref="System.NotImplementedException"></exception>
        private int blockY
        {
            get { return (int) Math.Round((Y)/16.0); }
        }

        /// <summary>
        ///     Gets the position x.
        /// </summary>
        /// <value>The position x.</value>
        internal double PosX
        {
            get { return (X + 8); }
        }

        /// <summary>
        ///     Gets the position y.
        /// </summary>
        /// <value>The position y.</value>
        internal double PosY
        {
            get { return (Y + 8); }
        }

        /// <summary>
        ///     Gets or sets the speed x.
        /// </summary>
        /// <value>The speed x.</value>
        internal double speedX
        {
            get { return SpeedX*_mult; }
            set { SpeedX = value/_mult; }
        }

        /// <summary>
        ///     Gets or sets the speed y.
        /// </summary>
        /// <value>The speed y.</value>
        internal double speedY
        {
            get { return SpeedY*_mult; }
            set { SpeedY = value/_mult; }
        }

        /// <summary>
        ///     Gets or sets the modifier x.
        /// </summary>
        /// <value>The modifier x.</value>
        internal double modifierX
        {
            get { return ModifierX*_mult; }
            set { ModifierX = value/_mult; }
        }

        /// <summary>
        ///     Gets or sets the modifier y.
        /// </summary>
        /// <value>The modifier y.</value>
        internal double modifierY
        {
            get { return ModifierY*_mult; }
            set { ModifierY = value/_mult; }
        }

        /// <summary>
        ///     Gets or sets the block x.
        /// </summary>
        /// <value>The block x.</value>
        public int BlockX
        {
            get { return blockX; }
            set { X = value*16; }
        }

        /// <summary>
        ///     Gets or sets the block y.
        /// </summary>
        /// <value>The block y.</value>
        public int BlockY
        {
            get { return blockY; }
            set { Y = value*16; }
        }

        /// <summary>
        ///     Gets or sets a value indicating whether this <see cref="Player" /> is levitation.
        /// </summary>
        /// <value><c>true</c> if levitation; otherwise, <c>false</c>.</value>
        private bool Levitation { get; set; }

        /// <summary>
        ///     Resets the coins.
        /// </summary>
        private void ResetCoins()
        {
            Coins = 0;
            BlueCoins = 0;
        }

        /// <summary>
        ///     Hits the test.
        /// </summary>
        /// <param name="param1">The param1.</param>
        /// <param name="param2">The param2.</param>
        /// <returns><c>true</c> if XXXX, <c>false</c> otherwise.</returns>
        private bool HitTest(int param1, int param2)
        {
            return param1 >= X && param2 >= Y && param1 <= X + Size && param2 <= Y + Size;
        }


        /// <summary>
        ///     Speeds the multiplier.
        /// </summary>
        /// <returns>System.Double.</returns>
        private double SpeedMultiplier()
        {
            double loc1 = 1;
            if (Zombie())
            {
                loc1 = loc1*0.6;
            }
            return loc1;
        }

        /// <summary>
        ///     Drags the mud.
        /// </summary>
        /// <returns>System.Double.</returns>
        private double DragMud()
        {
            return MudDrag;
        }

        /// <summary>
        ///     Overlapses the specified player.
        /// </summary>
        /// <param name="player">The player.</param>
        /// <returns>System.Int32.</returns>
        private int Overlaps(Player player)
        {
            if (player.X < 0 || player.Y < 0 || player.X >= PlayingIn.Width*16 - 8 ||
                player.Y >= PlayingIn.Height*16 - 8)
            {
                return 1;
            }

            Player loc2 = this;

            if (loc2.IsGod || loc2.IsMod)
            {
                return 0;
            }

            double loc3 = ((loc2.X)/16);
            double loc4 = ((loc2.Y)/16);
            for (int xx = -2; xx < 1; xx++)
            {
                for (int yy = -2; yy < 1; yy++)
                {
                    if (loc3 + xx > 0 && loc3 + xx < PlayingIn.Width && loc4 + yy > 0 &&
                        loc4 + yy <= PlayingIn.Height)
                    {
                        for (int xTest = 0; xTest < 16; xTest++)
                        {
                            for (int yTest = 0; yTest < 16; yTest++)
                            {
                                if (HitTest((int) (xTest + loc2.X + xx*16), (int) (yTest + loc2.Y + yy*16)))
                                {
                                    double loc9 = loc4;
                                    Block currentBlock = PlayingIn.Map[
                                        (int) (((xx*16) + loc2.X + xTest)/16),
                                        (int) (((yy*16) + loc2.Y + yTest)/16),
                                        0];
                                    int loc11 = currentBlock.Id;
                                    if (ItemId.IsSolid(loc11))
                                    {
                                        switch (loc11)
                                        {
                                            case 23:
                                            {
                                                if (PlayingIn.RedActivated)
                                                {
                                                    continue;
                                                }
                                                break;
                                            }
                                            case 24:
                                            {
                                                if (PlayingIn.GreenActivated)
                                                {
                                                    continue;
                                                }
                                                break;
                                            }
                                            case 25:
                                            {
                                                if (PlayingIn.BlueActivated)
                                                {
                                                    continue;
                                                }
                                                break;
                                            }
                                            case 26:
                                            {
                                                if (!PlayingIn.RedActivated)
                                                {
                                                    continue;
                                                }
                                                break;
                                            }
                                            case 27:
                                            {
                                                if (!PlayingIn.GreenActivated)
                                                {
                                                    continue;
                                                }
                                                break;
                                            }
                                            case 28:
                                            {
                                                if (!PlayingIn.BlueActivated)
                                                {
                                                    continue;
                                                }
                                                break;
                                            }
                                            case 156:
                                            {
                                                if (PlayingIn.TimeDoorsVisible)
                                                {
                                                    continue;
                                                }
                                                break;
                                            }
                                            case 157:
                                            {
                                                if (!PlayingIn.TimeDoorsVisible)
                                                {
                                                    continue;
                                                }
                                                break;
                                            }
                                            case BlockIds.Action.Doors.Switch:
                                            {
                                                if (SwitchOpened)
                                                {
                                                    continue;
                                                }
                                                break;
                                            }
                                            case BlockIds.Action.Gates.Switch:
                                            {
                                                if (!SwitchOpened)
                                                {
                                                    continue;
                                                }
                                                break;
                                            }
                                            case BlockIds.Action.Doors.Club:
                                            {
                                                if (HasClub)
                                                {
                                                    continue;
                                                }
                                                break;
                                            }
                                            case BlockIds.Action.Gates.Club:
                                            {
                                                if (!HasClub)
                                                {
                                                    continue;
                                                }
                                                break;
                                            }
                                            case BlockIds.Action.Doors.Coin:
                                            {
                                                if (currentBlock is CoinBlock &&
                                                    ((CoinBlock) currentBlock).CoinsRequired <= Coins)
                                                {
                                                    continue;
                                                }
                                                break;
                                            }
                                            case BlockIds.Action.Gates.Coin:
                                            {
                                                if (currentBlock is CoinBlock &&
                                                    ((CoinBlock) currentBlock).CoinsRequired > Coins)
                                                {
                                                    continue;
                                                }
                                                break;
                                            }
                                            case BlockIds.Action.Gates.Zombie:
                                            {
                                                if (_isZombie)
                                                {
                                                    continue;
                                                }
                                                break;
                                            }
                                            case BlockIds.Action.Doors.Zombie:
                                            {
                                                if (!_isZombie)
                                                {
                                                    continue;
                                                }
                                                break;
                                            }
                                            case 50:
                                            {
                                                break;
                                            }
                                            case 61:
                                            case 62:
                                            case 63:
                                            case 64:
                                            case 89:
                                            case 90:
                                            case 91:
                                            case 96:
                                            case 97:
                                            case 122:
                                            case 123:
                                            case 124:
                                            case 125:
                                            case 126:
                                            case 127:
                                            case 146:
                                            case 154:
                                            case 158:
                                            case 194:
                                            {
                                                if (loc2.speedY < 0 || loc9 <= loc2._overlapy)
                                                {
                                                    if (Math.Abs(loc9 - loc4) > 0.00000001 || loc2._overlapy == -1)
                                                    {
                                                        loc2._overlapy = (int) loc9;
                                                    }
                                                }
                                                break;
                                            }
                                            case 83:
                                            case 77:
                                            {
                                                continue;
                                            }
                                            default:
                                            {
                                                break;
                                            }
                                        }
                                        return loc11;
                                    }
                                }
                            }
                        }
                    }
                }
            }

            return 0;
        }

        /// <summary>
        ///     Stepxes this instance.
        /// </summary>
        private void Stepx()
        {
            if (_currentSx > 0)
            {
                if (_currentSx + _reminderX >= 1)
                {
                    X = X + (1 - _reminderX);
                    X = Math.Floor(X);
                    _currentSx = _currentSx - (1 - _reminderX);
                    _reminderX = 0;
                }
                else
                {
                    X = X + _currentSx;
                    _currentSx = 0;
                }
            }
            else if (_currentSx < 0)
            {
                if (Math.Abs(_reminderX) > 0.00000001 && _reminderX + _currentSx < 0)
                {
                    _currentSx = _currentSx + _reminderX;
                    X = X - _reminderX;
                    X = Math.Floor(X);
                    _reminderX = 1;
                }
                else
                {
                    X = X + _currentSx;
                    _currentSx = 0;
                }
            }

            if (Overlaps(this) != 0)
            {
                X = _ox;
                SpeedX = 0;
                _currentSx = _osx;
                _donex = true;
            }
        }

        /// <summary>
        ///     Stepies this instance.
        /// </summary>
        private void Stepy()
        {
            if (_currentSy > 0)
            {
                if (_currentSy + _reminderY >= 1)
                {
                    Y = Y + (1 - _reminderY);
                    Y = Math.Floor(Y);
                    _currentSy = _currentSy - (1 - _reminderY);
                    _reminderY = 0;
                }
                else
                {
                    Y = Y + _currentSy;
                    _currentSy = 0;
                }
            }
            else if (_currentSy < 0)
            {
                if (Math.Abs(_reminderY) > 0.00000001 && _reminderY + _currentSy < 0)
                {
                    Y = Y - _reminderY;
                    Y = Math.Floor(Y);
                    _currentSy = _currentSy + _reminderY;
                    _reminderY = 1;
                }
                else
                {
                    Y = Y + _currentSy;
                    _currentSy = 0;
                }
            }

            if (Overlaps(this) != 0)
            {
                Y = _oy;
                SpeedY = 0;
                _currentSy = _osy;
                _doney = true;
            }
        }

        /// <summary>
        ///     Processes the portals.
        /// </summary>
        private void ProcessPortals()
        {
            var targetPortalList = new List<Point>();
            CurrentBlockId = PlayingIn.Map[_cx, _cy, 0].Id;
            if (!_isgodmod && CurrentBlockId == BlockIds.Action.Portals.World)
            {
                if (_spacejustdown)
                {
                }
            }
            if (!_isgodmod && CurrentBlockId == 242)
            {
                if (_lastPortal.X == 0 && _lastPortal.Y == 0)
                {
                    _lastPortal = new Point(_cx << 4, _cy << 4);

                    Block currentBlock = PlayingIn.Map[_cx, _cy, 0];
                    var currentPortalBlock = (PortalBlock) currentBlock;
                    int currentTarget = currentPortalBlock.PortalDestination;

                    for (int x = 1; x < PlayingIn.Width; x++)
                    {
                        for (int y = 1; y < PlayingIn.Height; y++)
                        {
                            Block block = PlayingIn.Map[x, y, 0];
                            if (block is PortalBlock && ((PortalBlock) block).PortalId == currentTarget)
                            {
                                targetPortalList.Add(new Point(x << 4, y << 4));
                            }
                        }
                    }
                    const int loopIterator = 0;
                    while (loopIterator < targetPortalList.Count)
                    {
                        Point currentLoopPortal = targetPortalList[loopIterator];
                        int loc4 = PlayingIn.Map[_lastPortal.X >> 4, _lastPortal.Y >> 4, 0].Direction;
                        int loc5 = PlayingIn.Map[currentLoopPortal.X >> 4, currentLoopPortal.Y >> 4, 0].Direction;
                        if (loc4 < loc5)
                        {
                            loc4 = loc4 + 4;
                        }
                        double loc6 = speedX;
                        double loc7 = speedY;
                        double loc8 = modifierX;
                        double loc9 = modifierY;
                        int loc10 = loc4 - loc5;
                        const double loc11 = 1.42;
                        switch (loc10)
                        {
                            case 1:
                            {
                                speedX = loc7*loc11;
                                speedY = (-loc6)*loc11;
                                modifierX = loc9*loc11;
                                modifierY = (-loc8)*loc11;
                                _reminderY = -_reminderY;
                                _currentSy = -_currentSy;
                                break;
                            }
                            case 3:
                            {
                                speedX = (-loc7)*loc11;
                                speedY = loc6*loc11;
                                modifierX = (-loc9)*loc11;
                                modifierY = loc8*loc11;
                                _reminderX = -_reminderX;
                                _currentSx = -_currentSx;
                                break;
                            }
                            case 2:
                            {
                                speedX = (-loc6)*loc11;
                                speedY = (-loc7)*loc11;
                                modifierX = (-loc8)*loc11;
                                modifierY = (-loc9)*loc11;
                                _reminderY = -_reminderY;
                                _currentSy = -_currentSy;
                                _reminderX = -_reminderX;
                                _currentSx = -_currentSx;
                                break;
                            }
                            default:
                            {
                                break;
                            }
                        }

                        X = currentLoopPortal.X;
                        Y = currentLoopPortal.Y;
                        _lastPortal = currentLoopPortal;
                        break;
                    }
                }
            }
            else
            {
                _lastPortal = new Point(0, 0);
            }
        }

        /// <summary>
        ///     Ticks this instance.
        /// </summary>
        public void Tick()
        {
            _animoffset = _animoffset + 0.2;
            if (IsMod && !IsGod)
            {
                _modoffset = _modoffset + 0.2;
                if (_modoffset >= 16)
                {
                    _modoffset = 10;
                }
            }
            else if (HasClub)
            {
                _cluboffset = _cluboffset + 0.2;
                if (_cluboffset >= 14)
                {
                    _cluboffset = 0;
                }
            }
            else
            {
                _modoffset = 0;
            }
            if (_isDead)
            {
                _deadoffset = _deadoffset + 0.3;
            }
            else
            {
                _deadoffset = 0;
            }
            _cx = (int) ((X + 8)/16);
            _cy = (int) ((Y + 8)/16);
            int delayed = 0;
            if (_queue.Count >= 1)
            {
                delayed = _queue.Dequeue();
            }
            CurrentBlockId = PlayingIn.Map[_cx, _cy, 0].Id;
            _queue.Enqueue(CurrentBlockId);
            if (CurrentBlockId == 4 || ItemId.IsClimbable(CurrentBlockId))
            {
                delayed = _queue.Dequeue();
                _queue.Enqueue(CurrentBlockId);
            }
            if (_isDead)
            {
                Horizontal = 0;
                Vertical = 0;
                _spacejustdown = false;
            }
            _isgodmod = IsGod || IsMod;
            if (_isgodmod)
            {
                _morx = 0;
                _mory = 0;
                _oldHorizontalAcceleration = 0;
                _oldVerticalAcceleration = 0;
            }
            else
            {
                switch (CurrentBlockId)
                {
                    case 1:
                    {
                        _morx = -(int) Gravity;
                        _mory = 0;
                        break;
                    }
                    case 2:
                    {
                        _morx = 0;
                        _mory = -(int) Gravity;
                        break;
                    }
                    case 3:
                    {
                        _morx = (int) Gravity;
                        _mory = 0;
                        break;
                    }
                    case BlockIds.Action.Boost.Left:
                    case BlockIds.Action.Boost.Right:
                    case BlockIds.Action.Boost.Up:
                    case BlockIds.Action.Boost.Down:
                    case BlockIds.Action.Ladders.Chain:
                    case BlockIds.Action.Ladders.Ladder:
                    case BlockIds.Action.Ladders.Horizontalvine:
                    case BlockIds.Action.Ladders.Verticalvine:
                    case BlockIds.Action.Gravity.Zero:
                    {
                        _morx = 0;
                        _mory = 0;
                        break;
                    }
                    case BlockIds.Action.Liquids.Water:
                    {
                        _morx = 0;
                        _mory = (int) WaterBuoyancy;
                        break;
                    }
                    case BlockIds.Action.Liquids.Mud:
                    {
                        _morx = 0;
                        _mory = (int) MudBuoyancy;
                        break;
                    }
                    case BlockIds.Action.Hazards.Fire:
                    case BlockIds.Action.Hazards.Spike:
                    {
                        if (!_isDead && !_isInvulnerable)
                        {
                            KillPlayer();
                        }
                        break;
                    }
                    default:
                    {
                        _morx = 0;
                        _mory = (int) Gravity;
                        break;
                    }
                }
                switch (delayed)
                {
                    case 1:
                    {
                        _oldHorizontalAcceleration = -Gravity;
                        _oldVerticalAcceleration = 0;
                        break;
                    }
                    case 2:
                    {
                        _oldHorizontalAcceleration = 0;
                        _oldVerticalAcceleration = -Gravity;
                        break;
                    }
                    case 3:
                    {
                        _oldHorizontalAcceleration = Gravity;
                        _oldVerticalAcceleration = 0;
                        break;
                    }
                    case BlockIds.Action.Boost.Left:
                    case BlockIds.Action.Boost.Right:
                    case BlockIds.Action.Boost.Up:
                    case BlockIds.Action.Boost.Down:
                    case BlockIds.Action.Ladders.Chain:
                    case BlockIds.Action.Ladders.Ladder:
                    case BlockIds.Action.Ladders.Horizontalvine:
                    case BlockIds.Action.Ladders.Verticalvine:
                    case BlockIds.Action.Gravity.Zero:
                    {
                        _oldHorizontalAcceleration = 0;
                        _oldVerticalAcceleration = 0;
                        break;
                    }
                    case BlockIds.Action.Liquids.Water:
                    {
                        _oldHorizontalAcceleration = 0;
                        _oldVerticalAcceleration = WaterBuoyancy;
                        break;
                    }
                    case BlockIds.Action.Liquids.Mud:
                    {
                        _oldHorizontalAcceleration = 0;
                        _oldVerticalAcceleration = MudBuoyancy;
                        break;
                    }
                    default:
                    {
                        _oldHorizontalAcceleration = 0;
                        _oldVerticalAcceleration = Gravity;
                        break;
                    }
                }
            }
            if (Math.Abs(_oldVerticalAcceleration - WaterBuoyancy) < 0.00000001 ||
                Math.Abs(_oldVerticalAcceleration - MudBuoyancy) < 0.00000001)
            {
                _horizontalAcceleration = Horizontal;
                _verticalAcceleration = Vertical;
            }
            else if (Math.Abs(_oldVerticalAcceleration) > 0.00000001)
            {
                _horizontalAcceleration = Horizontal;
                _verticalAcceleration = 0;
            }
            else if (Math.Abs(_oldHorizontalAcceleration) > 0.00000001)
            {
                _horizontalAcceleration = 0;
                _verticalAcceleration = Vertical;
            }
            else
            {
                _horizontalAcceleration = Horizontal;
                _verticalAcceleration = Vertical;
            }
            _horizontalAcceleration = _horizontalAcceleration*SpeedMultiplier();
            _verticalAcceleration = _verticalAcceleration*SpeedMultiplier();
            _oldHorizontalAcceleration = _oldHorizontalAcceleration*GravityMultiplier;
            _oldVerticalAcceleration = _oldVerticalAcceleration*GravityMultiplier;
            modifierX = _oldHorizontalAcceleration + _horizontalAcceleration;

            modifierY = _oldVerticalAcceleration + _verticalAcceleration;
            if (Math.Abs(SpeedX) > 0.00000001 || Math.Abs(ModifierX) > 0.00000001)
            {
                SpeedX = SpeedX + ModifierX;
                SpeedX = SpeedX*Config.PhysicsBaseDrag;
                if (Math.Abs(_horizontalAcceleration) < 0.00000001 && Math.Abs(_oldVerticalAcceleration) > 0.00000001 ||
                    SpeedX < 0 && _horizontalAcceleration > 0 || SpeedX > 0 && _horizontalAcceleration < 0 ||
                    ItemId.IsClimbable(CurrentBlockId) && !_isgodmod)
                {
                    SpeedX = SpeedX*NoModifierDragX;
                }
                else if (CurrentBlockId == BlockIds.Action.Liquids.Water && !_isgodmod)
                {
                    SpeedX = SpeedX*WaterDrag;
                }
                else if (CurrentBlockId == BlockIds.Action.Liquids.Mud && !_isgodmod)
                {
                    SpeedX = SpeedX*DragMud();
                }
                if (SpeedX > 16)
                {
                    SpeedX = 16;
                }
                else if (SpeedX < -16)
                {
                    SpeedX = -16;
                }
                else if (SpeedX < 0.0001 && SpeedX > -0.0001)
                {
                    SpeedX = 0;
                }
            }
            if (Math.Abs(SpeedY) > 0.00000001 || Math.Abs(ModifierY) > 0.00000001)
            {
                SpeedY = SpeedY + ModifierY;
                SpeedY = SpeedY*Config.PhysicsBaseDrag;
                if (Math.Abs(_verticalAcceleration) < 0.00000001 && Math.Abs(_oldHorizontalAcceleration) > 0.00000001 ||
                    SpeedY < 0 && _verticalAcceleration > 0 || SpeedY > 0 && _verticalAcceleration < 0 ||
                    ItemId.IsClimbable(CurrentBlockId) && !_isgodmod)
                {
                    SpeedY = SpeedY*NoModifierDragY;
                }
                else if (CurrentBlockId == BlockIds.Action.Liquids.Water && !_isgodmod)
                {
                    SpeedY = SpeedY*WaterDrag;
                }
                else if (CurrentBlockId == BlockIds.Action.Liquids.Mud && !_isgodmod)
                {
                    SpeedY = SpeedY*DragMud();
                }
                if (SpeedY > 16)
                {
                    SpeedY = 16;
                }
                else if (SpeedY < -16)
                {
                    SpeedY = -16;
                }
                else if (SpeedY < 0.0001 && SpeedY > -0.0001)
                {
                    SpeedY = 0;
                }
            }
            if (!_isgodmod)
            {
                switch (CurrentBlockId)
                {
                    case BlockIds.Action.Boost.Left:
                    {
                        SpeedX = -Boost;
                        break;
                    }
                    case BlockIds.Action.Boost.Right:
                    {
                        SpeedX = Boost;
                        break;
                    }
                    case BlockIds.Action.Boost.Up:
                    {
                        SpeedY = -Boost;
                        break;
                    }
                    case BlockIds.Action.Boost.Down:
                    {
                        SpeedY = Boost;
                        break;
                    }
                    default:
                    {
                        break;
                    }
                }
            }
            _reminderX = X%1;
            _currentSx = SpeedX;
            _reminderY = Y%1;
            _currentSy = SpeedY;
            _donex = false;
            _doney = false;
            while (Math.Abs(_currentSx) > 0.00000001 && !_donex || Math.Abs(_currentSy) > 0.00000001 && !_doney)
            {
                ProcessPortals();
                _ox = X;
                _oy = Y;
                _osx = _currentSx;
                _osy = _currentSy;
                Stepx();
                Stepy();
            }

            if (Levitation)
            {
                UpdateThrust();
            }
            double imx = SpeedX*256;
            double imy = SpeedY*256;
            if (Math.Abs(imx) > 0.00000001 || CurrentBlockId == BlockIds.Action.Liquids.Water ||
                CurrentBlockId == BlockIds.Action.Liquids.Mud)
            {
            }
            else if (ModifierX < 0.1 && ModifierX > -0.1)
            {
                _tx = X%16;
                if (_tx < 2)
                {
                    if (_tx < 0.2)
                    {
                        X = Math.Floor(X);
                    }
                    else
                    {
                        X = X - _tx/15;
                    }
                }
                else if (_tx > 14)
                {
                    if (_tx > 15.8)
                    {
                        X = Math.Floor(X);
                        double loc3 = X + 1;
                        X = loc3;
                    }
                    else
                    {
                        X = X + (_tx - 14)/15;
                    }
                }
            }
            if (Math.Abs(imy) > 0.0000001 || CurrentBlockId == BlockIds.Action.Liquids.Water ||
                CurrentBlockId == BlockIds.Action.Liquids.Mud)
            {
            }
            else if (ModifierY < 0.1 && ModifierY > -0.1)
            {
                _ty = Y%16;
                if (_ty < 2)
                {
                    if (_ty < 0.2)
                    {
                        Y = Math.Floor(Y);
                    }
                    else
                    {
                        Y = Y - _ty/15;
                    }
                }
                else if (_ty > 14)
                {
                    if (_ty > 15.8)
                    {
                        Y = Math.Floor(Y);
                        double loc3 = Y + 1;
                        Y = loc3;
                    }
                    else
                    {
                        Y = Y + (_ty - 14)/15;
                    }
                }
            }
        }

        private void KillPlayer()
        {
            throw new NotImplementedException();
        }


        /// <summary>
        ///     Zombies this instance.
        /// </summary>
        /// <returns><c>true</c> if XXXX, <c>false</c> otherwise.</returns>
        private bool Zombie()
        {
            return !IsGod && !IsMod && _isZombie;
        }


        /// <summary>
        ///     Updates the thrust.
        /// </summary>
        private void UpdateThrust()
        {
            if (_mory != 0)
            {
                speedY = speedY - _currentThrust*(Config.PhysicsJumpHeight/2)*(_mory*0.5);
            }
            if (_morx != 0)
            {
                speedX = speedX - _currentThrust*(Config.PhysicsJumpHeight/2)*(_morx*0.5);
            }
            if (_isThrusting) return;
            _currentThrust = _currentThrust > 0 ? _currentThrust - ThrustBurnOff : 0;
        }
    }
}