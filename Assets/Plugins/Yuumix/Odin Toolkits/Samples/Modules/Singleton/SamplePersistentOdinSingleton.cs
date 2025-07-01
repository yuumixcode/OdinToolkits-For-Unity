using Sirenix.OdinInspector;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Yuumix.OdinToolkits.Common.InspectorMultiLanguage;
using Yuumix.OdinToolkits.Modules.Singleton;

namespace Yuumix.OdinToolkits.Samples.TestSingleton
{
    public class SamplePersistentOdinSingleton : TestPersistentSingleton<SamplePersistentOdinSingleton>
    {
        [MultiLanguageTitle("结论", "Conclusion")]
        [ShowInInspector]
        [DisplayAsString(overflow: false)]
        [EnableGUI]
        [HideLabel]
        public string Conclusion =>
            "此 Sample 主要测试 PersistentSingleton 的情况，" +
            "在场景加载前获取属性值，在执行 Instance 的 get 方法中，" +
            "创建新的物体并挂载脚本时，立刻触发 Awake，然后再进行 get 方法的下一步。" +
            "测试过程可以直接运行此场景";

        [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.BeforeSceneLoad)]
        public static void StartSingleton()
        {
            _ = Instance;
        }
    }
}
