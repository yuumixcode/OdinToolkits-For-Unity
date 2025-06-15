using Sirenix.OdinInspector;
using System;
using System.Collections.Generic;
using System.Linq;
#if UNITY_EDITOR
using UnityEditor;
#endif

namespace Yuumix.OdinToolkits.Common.Runtime.ResetTool
{
    public interface IPluginReset
    {
        /// <summary>
        /// 插件初始化方法，在插件导出包时，或者需要初始化时，调用此方法
        /// </summary>
        void PluginReset();
    }

    public class PluginResetToolSO : OdinScriptableSingleton<PluginResetToolSO>
    {
        public List<Type> Types;

#if UNITY_EDITOR
        [Button(ButtonSizes.Large)]
        public void GetImplementIPluginResetTypes()
        {
            Types = TypeCache.GetTypesDerivedFrom<IPluginReset>().ToList();
        }
#endif
    }
}
