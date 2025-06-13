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
