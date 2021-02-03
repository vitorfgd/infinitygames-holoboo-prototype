﻿using System.Collections.Generic;
using DG.Tweening;
using Models;
using UnityEngine;
using UnityEngine.Events;

public class NodesMapper : MonoBehaviour
{
    public Dictionary<Vector2, Node> Nodes { get; } = new Dictionary<Vector2, Node>();

    [HideInInspector]
    public UnityEvent ConstructionFinished = new UnityEvent();

    [Header("References")]
    [SerializeField]
    private Transform _container;

    [SerializeField]
    private GameObject _row;

    [SerializeField]
    private Node _battery;

    [SerializeField]
    private Node _connector;

    [SerializeField]
    private Node _connection;

    private Map _map;

    private readonly List<Vector2> _batteryPositions = new List<Vector2>();
    private readonly List<Vector2> _connectorPositions = new List<Vector2>();

    private readonly Vector2[] _connectionCandidate =
    {
        Vector2.up,
        Vector2.right,
        Vector2.down,
        Vector2.left
    };

    public void Create(Map map)
    {
        _map = map;
        DefinePositions();
        for (var r = 0; r < _map.Size.x; r++)
        {
            var row = Instantiate(_row, _container.transform);
            for (var c = 0; c < _map.Size.y; c++)
            {
                Node instance = null;
                var position = new Vector2(r, c);

                if (_batteryPositions.Contains(position))
                {
                    instance = Instantiate(_battery, row.transform);
                }

                if (_connectorPositions.Contains(position))
                {
                    instance = Instantiate(_connector, row.transform);
                }

                if (instance == null)
                {
                    instance = Instantiate(_connection, row.transform);
                }

                instance.View.DOFade(1f, .25f);
                Nodes[position] = instance;
                DefineSiblings(position, instance);
            }
        }

        ConstructionFinished?.Invoke();
    }

    public void Destroy()
    {
        foreach (Transform child in _container)
        {
            Destroy(child.gameObject);
        }

        _batteryPositions.Clear();
        _connectorPositions.Clear();

        foreach (var node in Nodes.Values)
        {
            Destroy(node.gameObject);
        }

        Nodes.Clear();
    }

    private void DefinePositions()
    {
        _batteryPositions.Add(RandomVector());
        for (var i = 0; i < _map.Connectors; i++)
        {
            var position = RandomVector();
            foreach (var batteryPosition in _batteryPositions)
            {
                while (!IsDistant(position, batteryPosition)
                    || _connectorPositions.Contains(position))
                {
                    position = RandomVector();
                }
            }

            _connectorPositions.Add(position);
        }
    }

    private void DefineSiblings(Vector2 position, Node instance)
    {
        foreach (var candidate in _connectionCandidate)
        {
            var reference = new Vector2(position.x + candidate.x, position.y + candidate.y);
            if (!Nodes.TryGetValue(reference, out var node))
            {
                continue;
            }

            var key = candidate * -1;
            node.Connections[key] = instance;
            instance.Connections[candidate] = node;
        }
    }

    private static bool IsDistant(Vector2 reference, Vector2 target) => !(Vector3.Distance(reference, target) <= 1);

    private Vector2 RandomVector() =>
        new Vector2(Random.Range(0, (int) _map.Size.x), Random.Range(0, (int) _map.Size.y));
}
