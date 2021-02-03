using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

namespace Models
{
    /// <summary>
    /// Path between batteries and connectors.
    /// </summary>
    public class Connection : Node
    {
        [SerializeField]
        private Image _connection;

        public override void Connect()
        {
            base.Connect();

            if (!Connected
                || !ConnectedToBattery)
            {
                return;
            }

            // Inside a connection there is another image,
            // this image is enabled when node is connected to show its status.
            _connection.enabled = true;
            _connection.DOFade(1f, 0.3f);
        }
    }
}
