using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using Yuumix.OdinToolkits.Core;

namespace Yuumix.OdinToolkits.Shared
{
    [MultiLanguageComment("路径工具类", "Path utility class")]
    public class PathUtil
    {
        [MultiLanguageComment("查找提供的文件夹中是否存在继承了抽象类的子类，非泛型",
            "Find if there is a non - generic subclass of an abstract class in the provided folder")]
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

        [MultiLanguageComment("查找提供的文件夹中是否存在继承了抽象类的子类，是抽象泛型类，泛型只有一个 T",
            "Find if there is a subclass of an abstract generic class (with one generic parameter T) in the provided folder and return the path")]
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

        [MultiLanguageComment("查找提供的文件夹中是否存在继承了抽象类的子类，是抽象泛型类，泛型只有一个 T，返回类名",
            "Find if there is a subclass of an abstract generic class (with one generic parameter T) in the provided folder and return the class name")]
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

        [MultiLanguageComment("返回当前项目中继承了该类的子类，是抽象泛型类，泛型只有一个 T，返回全类名",
            "Return the full names of subclasses of an abstract generic class (with one generic parameter T) in the current project")]
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

        [MultiLanguageComment("获取路径中以目标字符串结尾的子路径，且层级最深的路径",
            "Get the deepest sub - path that ends with the target string in the full path")]
        public static string GetSubPathByEndsWith(string fullPath, string endWithString)
        {
            if (string.IsNullOrEmpty(fullPath) || string.IsNullOrEmpty(endWithString))
            {
                YuumixLogger.OdinToolkitsError("路径或目标字符串不能为空！");
                return null;
            }

            if (!fullPath.StartsWith("Assets"))
            {
                YuumixLogger.OdinToolkitsError("完整路径不是以 Assets 开头的，需要使用相对路径。");
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

            YuumixLogger.OdinToolkitsWarning("路径中未找到以 " + endWithString + " 结尾的部分: " + fullPath);
            return null;
        }

        [MultiLanguageComment("组合两个路径，替换所有反斜杠为正斜杠",
            "Combine two paths and replace all backslashes with forward slashes")]
        public static string CombinePath(string a, string b)
        {
            a = a.Replace("\\", "/").TrimEnd('/');
            b = b.Replace("\\", "/").TrimStart('/');
            return a + "/" + b;
        }
    }
}
