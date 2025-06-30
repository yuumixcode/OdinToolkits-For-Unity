using Sirenix.OdinInspector;
using System;
using System.Collections.Generic;
using System.Linq;
using Yuumix.OdinToolkits.Common;
#if UNITY_EDITOR
using UnityEditor;
#endif

namespace Yuumix.OdinToolkits.Common.ResetTool
{
    public interface IOdinToolkitsReset
    {
        /// <summary>
        /// 插件初始化方法，在插件导出包时，或者需要初始化时，调用此方法
        /// </summary>
        void OdinToolkitsReset();
    }

    public class ResetOdinToolkitsSO : OdinScriptableSingleton<ResetOdinToolkitsSO>
    {
        public List<Type> Types;

#if UNITY_EDITOR
        [Button(ButtonSizes.Large)]
        public void GetImplementIPluginResetTypes()
        {
            Types = TypeCache.GetTypesDerivedFrom<IOdinToolkitsReset>().ToList();
        }
#endif
    }
}
