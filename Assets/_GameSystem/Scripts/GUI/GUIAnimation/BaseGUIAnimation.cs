using UnityEngine;

namespace Konzit.GameSystem
{
    public abstract class BaseGUIAnimation : MonoBehaviour
    {
        protected bool _animationCompleted;

        public bool AnimationCompleted => _animationCompleted;

        public abstract void PlayAnimation();
        public abstract void StopAnimation();
    }
}
