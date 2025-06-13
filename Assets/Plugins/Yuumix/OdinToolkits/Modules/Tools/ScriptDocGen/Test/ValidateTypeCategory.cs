using Sirenix.OdinInspector;
using System;

namespace Yuumix.OdinToolkits.Modules.Tools.ScriptDocGen.Test
{
    public class ValidateTypeCategory :
#if UNITY_EDITOR
        SerializedMonoBehaviour
#else
        MonoBehaviour
#endif
    {
        [InfoBox("选择不同类型种类进行验证")]
        public Type Type;
    }
}
