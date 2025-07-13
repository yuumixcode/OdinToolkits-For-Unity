using System.Collections.Generic;
using Sirenix.OdinInspector.Editor;

namespace Yuumix.OdinToolkits.Editor
{
    public class ValueDropdownContainer : OdinAttributeContainerSO
    {
        protected override string GetHeader() => "ValueDropdown";

        protected override string GetIntroduction() => "绘制一个下拉选择框，在设置值时，给定一个范围，避免出错";

        protected override List<string> GetTips() =>
            new List<string>
            {
                "Odin 提供了一个特殊的列表 ValueDropdownList，专门为 ValueDropdown 特性使用，类似于字典的映射，显示的是字符串，实际是设置特定类型的值",
                "Odin 3.3.1.11 版本 - IsUniqueList 使用有问题，实际应该出现的样式可以参考 AssetSelector 的下拉选择框"
            };

        protected override List<ParamValue> GetParamValues() =>
            new List<ParamValue>
            {
                new ParamValue
                {
                    returnType = "string",
                    paramName = "valuesGetter",
                    paramDescription = "返回特定类型数组的方法名，用于设置下拉选项，" + DescriptionConfigs.SupportAllResolver
                },
                new ParamValue
                {
                    returnType = "int",
                    paramName = "NumberOfItemsBeforeEnablingSearch",
                    paramDescription = "当可选列表的元素数量 >= 等于该值时，开启搜索框"
                },
                new ParamValue
                {
                    returnType = "bool",
                    paramName = "IsUniqueList",
                    paramDescription =
                        "*** 警告 *** Odin 3.3.1.11 版本中显示有问题，不推荐使用，该参数要求 ValueDropdown 作用于列表上，用于确定该列表中的元素是唯一的"
                },
                new ParamValue
                {
                    returnType = "bool",
                    paramName = "DrawDropdownForListElements",
                    paramDescription = "当 ValueDropdown 作用于列表上时，是否让元素修改时以下拉选择框的样式修改，而不是仅仅在添加时可以从列表中选择，默认为 true"
                },
                new ParamValue
                {
                    returnType = "bool",
                    paramName = "DisableListAddButtonBehaviour",
                    paramDescription = "禁用列表添加时触发下拉选择框，默认为 false"
                },
                new ParamValue
                {
                    returnType = "bool",
                    paramName = "ExcludeExistingValuesInList",
                    paramDescription = "是否剔除当前存在的值，包括现在字段的值也会剔除"
                },
                new ParamValue
                {
                    returnType = "bool",
                    paramName = "ExpandAllMenuItems",
                    paramDescription = "如果返回列表是 Odin 特制的 ValueDropdownList<T>，则可以支持树状显示，如果为 true，则全部展开，默认为 false"
                },
                new ParamValue
                {
                    returnType = "bool",
                    paramName = "AppendNextDrawer",
                    paramDescription = "将下拉选择框做成一个按钮进行绘制，然后按正常方式绘制值，而不是代替原本的样式，默认为 false"
                },
                new ParamValue
                {
                    returnType = "bool",
                    paramName = "DisableGUIInAppendedDrawer",
                    paramDescription = "在 AppendNextDrawer == true 的情况下，让原本的值的绘制变成无法交互，避免两个方式都可以修改值，默认为 false"
                },
                new ParamValue
                {
                    returnType = "bool",
                    paramName = "DisableListRemoveButtonBehaviour",
                    paramDescription = "禁用列表删除时触发下拉选择框，默认为 false"
                },
                new ParamValue
                {
                    returnType = "bool",
                    paramName = "DoubleClickToConfirm",
                    paramDescription = "设置成双击鼠标才可以确认选择，默认为 false"
                },
                new ParamValue
                {
                    returnType = "bool",
                    paramName = "FlattenTreeView",
                    paramDescription = "如果返回列表是 Odin 特制的 ValueDropdownList<T>，则可以支持树状显示，如果为 true，则放弃树状显示，取消缩进"
                },
                new ParamValue
                {
                    returnType = "int",
                    paramName = "DropdownWidth",
                    paramDescription = "设置整个下拉选择框的宽度"
                },
                new ParamValue
                {
                    returnType = "int",
                    paramName = "DropdownHeight",
                    paramDescription = "设置整个下拉选择框的高度，而不是一个选项的高度"
                },
                new ParamValue
                {
                    returnType = "string",
                    paramName = "DropdownTitle",
                    paramDescription = "选择框的标题"
                },
                new ParamValue
                {
                    returnType = "bool",
                    paramName = "SortDropdownItems",
                    paramDescription = "为可选列表中的元素排序"
                },
                new ParamValue
                {
                    returnType = "bool",
                    paramName = "HideChildProperties",
                    paramDescription = "是否隐藏子属性，例如 Vector3 的类型"
                },
                new ParamValue
                {
                    returnType = "bool",
                    paramName = "CopyValues",
                    paramDescription = "value 下拉框选择的值应该是原始值的副本还是引用（在引用类型的情况下），默认为 true"
                },
                new ParamValue
                {
                    returnType = "bool",
                    paramName = "OnlyChangeValueOnConfirm",
                    paramDescription = "如果设置为 true，当完全确认下拉框中的选择时，实际属性值将只更改一次"
                }
            };

        public override List<ResolvedParam> GetResolvedParams() =>
            new List<ResolvedParam>
            {
                new ResolvedParam
                {
                    ParamName = "ValueGetter",
                    ReturnType = "T 泛型",
                    ResolverType = ResolverType.ValueResolver,
                    ParamValues = new List<ParamValue>
                    {
                        new ParamValue
                        {
                            returnType = typeof(InspectorProperty).FullName,
                            paramName = "$property",
                            paramDescription = DescriptionConfigs.InspectorPropertyDesc
                        },
                        new ParamValue
                        {
                            returnType = "T 泛型",
                            paramName = "$value",
                            paramDescription = DescriptionConfigs.ValueDesc
                        }
                    }
                }
            };

        protected override string GetOriginalCode() => ReadCodeWithoutNamespace(typeof(ValueDropdownExample));
    }
}
