using System.Collections.Generic;
using System.Linq;
using Extensions;
using Models;
using UnityEngine;

public class GameManager : SingletonBehaviour<GameManager>
{
    [SerializeField]
    private Map[] _maps;

    [SerializeField]
    private NodesMapper _nodesMapper;

    private int _current;
    private readonly List<Node> _connectors = new List<Node>();

    protected override void Awake()
    {
        base.Awake();
        _nodesMapper.ConstructionFinished.AddListener(SubscribeToConnectors);
    }

    protected void Start()
    {
        _current = PlayerPrefs.GetInt("_currentMap", 0);
        _nodesMapper.Create(_maps[_current]);
    }
    
    private void SubscribeToConnectors()
    {
        Debug.Log(_nodesMapper.Nodes.Count);
        foreach (var node in _nodesMapper.Nodes.Where(node => node.Value is Connector))
        {
            var connector = (Connector) node.Value;
            _connectors.Add(connector);
            connector.ConnectorWasConnected.AddListener(EvaluateGame);
        }
    }

    private void EvaluateGame()
    {
        if (_connectors.All(node => node.ConnectedToBattery))
        {
            Debug.Log("Game ended");
        }
    }
}
