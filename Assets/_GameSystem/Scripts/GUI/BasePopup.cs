using UnityEngine;
using System;

namespace Konzit.GameSystem
{
    public class BasePopup : BaseGUIElement, IGUIElementInternalHide
    {
        internal Action HideAction;

        public virtual void Hide()
        {
            HideAction?.Invoke();
        }
    }

    public abstract class BasePopupWithData<T> : BasePopup
    {
        protected T value;
        public virtual void BindData(T value)
        {
            this.value = value; 
        }    
    }    
}
