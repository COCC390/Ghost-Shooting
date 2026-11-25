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
            MouseValueReceive();   
        }

        private void LateUpdate()
        {
            MouseValueReceive();
        }

        private void MouseValueReceive()
        {
            _mouseScreenPosition = Mouse.current.position.value;
            var ray = Camera.main.ScreenPointToRay(_mouseScreenPosition);
            var aimPlane = new Plane(Vector3.up, _aimObj.position);

            if (aimPlane.Raycast(ray, out var distance))
                _aimObj.position = ray.GetPoint(distance);
        }
    }
}
