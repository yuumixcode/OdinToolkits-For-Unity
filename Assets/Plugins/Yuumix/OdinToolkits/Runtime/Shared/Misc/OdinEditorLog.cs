using System.Diagnostics;
using Debug = UnityEngine.Debug;

namespace Yuumix.OdinToolkits.Shared
{
    public static class OdinEditorLog
    {
        [Conditional("UNITY_EDITOR")]
        public static void Log(string message)
        {
            const string prefix = "<color=#00FF00>" + "[Odin Toolkits Info]</color>";
            var str = prefix + " >>> " + message;
            Debug.Log(str);
        }

        [Conditional("UNITY_EDITOR")]
        public static void Warning(string message)
        {
            const string prefix = "<color=#FFFF00>[Odin Toolkits Warning]</color>";
            var str = prefix + " >>> " + message;
            Debug.LogWarning(str);
        }

        [Conditional("UNITY_EDITOR")]
        public static void Error(string message)
        {
            const string prefix = "<color=#FF0000>[Odin Toolkits Error]</color>";
            var str = prefix + " >>> " + message;
            Debug.LogError(str);
        }
    }
}
