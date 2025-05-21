using System.Diagnostics;
using Debug = UnityEngine.Debug;

namespace Yuumix.OdinToolkits.Common.Runtime
{
    public static class OdinEditorLog
    {
        [Conditional("UNITY_EDITOR")]
        public static void Log(string message)
        {
            const string prefix = "<color=#00FF00>[Odin Toolkits 提示]</color>";
            var str = prefix + " >>> " + message;
            Debug.Log(str);
        }

        [Conditional("UNITY_EDITOR")]
        public static void Warning(string message)
        {
            const string prefix = "<color=#FFFF00>[Odin Toolkits 警告]</color>";
            var str = prefix + " >>> " + message;
            Debug.LogWarning(str);
        }

        [Conditional("UNITY_EDITOR")]
        public static void Error(string message)
        {
            const string prefix = "<color=#FF0000>[Odin Toolkits 错误]</color>";
            var str = prefix + " >>> " + message;
            Debug.LogError(str);
        }
    }
}