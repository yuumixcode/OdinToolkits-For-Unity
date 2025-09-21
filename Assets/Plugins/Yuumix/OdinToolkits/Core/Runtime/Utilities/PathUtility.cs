using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using UnityEngine;

namespace Yuumix.OdinToolkits.Core
{
    public class PathUtility
    {
        public static string FindIsSubClassOfInFolder(Type abstractType, string folderPath)
        {
            var assembly = Assembly.GetExecutingAssembly();
            List<Type> subclasses = assembly.GetTypes().Where(t => t.IsSubclassOf(abstractType)).ToList();
            foreach (Type subClass in subclasses)
            {
                string scriptPath = Path.Combine(folderPath, subClass.Name + ".cs");
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
            List<Type> subTypes = Assembly.GetExecutingAssembly()
                .GetTypes()
                .Where(t => t.IsClass && !t.IsAbstract && t.BaseType is { IsGenericType: true }
                            && t.BaseType.GetGenericTypeDefinition() == abstractType &&
                            t.BaseType.GetGenericArguments()[0] == t)
                .ToList();

            foreach (Type subType in subTypes)
            {
                // Debug.Log(subType.FullName);
                string scriptPath = Path.Combine(folderPath, subType.Name + ".cs");
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
            List<Type> subTypes = Assembly.GetExecutingAssembly()
                .GetTypes()
                .Where(t => t.IsClass && !t.IsAbstract && t.BaseType is { IsGenericType: true }
                            && t.BaseType.GetGenericTypeDefinition() == abstractType &&
                            t.BaseType.GetGenericArguments()[0] == t)
                .ToList();
            foreach (Type subType in subTypes)
            {
                string scriptPath = Path.Combine(folderPath, subType.Name + ".cs");
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
            return AppDomain.CurrentDomain.GetAssemblies()
                .Where(assembly => assembly.FullName.Contains("Assembly-CSharp"))
                .SelectMany(assembly => assembly.GetTypes())
                .Where(t => t.IsClass && !t.IsAbstract && t.BaseType is { IsGenericType: true }
                            && t.BaseType.GetGenericTypeDefinition() == absType &&
                            t.BaseType.GetGenericArguments()[0] == t)
                .Select(type => type.FullName)
                .ToArray();
        }

        public static string GetSubPathByEndsWith(string fullPath, string endWithString)
        {
            if (string.IsNullOrEmpty(fullPath) || string.IsNullOrEmpty(endWithString))
            {
                Debug.LogError("路径或目标字符串不能为空！");
                return null;
            }

            if (!fullPath.StartsWith("Assets"))
            {
                Debug.LogError("完整路径不是以 Assets 开头的，需要使用相对路径。");
                return null;
            }

            // 分割路径
            string[] parts = fullPath.Split('/');
            int lastIndex = -1;

            // 遍历查找最后一个匹配的索引
            for (var i = 0; i < parts.Length; i++)
            {
                if (parts[i] == endWithString)
                {
                    lastIndex = i;
                }
            }

            // 未找到匹配项
            if (lastIndex != -1)
            {
                return string.Join("/", parts, 0, lastIndex + 1);
            }

            Debug.LogError("路径中未找到以 " + endWithString + " 结尾的部分: " + fullPath);
            return null;
        }

        public static string CombinePath(string a, string b)
        {
            a = a.Replace("\\", "/").TrimEnd('/');
            b = b.Replace("\\", "/").TrimStart('/');
            return a + "/" + b;
        }
    }
}
