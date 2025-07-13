using System;
using System.Collections.Generic;
using System.Text;
using UnityEngine;
using UnityEngine.LowLevel;
using Yuumix.OdinToolkits.Core;

namespace Yuumix.OdinToolkits.Shared
{
    [MultiLanguageComment("玩家循环工具类", "Player loop utility class")]
    public static class PlayerLoopUtil
    {
        [MultiLanguageComment("从玩家循环系统中移除指定系统", "Remove a specified system from the player loop system")]
        public static void RemoveSystem<T>(ref PlayerLoopSystem loop, PlayerLoopSystem systemToRemove)
        {
            if (loop.subSystemList == null || loop.subSystemList.Length == 0)
            {
                return;
            }

            var playerLoopSystemList = new List<PlayerLoopSystem>(loop.subSystemList);
            for (var i = 0; i < playerLoopSystemList.Count; ++i)
            {
                if (playerLoopSystemList[i].type == systemToRemove.type &&
                    playerLoopSystemList[i].updateDelegate == systemToRemove.updateDelegate)
                {
                    playerLoopSystemList.RemoveAt(i);
                    loop.subSystemList = playerLoopSystemList.ToArray();
                }
            }

            HandleSubSystemLoopForRemoval<T>(ref loop, systemToRemove);
        }

        [MultiLanguageComment("处理子系统循环以进行移除操作", "Handle the sub - system loop for removal operation")]
        static void HandleSubSystemLoopForRemoval<T>(ref PlayerLoopSystem loop, PlayerLoopSystem systemToRemove)
        {
            if (loop.subSystemList == null || loop.subSystemList.Length == 0)
            {
                return;
            }

            for (var i = 0; i < loop.subSystemList.Length; ++i)
            {
                RemoveSystem<T>(ref loop.subSystemList[i], systemToRemove);
            }
        }

        [MultiLanguageComment("在玩家循环系统中插入指定系统", "'Insert' a specified system into the player loop system")]
        public static bool InsertSystem<T>(ref PlayerLoopSystem loop, PlayerLoopSystem systemToInsert, int index)
        {
            if (loop.type != typeof(T))
            {
                return HandleSubSystemLoop<T>(ref loop, systemToInsert, index);
            }

            var playerLoopSystemList = new List<PlayerLoopSystem>();
            if (loop.subSystemList != null)
            {
                playerLoopSystemList.AddRange(loop.subSystemList);
            }

            playerLoopSystemList.Insert(index, systemToInsert);
            loop.subSystemList = playerLoopSystemList.ToArray();
            return true;
        }

        [MultiLanguageComment("处理子系统循环以进行插入操作", "Handle the sub - system loop for insertion operation")]
        static bool HandleSubSystemLoop<T>(ref PlayerLoopSystem loop, PlayerLoopSystem systemToInsert, int index)
        {
            if (loop.subSystemList == null || loop.subSystemList.Length == 0)
            {
                return false;
            }

            for (var i = 0; i < loop.subSystemList.Length; ++i)
            {
                if (!InsertSystem<T>(ref loop.subSystemList[i], systemToInsert, index))
                {
                    continue;
                }

                return true;
            }

            return false;
        }

        [MultiLanguageComment("打印玩家循环系统信息", "Print the player loop system information")]
        public static void PrintPlayerLoop(PlayerLoopSystem loop)
        {
            var sb = new StringBuilder();
            sb.AppendLine("Unity Player Loop:");
            foreach (PlayerLoopSystem subSystem in loop.subSystemList)
            {
                PrintSubSystem(subSystem, sb, 0);
            }

            Debug.Log(sb.ToString());
        }

        /// <summary>
        /// 打印玩家循环系统的子系统信息。
        /// </summary>
        /// <param name="system">当前要打印的玩家循环系统实例。</param>
        /// <param name="sb">用于累积输出信息的字符串构建器。</param>
        /// <param name="depth">当前打印的子系统的深度级别，用于格式化输出。</param>
        [MultiLanguageComment("打印玩家循环系统的子系统信息", "Print the subsystem information of the player loop system")]
        static void PrintSubSystem(PlayerLoopSystem system, StringBuilder sb, int depth)
        {
            // 在字符串构建器中添加当前子系统的类型信息，并换行
            sb.Append(' ', depth * 2).AppendLine(system.type.ToString());
            if (system.subSystemList == null || system.subSystemList.Length == 0)
            {
                return;
            }

            // 遍历当前子系统的所有子子系统，并递归调用PrintSubSystem进行打印
            foreach (PlayerLoopSystem subSubSystem in system.subSystemList)
            {
                PrintSubSystem(subSubSystem, sb, depth + 1);
            }
        }

        [MultiLanguageComment("获取新的自定义玩家循环系统", "Get a new custom player loop system")]
        public static PlayerLoopSystem GetNewCustomPlayerLoopSystem(
            Type target, PlayerLoopSystem.UpdateFunction update = null, IntPtr loopCondition = default,
            PlayerLoopSystem[] subSystems = null) =>
            new PlayerLoopSystem
            {
                type = target,
                updateDelegate = update,
                loopConditionFunction = loopCondition,
                subSystemList = subSystems
            };
    }
}
