using System.Collections.Generic;
using Yuumix.OdinToolkits.AttributeOverviewPro.Shared;

namespace Yuumix.OdinToolkits.AttributeOverviewPro.Deprecated.Editor
{
    public class AssetSelectorContainer : OdinAttributeContainerSO
    {
        protected override string GetHeader() => "AssetSelector";

        protected override string GetIntroduction() => "可以作用于单个字段或者列表，在字段选择框旁边增加一个小按钮，可以弹出一个下拉选择框";

        protected override List<string> GetTips() => new List<string>();

        protected override List<ParameterValue> GetParamValues() =>
            new List<ParameterValue>
            {
                new ParameterValue
                {
                    ReturnType = "bool",
                    ParameterName = "IsUniqueList",
                    ParameterDescription = "如果为 true，则列表中不允许有重复的元素，默认为 true，此时下拉选择框是正常的，而 ValueDropdown 的此参数显示有问题"
                },
                new ParameterValue
                {
                    ReturnType = "bool",
                    ParameterName = "DrawDropdownForListElements",
                    ParameterDescription = "如果为 true，为列表中的每个元素增加一个 AssetSelector 功能，默认为 true"
                },
                new ParameterValue
                {
                    ReturnType = "bool",
                    ParameterName = "DisableListAddButtonBehaviour",
                    ParameterDescription = "如果为 true，则列表添加按钮的行为被禁用，点击添加按钮不会有 AssetSelector 的下拉选择框，默认为 false"
                },
                new ParameterValue
                {
                    ReturnType = "bool",
                    ParameterName = "ExcludeExistingValuesInList",
                    ParameterDescription = "当 IsUniqueList 为 true，ExcludeExistingValuesInList 也为 true，则列表中不允许有重复的元素，" +
                                           "更换显示的样式，直接剔除已经存在的，不会出现勾选框"
                },
                new ParameterValue
                {
                    ReturnType = "bool",
                    ParameterName = "ExpandAllMenuItems",
                    ParameterDescription = "展开所有可选项，默认为 true"
                },
                new ParameterValue
                {
                    ReturnType = "bool",
                    ParameterName = "FlattenTreeView",
                    ParameterDescription = "默认情况下，下拉选择框具有树结构，如果为 true，将舍弃树结构的绘制"
                },
                new ParameterValue
                {
                    ReturnType = "int",
                    ParameterName = "DropdownWidth",
                    ParameterDescription = "下拉选择框的宽度"
                },
                new ParameterValue
                {
                    ReturnType = "int",
                    ParameterName = "DropdownHeight",
                    ParameterDescription = "整个下拉选择框的高度"
                },
                new ParameterValue
                {
                    ReturnType = "string",
                    ParameterName = "DropdownTitle",
                    ParameterDescription = "下拉选择框的标题"
                },
                new ParameterValue
                {
                    ReturnType = "string[]",
                    ParameterName = "SearchInFolders",
                    ParameterDescription = "在特定的文件夹中选择，相对路径，Assets/ 开头，使用 AssetDatabase.FindAssets() 的参数"
                },
                new ParameterValue
                {
                    ReturnType = "string",
                    ParameterName = "Filter",
                    ParameterDescription = "使用 AssetDatabase.FindAssets() 的参数"
                },
                new ParameterValue
                {
                    ReturnType = "string",
                    ParameterName = "Paths",
                    ParameterDescription = "根据相对路径进行筛选，类似 SearchInFolders 参数，可以使用 | 符号分隔多条路径"
                }
            };

        protected override string GetOriginalCode() => ReadCodeWithoutNamespace(typeof(AssetSelectorExample));
    }
}
