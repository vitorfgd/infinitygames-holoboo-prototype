using System;
using UnityEngine;
using UnityEngine.Events;

[Serializable]
public class TrackerChanged : UnityEvent<Vector2>
{
}

public class Dragger : MonoBehaviour
{
    public TrackerChanged PositionChanged = new TrackerChanged();

    private Vector2 _mousePositions;

    private void Update()
    {
        if (!Input.GetMouseButton(0))
        {
            return;
        }

        var mousePosition = Input.mousePosition;
        PositionChanged?.Invoke(mousePosition);
    }
}