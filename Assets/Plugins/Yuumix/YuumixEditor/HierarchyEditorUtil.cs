#if UNITY_EDITOR
using UnityEditor.Search;
using UnityEngine;

namespace Yuumix.YuumixEditor
{
    /// <summary>
    /// 关于场景层级的工具类，不包括预制体的 Stage 场景
    /// </summary>
    public static class HierarchyEditorUtil
    {
        public static string GetAbsolutePath(GameObject obj) => GetAbsolutePath(obj.transform);

        public static string GetAbsolutePath(Transform trans)
        {
            var path = SearchUtils.GetHierarchyPath(trans.gameObject, false)
                .TrimStart('/');
            // Debug.Log("GetHierarchyPath: " +  path);
            return path;
        }
    }
}
#endif
