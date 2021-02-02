using System;
using System.Collections.Generic;
using System.Linq;
using Models;
using UnityEngine;

public class ConnectionsEvaluator : MonoBehaviour
{
    [SerializeField]
    private Dragger _dragger;

    [SerializeField]
    private NodesMapper _mapper;

    private bool _pressed;
    private readonly List<Node> _connectors = new List<Node>();

    private void Awake()
    {
        _dragger.Pressed.AddListener(
            () => { _dragger.PositionChanged.AddListener(Intersect); }
        );
    }

    public void Reset()
    {
        _dragger.PositionChanged.RemoveListener(Intersect);
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
