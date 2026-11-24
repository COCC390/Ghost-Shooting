using UnityEngine;
using UnityEngine.InputSystem;

namespace Gameplay
{
    public class WeaponAimPoint : MonoBehaviour
    {
        [SerializeField] private CharacterInput _input;
        [SerializeField] private Transform _aimObj;

        private Vector2 _mouseScreenPosition;

        private void Start()
        {
            _input.onRotateStarted += MouseValueReceive;
            _input.onRotatePerformed += MouseValueReceive;
            _input.onRotateCancelled += MouseValueReceive;
        }

        private void MouseValueReceive(InputAction.CallbackContext callbackContext)
        {
            _mouseScreenPosition = callbackContext.ReadValue<Vector2>();
            var ray = Camera.main.ScreenPointToRay(_mouseScreenPosition);
            var aimPlane = new Plane(Vector3.up, _aimObj.position);

            if (aimPlane.Raycast(ray, out var distance))
                _aimObj.position = ray.GetPoint(distance);
        }
    }
}
