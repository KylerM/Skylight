using PlayerIOClient;



namespace Skylight
{
    public class Moderator
    {
        private readonly In _in;

        public Moderator(In @in)
        {
            _in = @in;
        }

        /// <summary>
        ///     All events that concern the player. This includes many messages that the player
        ///     gets from the world (such as server information and leveling up). Mostly these
        ///     events are shown from the server directly to the user in the form of a dialog
        ///     box or by prefixing a chat message with *SYSTEM.
        /// </summary>
        public event In.PlayerEvent
            ModModeEvent = delegate { };

        public void OnMod(Message m)
        {
            // Extract data.
            var isMod = m.GetBoolean(1);

            var id = m.GetInteger(0);

            // Update relevant objects.
            var subject = Tools.GetPlayerById(id, _in.Source);

            subject.IsMod = isMod;

            // Fire the event.
            var e = new PlayerEventArgs(subject, _in.Source, m);

            _in.Source.Pull.Moderator.ModModeEvent(e);
        }
    }
}