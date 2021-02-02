using System;
using UnityEngine;
using UnityEngine.Events;

[Serializable]
public class TrackerChanged : UnityEvent<Vector2>
{
}

public class Dragger : MonoBehaviour
{
    [HideInInspector]
    public UnityEvent Pressed = new UnityEvent();

    [HideInInspector]
    public TrackerChanged PositionChanged = new TrackerChanged();

    private bool _canPress;

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Pressed?.Invoke();
        }

        if (!Input.GetMouseButton(0))
        {
            return;
        }

        PositionChanged?.Invoke(Input.mousePosition);
    }
}
