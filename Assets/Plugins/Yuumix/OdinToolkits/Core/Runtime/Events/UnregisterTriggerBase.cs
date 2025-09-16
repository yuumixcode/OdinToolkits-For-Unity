using System.Collections.Generic;
using UnityEngine;

namespace Yuumix.OdinToolkits.Core
{
    /// <summary>
    /// 自动注销功能的抽象基类，提供在特定时刻自动注销已注册项的功能
    /// </summary>
    public abstract class UnregisterTriggerBase : MonoBehaviour
    {
        readonly List<IAutoUnregister> _autoUnregisters = new List<IAutoUnregister>();

        /// <summary>
        /// 注册一个需要自动注销的对象
        /// </summary>
        /// <param name="autoUnregister">需要自动注销的对象</param>
        public void Register(IAutoUnregister autoUnregister)
        {
            _autoUnregisters.Add(autoUnregister);
        }

        /// <summary>
        /// 在派生类中重写此方法以执行特定时刻的自动注销逻辑
        /// </summary>
        protected void UnregisterAll()
        {
            for (var i = 0; i < _autoUnregisters.Count; i++)
            {
                _autoUnregisters[i].Unregister();
            }
        }
    }
}
