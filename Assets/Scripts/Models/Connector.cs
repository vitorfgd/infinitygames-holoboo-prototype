using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace Models
{
    /// <summary>
    /// Every path ends in a Connector
    /// </summary>
    [RequireComponent(typeof(Image))]
    public class Connector : Node
    {
        // GameManager subscribes to this event on every connector during stage creation. 
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

            // Change icon's color when connected to a battery.
            GetComponent<Image>().color = _colorWhenConnected;
            ConnectorWasConnected?.Invoke();
        }
    }
}
