using UnityEngine;
using UnityEngine.InputSystem;

namespace Gameplay
{
    public class CharacterShooting : MonoBehaviour
    {
        [SerializeField] private CharacterInput _characterInput;
        [SerializeField] private CharacterAnimationManager _animationMgr;

        public void InitializeShooting()
        {
            _characterInput.onShootPerformed += ShootingHandler;
            _characterInput.onShootCancelled += ShootingHandler;

            _characterInput.onAimPerformed += AimingHandler;
            _characterInput.onAimCancelled += AimingHandler;
        }    

        private void ShootingHandler(InputAction.CallbackContext callbackContext)
        {
            if (callbackContext.performed)
                _animationMgr.PlayShootAnim(true);
            else if(callbackContext.canceled)
                _animationMgr.PlayShootAnim(false);
        }

        private void AimingHandler(InputAction.CallbackContext callbackContext)
        {
            if (callbackContext.performed)
                _animationMgr.PlayAimAnim(true);
            else
                _animationMgr.PlayAimAnim(false);
        }
    }
}
