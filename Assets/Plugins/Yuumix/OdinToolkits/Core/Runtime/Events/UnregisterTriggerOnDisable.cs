using UnityEngine;

namespace Yuumix.OdinToolkits.Core.Runtime
{
    /// <summary>
    /// 在MonoBehaviour的OnDisable事件中自动注销已注册项的组件
    /// </summary>
    public class UnregisterTriggerOnDisable : UnregisterTriggerBase
    {
        void OnDisable()
        {
            UnregisterAll();
        }
    }
}
