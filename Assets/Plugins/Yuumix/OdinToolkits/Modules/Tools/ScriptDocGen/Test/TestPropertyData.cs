using Sirenix.OdinInspector;
using UnityEngine;
using Yuumix.OdinToolkits.Common.Runtime;

namespace Yuumix.OdinToolkits.Modules.Tools.ScriptDocGen.Test
{
    [Searchable]
    internal class Property
    {
        /// <summary>
        /// 可以存在 get 为 private, set 为 public
        /// </summary>
        [ShowInInspector] public int PublicProperty { private get; set; }
    }

    public class TestPropertyData : MonoBehaviour
    {
        [ShowInInspector]  Property _property;

        [Button("Set Property",ButtonSizes.Large)]
        public void Set()
        {
            _property.PublicProperty = 1;
        }
    }
}
