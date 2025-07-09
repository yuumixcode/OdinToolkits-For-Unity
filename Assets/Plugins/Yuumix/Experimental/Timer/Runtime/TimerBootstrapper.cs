#if UNITY_EDITOR
using UnityEditor;
#endif
using UnityEngine;
using UnityEngine.LowLevel;
using UnityEngine.PlayerLoop;
using Yuumix.OdinToolkits.Modules.Utilities;

namespace Yuumix.OdinToolkits.Modules.Timer.Runtime
{
    internal static class TimerBootstrapper
    {
        [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.AfterAssembliesLoaded)]
        internal static void Initialize()
        {
            var playerLoop = PlayerLoop.GetCurrentPlayerLoop();

            if (!InsertTimerManager<Update>(ref playerLoop, 0))
            {
                Debug.LogWarning("ImprovedTimer: Failed to insert TimerManager into the player loop system.");
                return;
            }

            // Set the player loop system
            PlayerLoop.SetPlayerLoop(playerLoop);
            // Print the player loop system
            // PlayerLoopUtil.PrintPlayerLoop(playerLoop);

#if UNITY_EDITOR
            EditorApplication.playModeStateChanged -= OnPlayModeState;
            EditorApplication.playModeStateChanged += OnPlayModeState;

            static void OnPlayModeState(PlayModeStateChange state)
            {
                if (state == PlayModeStateChange.ExitingPlayMode)
                {
                    var currentPlayerLoop = PlayerLoop.GetCurrentPlayerLoop();
                    RemoveTimerManager<Update>(ref currentPlayerLoop);
                    PlayerLoop.SetPlayerLoop(currentPlayerLoop);
                    TimerManager.Clear();
                }
            }
#endif
        }

        static void RemoveTimerManager<T>(ref PlayerLoopSystem loop)
        {
            var timerSystem = PlayerLoopUtil.GetNewCustomPlayerLoopSystem(typeof(TimerManager), TimerManager.UpdateTimer);
            PlayerLoopUtil.RemoveSystem<T>(ref loop, timerSystem);
        }

        static bool InsertTimerManager<T>(ref PlayerLoopSystem loop, int index)
        {
            var timerSystem = PlayerLoopUtil.GetNewCustomPlayerLoopSystem(typeof(TimerManager), TimerManager.UpdateTimer);
            // TODO: Insert the timer system into the player loop system at the specified index
            return PlayerLoopUtil.InsertSystem<T>(ref loop, timerSystem, index);
        }
    }
}
