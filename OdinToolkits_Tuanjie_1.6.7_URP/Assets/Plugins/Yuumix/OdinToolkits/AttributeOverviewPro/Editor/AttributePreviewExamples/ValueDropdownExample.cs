using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Sirenix.OdinInspector;
using UnityEditor;
using UnityEngine;
using Yuumix.OdinToolkits.AttributeOverviewPro.Shared;
using Object = UnityEngine.Object;

namespace Yuumix.OdinToolkits.Modules.Editor
{
    [AttributeOverviewProExample]
    public class ValueDropdownExample : ExampleSO
    {
        static int[] _textureSizes = { 256, 512, 1024 };

        #region NumberOfItemsBeforeEnablingSearch 参数

        [FoldoutGroup("NumberOfItemsBeforeEnablingSearch 参数 下拉列表的搜索框")]
        [InfoBox("设置 NumberOfItemsBeforeEnablingSearch = 7，下拉列表中的元素数量达到 7 个出现搜索框，" +
                 "搜索框不是列表的搜索框")]
        [ValueDropdown("_treeViewOfInts", NumberOfItemsBeforeEnablingSearch = 7)]
        public List<int> intTreeviewMode2 = new List<int> { 1, 2, 7 };

        #endregion

        #region AppendNextDrawer 参数

        [PropertySpace(0, 10)]
        [FoldoutGroup("AppendNextDrawer 参数")]
        [InfoBox("AppendNextDrawer == true 表示添加一个小按钮来触发下拉选项，而不是直接替换原来的绘制样式")]
        [ValueDropdown("_friendlyTextureSizes", AppendNextDrawer = true)]
        public int someSize3;

        #endregion

        #region DisableGUIInAppendedDrawer 参数

        [PropertySpace(0, 10)]
        [FoldoutGroup("DisableGUIInAppendedDrawer 参数 需要配合 AppendNextDrawer 参数")]
        [InfoBox("DisableGUIInAppendedDrawer == true 让原本的值的绘制变成无法交互，避免两个方式都可以修改值")]
        [ValueDropdown("_friendlyTextureSizes", AppendNextDrawer = true, DisableGUIInAppendedDrawer = true)]
        public int someSize4;

        #endregion

        #region DropdownTitle 参数

        [PropertySpace(0, 10)]
        [FoldoutGroup("DropdownTitle 参数 设置下拉框的标题")]
        [ValueDropdown("_friendlyTextureSizes", DropdownTitle = "标题: TextureSize")]
        public int someSize5;

        #endregion

        #region DropdownWidth 和 DropdownHeight

        [PropertySpace(0, 10)]
        [FoldoutGroup("DropdownWidth 和 DropdownHeight")]
        [InfoBox("DropdownWidth 设置整个选择框宽度 = 100，DropdownHeight 设置整个选择框的高度 = 100")]
        [ValueDropdown("_friendlyTextureSizes", DropdownWidth = 100, DropdownHeight = 100)]
        public int someSize6;

        #endregion

        #region ExpandAllMenuItems 参数

        [PropertySpace(0, 10)]
        [FoldoutGroup("ExpandAllMenuItems 参数 是否展开树状图")]
        [InfoBox("ValueDropdown 作用在列表上时，默认修改的是其中元素的赋值，可以实现树状选择框，" +
                 "使用 ValueDropdownList 即可，" +
                 "根据列表的第一个参数来绘制 TreeView，ExpandAllMenuItems == true 默认情况展开树状图")]
        [ValueDropdown("_treeViewOfInts", ExpandAllMenuItems = true)]
        public List<int> intTreeview = new List<int> { 1, 2, 7 };

        #endregion

        #region ExcludeExistingValuesInList 参数

        [FoldoutGroup("ExcludeExistingValuesInList 参数")]
        [InfoBox("列表类型为 GameObject，在满足 IsUniqueList == true 的条件下，" +
                 "才能设置 ExcludeExistingValuesInList = true or false，" +
                 "如果为 true，将会直接剔除重复的，不需要手动勾选")]
        [ValueDropdown("GetAllSceneObjects", IsUniqueList = true, DropdownTitle = "标题: 选择场景物体",
            ExcludeExistingValuesInList = true)]
        public List<GameObject> uniqueGameObjectsIsUniqueListMode2;

        #endregion

        #region DrawDropdownForListElements 参数

        [FoldoutGroup("DrawDropdownForListElements 参数 关闭对列表中元素绘制的修改")]
        [InfoBox("列表类型为 GameObject，DrawDropdownForListElements = false，" +
                 "表示下拉选择框只在列表 + 号绘制，不为其中元素绘制")]
        [ValueDropdown("GetAllSceneObjects", IsUniqueList = true, DropdownTitle = "标题: 选择场景物体",
            ExcludeExistingValuesInList = true, DrawDropdownForListElements = false)]
        public List<GameObject> uniqueGameObjectsIsUniqueListMode3;

        #endregion

        #region DisableListAddButtonBehaviour 参数

        [FoldoutGroup("DisableListAddButtonBehaviour 参数 关闭添加元素时的选择框")]
        [ValueDropdown("_treeViewOfInts", DisableListAddButtonBehaviour = true)]
        public List<int> intTreeviewMode3 = new List<int> { 1, 2, 7 };

        #endregion

        #region DoubleClickToConfirm 参数

        [FoldoutGroup("DoubleClickToConfirm 参数 鼠标双击才确认选择")]
        [ValueDropdown("_treeViewOfInts", DoubleClickToConfirm = true)]
        public List<int> intTreeviewMode4 = new List<int> { 1, 2, 7 };

        #endregion

        #region FlattenTreeView 参数

        [FoldoutGroup("FlattenTreeView 参数 取消树状图缩进")]
        [ValueDropdown("_treeViewOfInts", FlattenTreeView = true)]
        public List<int> intTreeviewMode5 = new List<int> { 1, 2, 7 };

        #endregion

        #region SortDropdownItems 参数

        [FoldoutGroup("SortDropdownItems 参数 自动排序")]
        [InfoBox("自动排序，将不按照提供的数组的原本顺序")]
        [ValueDropdown("_customInts", SortDropdownItems = true)]
        public int customInt = 10;

        #endregion

        readonly int[] _customInts =
        {
            1, 5, 6, 3, 4, 5, 8, 76, 100
        };

        readonly IEnumerable _treeViewOfInts = new ValueDropdownList<int>
        {
            { "Node 1/Node 1.1", 1 },
            { "Node 1/Node 1.2", 2 },
            { "Node 2/Node 2.1", 3 },
            { "Node 3/Node 3.1", 4 },
            { "Node 3/Node 3.2", 5 },
            { "Node 1/Node 3.1/Node 3.1.1", 6 },
            { "Node 1/Node 3.1/Node 3.1.2", 7 }
        };

        public IEnumerable RangeVector3()
        {
            return Enumerable.Range(0, 10).Select(i => new Vector3(i, i, i));
        }

        static IEnumerable GetAllSceneObjects()
        {
            return FindObjectsByType<GameObject>(FindObjectsInactive.Include, FindObjectsSortMode.None)
                .Select(x => new ValueDropdownItem(GetPath(x.transform), x));

            string GetPath(Transform x)
            {
                return x ? GetPath(x.parent) + "/" + x.gameObject.name : "";
            }
        }

        static IEnumerable GetAllSirenixAssets()
        {
            const string root = "Assets/Plugins/Sirenix/";

            return AssetDatabase.GetAllAssetPaths()
                .Where(x => x.StartsWith(root))
                .Select(x => x.Substring(root.Length))
                .Select(x =>
                    new ValueDropdownItem(x, AssetDatabase.LoadAssetAtPath<Object>(root + x)));
        }

        #region ValueGetter

        [FoldoutGroup("ValueGetter 参数 支持多种解析字符串")]
        [LabelWidth(260)]
        public bool useAlternativeValues;

        [FoldoutGroup("ValueGetter 参数 支持多种解析字符串")]
        [LabelWidth(260)]
        public List<string> values = new List<string> { "Value 1", "Value 2", "Value 3" };

        [FoldoutGroup("ValueGetter 参数 支持多种解析字符串")]
        [LabelWidth(260)]
        public List<string> alternativeValues = new List<string> { "1", "2", "3" };

        [FoldoutGroup("ValueGetter 参数 支持多种解析字符串")]
        [ValueDropdown("@useAlternativeValues ? alternativeValues : ValuesProperty")]
        [LabelWidth(260)]
        public string valueGetterAttributeExpressionExample;

        [FoldoutGroup("ValueGetter 参数 支持多种解析字符串")]
        [ValueDropdown("values")]
        [LabelWidth(260)]
        public string valueGetterFieldNameExample;

        [FoldoutGroup("ValueGetter 参数 支持多种解析字符串")]
        [ValueDropdown("GetValues")]
        [LabelWidth(260)]
        public string valueGetterMethodNameExample;

        [FoldoutGroup("ValueGetter 参数 支持多种解析字符串")]
        [ValueDropdown("ValuesProperty")]
        [LabelWidth(260)]
        public string valueGetterPropertyNameExample;

        [FoldoutGroup("ValueGetter 参数 支持多种解析字符串")]
        [InfoBox("Odin 提供了一个特殊的列表 ValueDropdownList，专门为 ValueDropdown 特性使用\n" +
                 "需要特别注意: 这个值的类型是 int，但是使用 Small 等字符串表示，实际上对应的是 int 类型的值")]
        [ValueDropdown("_friendlyTextureSizes")]
        [LabelWidth(260)]
        [InlineButton("@Debug.Log(\"someSize2 的值为: \" + someSize2)", "输出值")]
        public int someSize2;

        static readonly IEnumerable _friendlyTextureSizes = new ValueDropdownList<int>
        {
            { "Small", 256 },
            { "Medium", 512 },
            { "Large", 1024 }
        };

        List<string> ValuesProperty => useAlternativeValues ? alternativeValues : values;

        IEnumerable GetValues() => useAlternativeValues ? alternativeValues : values;

        #endregion

        #region IsUniqueList 参数

        [FoldoutGroup("IsUniqueList 参数")]
        [InfoBox("列表类型为 GameObject，此时 IsUniqueList == false，内容非唯一")]
        [ValueDropdown("GetAllSceneObjects")]
        public List<GameObject> uniqueGameObjectsNoUniqueList;

        [FoldoutGroup("IsUniqueList 参数")]
        [InfoBox("列表类型为 GameObject，此时 IsUniqueList == true，表示内容是唯一的，" +
                 "再次选择同一个物体，点击 CheckBox，将会是移除操作")]
        [InfoBox("这个参数实现有问题，实际上应该是要绘制一个 CheckBox 在前面，然后选择是否勾选，" +
                 "Odin 问题，当前版本 3.3.1.11", InfoMessageType.Warning)]
        [ValueDropdown("GetAllSceneObjects", IsUniqueList = true)]
        public List<GameObject> uniqueGameObjectsIsUniqueList;

        #endregion

        #region OnlyChangeValueOnConfirm

        [FoldoutGroup("OnlyChangeValueOnConfirm 参数")]
        [InfoBox("设置 OnlyChangeValueOnConfirm = true，如果设置为 true，当完全确认下拉框中的选择时，" +
                 "实际属性值将只更改一次")]
        [ValueDropdown("GetAllSirenixAssets", OnlyChangeValueOnConfirm = true, DoubleClickToConfirm = true)]
        [OnValueChanged("@Debug.Log(\" sirenixAsset 的值改变了\")")]
        [InlineButton("@Debug.Log(\" sirenixAsset 的值为: \" + sirenixAssetMode2.ToString())"
            , "输出值")]
        public Object sirenixAssetMode;

        [FoldoutGroup("OnlyChangeValueOnConfirm 参数")]
        [InfoBox("默认 OnlyChangeValueOnConfirm = false，双击才能确认")]
        [ValueDropdown("GetAllSirenixAssets", DoubleClickToConfirm = true)]
        [OnValueChanged("@Debug.Log(\" sirenixAsset 的值改变了\")")]
        [InlineButton("@Debug.Log(\" sirenixAsset 的值为: \" + sirenixAssetMode2.ToString())"
            , "输出值")]
        public Object sirenixAssetMode2;

        [PropertyOrder(4)]
        [FoldoutGroup("OnlyChangeValueOnConfirm 参数")]
        [Title("验证默认情况下是否为引用")]
        [Button("Mode 和 Mode2 的 InstanceID 是否相同", ButtonSizes.Large)]
        void LogInstanceID()
        {
            Debug.Log("sirenixAssetMode 的 InstanceID 为: " + sirenixAssetMode.GetInstanceID());
            Debug.Log("sirenixAssetMode2 的 InstanceID 为: " + sirenixAssetMode2.GetInstanceID());
            if (sirenixAssetMode.GetInstanceID() == sirenixAssetMode2.GetInstanceID())
            {
                Debug.Log("两个 Object 类型（引用类型）的 InstanceId 相同，说明是同一个资源，说明默认情况下是引用");
            }
        }

        #endregion

        #region CopyValues

        [FoldoutGroup("CopyValues 参数")]
        [InfoBox("默认 CopyValues = true")]
        [ValueDropdown("GetOptions")]
        public MyComplexType selectedOption1;

        [FoldoutGroup("CopyValues 参数")]
        [InfoBox("CopyValues = false")]
        [ValueDropdown("GetOptions", CopyValues = false)]
        public MyComplexType selectedOption2;

        ValueDropdownList<MyComplexType> GetOptions() =>
            new ValueDropdownList<MyComplexType>
            {
                new ValueDropdownItem<MyComplexType>("Option 0", new MyComplexType { Name = "Option 0", value = 0 }),
                new ValueDropdownItem<MyComplexType>("Option 1", new MyComplexType { Name = "Option 1", value = 1 })
            };

        [Serializable]
        public class MyComplexType
        {
            public string Name;
            public int value;
        }

        #endregion

        #region HideChildProperties

        [PropertyOrder(5)]
        [FoldoutGroup("HideChildProperties 参数")]
        [InfoBox("HideChildProperties 默认为 false")]
        [ValueDropdown("RangeVector3")]
        [LabelWidth(200)]
        public Vector3 vector3ShowChildProperties;

        [PropertyOrder(5)]
        [FoldoutGroup("HideChildProperties 参数")]
        [InfoBox("设置 HideChildProperties = true，隐藏子属性")]
        [ValueDropdown("RangeVector3", HideChildProperties = true)]
        [OnValueChanged("@Debug.Log(\" vector3HideChildProperties 的值改变了\")")]
        [LabelWidth(200)]
        public Vector3 vector3HideChildProperties;

        #endregion
    }
}
