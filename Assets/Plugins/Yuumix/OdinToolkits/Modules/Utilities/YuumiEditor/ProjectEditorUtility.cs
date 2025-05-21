#if UNITY_EDITOR
using UnityEditor;
#endif
using System;
using System.IO;
using System.Linq;
using System.Reflection;
using UnityEngine;
using Yuumix.OdinToolkits.Common.Runtime;
using Object = UnityEngine.Object;

namespace Yuumix.OdinToolkits.Modules.Utilities.YuumiEditor
{
    /// <summary>
    /// ProjectEditorUtility 仅限编辑器使用
    /// </summary>
    public static class ProjectEditorUtility
    {
        public static class SO
        {
#if UNITY_EDITOR
            /// <summary>
            /// 是否存在该类型的 SO 资源
            /// </summary>
            /// <param name="type"></param>
            /// <returns></returns>
            public static bool IsExistScriptableObjectAsset(Type type)
            {
                var assetPaths = AssetDatabase.FindAssets("t:" + type);
                return assetPaths.Length > 0;
            }

            /// <summary>
            /// 根据类型获取单个资源的路径，更加通用，非脚本类型
            /// </summary>
            /// <typeparam name="T"> 资源类型 </typeparam>
            /// <returns> 字符串路径 </returns>
            public static string GetScriptableObjectAssetPath<T>() where T : ScriptableObject
            {
                var assetPath = AssetDatabase.FindAssets("t:" + typeof(T))
                    .Select(AssetDatabase.GUIDToAssetPath)
                    .FirstOrDefault();
                if (!string.IsNullOrEmpty(assetPath))
                {
                    return assetPath;
                }

                OdinEditorLog.Error("没有找到对应类型的 ScriptableObject 文件，需要手动生成对应的文件");
                return null;
            }

            /// <summary>
            /// 获取或创建一个单例 SO 资源，如果资源不存在则创建，如果有多个 SO 资源，则只返回第一个，并删除其他资源
            /// </summary>
            public static T GetScriptableObjectDeleteExtra<T>(string filePath = "") where T : ScriptableObject
            {
                T wantToAsset;
                var guids = AssetDatabase.FindAssets("t:" + typeof(T));
                var allPaths = new string[guids.Length];
                if (guids.Length > 0)
                {
                    allPaths[0] = AssetDatabase.GUIDToAssetPath(guids[0]);

                    // 只获取一个资源 0 号
                    wantToAsset = AssetDatabase.LoadAssetAtPath<T>(allPaths[0]);
                    if (wantToAsset == null)
                    {
                        OdinEditorLog.Warning("GetScriptableObjectDeleteExtra 中加载资源失败");
                    }

                    // 删除从序号 1 开始的所有资源
                    for (var i = 1; i < guids.Length; i++)
                    {
                        // 能获得扩展名
                        allPaths[i] = AssetDatabase.GUIDToAssetPath(guids[i]);
                        AssetDatabase.DeleteAsset(allPaths[i]);
                    }

                    AssetDatabase.Refresh();
                    return wantToAsset;
                }

                if (string.IsNullOrEmpty(filePath))
                {
                    OdinEditorLog.Warning("没有找到对应的 ScriptableObject 资源，且没有设置路径，不会立即生成新资源");
                    return null;
                }

                wantToAsset = ScriptableObject.CreateInstance<T>();
                if (!filePath.EndsWith(".asset"))
                {
                    filePath = Path.Combine(filePath, ".asset");
                }

                AssetDatabase.CreateAsset(wantToAsset, filePath);
                AssetDatabase.ImportAsset(filePath);
                AssetDatabase.SaveAssets();
                AssetDatabase.Refresh();
                return wantToAsset;
            }
#endif
        }

        public static class Script
        {
#if UNITY_EDITOR
            /// <summary>
            /// 查找脚本，并选择这个脚本文件
            /// 注意：查找的是 MonoScript，而不是 ScriptableObject，加载的也是 MonoScript
            /// </summary>
            public static MonoScript FindAndSelectedScript(string scriptName)
            {
                MonoScript foundMonoScript = null;
                var scriptAssetPath = AssetDatabase.FindAssets("t:MonoScript " + scriptName)
                    .Select(AssetDatabase.GUIDToAssetPath)
                    .FirstOrDefault();

                if (!string.IsNullOrEmpty(scriptAssetPath))
                {
                    foundMonoScript = AssetDatabase.LoadAssetAtPath<MonoScript>(scriptAssetPath);
                }

                if (foundMonoScript)
                {
                    Selection.activeObject = foundMonoScript;
                }

                return foundMonoScript;
            }

            /// <summary>
            /// 通过脚本名字找到脚本路径，同名脚本可能会找错
            /// </summary>
            /// <param name="scriptName"> </param>
            /// <returns> </returns>
            public static string FindScriptPath(string scriptName)
            {
                var scriptAssetPath = AssetDatabase.FindAssets("t:MonoScript " + scriptName)
                    .Select(AssetDatabase.GUIDToAssetPath)
                    .FirstOrDefault();
                return !string.IsNullOrEmpty(scriptAssetPath) ? scriptAssetPath : null;
            }
#endif
        }

        public static class Paths
        {
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
            /// <param name="target">目标结尾字符串（如："OdinToolkits"）</param>
            /// <returns>如：Assets/.../OdinToolkits</returns>
            public static string GetSubPathByEndsWith(string fullPath, string target)
            {
                if (string.IsNullOrEmpty(fullPath) || string.IsNullOrEmpty(target))
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
                    if (parts[i] == target)
                    {
                        lastIndex = i;
                    }
                }

                // 未找到匹配项
                if (lastIndex != -1)
                {
                    return string.Join("/", parts, 0, lastIndex + 1);
                }

                OdinEditorLog.Warning("路径中未找到以 " + target + " 结尾的部分: " + fullPath);
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
        }
#if UNITY_EDITOR
        /// <summary>
        /// Ping 项目中的任何资源，可以是文件夹路径，需要相对路径
        /// </summary>
        /// <param name="path">相对路径</param>
        public static void PingAndSelectAsset(string path)
        {
            // Debug.Assert(path.StartsWith("Assets"), "PingFolder 中传入的相对路径必须以 Assets 开头");
            if (!path.StartsWith("Assets"))
            {
                OdinEditorLog.Error("PingFolder 中传入的相对路径必须以 Assets 开头");
            }

            var folder = AssetDatabase.LoadAssetAtPath<Object>(path);
            Selection.activeObject = folder;
            EditorGUIUtility.PingObject(folder);
        }

#endif
    }
}
