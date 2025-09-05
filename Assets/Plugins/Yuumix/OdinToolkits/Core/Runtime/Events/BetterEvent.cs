using System;
using System.Collections.Generic;

namespace Yuumix.OdinToolkits.Core.Runtime
{
    public interface IBetterEvent { }

    public delegate void CustomEventHandler(object sender);

    public delegate void CustomEventHandler<in TEvent>(object sender, TEvent e);

    public partial class BetterEvent
    {
        event CustomEventHandler EventNoArgs;

        public void Register(CustomEventHandler method)
        {
            EventNoArgs += method;
        }

        public void Unregister(CustomEventHandler method)
        {
            EventNoArgs -= method;
        }

        public void Publish(object sender)
        {
            EventNoArgs?.Invoke(sender);
        }
        
        public void Publish(object sender, EventArgs e)
        {
          
        }
    }

    public sealed class BetterEvent<T> where T : IEventArgs
    {
        // 新增：缓存委托实例，键为原始Action，值为对应的CustomEventHandler
        readonly Dictionary<Action<object, T>, CustomEventHandler<T>> _delegateCache =
            new Dictionary<Action<object, T>, CustomEventHandler<T>>();

        List<IEventBinding<T>> _bindings;

        event CustomEventHandler<T> Event;

        public void Register(Action<object, T> method)
        {
            // 检查是否已缓存，避免重复创建
            if (!_delegateCache.TryGetValue(method, out CustomEventHandler<T> handler))
            {
                handler = new CustomEventHandler<T>(method);
                _delegateCache[method] = handler; // 缓存委托实例
            }

            Event += handler; // 使用缓存的实例注册
        }

        public void Unregister(Action<object, T> method)
        {
            // 从缓存中获取注册时使用的同一个委托实例
            if (_delegateCache.TryGetValue(method, out CustomEventHandler<T> handler))
            {
                Event -= handler;              // 移除缓存的实例
                _delegateCache.Remove(method); // 清理缓存
            }
        }

        public void Register(IEventBinding<T> binding)
        {
            EnsureBindingsIsNotNull();
            _bindings.Add(binding);
            _bindings.Sort((a, b) => a.Order.CompareTo(b.Order));
            EventCenter.RegisterEventBus<T>(this);
        }

        public void Unregister(IEventBinding<T> binding)
        {
            EnsureBindingsIsNotNull();
            _bindings.Remove(binding);
        }

        public void Publish(object sender, T e)
        {
            Event?.Invoke(sender, e);
            // 创建绑定列表的副本进行遍历，避免在发布过程中修改集合导致的异常
            IEventBinding<T>[] bindingsCopy = _bindings.ToArray();

            foreach (IEventBinding<T> binding in bindingsCopy)
            {
                if (_bindings.Contains(binding))
                {
                    binding.Trigger(e);
                }
            }
        }

        public void Clear()
        {
            Event = null;
            _delegateCache.Clear(); // 清空缓存
            _bindings?.Clear();
        }

        void EnsureBindingsIsNotNull()
        {
            _bindings ??= new List<IEventBinding<T>>();
        }
    }
}
