using System.Collections.Generic;
using System.Linq;
using Models;
using Sounds;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Serialization;

namespace Managers
{
    public class GameManager : MonoBehaviour
    {
        [Header("References")]
        [SerializeField]
        private NodesMapper _mapper;

        [SerializeField]
        private ConnectionsEvaluator _connectionsEvaluator;

        [SerializeField]
        private TMP_Text _map;

        [Header("Level Configurations")]
        [SerializeField]
        private Map[] _maps;

        private int _current;
        private readonly List<Node> _connectors = new List<Node>();

        protected void Awake()
        {
            _mapper.ConstructionFinished.AddListener(SubscribeToConnectors);
        }

        protected void Start()
        {
            _current = PlayerPrefs.GetInt("CurrentMap", 0);
            Create();
        }

        private void SubscribeToConnectors()
        {
            foreach (var node in _mapper.Nodes.Where(node => node.Value is Connector))
            {
                var connector = (Connector) node.Value;
                _connectors.Add(connector);
                connector.ConnectorWasConnected.AddListener(EvaluateGame);
            }
        }

        private void EvaluateGame()
        {
            if (!_connectors.All(node => node.ConnectedToBattery))
            {
                return;
            }

            Next();
        }

        private void Next()
        {
            _connectionsEvaluator.Reset();
            _mapper.Destroy();

            if (_maps.Length <= _current + 1)
            {
                Debug.Log("Game ended");
                return;
            }

            _current += 1;
            PlayerPrefs.SetInt("CurrentMap", _current);
            Create();
        }

        private void Create()
        {
            _map.text = $"#{_current + 1}";
            _mapper.Create(_maps[_current]);
        }
    }
}
