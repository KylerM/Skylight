﻿// <copyright file="Config.cs" company="">
//     Copyright 2014 (c) . All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************

using System;

namespace Skylight
{
    /// <summary>
    ///     Class Configuration.
    /// </summary>
    public class Config : object
    {
        /// <summary>
        ///     The PlayerIO game id.
        /// </summary>
        public static string PlayerioGameId = "everybody-edits-su9rn58o40itdbnw69plyw";

        /// <summary>
        ///     The server version.
        /// </summary>
        public static int ServerTypeVersion = 176;

        /// <summary>
        ///     The server type normal room combined with the everybody edits version.
        /// </summary>
        public static string ServerTypeNormalroom = "Everybodyedits" + ServerTypeVersion;

        /// <summary>
        ///     The beta room
        /// </summary>
        public static string ServerTypeBetaroom = "Beta" + ServerTypeVersion;

        /// <summary>
        ///     The guest only room
        /// </summary>
        public static string ServerTypeGuestserviceroom = "LobbyGuest" + ServerTypeVersion;

        /// <summary>
        ///     The service room
        /// </summary>
        public static string ServerTypeServiceroom = "Lobby" + ServerTypeVersion;

        /// <summary>
        ///     The authentication room used for ArmorGames authentication
        /// </summary>
        public static string ServerTypeAuthroom = "Auth" + ServerTypeVersion;

        /// <summary>
        ///     The blacklisted room
        /// </summary>
        public static string ServerTypeBlacklistroom = "QuickInviteHandler" + ServerTypeVersion;

        /// <summary>
        ///     The tutorial room
        /// </summary>
        public static string ServerTypeTutorialroom = "Tutorial" + ServerTypeVersion + "_world_";

        /// <summary>
        ///     The tracking room
        /// </summary>
        public static string ServerTypeTrackingroom = "Tracking" + ServerTypeVersion;

        /// <summary>
        ///     The url for the Everybody Edits blog
        /// </summary>
        public static string UrlBlog = "http://blog.everybodyedits.com";

        /// <summary>
        ///     The url to the club member page
        /// </summary>
        public static string UrlClubmemberAboutPage = "http://everybodyedits.com/club";

        /// <summary>
        ///     The terms of service website
        /// </summary>
        public static string UrlTermsPage = "http://everybodyedits.com/terms";

        /// <summary>
        ///     The help website
        /// </summary>
        public static string UrlHelpPage = "http://everybodyedits.com/help";

        /// <summary>
        ///     Whether or not to use the debug server
        /// </summary>
        public static bool UseDebugServer = false;

        /// <summary>
        ///     Whether or not to run in development mode
        /// </summary>
        public static bool RunInDevelopmentMode = false;

        /// <summary>
        ///     Whether or not to show disabled shop items
        /// </summary>
        public static bool ShowDisabledShopitems = false;

        /// <summary>
        ///     If in development mode, auto join a room
        /// </summary>
        public static string DevelopmentModeAutojoinRoom = "PWvOaRIeIvbUI";

        /// <summary>
        ///     The debug news
        /// </summary>
        public static string DebugNews = "";

        /// <summary>
        ///     The developer server (locally)
        /// </summary>
        public static string DeveloperServer = "127.0.0.1:8184";

        /// <summary>
        ///     The force armor authentication option
        /// </summary>
        public static bool ForceArmor = false;

        /// <summary>
        ///     The ArmorGames userid
        /// </summary>
        public static string ArmorUserid = null;

        /// <summary>
        ///     The ArmorGames authentication token
        /// </summary>
        public static string ArmorAuthtoken = null;

        /// <summary>
        ///     The force mouse breaker option
        /// </summary>
        public static bool ForceMouseBreaker = false;

        /// <summary>
        ///     The mousebreaker authtoken
        /// </summary>
        public static string MousebreakerAuthtoken = null;

        /// <summary>
        ///     The force beta
        /// </summary>
        public static bool ForceBeta = false;

        /// <summary>
        ///     Whether or not to show debug profile
        /// </summary>
        public static bool ShowDebugProfile = true;

        /// <summary>
        ///     The debug profile
        /// </summary>
        public static string DebugProfile = "";

        /// <summary>
        ///     The disable cookie option
        /// </summary>
        public static bool DisableCookie = false;

        /// <summary>
        ///     Whether or not to show debug information concerning a friend request
        /// </summary>
        public static bool ShowDebugFriendrequest = false;

        /// <summary>
        ///     The debug inforation for friend request
        /// </summary>
        public static string DebugFriendrequest = "";

        /// <summary>
        ///     Whether or not to show blacklist invitation
        /// </summary>
        public static bool ShowBlacklistInvitation = false;

        /// <summary>
        ///     The debug invitation
        /// </summary>
        public static string DebugInvitation = "";

        /// <summary>
        ///     The milliseconds that constitute one player physics tick
        /// </summary>
        public static int PhysicsMsPerTick = 10;

        /// <summary>
        ///     The physics variable multiplyer
        /// </summary>
        public static double PhysicsVariableMultiplyer = 7.752;

        /// <summary>
        ///     The physics base drag
        /// </summary>
        public static double PhysicsBaseDrag = Math.Pow(0.9981, PhysicsMsPerTick)*1.00016;

        /// <summary>
        ///     The physics no modifier drag
        /// </summary>
        public static double PhysicsNoModifierDrag = Math.Pow(0.99, PhysicsMsPerTick)*1.00016;

        /// <summary>
        ///     The physics water drag
        /// </summary>
        public static double PhysicsWaterDrag = Math.Pow(0.995, PhysicsMsPerTick)*1.00016;

        /// <summary>
        ///     The physics mud drag
        /// </summary>
        public static double PhysicsMudDrag = Math.Pow(0.975, PhysicsMsPerTick)*1.00016;

        /// <summary>
        ///     The jump height
        /// </summary>
        public static double PhysicsJumpHeight = 26;

        /// <summary>
        ///     The gravity
        /// </summary>
        public static double PhysicsGravity = 2;

        /// <summary>
        ///     The physics boost
        /// </summary>
        public static double PhysicsBoost = 16;

        /// <summary>
        ///     The physics water buoyancy
        /// </summary>
        public static double PhysicsWaterBuoyancy = -0.5;

        /// <summary>
        ///     The physics mud buoyancy
        /// </summary>
        public static double PhysicsMudBuoyancy = 0.4;

        /// <summary>
        ///     The physics queue length
        /// </summary>
        public static int PhysicsQueueLength = 2;

        /// <summary>
        ///     The shop_potion_max
        /// </summary>
        public static int ShopPotionMax = 10;

        /// <summary>
        ///     The camera lag
        /// </summary>
        public static double CameraLag = 0.0625;

        /// <summary>
        ///     The is mobile option
        /// </summary>
        public static bool IsMobile = false;

        /// <summary>
        ///     The enable debug shadow
        /// </summary>
        public static bool EnableDebugShadow = false;

        /// <summary>
        ///     The max width of a room
        /// </summary>
        public static int Maxwidth = 850;

        /// <summary>
        ///     The min width of a room
        /// </summary>
        public static int Minwidth = 640;

        /// <summary>
        ///     The default width of a room
        /// </summary>
        public static int Width = 640;

        /// <summary>
        ///     The default height of a room
        /// </summary>
        public static int Height = 500;

        /// <summary>
        ///     The maximum frame rate
        /// </summary>
        public static int MaxFrameRate = 120;

        /// <summary>
        ///     The maximum daily woots for a player
        /// </summary>
        public static int MaxDailyWoot = 10;

        /// <summary>
        ///     The color of a guest
        /// </summary>
        public static uint GuestColor = 3355443;

        /// <summary>
        ///     The default color
        /// </summary>
        public static uint DefaultColor = 15658734;

        /// <summary>
        ///     The default dark color
        /// </summary>
        public static uint DefaultColorDark = 13421772;

        /// <summary>
        ///     The friend color
        /// </summary>
        public static uint FriendColor = 65280;

        /// <summary>
        ///     The darker friend color
        /// </summary>
        public static uint FriendColorDark = 47872;

        /// <summary>
        ///     The mod color
        /// </summary>
        public static uint ModColor = 16759552;

        /// <summary>
        ///     The admin color
        /// </summary>
        public static uint AdminColor = 16757760;

        /// <summary>
        ///     The tutorial room names
        /// </summary>
        public static string[] TutorialNames = {"Moving", "Gravity", "Edit"};

        /// <summary>
        ///     The disable tracking options
        /// </summary>
        public static bool DisableTracking = false;
    }
}