using UnityEngine;
using Yuumix.OdinToolkits.Core;

namespace Dev.ReloadProof
{
    /// <summary>
    /// 域重新加载安全的 ScriptableObject 单例，每次打开编辑器重置为初始状态。
    /// </summary>
    [Summary("域重新加载安全的 ScriptableObject 单例，每次打开编辑器重置为初始状态")]
    public abstract class ReloadProofSingletonSO<T> : ScriptableObject where T : ScriptableObject
    {
        static T _instance;

        public static T Instance
        {
            get
            {
                if (_instance)
                {
                    return _instance;
                }

                var assets = Resources.FindObjectsOfTypeAll<T>();
                var targetAsset = assets.Length == 0 ? CreateInstance<T>() : assets[0];
                _instance = targetAsset;
                _instance.hideFlags = HideFlags.DontUnloadUnusedAsset;
                if (assets.Length > 1)
                {
                    Resources.UnloadUnusedAssets();
                }

                return _instance;
            }
        }
    }
}
