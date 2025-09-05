using System;

namespace Yuumix.OdinToolkits.Core.Runtime
{
    public class EventBinding : IEventBinding
    {
        Action _onEventNoArgs;

        Action IEventBinding.OnEventNoArgs
        {
            get => _onEventNoArgs;
            set => _onEventNoArgs = value;
        }

        public int Order { get; }

        public void Add(Action methodNoArgs)
        {
            _onEventNoArgs += methodNoArgs;
        }

        public void Remove(Action methodNoArgs)
        {
            _onEventNoArgs -= methodNoArgs;
        }

        public void Clear()
        {
            _onEventNoArgs = () => { };
        }

        void IEventBinding.Trigger()
        {
            _onEventNoArgs();
        }

        public EventBinding(Action methodNoArgs, int order = 0)
        {
            _onEventNoArgs = methodNoArgs;
            Order = order;
        }
    }

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

        public void Add(Action methodNoArgs)
        {
            _onEventNoArgs += methodNoArgs;
        }

        Action IEventBinding.OnEventNoArgs
        {
            get => _onEventNoArgs;
            set => _onEventNoArgs = value;
        }

        Action<T> IEventBinding<T>.OnEvent
        {
            get => _onEvent;
            set => _onEvent = value;
        }

        public int Order { get; }

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

        public void Clear()
        {
            _onEventNoArgs = () => { };
            _onEvent = (_) => { };
        }

        void IEventBinding<T>.Trigger(T args)
        {
            _onEventNoArgs();
            _onEvent(args);
        }
    }
}
