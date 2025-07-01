#if UNITY_EDITOR
using UnityEditor;
using Object = UnityEngine.Object;

namespace Yuumix.YuumixEditor
{
    public static class ProjectEditorUtil
    {
        /// <summary>
        /// Ping 项目中的任何资源，可以是文件夹路径，需要相对路径
        /// </summary>
        /// <param name="relativePath">相对路径</param>
        public static void PingAndSelectAsset(string relativePath)
        {
            // Debug.Assert(path.StartsWith("Assets"), "PingFolder 中传入的相对路径必须以 Assets 开头");
            if (!relativePath.StartsWith("Assets"))
            {
                YuumixEditorEasyLog.Error("相对路径必须以 Assets 开头");
                return;
            }

            var asset = AssetDatabase.LoadAssetAtPath<Object>(relativePath);
            Selection.activeObject = asset;
            EditorGUIUtility.PingObject(asset);
        }
    }
}
#endif
