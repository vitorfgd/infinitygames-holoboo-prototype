using System.Collections.Generic;
using System.Linq;
using Models;
using UnityEngine;

namespace Gameplay
{
    public class ConnectionsEvaluator : MonoBehaviour
    {
        [SerializeField]
        private UserInputHandler _userInputHandler;

        [SerializeField]
        private NodesMapper _mapper;

        private bool _pressed;
        private readonly List<Node> _connectors = new List<Node>();

        private void Awake()
        {
            _userInputHandler.Pressed.AddListener(
                () => { _userInputHandler.PositionChanged.AddListener(Intersect); }
            );
        }

        public void Reset()
        {
            _userInputHandler.PositionChanged.RemoveListener(Intersect);
        }

        private void Intersect(Vector2 position)
        {
            foreach (var node in _mapper.Nodes.ToList()
                .Where(
                    node => Vector2.Distance(position, node.Value.Coordinates()) < node.Value.Size().x / 2
                ))
            {
                if (node.Value.ConnectedToBattery)
                {
                    continue;
                }

                node.Value.Connect();
            }
        }
    }
}
