using Sirenix.OdinInspector;
using Sirenix.OdinInspector.Editor;
using System;
using System.Collections.Generic;
using Yuumix.OdinToolkits.Common.Runtime.ResetTool;

namespace Yuumix.OdinToolkits.Modules.Utilities.Test
{
    public class TestTypeUtilDataSO :
#if UNITY_EDITOR
        SerializedScriptableObject
#else
        MonoBehaviour
#endif
    ,IPluginReset
    {
        public List<Type> TypeListForTest;
        public void PluginReset(){
            TypeListForTest = new List<Type>()
            {
                typeof(TestTypeExtensions),
                typeof(OdinEditor),
                typeof(ForTestTypeUtilGenericNestedClass<,>)
            };
        }
    }
}
