#if UNITY_EDITOR
using UnityEditor;
using Yuumix.OdinToolkits.Common.Runtime;
using Object = UnityEngine.Object;

namespace Yuumix.OdinToolkits.YuumiEditor
{
    public static class ProjectEditorUtil
    {
        /// <summary>
        /// Ping 项目中的任何资源，可以是文件夹路径，需要相对路径
        /// </summary>
        /// <param name="path">相对路径</param>
        public static void PingAndSelectAsset(string path)
        {
            // Debug.Assert(path.StartsWith("Assets"), "PingFolder 中传入的相对路径必须以 Assets 开头");
            if (!path.StartsWith("Assets"))
            {
                OdinEditorLog.Error("相对路径必须以 Assets 开头");
                return;
            }

            var asset = AssetDatabase.LoadAssetAtPath<Object>(path);
            Selection.activeObject = asset;
            EditorGUIUtility.PingObject(asset);
        }

    }
}
#endif
