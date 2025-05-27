#if UNITY_EDITOR
using UnityEngine;
using UnityEditor.Search;

namespace Yuumix.OdinToolkits.Modules.Utilities.YuumiEditor
{
    /// <summary>
    /// 关于场景层级的工具类，不包括预制体的 Stage 场景
    /// </summary>
    public static class HierarchyEditorUtility
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
