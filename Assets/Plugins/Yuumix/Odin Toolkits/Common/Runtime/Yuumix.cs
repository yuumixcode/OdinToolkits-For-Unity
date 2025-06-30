using UnityEngine;
using Yuumix.OdinToolkits.Common.Logger;
using Yuumix.OdinToolkits.Modules.Singleton;

namespace Yuumix.OdinToolkits.Common
{
    public sealed class Yuumix : PersistentOdinSingleton<Yuumix>
    {
        public OdinToolkitsRuntimeConfig OdinToolkitsRuntimeConfig
        {
            get
            {
                if (!_odinToolkitsRuntimeConfig)
                {
                    _odinToolkitsRuntimeConfig = Resources.Load<OdinToolkitsRuntimeConfig>("Runtime_OdinToolkitsRuntimeConfig");
                }

                return _odinToolkitsRuntimeConfig;
            }
        }

        OdinToolkitsRuntimeConfig _odinToolkitsRuntimeConfig;

        protected override void Awake()
        {
            base.Awake();
            // 初始加载一次
            _odinToolkitsRuntimeConfig = Resources.Load<OdinToolkitsRuntimeConfig>("Runtime_OdinToolkitsRuntimeConfig");
            if (!_odinToolkitsRuntimeConfig)
            {
                YuumixLogger.EditorLogError("OdinToolkitsRuntimeConfig 配置资源加载失败，需要检查 Resources 路径！", prefix: "Odin Toolkits");
            }
        }

        [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.BeforeSceneLoad)]
        static void YuumixStart()
        {
            _ = Instance;
        }
    }
}
