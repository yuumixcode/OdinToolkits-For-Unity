using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using Sirenix.OdinInspector;

namespace Yuumix.OdinToolkits.Modules.Editor
{
    [OdinToolkitsAttributeExample]
    public class TypeDrawerSettingsExample : ExampleOdinSO
    {
        public Type Default;

        [FoldoutGroup(nameof(TypeDrawerSettingsAttribute.BaseType))]
        [LabelText("Set")]
        [TypeDrawerSettings(BaseType = typeof(IEnumerable<>))]
        public Type BaseType_Set;

        [FoldoutGroup(nameof(TypeDrawerSettingsAttribute.BaseType))]
        [LabelText("Not Set")]
        [TypeDrawerSettings(BaseType = null)]
        public Type BaseType_NotSet;

        [FoldoutGroup(nameof(TypeDrawerSettingsAttribute.Filter))]
        [LabelText("具体实例类型")]
        [TypeDrawerSettings(BaseType = typeof(IBaseGeneric<>),
            Filter = TypeInclusionFilter.IncludeConcreteTypes)]
        public Type Filter_Default;

        [FoldoutGroup(nameof(TypeDrawerSettingsAttribute.Filter))]
        [LabelText("具体实例类型 && 泛型")]
        [TypeDrawerSettings(BaseType = typeof(IBaseGeneric<>),
            Filter = TypeInclusionFilter.IncludeConcreteTypes | TypeInclusionFilter.IncludeGenerics)]
        public Type Filter_Generics;

        [FoldoutGroup(nameof(TypeDrawerSettingsAttribute.Filter))]
        [LabelText("具体实例类型 && 接口类型")]
        [TypeDrawerSettings(BaseType = typeof(IBaseGeneric<>),
            Filter = TypeInclusionFilter.IncludeConcreteTypes | TypeInclusionFilter.IncludeInterfaces)]
        public Type Filter_Interfaces;

        [FoldoutGroup(nameof(TypeDrawerSettingsAttribute.Filter))]
        [LabelText("具体实例类型 && 抽象类")]
        [TypeDrawerSettings(BaseType = typeof(IBaseGeneric<>),
            Filter = TypeInclusionFilter.IncludeConcreteTypes | TypeInclusionFilter.IncludeAbstracts)]
        public Type Filter_Abstracts;

        [FoldoutGroup(nameof(TypeDrawerSettingsAttribute.Filter))]
        [LabelText("具体实例类型, 抽象类 && 泛型")]
        [TypeDrawerSettings(BaseType = typeof(IBaseGeneric<>),
            Filter = TypeInclusionFilter.IncludeConcreteTypes |
                     TypeInclusionFilter.IncludeAbstracts |
                     TypeInclusionFilter.IncludeGenerics)]
        public Type Filter_Abstracts_Generics;

        [FoldoutGroup(nameof(TypeDrawerSettingsAttribute.Filter))]
        [LabelText("具体实例类型, 接口类型 && 泛型")]
        [TypeDrawerSettings(BaseType = typeof(IBaseGeneric<>),
            Filter = TypeInclusionFilter.IncludeConcreteTypes |
                     TypeInclusionFilter.IncludeInterfaces |
                     TypeInclusionFilter.IncludeGenerics)]
        public Type Filter_Interfaces_Generics;

        [FoldoutGroup(nameof(TypeDrawerSettingsAttribute.Filter))]
        [LabelText("具体实例类型, 接口类型 && 抽象类")]
        [TypeDrawerSettings(BaseType = typeof(IBaseGeneric<>),
            Filter = TypeInclusionFilter.IncludeConcreteTypes |
                     TypeInclusionFilter.IncludeInterfaces |
                     TypeInclusionFilter.IncludeAbstracts)]
        public Type Filter_Interfaces_Abstracts;

        [FoldoutGroup(nameof(TypeDrawerSettingsAttribute.Filter))]
        [LabelText("All")]
        [TypeDrawerSettings(BaseType = typeof(IBaseGeneric<>),
            Filter = TypeInclusionFilter.IncludeAll)]
        public Type Filter_All;

        public interface IBaseGeneric<T> { }

        public interface IBase : IBaseGeneric<int> { }

        public abstract class Base : IBase { }

        public class Concrete : Base { }

        public class ConcreteGeneric<T> : Base { }

        public abstract class BaseGeneric<T> : IBase { }

        [CompilerGenerated]
        public class ConcreteGenerated : Base { }
    }
}
