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

    private readonly List<Node> _connectors = new List<Node>();

    private void Awake()
    {
        _dragger.PositionChanged.AddListener(Intersect);
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
