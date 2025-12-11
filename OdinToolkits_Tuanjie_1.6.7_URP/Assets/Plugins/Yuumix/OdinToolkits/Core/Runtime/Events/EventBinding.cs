using System;
using Sirenix.OdinInspector;

namespace Yuumix.OdinToolkits.Core
{
    [Serializable]
    public class EventBinding<T> : IEventBinding<T> where T : IEventArgs
    {
        Action<T> _onEvent = _ => { };
        Action _onEventNoArgs = () => { };

        public EventBinding(Action methodNoArgs, int order = 0)
        {
            _onEventNoArgs = methodNoArgs;
            Order = order;
        }

        public EventBinding(Action<T> method, int order = 0)
        {
            _onEvent = method;
            Order = order;
        }

        #region IEventBinding<T> Members

        [ShowInInspector]
        public int Order { get; }

        public void Add(Action methodNoArgs)
        {
            _onEventNoArgs += methodNoArgs;
        }

        public void Add(Action<T> method)
        {
            _onEvent += method;
        }

        public void Remove(Action methodNoArgs)
        {
            _onEventNoArgs -= methodNoArgs;
        }

        public void Remove(Action<T> method)
        {
            _onEvent -= method;
        }

        void IEventBinding<T>.Trigger(T e)
        {
            _onEventNoArgs?.Invoke();
            _onEvent?.Invoke(e);
        }

        public void Clear()
        {
            _onEventNoArgs = null;
            _onEvent = null;
        }

        #endregion
    }
}
