using System.Diagnostics;
using System.Linq;
#if UNITY_EDITOR
using UnityEditor;
#endif

namespace Yuumix.OdinToolkits.Core.SafeEditor
{
    [Summary("关于 MonoScript 的工具类。仅在编辑器阶段可用。")]
    public static class MonoScriptSafeEditorUtility
    {
        [Conditional("UNITY_EDITOR")]
        [Summary("在项目中根据脚本文件名称查找脚本文件，并在编辑器中选择。仅在编辑器阶段可用。")]
        public static void SelectMonoScript(string scriptName)
        {
            Selection.activeObject = GetMonoScript(scriptName);
        }

        [Summary("在项目中根据脚本文件名称查找脚本文件，返回找到的 MonoScript。仅在编辑器阶段可用，打包后直接返回 null")]
        public static MonoScript GetMonoScript(string scriptName)
        {
#if UNITY_EDITOR
            MonoScript foundMonoScript = null;
            var scriptAssetPath = FindScriptPath(scriptName);
            if (!string.IsNullOrWhiteSpace(scriptAssetPath))
            {
                foundMonoScript = AssetDatabase.LoadAssetAtPath<MonoScript>(scriptAssetPath);
            }

            return foundMonoScript;
#else
            return null;
#endif
        }

        [Summary("在项目中根据脚本文件名称查找脚本文件，返回脚本文件路径。仅在编辑器阶段可用，打包后直接返回 string.Empty")]
        public static string FindScriptPath(string scriptName)
        {
#if UNITY_EDITOR
            var scriptAssetPath = AssetDatabase.FindAssets("t:MonoScript " + scriptName)
                .Select(AssetDatabase.GUIDToAssetPath)
                .FirstOrDefault();
            return !string.IsNullOrWhiteSpace(scriptAssetPath) ? scriptAssetPath : null;
#else
            return string.Empty;
#endif
        }
    }
}
