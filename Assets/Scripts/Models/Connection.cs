using UnityEngine;
using UnityEngine.UI;

namespace Models
{
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

            _connection.enabled = true;
        }
    }
}
