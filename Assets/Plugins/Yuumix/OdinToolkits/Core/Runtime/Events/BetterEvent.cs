using Sirenix.OdinInspector;
using System;
using System.Collections.Generic;

namespace Yuumix.OdinToolkits.Core
{
    /// <summary>
    /// 支持特定事件参数类型的 BetterEvent 泛型版本
    /// </summary>
    /// <typeparam name="T">事件参数类型，必须实现 IEventArgs 接口</typeparam>
    /// <remarks>扩展自动注销和有序执行订阅方法机制</remarks>
    [Serializable]
    [InlineProperty]
    public class BetterEvent<T> where T : IEventArgs
    {
        List<IEventBinding<T>> _bindings;

        /// <summary>
        /// 订阅事件绑定
        /// </summary>
        /// <param name="binding">事件绑定对象</param>
        /// <returns>用于自动注销的命令对象</returns>
        public IAutoUnregister Register(IEventBinding<T> binding)
        {
            EnsureBindingsIsNotNull();
            _bindings.Add(binding);
            _bindings.Sort((a, b) => a.Order.CompareTo(b.Order));
            EventsDebugger.CollectBetterEvent<T>(this);
            return new UnregisterCommand(() => Unregister(binding));
        }

        /// <summary>
        /// 取消订阅事件绑定
        /// </summary>
        /// <param name="binding">要取消订阅的事件绑定对象</param>
        public void Unregister(IEventBinding<T> binding)
        {
            EnsureBindingsIsNotNull();
            _bindings.Remove(binding);
        }

        /// <summary>
        /// 发布带参数的事件
        /// </summary>
        /// <param name="e">事件参数</param>
        [Button("发布事件", ButtonSizes.Medium, ButtonStyle.FoldoutButton)]
        [HideInEditorMode]
        public void Publish(T e)
        {
            EnsureBindingsIsNotNull();
            // 创建绑定列表的副本进行遍历，避免在发布过程中修改集合导致的异常
            var bindingsCopy = _bindings.ToArray();

            foreach (var binding in bindingsCopy)
            {
                if (_bindings.Contains(binding))
                {
                    binding.Trigger(e);
                }
            }
        }

        /// <summary>
        /// 清空所有事件订阅
        /// </summary>
        public void Clear()
        {
            _bindings?.Clear();
        }

        void EnsureBindingsIsNotNull()
        {
            _bindings ??= new List<IEventBinding<T>>();
        }
    }
}
