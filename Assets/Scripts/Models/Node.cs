using System.Collections.Generic;
using System.Linq;
using Managers;
using UnityEngine;
using UnityEngine.UI;

namespace Models
{
    [RequireComponent(typeof(RectTransform))]
    public class Node : MonoBehaviour
    {
        public readonly Dictionary<Vector2, Node> Connections = new Dictionary<Vector2, Node>();
        public Image Icon { get; private set; }
        public bool ConnectedToBattery { get; protected set; }

        protected bool Connected { get; private set; }

        [SerializeField]
        private AudioClip _connectionSound;

        private RectTransform _transform;

        protected virtual void Awake()
        {
            _transform = GetComponent<RectTransform>();
            Icon = GetComponent<Image>();
        }

        public Vector2 Size() => _transform.rect.size;

        public Vector2 Coordinates() => transform.position;

        // A node connection must always have its origin in a battery.
        // Different nodes may have different sounds when connected.
        // A node always pass 
        public virtual void Connect()
        {
            if (!Connections.Values.Any(node => node.ConnectedToBattery))
            {
                return;
            }

            if (_connectionSound != null)
            {
                AudioManager.Instance.Play(_connectionSound);
            }

            Connected = true;
            ConnectedToBattery = true;
        }
    }
}
