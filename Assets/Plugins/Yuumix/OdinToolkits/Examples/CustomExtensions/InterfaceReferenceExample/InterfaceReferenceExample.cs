using Sirenix.OdinInspector;
using System.Collections.Generic;
using UnityEngine;
using Yuumix.OdinToolkits.Modules.CustomExtensions.Attributes;
using Yuumix.OdinToolkits.Modules.CustomExtensions.Classes;

namespace Yuumix.OdinToolkits.Examples.CustomExtensions.InterfaceReferenceExample
{
    public class InterfaceReferenceExample : MonoBehaviour
    {
        [Title("InterfaceReference 自定义类")] public InterfaceReference<IDamageable> scriptableObjectReference;

        public InterfaceReference<IDamageable> componentReference;

        [Title("RequiredInterface Attribute")] [RequiredInterface(typeof(IDamageable))]
        public MonoBehaviour target;

        [RequiredInterface(typeof(IDamageable))]
        public ScriptableObject asset;

        [Title("数组和列表")] public InterfaceReference<IDamageable>[] array;
        public List<InterfaceReference<IDamageable>> list = new List<InterfaceReference<IDamageable>>();

        void Start()
        {
            scriptableObjectReference.InterfaceValue.Damage(10);
            var damage = componentReference.InterfaceValue;
            damage.Damage(20);
        }
    }
}