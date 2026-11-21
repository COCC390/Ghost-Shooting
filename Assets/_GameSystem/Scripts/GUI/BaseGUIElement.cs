using UnityEngine;

namespace Konzit.GameSystem
{
    public class BaseGUIElement : MonoBehaviour, IGUIElement
    {
        [SerializeField] protected BaseGUIAnimation showAnimation;
        [SerializeField] protected BaseGUIAnimation hideAnimation;

        public BaseGUIAnimation ShowAnimation => showAnimation;
        public BaseGUIAnimation HideAnimation => hideAnimation;

        public virtual void Initialize()
        {
        }

        public virtual void OnShow()
        {
        }

        public virtual void OnShown()
        {
        }

        public virtual void OnHide()
        {
        }

        public virtual void OnHidden()
        {
        }
    }
}
