using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace Models
{
    [RequireComponent(typeof(Image))]
    public class Connector : Node
    {
        public UnityEvent ConnectorWasConnected = new UnityEvent();

        [SerializeField]
        private Color _colorWhenConnected;

        public override void Connect()
        {
            base.Connect();
            if (!Connected
                || !ConnectedToBattery)
            {
                return;
            }

            GetComponent<Image>().color = _colorWhenConnected;
            ConnectorWasConnected?.Invoke();
        }
    }
}
