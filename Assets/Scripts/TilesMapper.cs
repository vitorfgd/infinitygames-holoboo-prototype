using System.Collections.Generic;
using UnityEngine;

public class TilesMapper : MonoBehaviour
{
    [Header("Prefab References")]
    [SerializeField]
    private GameObject _row;

    [SerializeField]
    private GameObject _battery;

    [SerializeField]
    private GameObject _connector;

    [SerializeField]
    private GameObject _connection;

    [Header("Settings")]
    [SerializeField]
    private Vector2 _size;

    [SerializeField]
    private int _connectors;

    private readonly List<Vector2> _batteryPositions = new List<Vector2>();
    private readonly List<Vector2> _connectorPositions = new List<Vector2>();

    private void Awake()
    {
        _batteryPositions.Add(RandomVector());
        for (var i = 0; i < _connectors; i++)
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

        for (var r = 0; r < _size.x; r++)
        {
            var row = Instantiate(_row, transform);
            for (var c = 0; c < _size.y; c++)
            {
                var position = new Vector2(r, c);
                if (_batteryPositions.Contains(position))
                {
                    var battery = Instantiate(_battery, row.transform);
                    Debug.Log($"Instancing battery.");
                    continue;
                }

                if (_connectorPositions.Contains(position))
                {
                    var connector = Instantiate(_connector, row.transform);
                    Debug.Log($"Instancing connector.");
                    continue;
                }

                var connection = Instantiate(_connection, row.transform);
                Debug.Log($"Instancing connection.");
            }
        }
    }

    private static bool IsDistant(Vector2 reference, Vector2 target) => !(Vector3.Distance(reference, target) <= 1);

    private Vector2 RandomVector() => new Vector2(Random.Range(0, (int) _size.x), Random.Range(0, (int) _size.y));
}
