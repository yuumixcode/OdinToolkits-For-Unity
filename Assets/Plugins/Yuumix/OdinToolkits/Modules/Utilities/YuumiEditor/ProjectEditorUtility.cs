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
    public static partial class ProjectEditorUtility
    {
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

            var asset = AssetDatabase.LoadAssetAtPath<Object>(path);
            Selection.activeObject = asset;
            EditorGUIUtility.PingObject(asset);
        }

#endif
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


    }
}
