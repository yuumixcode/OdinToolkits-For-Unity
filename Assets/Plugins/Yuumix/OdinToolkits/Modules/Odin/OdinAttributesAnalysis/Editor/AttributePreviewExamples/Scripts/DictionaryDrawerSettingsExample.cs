using Sirenix.OdinInspector;
using System.Collections.Generic;
using UnityEngine;
using Yuumix.OdinToolkits.Modules.Odin.OdinAttributesAnalysis.Common.Editor;

namespace Yuumix.OdinToolkits.Modules.Odin.OdinAttributesAnalysis.Editor.AttributePreviewExamples.Scripts
{
    [IsChineseAttributeExample]
    public class DictionaryDrawerSettingsExample : ExampleOdinScriptableObject
    {
        public enum SomeEnum
        {
            First,
            Second,
            Third,
            Fourth,
            AndSoOn
        }

        [PropertyOrder(1)]
        [FoldoutGroup("DictionaryDrawerSettings 基础使用")]
        [InfoBox("修改 KeyLabel 和 ValueLabel，设置 KeyColumnWidth = 50（无效），" +
                 "默认的 DisplayMode 为 OneLine")]
        [DictionaryDrawerSettings(KeyLabel = "序号", ValueLabel = "名称", KeyColumnWidth = 50)]
        public Dictionary<int, string> IntStringDictionary = new Dictionary<int, string>
        {
            { 1, "Sirenix" },
            { 7, "Yuumi" }
        };

        [PropertyOrder(10)]
        [FoldoutGroup("DictionaryDrawerSettings 基础使用")]
        [InfoBox("修改 DictionaryDisplayOptions 模式为 Foldout")]
        [DictionaryDrawerSettings(DisplayMode = DictionaryDisplayOptions.Foldout)]
        public Dictionary<string, List<int>> StringListDictionary = new Dictionary<string, List<int>>
        {
            { "Numbers", new List<int> { 1, 2, 3, 4 } }
        };

        [PropertyOrder(20)]
        [FoldoutGroup("DictionaryDrawerSettings 基础使用")]
        [InfoBox("设置 IsReadOnly = true，不能在面板上修改字典的元素数量和字典结构，" +
                 "可以通过代码修改，同时可以在面板上修改具体元素内部的 Property")]
        [DictionaryDrawerSettings(IsReadOnly = true)]
        public Dictionary<SomeEnum, MyCustomType> EnumObjectLookup = new Dictionary<SomeEnum, MyCustomType>
        {
            { SomeEnum.Third, new MyCustomType() },
            { SomeEnum.Fourth, new MyCustomType() }
        };

        [PropertyOrder(20)]
        [FoldoutGroup("DictionaryDrawerSettings 基础使用")]
        [HorizontalGroup("DictionaryDrawerSettings 基础使用/Button")]
        [Button("重置字典")]
        void ResetDictionary()
        {
            EnumObjectLookup = new Dictionary<SomeEnum, MyCustomType>
            {
                { SomeEnum.Third, new MyCustomType() },
                { SomeEnum.Fourth, new MyCustomType() }
            };
        }

        [PropertyOrder(20)]
        [FoldoutGroup("DictionaryDrawerSettings 基础使用")]
        [HorizontalGroup("DictionaryDrawerSettings 基础使用/Button")]
        [Button("代码修改字典")]
        void CodeChangeDictionary()
        {
            EnumObjectLookup = new Dictionary<SomeEnum, MyCustomType>
            {
                {
                    SomeEnum.First, new MyCustomType
                    {
                        SomeMember = 1
                    }
                }
            };
        }

        public struct MyCustomType
        {
            public int SomeMember;
            public GameObject SomePrefab;
        }
    }
}
