using Sirenix.OdinInspector;
using UnityEngine;

namespace Yuumix.OdinToolkits.Editor
{
    [OdinToolkitsAttributeExample]
    public class DelayedPropertyExample : ExampleSO
    {
        [PropertyOrder(0)]
        [InfoBox("没有标记任何延迟赋值的字段，只要有修改就会触发事件")]
        [OnValueChanged("OnValueChanged")]
        public string normal;

        [PropertyOrder(1)]
        [InfoBox("标记 Unity 内置的 Delayed 特性")]
        [OnValueChanged("OnValueChanged")]
        [Delayed]
        public string delayedField;

        [PropertyOrder(10)]
        [InfoBox("标记 Odin 的 DelayedProperty，可以作用于普通字段")]
        [OnValueChanged("OnValueChanged")]
        [DelayedProperty]
        public string odinDelayedField;

        [ShowInInspector]
        [PropertyOrder(20)]
        [InfoBox("标记 Odin 的 DelayedProperty，可以对属性生效，" +
                 "需要注意使用 [ShowInspector] 显示属性时，并没有序列化")]
        [DelayedProperty]
        [OnValueChanged("OnValueChanged")]
        public string DelayedProperty { get; set; }

        void OnValueChanged()
        {
            Debug.Log("Value changed!");
        }

        public override void SetDefaultValue()
        {
            normal = "normal";
            delayedField = "delayedField";
            odinDelayedField = "odinDelayedField";
            DelayedProperty = "DelayedProperty";
        }
    }
}
