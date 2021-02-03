using System.Collections;
using System.Collections.Generic;
using System.Linq;
using DG.Tweening;
using Extensions;
using Gameplay;
using Models;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Managers
{
    /// <summary>
    /// TODO: Rewrite this class and remove any view in its separate class.
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
        private TMP_Text _current;

        [SerializeField]
        private TMP_Text _message;

        [Header("Level Configurations")]
        [SerializeField]
        private Map[] _maps;

        [SerializeField]
        private AudioClip _successClip;

        private bool _loading;
        private int _currentStage;
        private int _unlockedStages;
        private PersistenceHandler _persistenceHandler;
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
            _persistenceHandler = PersistenceHandler.Instance;
            _currentStage = _persistenceHandler.Load(_persistenceHandler.CurrentStagePersistenceKey);
            _unlockedStages = _persistenceHandler.Load(_persistenceHandler.UnlockedStagesPersistenceKey);

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

            _loading = false;
            EvaluatePageButtonStatus();
        }

        private void ChangePage(int modifier)
        {
            _loading = true;
            _currentStage += modifier;
            EvaluatePageButtonStatus();
            Next(_currentStage);
        }

        private void EvaluatePageButtonStatus()
        {
            var previousButtonStatus = _currentStage != 0;
            var nextButtonStatus = _currentStage < _maps.Length && _currentStage < _unlockedStages;

            if (_loading)
            {
                previousButtonStatus = nextButtonStatus = false;
            }

            _previousButton.interactable = previousButtonStatus;
            _nextButton.interactable = nextButtonStatus;
        }

        private void EvaluateGameStatus()
        {
            if (!_connectors.All(node => node.ConnectedToBattery))
            {
                return;
            }

            AudioManager.Instance.Play(_successClip, true);
            _container.transform.DOShakeScale(1f, .1f, 10);
            _currentStage += 1;
            Next(_currentStage);
        }

        /// <summary>
        /// Loads new stage.
        /// Destroys the current stage.
        /// Checks whether there are more stages to be played;
        /// End game or loads new stage depending on the result.
        /// </summary>
        private async void Next(int map, bool destructionIsImmediate = false)
        {
            // Extension method that allows me to use an IEnumerator as an async.
            // Implemented using IEnumerator over Task.Delay because of previous problem regarding multiple threads on different platforms.
            await this.AsyncCoroutine(Utilities.Wait(1));
            Clear();
            await _mapper.DestroyNodes();

            if (_maps.Length == map)
            {
                _currentStage = 0;
                _persistenceHandler.Save(_persistenceHandler.CurrentStagePersistenceKey, _currentStage);
                _message.gameObject.SetActive(true);
                return;
            }

            Create();
        }

        private void Create()
        {
            _persistenceHandler.Save(_persistenceHandler.CurrentStagePersistenceKey, _currentStage);

            if (_unlockedStages < _currentStage)
            {
                _unlockedStages = _currentStage;
                _persistenceHandler.Save(_persistenceHandler.UnlockedStagesPersistenceKey, _currentStage);
            }

            _current.text = $"#{_currentStage + 1}";
            _mapper.Create(_maps[_currentStage]);
        }

        private void Clear()
        {
            _connectors.Clear();
            _connectionsEvaluator.Reset();
        }
    }
}
