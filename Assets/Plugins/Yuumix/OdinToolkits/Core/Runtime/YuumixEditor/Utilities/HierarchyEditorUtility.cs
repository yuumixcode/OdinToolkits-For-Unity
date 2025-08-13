#if UNITY_EDITOR
using UnityEditor.Search;
using UnityEngine;
using Yuumix.Universal;

namespace YuumixEditor
{
    /// <summary>
    /// 关于场景层级的工具类，不包括预制体的 Stage 场景
    /// </summary>
    public static class HierarchyEditorUtility
    {
        [BilingualComment("获取 GameObject 的绝对路径", "Get the absolute path of a GameObject.")]
        public static string GetAbsolutePath(GameObject obj) => GetAbsolutePath(obj.transform);

        /// <summary>
        /// 获取 Transform 的绝对路径
        /// </summary>
        [BilingualComment("获取 Transform 的绝对路径", "Get the absolute path of a Transform.")]
        public static string GetAbsolutePath(Transform trans)
        {
            string path = SearchUtils.GetHierarchyPath(trans.gameObject, false)
                .TrimStart('/');
            // Debug.Log("GetHierarchyPath: " +  path);
            return path;
        }
    }
}
#endif
