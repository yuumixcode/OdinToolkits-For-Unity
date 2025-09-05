using System;

namespace Yuumix.OdinToolkits.Core.Runtime
{
    public interface IEventBinding
    {
        Action OnEventNoArgs { get; set; }
        int Order { get; }
        void Add(Action methodNoArgs);
        void Remove(Action methodNoArgs);
        void Clear();
        void Trigger();
    }

    public interface IEventBinding<T> : IEventBinding where T : IEventArgs
    {
        Action<T> OnEvent { get; set; }
        void Add(Action<T> method);
        void Remove(Action<T> method);
        void Trigger(T args);
        void IEventBinding.Trigger() { }
    }
}
