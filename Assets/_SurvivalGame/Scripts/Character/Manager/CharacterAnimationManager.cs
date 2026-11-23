using UnityEngine;

namespace Gameplay
{
    public class CharacterAnimationManager : MonoBehaviour
    {
        [SerializeField] private Animator _characterAnimator;

        public void PlayMovementAnim(float value)
        {
            _characterAnimator.SetFloat("Speed", value);
        }

        public void SetMovementDirection(float moveX, float moveY)
        {
            _characterAnimator.SetFloat("X_Axis", moveX);
            _characterAnimator.SetFloat("Y_Axis", moveY);
        }

        public void PlayShootAnim(bool value)
        {
            _characterAnimator.SetBool("IsShoot", value);
        }

        public void PlayAimAnim(bool value)
        {
            _characterAnimator.SetBool("IsAim", value);
        }
    }
}
