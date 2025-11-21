using Sirenix.OdinInspector;
using System;
using Yuumix.OdinToolkits.AttributeOverviewPro.Shared;

namespace Yuumix.OdinToolkits.Modules.Editor
{
    [AttributeOverviewProExample]
    public class ToggleExample : ExampleSO
    {
        #region Serialized Fields

        public ToggleableClass toggleable = new ToggleableClass();

        // 对单一字段进行修改
        [Toggle(nameof(MyToggleable.enabled))]
        public MyToggleable toggler = new MyToggleable();

        #endregion

        #region Nested type: ${0}

        [Serializable]
        public class MyToggleable
        {
            #region Serialized Fields

            public bool enabled;
            public int myValue;

            #endregion
        }

        // 可以直接把特性赋值给类，不需要对单一字段进行修改
        [Serializable]
        [Toggle(nameof(enabled))]
        public class ToggleableClass
        {
            #region Serialized Fields

            public bool enabled;
            public string text;

            #endregion
        }

        #endregion
    }
}
