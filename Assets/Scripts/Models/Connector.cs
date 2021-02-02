using UnityEngine.Events;

namespace Models
{
    public class Connector : Node
    {
        public UnityEvent Connected = new UnityEvent();

        protected override void Awake()
        {
            base.Awake();
        }

        public override void Connect()
        {
            base.Connect();
            if (ConnectedToBattery)
            {
                Connected?.Invoke();
            }
        }
    }
}
