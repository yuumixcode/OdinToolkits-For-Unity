#if UNITY_EDITOR
using System.Linq;
using UnityEditor;
using Yuumix.OdinToolkits.Core;

namespace YuumixEditor
{
    public static class MonoScriptEditorUtility
    {
        public static void SelectMonoScript(string scriptName)
        {
            Selection.activeObject = GetMonoScript(scriptName);
        }

        /// <summary>
        /// 查找脚本，并选择这个脚本文件
        /// 注意：查找的是 MonoScript，而不是 ScriptableObject，加载的也是 MonoScript
        /// </summary>
        public static MonoScript GetMonoScript(string scriptName)
        {
            MonoScript foundMonoScript = null;
            string scriptAssetPath = AssetDatabase.FindAssets("t:MonoScript " + scriptName)
                .Select(AssetDatabase.GUIDToAssetPath)
                .FirstOrDefault();

            if (!string.IsNullOrEmpty(scriptAssetPath))
            {
                foundMonoScript = AssetDatabase.LoadAssetAtPath<MonoScript>(scriptAssetPath);
            }

            if (foundMonoScript)
            {
                Selection.activeObject = foundMonoScript;
            }

            return foundMonoScript;
        }

        /// <summary>
        /// 通过脚本名字找到脚本路径，同名脚本可能会找错
        /// </summary>
        public static string FindScriptPath(string scriptName)
        {
            string scriptAssetPath = AssetDatabase.FindAssets("t:MonoScript " + scriptName)
                .Select(AssetDatabase.GUIDToAssetPath)
                .FirstOrDefault();
            return !string.IsNullOrEmpty(scriptAssetPath) ? scriptAssetPath : null;
        }
    }
}
#endif
