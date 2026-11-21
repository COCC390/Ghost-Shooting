using UnityEngine;

namespace Konzit.GameSystem
{
    public class ScrollViewItem : MonoBehaviour
    {
        public int DataIndex { get; private set; } = -1;

        public void Bind(object model, int dataIndex)
        {
            DataIndex = dataIndex;
            OnBind(model, dataIndex);
        }

        public void Unbind()
        {
            DataIndex = -1;
            OnUnbind();
        }

        protected virtual void OnBind(object model, int dataIndex)
        {
        }

        protected virtual void OnUnbind()
        {
        }
    }

    public abstract class ScrollViewItem<TModel> : ScrollViewItem
    {
        protected sealed override void OnBind(object model, int dataIndex)
        {
            if (model is TModel typedModel)
            {
                Bind(typedModel, dataIndex);
                return;
            }

            Debug.LogError($"Expected {typeof(TModel).Name}, received {model?.GetType().Name ?? "null"}.", this);
        }

        protected abstract void Bind(TModel model, int dataIndex);
    }
}
