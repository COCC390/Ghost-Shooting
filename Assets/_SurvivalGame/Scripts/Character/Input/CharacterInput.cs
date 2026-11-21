using System;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Gameplay
{
    [CreateAssetMenu(fileName = "CharacterInput", menuName = "Game/Gameplay/Input/GameplayInput")]
    public class CharacterInput : BaseInput
    {
        public event Action<InputAction.CallbackContext> onMoveStarted;
        public event Action<InputAction.CallbackContext> onMovePerformed;
        public event Action<InputAction.CallbackContext> onMoveCancelled;

        public event Action<InputAction.CallbackContext> onRotateStarted;
        public event Action<InputAction.CallbackContext> onRotatePerformed;
        public event Action<InputAction.CallbackContext> onRotateCancelled;

        public event Action<InputAction.CallbackContext> onLShiftPerformed;
        public event Action<InputAction.CallbackContext> onLShiftCancelled;

        public event Action<InputAction.CallbackContext> onShootPerformed;
        public event Action<InputAction.CallbackContext> onShootCancelled;

        public override void EnableInput()
        {
            gameInputAction.Gameplay.Move.started += MoveStarted;
            gameInputAction.Gameplay.Move.performed += MovePerformed;
            gameInputAction.Gameplay.Move.canceled += MoveCancelled;

            gameInputAction.Gameplay.Rotate.started += RotateStarted;
            gameInputAction.Gameplay.Rotate.performed += RotatePerformed;
            gameInputAction.Gameplay.Rotate.canceled += RotateCancelled;

            gameInputAction.Gameplay.Shift_Run.performed += LShiftPerformed;
            gameInputAction.Gameplay.Shift_Run.canceled += LShiftCancelled;

            gameInputAction.Gameplay.Shoot.performed += ShootPerformed;
            gameInputAction.Gameplay.Shoot.canceled += ShootCancelled;
        }

        public override void DisableInput()
        {
            gameInputAction.Gameplay.Move.started -= MoveStarted;
            gameInputAction.Gameplay.Move.performed -= MovePerformed;
            gameInputAction.Gameplay.Move.canceled -= MoveCancelled;

            gameInputAction.Gameplay.Rotate.started -= RotateStarted;
            gameInputAction.Gameplay.Rotate.performed -= RotatePerformed;
            gameInputAction.Gameplay.Rotate.canceled -= RotateCancelled;

            gameInputAction.Gameplay.Shift_Run.performed -= LShiftPerformed;
            gameInputAction.Gameplay.Shift_Run.canceled -= LShiftCancelled;

            gameInputAction.Gameplay.Shoot.performed -= ShootPerformed;
            gameInputAction.Gameplay.Shoot.canceled -= ShootCancelled;
        }

        #region Move
        private void MoveStarted(InputAction.CallbackContext callbackContext)
        {
            onMoveStarted?.Invoke(callbackContext);
        }

        private void MovePerformed(InputAction.CallbackContext callbackContext)
        {
            onMovePerformed?.Invoke(callbackContext);
        }

        private void MoveCancelled(InputAction.CallbackContext callbackContext)
        {
            onMoveCancelled?.Invoke(callbackContext);
        }
        #endregion

        #region LShift_Run
        private void LShiftPerformed(InputAction.CallbackContext callbackContext)
        {
            onLShiftPerformed?.Invoke(callbackContext);
        }

        private void LShiftCancelled(InputAction.CallbackContext callbackContext)
        {
            onLShiftCancelled?.Invoke(callbackContext);
        }
        #endregion

        #region Rotate
        private void RotateStarted(InputAction.CallbackContext callbackContext)
        {
            onRotateStarted?.Invoke(callbackContext);
        }

        private void RotatePerformed(InputAction.CallbackContext callbackContext)
        {
            onRotatePerformed?.Invoke(callbackContext);
        }

        private void RotateCancelled(InputAction.CallbackContext callbackContext)
        {
            onRotateCancelled?.Invoke(callbackContext);
        }
        #endregion

        #region Shooting
        private void ShootPerformed(InputAction.CallbackContext callbackContext)
        {
            onShootPerformed?.Invoke(callbackContext);
        }

        private void ShootCancelled(InputAction.CallbackContext callbackContext)
        {
            onShootCancelled?.Invoke(callbackContext);
        }
        #endregion
    }
}