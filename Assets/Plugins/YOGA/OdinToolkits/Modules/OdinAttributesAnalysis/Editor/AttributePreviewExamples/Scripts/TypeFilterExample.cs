using System;
using System.Collections.Generic;
using System.Linq;
using Sirenix.OdinInspector;
using Sirenix.Utilities;
using UnityEngine;

namespace YOGA.OdinToolkits.AnalysisManual.OdinAttributes.Editor
{
    [IsChineseAttributeExample]
    public class TypeFilterExample : ExampleOdinScriptableObject
    {
        [PropertyOrder(1)] [FoldoutGroup("FilterGetter 参数")] [TypeFilter("GetFilteredTypeList")]
        public BaseClass A;

        [PropertyOrder(15)]
        [FoldoutGroup("TypeFilter 扩展")]
        [InfoBox("集合可以添加该特性，改变的是集合内部元素的绘制方式")]
        [TypeFilter("GetFilteredTypeList")]
        public BaseClass[] Array = new BaseClass[3];

        [PropertyOrder(5)]
        [FoldoutGroup("DropdownTitle 参数")]
        [TypeFilter("GetFilteredTypeList", DropdownTitle = "Dropdown 的标题")]
        public BaseClass B;

        [PropertyOrder(10)]
        [FoldoutGroup("DrawValueNormally 参数")]
        [InfoBox(" DrawValueNormally = true，额外绘制一个完整的 B 的实例对象，默认为 false，一般不设置")]
        [TypeFilter("GetFilteredTypeList", DrawValueNormally = true)]
        public BaseClass C;

        public override void SetDefaultValue()
        {
            A = null;
            B = null;
            C = null;
            Array = new BaseClass[3];
        }

        public IEnumerable<Type> GetFilteredTypeList()
        {
            var q = typeof(BaseClass).Assembly.GetTypes()
                .Where(x => !x.IsAbstract) // Excludes BaseClass
                .Where(x => !x.IsGenericTypeDefinition) // Excludes C1<>
                .Where(x => typeof(BaseClass)
                    .IsAssignableFrom(x)); // Excludes classes not inheriting from BaseClass

            // Adds various C1<T> type variants.
            q = q.AppendWith(typeof(C1<>).MakeGenericType(typeof(GameObject)));
            q = q.AppendWith(typeof(C1<>).MakeGenericType(typeof(AnimationCurve)));
            q = q.AppendWith(typeof(C1<>).MakeGenericType(typeof(List<float>)));

            return q;
        }

        public abstract class BaseClass
        {
            public int BaseField;
        }

        private class A1 : BaseClass
        {
            public int _A;
        }

        private class A2 : A1
        {
            public int _A2;
        }

        private class B1 : BaseClass
        {
            public int _B1;
        }

        private class B2 : B1
        {
            public int _B2;
        }

        private class C1<T> : BaseClass
        {
            public T C;
        }
    }
}