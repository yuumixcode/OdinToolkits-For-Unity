#if UNITY_EDITOR
using UnityEngine;

namespace Yuumix.YuumixEditor
{
    public static class YuumixEditorEasyLog
    {
        public static void Log(string message)
        {
            Debug.Log("[YuumixEditor] " + message);
        }

        public static void Warning(string message)
        {
            Debug.LogWarning("[YuumixEditor] " + message);
        }

        public static void Error(string message)
        {
            Debug.LogError("[YuumixEditor] " + message);
        }
    }
}
#endif
