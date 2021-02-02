namespace Models
{
    public class Battery : Node
    {
        protected override void Awake()
        {
            base.Awake();
            ConnectedToBattery = true;
            Connect();
        }
    }
}
