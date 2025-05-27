using System;
using System.IO;
using System.Linq;
using System.Reflection;
using UnityEditor;
using Yuumix.OdinToolkits.Common.Runtime;

namespace Yuumix.OdinToolkits.Modules.Utilities.YuumiEditor
{
   public static partial class ProjectEditorUtility
    {
        public static class Paths
        {
            /// <summary>
            /// 查找提供的文件夹中是否存在继承了抽象类的子类，非泛型
            /// </summary>
            /// <param name="abstractType"> 抽象基类 </param>
            /// <param name="folderPath"> 返回带后缀名的脚本路径 </param>
            public static string FindIsSubClassOfInFolder(Type abstractType, string folderPath)
            {
                var assembly = Assembly.GetExecutingAssembly();
                var subclasses = assembly.GetTypes().Where(t => t.IsSubclassOf(abstractType)).ToList();
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

            /// <summary>
            /// 查找提供的文件夹中是否存在继承了抽象类的子类，是抽象泛型类，泛型只有一个 T
            /// </summary>
            /// <param name="abstractType"> 泛型抽象基类，一个泛型参数 </param>
            /// <param name="folderPath"> 文件夹路径 </param>
            /// <returns> 返回带后缀名的脚本路径 </returns>
            public static string FindIsGenericSubClassOfInFolderReturnPath(Type abstractType, string folderPath)
            {
                var subTypes = Assembly.GetExecutingAssembly()
                    .GetTypes()
                    .Where(t => t.IsClass && !t.IsAbstract && t.BaseType is { IsGenericType: true }
                                && t.BaseType.GetGenericTypeDefinition() == abstractType &&
                                t.BaseType.GetGenericArguments()[0] == t)
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

            /// <summary>
            /// 查找提供的文件夹中是否存在继承了抽象类的子类，是抽象泛型类，泛型只有一个 T
            /// </summary>
            /// <param name="abstractType"> </param>
            /// <param name="folderPath"> </param>
            /// <returns> 返回类名 </returns>
            public static string FindIsGenericSubClassOfInFolderReturnName(Type abstractType, string folderPath)
            {
                var subTypes = Assembly.GetExecutingAssembly()
                    .GetTypes()
                    .Where(t => t.IsClass && !t.IsAbstract && t.BaseType is { IsGenericType: true }
                                && t.BaseType.GetGenericTypeDefinition() == abstractType &&
                                t.BaseType.GetGenericArguments()[0] == t)
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

            /// <summary>
            /// 返回当前项目中继承了该类的子类，是抽象泛型类，泛型只有一个 T
            /// </summary>
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

            /// <summary>
            /// 获取路径中以目标字符串结尾的子路径，且层级最深的路径
            /// </summary>
            /// <param name="fullPath">相对于 Assets 的完整路径（如：Assets/Plugins/.../OdinToolkitsMarker.asset）</param>
            /// <param name="endWithString">目标结尾字符串（如："OdinToolkits"）</param>
            /// <returns>如：Assets/.../OdinToolkits</returns>
            public static string GetSubPathByEndsWith(string fullPath, string endWithString)
            {
                if (string.IsNullOrEmpty(fullPath) || string.IsNullOrEmpty(endWithString))
                {
                    OdinEditorLog.Error("路径或目标字符串不能为空！");
                    return null;
                }

                if (!fullPath.StartsWith("Assets"))
                {
                    OdinEditorLog.Error("完整路径不是以 Assets 开头的，需要使用相对路径。");
                    return null;
                }

                // 分割路径
                var parts = fullPath.Split('/');
                var lastIndex = -1;

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

                OdinEditorLog.Warning("路径中未找到以 " + endWithString + " 结尾的部分: " + fullPath);
                return null;
            }

            public static Type GetScriptFullType(string nameSpace, string typeName)
            {
                try
                {
                    return Type.GetType(nameSpace + "." + typeName);
                }
                catch (Exception)
                {
                    return null;
                }
            }

            /// <summary>
            /// Combines two paths 替换所有反斜杠为正斜杠，Window 系统资源管理器是 '\', MacOS 系统是 '/'
            /// </summary>
            public static string CombinePath(string a, string b)
            {
                a = a.Replace("\\", "/").TrimEnd('/');
                b = b.Replace("\\", "/").TrimStart('/');
                return a + "/" + b;
            }
#if UNITY_EDITOR

            /// <summary>
            /// 递归创建 Assets 下的文件夹路径
            /// </summary>
            /// <param name="relativePath">以 Assets 开头的相对路径</param>
            public static void EnsureFolderRecursively(string relativePath)
            {
                // 移除 Assets 前缀，获取实际的文件夹路径
                var pathWithoutAssets = relativePath.Replace("Assets/", "");
                var folders = pathWithoutAssets.Split('/');
                var currentPath = "Assets";
                foreach (var folder in folders)
                {
                    currentPath = CombinePath(currentPath, folder);
                    if (!AssetDatabase.IsValidFolder(currentPath))
                    {
                        var parentPath = Path.GetDirectoryName(currentPath);
                        var folderName = Path.GetFileName(currentPath);
                        AssetDatabase.CreateFolder(parentPath, folderName);
                    }
                }

                // 刷新 AssetDatabase 使创建的文件夹在 Unity 编辑器中可见
                AssetDatabase.Refresh();
            }

            /// <summary>
            /// 获取目标文件夹路径
            /// </summary>
            public static string GetTargetFolderPath(string targetFolderName, params string[] containFolderName)
            {
                var paths = AssetDatabase.GetAllAssetPaths();
                foreach (var path in paths)
                {
                    // 检查路径是否不以目标文件夹名称结束，或者路径不包含所有指定的文件夹名称
                    if (!path.EndsWith(targetFolderName) || !containFolderName.All(path.Contains))
                    {
                        continue;
                    }

                    return path;
                }

                OdinEditorLog.Error("在项目中没有找到 " + targetFolderName + " 文件夹，请检查是否改名，或者条件不满足");
                return null;
            }
#endif
        }
    }
}
