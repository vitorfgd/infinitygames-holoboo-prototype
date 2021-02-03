using System.Collections.Generic;
using System.Linq;
using JetBrains.Annotations;
using Managers;
using UnityEngine;
using UnityEngine.UI;

namespace Models
{
    [RequireComponent(typeof(RectTransform))]
    public class Node : MonoBehaviour
    {
        public readonly Dictionary<Vector2, Node> Connections = new Dictionary<Vector2, Node>();
        public Image View => _view;

        public bool ConnectedToBattery { get; protected set; }

        protected bool Connected { get; private set; }

        [CanBeNull]
        [SerializeField]
        private AudioClip _connectionSound;

        private RectTransform _transform;
        private Image _view;

        protected virtual void Awake()
        {
            _transform = GetComponent<RectTransform>();
            _view = GetComponent<Image>();
        }

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
                SoundsController.Instance.Play(_connectionSound);
            }

            Connected = true;
            ConnectedToBattery = true;
        }
    }
}
