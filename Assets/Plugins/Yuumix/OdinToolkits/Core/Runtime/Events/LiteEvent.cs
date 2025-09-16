using System;

namespace Yuumix.OdinToolkits.Core
{
    /// <summary>
    /// LiteEvent 是简单的封装事件，只能支持无参数方法的订阅。
    /// </summary>
    /// <remarks>基于 <c>event Action</c> 封装，扩展自动注销机制</remarks>
    
    public class LiteEvent
    {
        event Action Event;

        /// <summary>
        /// 订阅事件
        /// </summary>
        /// <param name="method">要订阅事件的方法</param>
        /// <returns>用于自动注销的命令对象</returns>

        public IAutoUnregister Register(Action method)
        {
            Event += method;
            return new UnregisterCommand(() => Unregister(method));
        }

        /// <summary>
        /// 取消订阅事件
        /// </summary>
        /// <param name="method">要取消订阅的方法</param>
    
        public void Unregister(Action method)
        {
            Event -= method;
        }

        /// <summary>
        /// 发布事件，通知所有订阅者
        /// </summary>
       
        public void Publish()
        {
            Event?.Invoke();
        }

        /// <summary>
        /// 清空所有订阅者
        /// </summary>
        public void Clear()
        {
            Event = null;
        }
    }

    /// <summary>
    /// 支持一个泛型参数的 LiteEvent 事件
    /// </summary>
    /// <typeparam name="T">事件参数的类型</typeparam>
    /// <remarks>基于 <c>event Action&lt;T&gt;</c> 封装，扩展自动注销机制</remarks>
   
    public class LiteEvent<T>
    {
        event Action<T> Event;

        /// <summary>
        /// 订阅带参数的事件
        /// </summary>
        /// <param name="method">要订阅事件的方法，该方法接受一个T类型的参数</param>
        /// <returns>用于自动注销的命令对象</returns>
        
        public IAutoUnregister Register(Action<T> method)
        {
            Event += method;
            return new UnregisterCommand(() => Unregister(method));
        }

        /// <summary>
        /// 取消订阅带参数的事件
        /// </summary>
        /// <param name="method">要取消订阅的方法</param>
      
        public void Unregister(Action<T> method)
        {
            Event -= method;
        }

        /// <summary>
        /// 发布带参数的事件，通知所有订阅者
        /// </summary>
        /// <param name="value">传递给订阅方法的参数值</param>
        public void Publish(T value)
        {
            Event?.Invoke(value);
        }

        /// <summary>
        /// 清空所有订阅者
        /// </summary>
        public void Clear()
        {
            Event = null;
        }
    }
}
