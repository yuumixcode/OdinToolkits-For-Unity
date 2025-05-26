using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Yuumix.OdinToolkits.Common.Runtime;
#if UNITY_EDITOR
using UnityEditor.Search;
#endif

namespace Yuumix.OdinToolkits.Modules.Utilities.YuumiEditor
{
    /// <summary>
    /// 关于场景层级的工具类，不包括预制体的 Stage 场景
    /// </summary>
    public static class HierarchyUtility
    {
        public static string GetRelativePath(string parentPath, string childPath)
        {
            if (parentPath == string.Empty)
            {
                OdinEditorLog.Error("父物体路径为空");
                return "ParentPath == null";
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
#if UNITY_EDITOR
        public static string GetAbsolutePath(GameObject obj) => GetAbsolutePath(obj.transform);

        public static string GetAbsolutePath(Transform trans)
        {
            var path = SearchUtils.GetHierarchyPath(trans.gameObject, false)
                .TrimStart('/');
            // Debug.Log("GetHierarchyPath: " +  path);
            return path;
        }
#endif
    }
}
