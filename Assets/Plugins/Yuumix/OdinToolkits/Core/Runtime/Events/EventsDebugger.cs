using System;
using System.Collections.Generic;
using Sirenix.OdinInspector;
#if UNITY_EDITOR
using UnityEditor;
#endif

namespace Yuumix.OdinToolkits.Core
{
    /// <summary>
    /// 事件调试类，用于在编辑器中查看 BetterEvent 中注册的 BetterEvent 实例
    /// </summary>
    [Serializable]
    public class EventsDebugger
    {
        static Lazy<EventsDebugger> _lazyInstance = new Lazy<EventsDebugger>(() => new EventsDebugger());

        /// <summary>
        /// 缓存游戏中使用的 BetterEvent 实例，用于在编辑器中查看
        /// </summary>
        /// <remarks>在向 BetterEvent 中注册 EventBinding 时缓存</remarks>
        [ShowInInspector]
        static readonly Dictionary<Type, List<object>> BetterEventCache = new Dictionary<Type, List<object>>();

        EventsDebugger() { }
        public static EventsDebugger Instance => _lazyInstance.Value;

        public static void CollectBetterEvent<T>(object betterEvent) where T : IEventArgs
        {
            Type eventArgsType = typeof(T);
            if (BetterEventCache.TryGetValue(eventArgsType, out List<object> objects))
            {
                objects.Add(betterEvent);
            }
            else
            {
                BetterEventCache[typeof(T)] = new List<object>
                {
                    betterEvent
                };
            }
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
                BetterEventCache.Clear();
            }
        }
#endif
    }
}
