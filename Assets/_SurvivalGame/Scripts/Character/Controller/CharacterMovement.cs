using UnityEngine;
using UnityEngine.InputSystem;

namespace Gameplay
{
    public class CharacterMovement : MonoBehaviour
    {
        private const float MinimumAimDistanceSquared = 0.0025f;

        [SerializeField] private CharacterInput _characterInput;
        [SerializeField] private CharacterAnimationManager _animationMgr;
        private Camera _gameplayCamera;
        private Vector3 _inputValue;
        private Vector2 _mouseScreenPosition;
        private bool _hasAimInput;

        private float _movementSpeed;
        private float _runMultiplier;
        private float _rotateSpeed;

        private bool _isRun = false;

        public void InitializeMovement(CharacterConfig characterConfig)
        {
            _gameplayCamera = Camera.main;
            if (_gameplayCamera == null && Camera.allCamerasCount == 1)
                _gameplayCamera = Camera.allCameras[0];

            _characterInput.onMoveStarted += MoveInput;
            _characterInput.onMovePerformed += MoveInput;
            _characterInput.onMoveCancelled += MoveInput;

            _characterInput.onRotateStarted += RotateInput;
            _characterInput.onRotatePerformed += RotateInput;
            _characterInput.onRotateCancelled += RotateInput;

            _characterInput.onLShiftPerformed += RunTriggered;
            _characterInput.onLShiftCancelled += RunTriggered;

            _movementSpeed = characterConfig.MoveSpeed;
            _rotateSpeed = characterConfig.RotateSpeed;
            _runMultiplier = characterConfig.RunMultiplier;
        }

        private void MoveInput(InputAction.CallbackContext callbackContext)
        {
            var value = callbackContext.ReadValue<Vector2>();
            _inputValue = new Vector3(value.x, 0, value.y);
        }

        private void RotateInput(InputAction.CallbackContext callbackContext)
        {
            _hasAimInput = !callbackContext.canceled;
            if (_hasAimInput)
                _mouseScreenPosition = callbackContext.ReadValue<Vector2>();
        }

        private void RunTriggered(InputAction.CallbackContext callbackContext)
        {
            if(callbackContext.performed)
                _isRun = true;
            if(callbackContext.canceled)
                _isRun = false;
        }

        private void Update()
        {
            var moveSpeed = _isRun ? _movementSpeed * _runMultiplier : _movementSpeed;
            _animationMgr.PlayMovementAnim(_inputValue.magnitude * moveSpeed);

            float moveX = Vector3.Dot(_inputValue, transform.right);
            float moveY = Vector3.Dot(_inputValue, transform.forward);

            _animationMgr.SetMovementDirection(moveX, moveY);

            this.transform.position += _inputValue * moveSpeed * Time.deltaTime;
        }

        private void LateUpdate()
        {
            if (!_hasAimInput || _gameplayCamera == null)
                return;

            var position = transform.position;
            var aimPlane = new Plane(Vector3.up, position);
            var aimRay = _gameplayCamera.ScreenPointToRay(_mouseScreenPosition);
            if (!aimPlane.Raycast(aimRay, out var distance))
                return;

            var direction = aimRay.GetPoint(distance) - position;
            if (direction.sqrMagnitude < MinimumAimDistanceSquared)
                return;

            var targetYaw = Mathf.Atan2(direction.x, direction.z) * Mathf.Rad2Deg;
            var blend = 1f - Mathf.Exp(-_rotateSpeed * Time.deltaTime);
            var yaw = Mathf.LerpAngle(transform.eulerAngles.y, targetYaw, blend);
            transform.rotation = Quaternion.Euler(0f, yaw, 0f);
        }
    }
}
