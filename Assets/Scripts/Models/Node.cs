using System.Collections.Generic;
using System.Linq;
using JetBrains.Annotations;
using Sounds;
using UnityEngine;

namespace Models
{
    [RequireComponent(typeof(RectTransform))]
    public class Node : MonoBehaviour
    {
        public readonly Dictionary<Vector2, Node> Connections = new Dictionary<Vector2, Node>();

        public bool ConnectedToBattery { get; protected set; }

        protected bool Connected { get; private set; }

        [CanBeNull]
        [SerializeField]
        private AudioClip _connectionSound;

        private RectTransform _transform;

        protected virtual void Awake() => _transform = GetComponent<RectTransform>();

        public Vector2 Size() => _transform.rect.size;

        public Vector2 Coordinates() => transform.position;

        public virtual void Connect()
        {
            var connections = Connections.Where(connection => connection.Value.Connected)
                .Aggregate(Vector2.zero, (current, connection) => current + connection.Key);

            if (!Connections.Values.Any(node => node.ConnectedToBattery))
            {
                return;
            }

            if (_connectionSound != null)
            {
                SoundsManager.Instance.Play(_connectionSound);
            }

            Connected = true;
            ConnectedToBattery = true;
        }
    }
}
