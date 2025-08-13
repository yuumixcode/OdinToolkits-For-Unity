using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Yuumix.Universal
{
    [BilingualComment("层级结构工具类", "Hierarchy utility class")]
    public static class HierarchyUtility
    {
        [BilingualComment("获取相对路径", "Get the relative path")]
        public static string GetRelativePath(string parentPath, string childPath)
        {
            if (parentPath == string.Empty)
            {
                Debug.LogError("父物体路径为空");
                return "ParentPath == null";
            }

            string[] parentPathArray = parentPath.Split('/');
            string[] childPathArray = childPath.Split('/');
            var targetPathList = new List<string>();
            if (parentPathArray.Where((path, i) => childPathArray[i] != path).Any())
            {
                Debug.LogError("路径错误，并不是子物体");
                return null;
            }

            for (int i = parentPathArray.Length; i < childPathArray.Length; i++)
            {
                targetPathList.Add(childPathArray[i]);
            }

            return string.Join("/", targetPathList);
        }
    }
}
