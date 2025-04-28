using System;
using Sirenix.OdinInspector;
using Sirenix.Serialization;
using Sirenix.Utilities;
using UnityEngine;

namespace YOGA.OdinToolkits.AnalysisManual.OdinAttributes.Editor
{
    [IsChineseAttributeExample]
    public class PolymorphicDrawerSettingsExample : ExampleOdinScriptableObject
    {
        [FoldoutGroup("CreateInstanceFunction")]
        [PolymorphicDrawerSettings(CreateInstanceFunction = nameof(CreateExampleInstance))]
        public IVector2<int> CreateCustomInstance;

        [FoldoutGroup("Odin 默认的多态样式，不参与序列化")] public IDemo<int> Default;

        [FoldoutGroup("NonDefaultConstructorPreference")]
        [LabelText("Construct Ideal")]
        [PolymorphicDrawerSettings(NonDefaultConstructorPreference = NonDefaultConstructorPreference.ConstructIdeal)]
        public IVector2<int> NonDefaultConstructorPreference_ConstructIdeal;

        [FoldoutGroup("NonDefaultConstructorPreference")]
        [LabelText("Exclude")]
        [PolymorphicDrawerSettings(NonDefaultConstructorPreference = NonDefaultConstructorPreference.Exclude)]
        public IVector2<int> NonDefaultConstructorPreference_Ignore;

        [FoldoutGroup("NonDefaultConstructorPreference")]
        [LabelText("Log Warning")]
        [PolymorphicDrawerSettings(NonDefaultConstructorPreference = NonDefaultConstructorPreference.LogWarning)]
        public IVector2<int> NonDefaultConstructorPreference_LogWarning;

        [FoldoutGroup("NonDefaultConstructorPreference")]
        [LabelText("Prefer Uninitialized")]
        [PolymorphicDrawerSettings(NonDefaultConstructorPreference =
            NonDefaultConstructorPreference.PreferUninitialized)]
        public IVector2<int> NonDefaultConstructorPreference_PreferUninit;

        [FoldoutGroup("ReadOnlyIfNotNullReference")]
        [LabelText("Off")]
        [PolymorphicDrawerSettings(ReadOnlyIfNotNullReference = false)]
        public IDemo<int> ReadOnlyIfNotNullReference_Off;

        [FoldoutGroup("ReadOnlyIfNotNullReference")]
        [LabelText("On")]
        [PolymorphicDrawerSettings(ReadOnlyIfNotNullReference = true)]
        public IDemo<int> ReadOnlyIfNotNullReference_On;

        [FoldoutGroup("ShowBaseType")] [LabelText("Off")] [PolymorphicDrawerSettings(ShowBaseType = false)]
        public IDemo<int> ShowBaseType_Off;

        [FoldoutGroup("ShowBaseType")] [LabelText("On")] [PolymorphicDrawerSettings(ShowBaseType = true)]
        public IDemo<int> ShowBaseType_On;

        private IVector2<int> CreateExampleInstance(Type type)
        {
            Debug.Log("Constructor called for " + type + '.');

            if (typeof(SomeNonDefaultCtorClass) == type) return new SomeNonDefaultCtorClass(485);

            return type.InstantiateDefault(false) as IVector2<int>;
        }

        public interface IVector2<T>
        {
            T X { get; set; }
            T Y { get; set; }
        }

        [Serializable]
        public class SomeNonDefaultCtorClass : IVector2<int>
        {
            public SomeNonDefaultCtorClass(int x)
            {
                X = x;
                Y = (x + 1) * 4;
            }

            [OdinSerialize] public int X { get; set; }

            [OdinSerialize] public int Y { get; set; }
        }

        public interface IDemo<T>
        {
            T Value { get; set; }
        }

        // 使用 Odin 序列化要继承 SerializedScriptableObject
        [Serializable]
        public class DemoSOInt32 : SerializedScriptableObject, IDemo<int>
        {
            [OdinSerialize] public int Value { get; set; }
        }

        [Serializable]
        public class DemoSOInt32Target : SerializedScriptableObject, IDemo<int>
        {
            public int target;

            [OdinSerialize] public int Value { get; set; }
        }

        [Serializable]
        public class DemoSOFloat32 : SerializedScriptableObject, IDemo<float>
        {
            [OdinSerialize] public float Value { get; set; }
        }

        [Serializable]
        public class Demo<T> : IDemo<T>
        {
            [OdinSerialize] public T Value { get; set; }
        }

        [Serializable]
        public class DemoInt32Interface : IDemo<int>
        {
            [OdinSerialize] public int Value { get; set; }
        }

        public class DemoInt32 : Demo<int>
        {
        }

        public struct DemoStructInt32 : IDemo<int>
        {
            [OdinSerialize] public int Value { get; set; }
        }
    }
}