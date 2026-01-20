#if UNITY_EDITOR
using UnityEditor;
#endif
using System.Diagnostics;
using Object = UnityEngine.Object;

namespace Yuumix.OdinToolkits.Core.SafeEditor
{
    [Summary("关于 Project 操作的工具类。仅在编辑器阶段可用。")]
    public static class ProjectSafeEditorUtility
    {
#if UNITY_EDITOR
        [Summary("Ping 项目中的任何资源，可以是文件夹路径。传入相对路径。仅在编辑器阶段可用，打包后自动剔除。")]
        [Conditional("UNITY_EDITOR")]
        public static void PingAndSelectAsset(string relativePath)
        {
            if (!relativePath.StartsWith("Assets"))
            {
                YuumixLogger.LogError("相对路径必须以 Assets 开头");
                return;
            }

            var asset = AssetDatabase.LoadAssetAtPath<Object>(relativePath);
            Selection.activeObject = asset;
            EditorGUIUtility.PingObject(asset);
        }
#endif
    }
}
