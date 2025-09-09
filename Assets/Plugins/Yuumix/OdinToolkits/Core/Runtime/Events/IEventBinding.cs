using System;

namespace Yuumix.OdinToolkits.Core.Runtime
{
    public interface IEventBinding<T> where T : IEventArgs
    {
        int Order { get; }
        void Add(Action methodNoArgs);
        void Add(Action<T> method);
        void Remove(Action methodNoArgs);
        void Remove(Action<T> method);
        void Trigger(T e);
        void Clear();
    }
}
