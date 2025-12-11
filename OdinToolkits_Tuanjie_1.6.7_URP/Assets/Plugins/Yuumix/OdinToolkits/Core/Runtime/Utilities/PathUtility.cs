using System;
using System.IO;
using System.Linq;
using System.Reflection;

namespace Yuumix.OdinToolkits.Core
{
    [Summary("Path 路径字符串工具类，提供路径相关的操作方法。")]
    public class PathUtility
    {
        [Summary("尝试获取完整路径中以目标字符串结尾的子路径，匹配最后一个出现的目标字符串。如果没有找到，返回 false。")]
        public static bool TryGetSubPathWithSpecialEnd(string fullRelativePath, string endWithString,
            out string subPath)
        {
            if (string.IsNullOrWhiteSpace(fullRelativePath) || string.IsNullOrWhiteSpace(endWithString))
            {
                YuumixLogger.LogError("路径或目标字符串不能为空！");
                subPath = string.Empty;
                return false;
            }

            if (!fullRelativePath.StartsWith("Assets"))
            {
                YuumixLogger.LogError("完整路径不是以 Assets 开头的，需要使用相对路径。");
                subPath = string.Empty;
                return false;
            }

            var splits = fullRelativePath.Split('/');
            var finalIndex = -1;
            for (var i = 0; i < splits.Length; i++)
            {
                if (splits[i] == endWithString)
                {
                    finalIndex = i;
                }
            }

            if (finalIndex == -1)
            {
                YuumixLogger.LogError("完整路径中没有找到能够以 " + endWithString + " 为结尾的子路径。");
                subPath = string.Empty;
                return false;
            }

            subPath = string.Join("/", splits.Take(finalIndex + 1));
            return true;
        }

        public static string FindIsSubClassOfInFolder(Type abstractType, string folderPath)
        {
            var assembly = Assembly.GetExecutingAssembly();
            var subclasses = assembly.GetTypes()
                .Where(t => t.IsSubclassOf(abstractType))
                .ToList();
            foreach (var subClass in subclasses)
            {
                var scriptPath = Path.Combine(folderPath, subClass.Name + ".cs");
                if (!File.Exists(scriptPath))
                {
                    continue;
                }

                return Path.Combine(folderPath, subClass.Name + ".cs");
            }

            return null;
        }

        public static string FindIsGenericSubClassOfInFolderReturnPath(Type abstractType, string folderPath)
        {
            var subTypes = Assembly.GetExecutingAssembly()
                .GetTypes()
                .Where(t => t.IsClass && !t.IsAbstract && t.BaseType is { IsGenericType: true } &&
                            t.BaseType.GetGenericTypeDefinition() == abstractType && t.BaseType
                                .GetGenericArguments()[0] == t)
                .ToList();

            foreach (var subType in subTypes)
            {
                // Debug.Log(subType.FullName);
                var scriptPath = Path.Combine(folderPath, subType.Name + ".cs");
                // Debug.Log(scriptPath);
                if (!File.Exists(scriptPath))
                {
                    continue;
                }

                return Path.Combine(folderPath, subType.Name + ".cs");
            }

            return null;
        }

        public static string FindIsGenericSubClassOfInFolderReturnName(Type abstractType, string folderPath)
        {
            var subTypes = Assembly.GetExecutingAssembly()
                .GetTypes()
                .Where(t => t.IsClass && !t.IsAbstract && t.BaseType is { IsGenericType: true } &&
                            t.BaseType.GetGenericTypeDefinition() == abstractType && t.BaseType
                                .GetGenericArguments()[0] == t)
                .ToList();
            foreach (var subType in subTypes)
            {
                var scriptPath = Path.Combine(folderPath, subType.Name + ".cs");
                if (!File.Exists(scriptPath))
                {
                    continue;
                }

                return subType.Name;
            }

            return null;
        }

        public static string[] GetIsGenericSubClassOfInProjectReturnFullName(Type absType)
        {
            return AppDomain.CurrentDomain
                .GetAssemblies()
                .Where(assembly => assembly.FullName.Contains("Assembly-CSharp"))
                .SelectMany(assembly => assembly.GetTypes())
                .Where(t => t.IsClass && !t.IsAbstract && t.BaseType is { IsGenericType: true } &&
                            t.BaseType.GetGenericTypeDefinition() == absType && t.BaseType
                                .GetGenericArguments()[0] == t)
                .Select(type => type.FullName)
                .ToArray();
        }

        public static string CombinePath(string a, string b)
        {
            a = a.Replace("\\", "/")
                .TrimEnd('/');
            b = b.Replace("\\", "/")
                .TrimStart('/');
            return a + "/" + b;
        }
    }
}
