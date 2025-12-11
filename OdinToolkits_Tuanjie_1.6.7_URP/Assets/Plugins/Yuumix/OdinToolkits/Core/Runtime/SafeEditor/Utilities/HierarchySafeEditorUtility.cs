#if UNITY_EDITOR
using UnityEditor.Search;
#endif
using UnityEngine;

namespace Yuumix.OdinToolkits.Core.SafeEditor
{
    [Summary("关于 Hierarchy 的工具类，不包括预制体的 Stage 场景。仅在编辑器阶段可用。")]
    public static class HierarchySafeEditorUtility
    {
        public static string GetAbsolutePath(GameObject obj) => GetAbsolutePath(obj.transform);

        /// <summary>
        /// 获取 Transform 的绝对路径
        /// </summary>
        [Summary("获取 Transform 的绝对路径。仅在编辑器阶段可用，打包后直接返回 string.Empty")]
        public static string GetAbsolutePath(Transform trans)
        {
#if UNITY_EDITOR
            var path = SearchUtils.GetHierarchyPath(trans.gameObject, false)
                .TrimStart('/');
            // Debug.Log("GetHierarchyPath: " +  path);
            return path;
#else
            return string.Empty;
#endif
        }
    }
}
