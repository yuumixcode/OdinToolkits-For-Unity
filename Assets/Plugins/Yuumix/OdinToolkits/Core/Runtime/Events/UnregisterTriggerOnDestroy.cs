using UnityEngine;

namespace Yuumix.OdinToolkits.Core
{
    /// <summary>
    /// 在MonoBehaviour的OnDestroy事件中自动注销已注册项的组件
    /// </summary>
    public class UnregisterTriggerOnDestroy : UnregisterTriggerBase
    {
        void OnDestroy()
        {
            UnregisterAll();
        }
    }
}