#if UNITY_EDITOR
using System.Linq;
using UnityEditor;
using Yuumix.Universal;

namespace YuumixEditor
{
    public static class MonoScriptEditorUtility
    {
        [BilingualComment("选择指定名称的 MonoScript 脚本", "Select a MonoScript with the specified name")]
        public static void SelectMonoScript(string scriptName)
        {
            Selection.activeObject = GetMonoScript(scriptName);
        }

        /// <summary>
        /// 查找脚本，并选择这个脚本文件
        /// 注意：查找的是 MonoScript，而不是 ScriptableObject，加载的也是 MonoScript
        /// </summary>
        [BilingualComment("查找脚本，并选择这个脚本文件，查找的是 MonoScript，而不是 ScriptableObject，加载的也是 MonoScript",
            "Find a script and select the script file. Note: It searches for MonoScript, not ScriptableObject, and also loads MonoScript")]
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
        /// <param name="scriptName"> </param>
        /// <returns> </returns>
        [BilingualComment("通过脚本名字找到脚本路径，同名脚本可能会找错",
            "Find the script path by the script name. There may be errors if there are scripts with the same name")]
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
