using Sirenix.OdinInspector;
using UnityEngine;

namespace Yuumix.OdinToolkits.Modules
{
    public class ShowInInspectorComponent : MonoBehaviour
    {
        [PropertyOrder(12)]
        [InfoBox("静态字段")]
        [ShowInInspector]
        [InlineButton("Log6", "输出值")]
        public static int StaticField;

        [PropertyOrder(30)]
        [SerializeField]
        [InfoBox("依靠私有字段序列化来保存值，通常会隐藏私有字段")]
        int evenNumber;

        [PropertyOrder(1)]
        [ShowInInspector]
        [Title("基础显示", "修改值可以生效，但是没有序列化保存，Play 将会丢失")]
        [InlineButton("Log1", "输出值")]
        int _myPrivateInt = 5;

        [PropertyOrder(5)]
        [InfoBox("显示属性")]
        [InlineButton("Log2", "输出值")]
        [ShowInInspector]
        public int MyPropertyInt { get; set; } = 5;

        [PropertyOrder(10)]
        [InfoBox("显示只读属性")]
        [ShowInInspector]
        public int ReadOnlyProperty => _myPrivateInt;

        [PropertyOrder(15)]
        [InfoBox("显示静态属性")]
        [InlineButton("Log4", "输出值")]
        [ShowInInspector]
        public static bool StaticProperty { get; set; }

        [PropertyOrder(25)]
        [Title("扩展", "通过和序列化的字段配合使用，也可以保存，同时还可以自定义处理数据")]
        [ShowInInspector]
        [InlineButton("Log5", "输出值")]
        public int EvenNumberProperty
        {
            get => evenNumber;
            set => evenNumber = value - value % 2;
        }

        [PropertyOrder(50)]
        [ShowInInspector]
        [Title("修改项目静态字段", "此值对应 ProjectSettings 中的 FixedDeltaTime，可以直接修改，不会丢失")]
        [PropertyRange(0, 0.1f)]
        [InlineButton("SetFixedDeltaTimeDefault", "重置为默认值")]
        public static float FixedDeltaTime
        {
            get => Time.fixedDeltaTime;
            set => Time.fixedDeltaTime = value;
        }

        void Log1()
        {
            Debug.Log(_myPrivateInt);
        }

        void Log2()
        {
            Debug.Log(MyPropertyInt);
        }

        void Log4()
        {
            Debug.Log(StaticProperty);
        }

        void Log6()
        {
            Debug.Log(StaticField.ToString());
        }

        void Log5()
        {
            Debug.Log(EvenNumberProperty.ToString());
        }

        void SetFixedDeltaTimeDefault()
        {
            FixedDeltaTime = 0.02f;
        }
    }
}
