using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Assertions.Must;
using UnityEngine.Serialization;
using UnityEngine.UI;

namespace Models
{
    [RequireComponent(typeof(RectTransform))]
    public class Node : MonoBehaviour
    {
        public readonly Dictionary<Vector2, Node> Connections = new Dictionary<Vector2, Node>();
        private bool Connected { get; set; }
        public bool ConnectedToBattery { get; protected set; }

        private RectTransform _transform;

        protected virtual void Awake() => _transform = GetComponent<RectTransform>();

        public Vector2 Size() => _transform.rect.size;

        public Vector2 Coordinates() => transform.position;

        public virtual void Connect()
        {
            Connected = true;

            var connections = Connections.Where(connection => connection.Value.Connected)
                .Aggregate(Vector2.zero, (current, connection) => current + connection.Key);

            if (!(connections.magnitude >= 1))
            {
                return;
            }

            if (Connected || Connections.Values.Any(node => node.ConnectedToBattery))
            {
                ConnectedToBattery = true;
            }
        }
    }
}
