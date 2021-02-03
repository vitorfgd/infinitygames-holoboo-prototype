namespace Models
{
    public class Battery : Node
    {
        /// <summary>
        /// Every path begins in a battery.
        /// </summary>
        protected override void Awake()
        {
            base.Awake();
            
            ConnectedToBattery = true;
            Connect();
        }
    }
}
