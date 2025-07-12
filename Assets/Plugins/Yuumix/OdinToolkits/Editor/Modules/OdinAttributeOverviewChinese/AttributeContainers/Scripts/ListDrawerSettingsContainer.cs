using System.Collections.Generic;
using Yuumix.OdinToolkits.Modules.Odin.OdinAttributesAnalysis.Common.Editor;
using Yuumix.OdinToolkits.Modules.Odin.OdinAttributesAnalysis.Editor.AttributePreviewExamples.Scripts;

namespace Yuumix.OdinToolkits.Modules.Odin.OdinAttributesAnalysis.Editor.AttributeContainers.Scripts
{
    public class ListDrawerSettingsContainer : AbsContainer
    {
        protected override string SetHeader() => "ListDrawerSettings";

        protected override string SetBrief() => "自定义 List 的绘制样式";

        protected override List<string> SetTip() => new List<string>();

        protected override List<ParamValue> SetParamValues() =>
            new List<ParamValue>
            {
                new ParamValue
                {
                    returnType = "bool",
                    paramName = "IsReadOnly",
                    paramDescription = "是否列表只读，无法添加和删除元素，但可以修改元素内部的值"
                },
                new ParamValue
                {
                    returnType = "bool",
                    paramName = "ShowFoldout",
                    paramDescription = "是否以 Foldout 的方式绘制，默认为 true，如果为 false，则将永远是展开状态，无法收起"
                },
                new ParamValue
                {
                    returnType = "bool",
                    paramName = "ShowIndexLabels",
                    paramDescription = "是否显示元素序号，默认为 false"
                },
                new ParamValue
                {
                    returnType = "string",
                    paramName = "ListElementLabelName",
                    paramDescription = "自定义列表元素的名称，但是元素名称需要引用元素内部的成员名进行解析"
                },
                new ParamValue
                {
                    returnType = "bool",
                    paramName = "ShowPaging",
                    paramDescription = "是否可以分页，默认为 true"
                },
                new ParamValue
                {
                    returnType = "int",
                    paramName = "NumberOfItemsPerPage",
                    paramDescription = "每页的元素数量"
                },
                new ParamValue
                {
                    returnType = "bool",
                    paramName = "ShowItemCount",
                    paramDescription = "是否显示元素数量，默认为 true"
                },
                new ParamValue
                {
                    returnType = "bool",
                    paramName = "DraggableItems",
                    paramDescription = "是否可以拖拽元素，默认为 true"
                },
                new ParamValue
                {
                    returnType = "string",
                    paramName = "ElementColor",
                    paramDescription = DescriptionConfigs.ColorDescription
                },
                new ParamValue
                {
                    returnType = "string",
                    paramName = "OnTitleBarGUI",
                    paramDescription = "自定义注入列表头部的样式，" + DescriptionConfigs.SupportMemberResolverLite
                },
                new ParamValue
                {
                    returnType = "bool",
                    paramName = "HideAddButton",
                    paramDescription = "隐藏添加按钮"
                },
                new ParamValue
                {
                    returnType = "string",
                    paramName = "CustomAddFunction",
                    paramDescription = "自定义添加元素的方法名，覆盖将对象添加到列表的默认行为。方法返回值为列表元素。"
                },
                new ParamValue
                {
                    returnType = "bool",
                    paramName = "HideRemoveButton",
                    paramDescription = "隐藏删除按钮"
                },
                new ParamValue
                {
                    returnType = "string",
                    paramName = "CustomRemoveFunction",
                    paramDescription = "自定义删除元素的方法名，方法参数可选 List<T> list，T value，InspectorProperty property（仅 Editor ）"
                },
                new ParamValue
                {
                    returnType = "string",
                    paramName = "CustomRemoveIndexFunction",
                    paramDescription = "自定义移除序号的方法名，方法参数可选 List<T> list，int index，InspectorProperty property（仅 Editor ）"
                },
                new ParamValue
                {
                    returnType = "string",
                    paramName = "OnBeginListElementGUI",
                    paramDescription = "填写成员方法名，在每个列表元素之后调用一个方法。被引用的方法必须有一个返回类型 void，" +
                                       "以及一个表示绘制的元素索引的int类型的索引形参。"
                },
                new ParamValue
                {
                    returnType = "string",
                    paramName = "OnEndListElementGUI",
                    paramDescription = "填写成员方法名，在每个列表元素之后调用一个方法。被引用的方法必须有一个返回类型 void，" +
                                       "以及一个表示绘制的元素索引的int类型的索引形参。"
                },
                new ParamValue
                {
                    returnType = "bool",
                    paramName = "AlwaysAddDefaultValue",
                    paramDescription = "如果为true，则在单击列表添加按钮时永远不会显示对象/类型选择器，而默认值(T)将总是立即添加，其中T是列表的元素类型。"
                },
                new ParamValue
                {
                    returnType = "bool",
                    paramName = "AddCopiesLastElement",
                    paramDescription = "是否在添加新元素时立刻复制最后一个元素。默认为False。"
                }
            };

        protected override string SetOriginalCode() => ReadCodeWithoutNamespace(typeof(ListDrawerSettingsExample));
    }
}
