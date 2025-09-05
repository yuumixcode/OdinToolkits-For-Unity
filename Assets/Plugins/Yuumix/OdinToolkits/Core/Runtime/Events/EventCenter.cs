using System;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEditor;

namespace Yuumix.OdinToolkits.Core.Runtime
{
    [Serializable]
    public class EventCenter
    {
        public static EventCenter Instance => _lazyInstance.Value;

        static Lazy<EventCenter> _lazyInstance = new Lazy<EventCenter>(() => new EventCenter());
        EventCenter() { }

        /// <summary>
        /// 缓存游戏中使用的事件总线，用于编辑器阶段检查
        /// </summary>
        /// <remarks>在向 EventBus 注册 EventBinding 时缓存</remarks>
        [ShowInInspector]
        static readonly Dictionary<Type, object> EventBusesCache = new Dictionary<Type, object>();

        public static void RegisterEventBus<T>(object eventBus) where T : IEventArgs
        {
            EventBusesCache[typeof(T)] = eventBus;
        }

#if UNITY_EDITOR
        [InitializeOnLoadMethod]
        static void InitializeEditor()
        {
            EditorApplication.playModeStateChanged -= EditorApplication_OnPlayModeStateChanged;
            EditorApplication.playModeStateChanged += EditorApplication_OnPlayModeStateChanged;
        }

        static void EditorApplication_OnPlayModeStateChanged(PlayModeStateChange state)
        {
            if (state == PlayModeStateChange.ExitingPlayMode)
            {
                EventBusesCache.Clear();
            }
        }
#endif
    }
}
