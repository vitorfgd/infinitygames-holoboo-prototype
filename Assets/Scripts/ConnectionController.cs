using System.Collections.Generic;
using System.Linq;
using Models;
using UnityEngine;

public class ConnectionController : MonoBehaviour
{
    [SerializeField]
    private Dragger _dragger;

    [SerializeField]
    private NodesMapper _mapper;

    private List<Node> _connectors;

    private void Awake()
    {
        _dragger.PositionChanged.AddListener(Intersect);
        foreach (var node in _mapper.Nodes.Where(node => node.Value is Connector))
        {
            var connector = (Connector) node.Value;
            _connectors.Add(connector);
            connector.Connected.AddListener(EvaluateGame);
        }
        
    }

    private void EvaluateGame()
    {
        if (_connectors.All(node => node.ConnectedToBattery))
        {
            Debug.Log("Game ended");
        }
    }

    private void Intersect(Vector2 position)
    {
        foreach (var node in _mapper.Nodes.Where(
            node => Vector2.Distance(position, node.Value.Coordinates()) < node.Value.Size().x / 2
        ))
        {
            node.Value.Connect();
        }
    }
}
