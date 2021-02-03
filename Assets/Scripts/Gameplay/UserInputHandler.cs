using Events;
using UnityEngine;
using UnityEngine.Events;

namespace Gameplay
{
    public class UserInputHandler : MonoBehaviour
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
                // Responsible for resetting some values
                Pressed?.Invoke();
            }

            if (!Input.GetMouseButton(0))
            {
                return;
            }

            // Invoked every new finger position when screen is being pressed.
            PositionChanged?.Invoke(Input.mousePosition);
        }
    }
}
