using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Models;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Managers
{
    /// <summary>
    /// TODO: Refactor code and split view from logic.
    /// </summary>
    public class GameManager : MonoBehaviour
    {
        [Header("References")]
        [SerializeField]
        private NodesMapper _mapper;

        [SerializeField]
        private ConnectionsEvaluator _connectionsEvaluator;

        [Header("UI References")]
        [Header("Buttons")]
        [SerializeField]
        private CanvasGroup _container;

        [SerializeField]
        private Button _previousButton;

        [SerializeField]
        private Button _nextButton;

        [Header("Texts")]
        [SerializeField]
        private TMP_Text _map;

        [SerializeField]
        private TMP_Text _message;

        [Header("Level Configurations")]
        [SerializeField]
        private string _saveIdentifier;

        [SerializeField]
        private Map[] _maps;

        private int _current;
        private readonly List<Node> _connectors = new List<Node>();

        protected void Awake()
        {
            // This event ensures that every node is spawn before any iteration is done.
            _mapper.ConstructionFinished.AddListener(SubscribeToConnectors);
            _previousButton.onClick.AddListener(() => ChangePage(-1));
            _nextButton.onClick.AddListener(() => ChangePage(1));
        }

        protected void Start()
        {
            _current = PlayerPrefs.GetInt(_saveIdentifier, 0);
            EvaluatePageButtonStatus();
            Create();
        }

        /// <summary>
        /// Checks whether all Connectors are connected to a battery.
        /// Waits for mapper to finish before iterating over the nodes.
        /// </summary>
        private void SubscribeToConnectors()
        {
            foreach (var node in _mapper.Nodes.Where(node => node.Value is Connector))
            {
                var connector = (Connector) node.Value;
                _connectors.Add(connector);
                connector.ConnectorWasConnected.AddListener(EvaluateGameStatus);
            }
        }

        private void ChangePage(int modifier)
        {
            _current += modifier;
            EvaluatePageButtonStatus();
            Next(_current);
        }

        private void EvaluatePageButtonStatus()
        {
            _previousButton.interactable = _current != 0;
            _previousButton.interactable = _current < _maps.Length;
        }

        private void EvaluateGameStatus()
        {
            if (!_connectors.All(node => node.ConnectedToBattery))
            {
                return;
            }

            _current += 1;
            Next(_current);
        }

        private async void Next(int map)
        {
            await this.AsyncCoroutine(Destroy());

            if (_maps.Length == map)
            {
                _current = 0;
                PlayerPrefs.SetInt(_saveIdentifier, _current);
                
                _message.gameObject.SetActive(true);
                return;
            }

            Create();
        }

        private void Create()
        {
            PlayerPrefs.SetInt(_saveIdentifier, _current);
            _map.text = $"#{_current + 1}";
            _mapper.Create(_maps[_current]);
        }

        private IEnumerator Destroy()
        {
            yield return new WaitForSeconds(1);
            _connectors.Clear();
            _connectionsEvaluator.Reset();
            _mapper.Destroy();
        }
    }
}
