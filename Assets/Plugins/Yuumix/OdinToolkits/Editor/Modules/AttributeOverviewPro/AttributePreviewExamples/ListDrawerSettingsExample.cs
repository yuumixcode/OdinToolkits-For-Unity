using System;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using Sirenix.Utilities.Editor;
using UnityEngine;
using ObjectFieldAlignment = Sirenix.OdinInspector.ObjectFieldAlignment;

namespace Yuumix.OdinToolkits.Editor
{
    [OdinToolkitsAttributeExample]
    public class ListDrawerSettingsExample : ExampleSO
    {
        [FoldoutGroup("ListDrawerSettings 基础使用")]
        [Title("IsReadOnly", "IsReadOnly = true")]
        [InfoBox("无法修改列表的元素数量，但是可以修改具体元素内部的值")]
        [ListDrawerSettings(IsReadOnly = true)]
        public int[] readOnlyArray1 = { 1, 2, 3 };

        [FoldoutGroup("ListDrawerSettings 基础使用")]
        [ReadOnly]
        [InfoBox("另外一种只读方式 [ReadOnly] ，直接让列表全部无法获取焦点")]
        public int[] readOnlyArray2 = { 1, 2, 3 };

        [FoldoutGroup("ListDrawerSettings 基础使用")]
        [Title("ShowFoldout", "ShowFoldout = false")]
        [ListDrawerSettings(ShowFoldout = false)]
        [LabelText("关闭列表折叠显示，无法收起")]
        public List<int> showFoldoutList;

        [FoldoutGroup("ListDrawerSettings 基础使用")]
        [Title("ShowIndexLabels", "ShowIndexLabels = true")]
        [ListDrawerSettings(ShowIndexLabels = true)]
        [LabelText("显示序号列表")]
        public List<SomeStruct> showIndexLabelsList;

        [FoldoutGroup("ListDrawerSettings 基础使用")]
        [Title("ListElementLabelName ", "ListElementLabelName = \"someString\"")]
        [ListDrawerSettings(ListElementLabelName = "someString")]
        [LabelText("自定义元素标签名称")]
        public List<SomeStruct> listElementLabelNameList;

        [FoldoutGroup("ListDrawerSettings 基础使用")]
        [Title("NumberOfItemsPerPage", "NumberOfItemsPerPage = 5")]
        [ListDrawerSettings(NumberOfItemsPerPage = 5)]
        [LabelText("控制每页的元素的长度为 5")]
        public int[] fiveItemsPerPage;

        [FoldoutGroup("ListDrawerSettings 基础使用")]
        [Title("DraggableItems", "DraggableItems = false")]
        [ListDrawerSettings(DraggableItems = false)]
        [LabelText("禁止拖拽")]
        public int[] dontDragArray;

        [FoldoutGroup("ListDrawerSettings 基础使用")]
        [Title("ShowPaging 和 ShowItemCount", "ShowPaging = false, ShowItemCount = false")]
        [ListDrawerSettings(ShowPaging = false, ShowItemCount = false)]
        [LabelText("关闭分页和关闭元素数量显示")]
        public int[] moreListSettings = { 1, 2, 3 };

        [FoldoutGroup("ListDrawerSettings 基础使用")]
        [Title("AlwaysAddDefaultValue", "AlwaysAddDefaultValue = true")]
        [ListDrawerSettings(AlwaysAddDefaultValue = true)]
        [LabelText("直接添加默认值到列表")]
        public List<int> alwaysAddDefaultValueList;

        [FoldoutGroup("ListDrawerSettings 基础使用")]
        [Title("AddCopiesLastElement", "AddCopiesLastElement = true")]
        [ListDrawerSettings(AddCopiesLastElement = true)]
        [LabelText("复制最后一个添加的元素")]
        public List<int> addCopiesLastElementList;

        [FoldoutGroup("ListDrawerSettings 基础使用")]
        [Title("ElementColor")]
        [ListDrawerSettings(ElementColor = "lightblue")]
        [LabelText("使用颜色名称自定义颜色")]
        public int[] colorArray = { 1, 2, 3 };

        [FoldoutGroup("ListDrawerSettings 基础使用")]
        public Color firstColor = new Color(1f, 0.79f, 0.14f, 1f);

        [FoldoutGroup("ListDrawerSettings 基础使用")]
        public Color secondColor = new Color(0.11f, 0.77f, 0.5f, 1f);

        [FoldoutGroup("ListDrawerSettings 基础使用")]
        [ListDrawerSettings(ElementColor = "GetElementColor")]
        [LabelText("使用方法来控制不同元素的颜色")]
        public int[] colorArray2 = { 1, 2, 3 };

        [FoldoutGroup("ListDrawerSettings 基础使用")]
        [ListDrawerSettings(ElementColor = "@($index%2 == 0) ? firstColor : secondColor")]
        [LabelText("使用表达式控制颜色")]
        public int[] expressionColorArray;

        [FoldoutGroup("ListDrawerSettings 进阶使用")]
        [Title("OnTitleBarGUI", "OnTitleBarGUI = \"DrawRefreshButton\"")]
        [ListDrawerSettings(OnTitleBarGUI = "DrawRefreshButton")]
        [LabelText("新增列表名位置的 GUI 图形")]
        public List<int> customTitleBarGUI;

        [FoldoutGroup("ListDrawerSettings 进阶使用")]
        [Title("HideAddButton", "HideAddButton = true")]
        [ListDrawerSettings(HideAddButton = true)]
        [LabelText("隐藏添加按钮")]
        public int[] someStructList1;

        [FoldoutGroup("ListDrawerSettings 进阶使用")]
        [Title("CustomAddFunction", "CustomAddFunction = \"CustomAddFunction\"")]
        [ListDrawerSettings(CustomAddFunction = "CustomAddFunction")]
        [LabelText("自定义添加按钮的逻辑")]
        public List<int> customAddBehaviour;

        [FoldoutGroup("ListDrawerSettings 进阶使用")]
        [Title("HideRemoveButton", "HideRemoveButton = true")]
        [ListDrawerSettings(HideRemoveButton = true)]
        [LabelText("隐藏移除按钮")]
        public List<int> customBehaviour1;

        [FoldoutGroup("ListDrawerSettings 进阶使用")]
        [Title("CustomRemoveElementFunction", "CustomRemoveElementFunction " +
                                              "= \"CustomRemoveElementIndex\"")]
        [LabelText("自定义移除元素的方法")]
        [ListDrawerSettings(CustomRemoveElementFunction = "CustomRemoveElementFunction")]
        public List<int> customRemoveBehaviour;

        [FoldoutGroup("ListDrawerSettings 进阶使用")]
        [Title("OnBeginListElementGUI 和 OnEndListElementGUI",
            "OnBeginListElementGUI = \"BeginDrawListElement\", " +
            "OnEndListElementGUI = \"EndDrawListElement\"")]
        [ListDrawerSettings(OnBeginListElementGUI = "BeginDrawListElement",
            OnEndListElementGUI = "EndDrawListElement")]
        [LabelText("对一个元素设置两个 GUI 方法")]
        public SomeStruct[] injectListElementGUI;

        Color GetElementColor(int index) => index % 2 == 0 ? firstColor : secondColor;

        void BeginDrawListElement(int index)
        {
            SirenixEditorGUI.BeginBox(injectListElementGUI[index].someString);
        }

        void EndDrawListElement(int index)
        {
            SirenixEditorGUI.EndBox();
        }

        void DrawRefreshButton()
        {
            if (SirenixEditorGUI.ToolbarButton(EditorIcons.Refresh))
            {
                Debug.Log("执行自定义 Refresh 方法");
            }
        }

        void CustomRemoveElementFunction(List<int> list, int element)
        {
            Debug.Log("移除的元素为: " + element);
            list.Remove(element);
        }

        void CustomRemoveElementIndex(List<int> list, int index)
        {
            list.RemoveAt(index);
            Debug.Log("Custom Remove Element Index Function Called");
        }

        int CustomAddFunction() => customAddBehaviour.Count;

        [Serializable]
        public struct SomeStruct
        {
            public string someString;
            public int one;
            public int two;
        }

        [Serializable]
        public struct SomeOtherStruct
        {
            [HorizontalGroup("Split", 55)]
            [PropertyOrder(-1)]
            [PreviewField(50, ObjectFieldAlignment.Left)]
            [HideLabel]
            public MonoBehaviour someObject;

            [FoldoutGroup("Split/$Name", false)]
            public int a, b, c;

            string Name => someObject ? someObject.name : "Null";
        }
    }
}
