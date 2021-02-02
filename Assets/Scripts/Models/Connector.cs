using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Serialization;

namespace Models
{
    public class Connector : Node
    {
        public UnityEvent ConnectorWasConnected = new UnityEvent();

        protected override void Awake()
        {
            base.Awake();
        }

        public override void Connect()
        {
            base.Connect();
            if (!Connected
                || !ConnectedToBattery)
            {
                return;
            }

            ConnectorWasConnected?.Invoke();
        }
    }
}
