#if UNITY_EDITOR
using System.Linq;
using UnityEditor;
using UnityEngine;
using Yuumix.OdinToolkits.Core;

namespace YuumixEditor
{
    public static class ScriptableObjectEditorUtility
    {
        /// <summary>
        /// 是否存在该类型的 SO 资源
        /// </summary>
        public static bool HasAssetInProject<T>() where T : ScriptableObject
        {
            string[] assetPaths = AssetDatabase.FindAssets("t:" + typeof(T));
            return assetPaths.Length > 0;
        }

        /// <summary>
        /// 根据类型获取单个资源的路径，更加通用，非脚本类型
        /// </summary>
        /// <typeparam name="T"> 资源类型 </typeparam>
        /// <returns> 字符串路径 </returns>
        public static string GetAssetPath<T>() where T : ScriptableObject
        {
            string assetPath = AssetDatabase.FindAssets("t:" + typeof(T))
                .Select(AssetDatabase.GUIDToAssetPath)
                .FirstOrDefault();
            if (!string.IsNullOrEmpty(assetPath))
            {
                return assetPath;
            }

            YuumixLogger.LogError("没有找到对应类型的 ScriptableObject 文件，需要手动生成对应的文件");
            return null;
        }

        /// <summary>
        /// 获取或创建一个单例 SO 资源，如果资源不存在则创建，如果有多个 SO 资源，则只返回第一个，并删除其他资源
        /// </summary>
        public static T GetAssetAndDeleteExtra<T>(string relativeFolderPath = "") where T : ScriptableObject
        {
            T wantToAsset;
            string[] guids = AssetDatabase.FindAssets("t:" + typeof(T));
            var allPaths = new string[guids.Length];
            if (guids.Length > 0)
            {
                allPaths[0] = AssetDatabase.GUIDToAssetPath(guids[0]);
                wantToAsset = AssetDatabase.LoadAssetAtPath<T>(allPaths[0]);
                // 删除从序号 1 开始的所有资源
                for (var i = 1; i < guids.Length; i++)
                {
                    allPaths[i] = AssetDatabase.GUIDToAssetPath(guids[i]);
                    AssetDatabase.DeleteAsset(allPaths[i]);
                }

                AssetDatabase.Refresh();
                return wantToAsset;
            }

            if (string.IsNullOrEmpty(relativeFolderPath))
            {
                #region 硬编码路径

                relativeFolderPath = "Assets/OdinToolkitsData/SO";

                #endregion

                YuumixLogger.LogWarning($"没有找到对应的 ScriptableObject 资源，且没有设置路径。自动生成资源在 {relativeFolderPath}");
            }

            PathEditorUtility.EnsureFolderRecursively(relativeFolderPath);
            string filePath = relativeFolderPath + "/" + typeof(T).Name + "[AutoGen]" + ".asset";
            wantToAsset = ScriptableObject.CreateInstance<T>();
            AssetDatabase.CreateAsset(wantToAsset, filePath);
            AssetDatabase.ImportAsset(filePath);
            AssetDatabase.SaveAssets();
            AssetDatabase.Refresh();
            ProjectEditorUtility.PingAndSelectAsset(filePath);
            return wantToAsset;
        }
    }
}
#endif
