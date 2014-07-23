using PlayerIOClient;

namespace Skylight
{
    /// <summary>
    /// Class Potion.
    /// </summary>
    public class Potion
    {
        /// <summary>
        /// The _in
        /// </summary>
        private readonly In _in;

        /// <summary>
        /// Initializes a new instance of the <see cref="Potion"/> class.
        /// </summary>
        /// <param name="in">The in.</param>
        public Potion(In @in)
        {
            _in = @in;
        }

        /// <summary>
        /// All events that concern the player. This includes many messages that the player
        /// gets from the world (such as server information and leveling up). Mostly these
        /// events are shown from the server directly to the user in the form of a dialog
        /// box or by prefixing a chat message with *SYSTEM.
        /// </summary>
        public event In.PlayerEvent
            PotionEvent = delegate { };

        /// <summary>
        /// Called when a potion is activated.
        /// </summary>
        /// <param name="m">The message.</param>
        public void OnP(Message m)
        {
            // Extract data.
            int id = m.GetInteger(0),
                potionId = m.GetInteger(1);

            bool isActive = m.GetBoolean(2);

            // Update relevant objects
            Player subject = Tools.GetPlayer(id, _in.Source);

            if (isActive)
            {
                subject.PotionEffects.Add(potionId);
            }
            else
            {
                subject.PotionEffects.Remove(potionId);
            }

            // Fire the event.
            var e = new PlayerEventArgs(subject, _in.Source, m);

            _in.Source.Pull.Potion.PotionEvent(e);
        }
    }
}