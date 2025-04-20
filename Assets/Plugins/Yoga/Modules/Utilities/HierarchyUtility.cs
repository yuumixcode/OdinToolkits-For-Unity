using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace YOGA.Modules.Utilities
{
    /// <summary>
    /// 关于场景层级的工具类，不包括预制体的 Stage 场景
    /// </summary>
    public static class HierarchyUtility
    {
        public static string GetAbsolutePath(Transform trans)
        {
#if UNITY_EDITOR
            var path = UnityEditor.Search.SearchUtils.GetHierarchyPath(trans.gameObject, false)
                .TrimStart('/');
            // Debug.Log("GetHierarchyPath: " +  path);
            return path;
#else
            Debug.Log("HierarchyUtilities.GetAbsolutePath() 方法属于编辑器状态使用");
#endif
        }

#if UNITY_EDITOR

        public static string GetAbsolutePath(GameObject obj)
        {
            return GetAbsolutePath(obj.transform);
        }
#endif
        public static string GetRelativePath(string parentPath, string childPath)
        {
            var parentPathArray = parentPath.Split('/');
            var childPathArray = childPath.Split('/');
            var targetPathList = new List<string>();
            if (parentPathArray.Where((path, i) => childPathArray[i] != path).Any())
            {
                Debug.LogError("路径错误，并不是子物体");
                return null;
            }

            for (var i = parentPathArray.Length; i < childPathArray.Length; i++)
            {
                targetPathList.Add(childPathArray[i]);
            }

            return string.Join("/", targetPathList);
        }

        public static bool IsRootTransform(Transform trans)
        {
            return trans.parent == null;
        }
    }
}
