using System.Collections;
using UnityEngine;

namespace Konzit.GameSystem
{
    public class BaseGUIElementManager : MonoBehaviour
    {
        [SerializeField] protected GUIElementStored viewStored;

        protected virtual IEnumerator ShowInternal(BaseGUIElement guiElement)
        {
            guiElement.OnShow();
            guiElement.gameObject.SetActive(true);
            if (guiElement.ShowAnimation != null)
            {
                guiElement.ShowAnimation.PlayAnimation();
                yield return new WaitUntil(() => guiElement.ShowAnimation.AnimationCompleted);
            }
            guiElement.OnShown();
        }

        protected virtual IEnumerator HideInternal(BaseGUIElement guiElement)
        {
            guiElement.OnHide();
            if (guiElement.HideAnimation != null)
            {
                guiElement.HideAnimation.PlayAnimation();
                yield return new WaitUntil(() => guiElement.HideAnimation.AnimationCompleted);
            }
            guiElement.gameObject.SetActive(false);
            guiElement.OnHidden();
        }
    }
}
