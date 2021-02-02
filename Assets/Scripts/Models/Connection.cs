using UnityEngine;
using UnityEngine.UI;

namespace Models
{
    [RequireComponent(typeof(Image))]
    public class Connection : Node
    {
        [SerializeField]
        private Sprite _connection;

        public override void Connect()
        {
            base.Connect();

            if (!Connected
                || !ConnectedToBattery)
            {
                return;
            }

            var image = GetComponent<Image>();
            image.sprite = _connection;
            image.color = new Color(255, 255, 255, 128f);
        }
    }
}
