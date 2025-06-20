using Sirenix.OdinInspector;
using System;

namespace Yuumix.OdinToolkits.Modules.Tools.ScriptDocGen.Test
{
    public class TestTypeCategory : SerializedMonoBehaviour
    {
        [InfoBox("选择不同类型种类进行验证")]
        public Type Type;
    }
}
