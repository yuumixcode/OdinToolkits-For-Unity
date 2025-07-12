using System.Collections.Generic;
using System.Linq;
using Yuumix.OdinToolkits.Shared;

namespace Yuumix.OdinToolkits.LowLevel
{
    public static class HierarchyUtil
    {
        public static string GetRelativePath(string parentPath, string childPath)
        {
            if (parentPath == string.Empty)
            {
                YuumixLogger.LogError("父物体路径为空");
                return "ParentPath == null";
            }

            var parentPathArray = parentPath.Split('/');
            var childPathArray = childPath.Split('/');
            var targetPathList = new List<string>();
            if (parentPathArray.Where((path, i) => childPathArray[i] != path).Any())
            {
                YuumixLogger.LogError("路径错误，并不是子物体");
                return null;
            }

            for (var i = parentPathArray.Length; i < childPathArray.Length; i++)
            {
                targetPathList.Add(childPathArray[i]);
            }

            return string.Join("/", targetPathList);
        }
    }
}
