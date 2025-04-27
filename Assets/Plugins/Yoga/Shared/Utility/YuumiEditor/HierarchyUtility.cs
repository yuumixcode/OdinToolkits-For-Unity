using System.Collections.Generic;
using System.Linq;
using UnityEngine;
#if UNITY_EDITOR
using UnityEditor.Search;
#endif

namespace Yoga.Shared.Utility.YuumiEditor
{
    /// <summary>
    /// 关于场景层级的工具类，不包括预制体的 Stage 场景
    /// </summary>
    public static class HierarchyUtility
    {
#if UNITY_EDITOR
        public static string GetAbsolutePath(Transform trans)
        {
            var path = SearchUtils.GetHierarchyPath(trans.gameObject, false)
                .TrimStart('/');
            // Debug.Log("GetHierarchyPath: " +  path);
            return path;
        }

        public static string GetAbsolutePath(GameObject obj) => GetAbsolutePath(obj.transform);
#endif
        public static string GetRelativePath(string parentPath, string childPath)
        {
            if (parentPath == string.Empty)
            {
                return "父物体路径为空，可能是处于打包成品阶段";
            }

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

        public static bool IsRootTransform(Transform trans) => trans.parent == null;
    }
}
