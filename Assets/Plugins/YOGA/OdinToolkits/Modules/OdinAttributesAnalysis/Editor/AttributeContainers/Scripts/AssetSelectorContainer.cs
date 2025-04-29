using Plugins.YOGA.OdinToolkits.Modules.OdinAttributesAnalysis.Common.Editor;
using Plugins.YOGA.OdinToolkits.Modules.OdinAttributesAnalysis.Editor.AttributePreviewExamples.Scripts;
using System.Collections.Generic;

namespace Plugins.YOGA.OdinToolkits.Modules.OdinAttributesAnalysis.Editor.AttributeContainers.Scripts
{
    public class AssetSelectorContainer : AbsContainer
    {
        protected override string SetHeader()
        {
            return "AssetSelector";
        }

        protected override string SetBrief()
        {
            return "可以作用于单个字段或者列表，在字段选择框旁边增加一个小按钮，可以弹出一个下拉选择框";
        }

        protected override List<string> SetTip()
        {
            return new List<string>();
        }

        protected override List<ParamValue> SetParamValues()
        {
            return new List<ParamValue>
            {
                new()
                {
                    returnType = "bool",
                    paramName = "IsUniqueList",
                    paramDescription = "如果为 true，则列表中不允许有重复的元素，默认为 true，此时下拉选择框是正常的，而 ValueDropdown 的此参数显示有问题"
                },
                new()
                {
                    returnType = "bool",
                    paramName = "DrawDropdownForListElements",
                    paramDescription = "如果为 true，为列表中的每个元素增加一个 AssetSelector 功能，默认为 true"
                },
                new()
                {
                    returnType = "bool",
                    paramName = "DisableListAddButtonBehaviour",
                    paramDescription = "如果为 true，则列表添加按钮的行为被禁用，点击添加按钮不会有 AssetSelector 的下拉选择框，默认为 false"
                },
                new()
                {
                    returnType = "bool",
                    paramName = "ExcludeExistingValuesInList",
                    paramDescription = "当 IsUniqueList 为 true，ExcludeExistingValuesInList 也为 true，则列表中不允许有重复的元素，" +
                                       "更换显示的样式，直接剔除已经存在的，不会出现勾选框"
                },
                new()
                {
                    returnType = "bool",
                    paramName = "ExpandAllMenuItems",
                    paramDescription = "展开所有可选项，默认为 true"
                },
                new()
                {
                    returnType = "bool",
                    paramName = "FlattenTreeView",
                    paramDescription = "默认情况下，下拉选择框具有树结构，如果为 true，将舍弃树结构的绘制"
                },
                new()
                {
                    returnType = "int",
                    paramName = "DropdownWidth",
                    paramDescription = "下拉选择框的宽度"
                },
                new()
                {
                    returnType = "int",
                    paramName = "DropdownHeight",
                    paramDescription = "整个下拉选择框的高度"
                },
                new()
                {
                    returnType = "string",
                    paramName = "DropdownTitle",
                    paramDescription = "下拉选择框的标题"
                },
                new()
                {
                    returnType = "string[]",
                    paramName = "SearchInFolders",
                    paramDescription = "在特定的文件夹中选择，相对路径，Assets/ 开头，使用 AssetDatabase.FindAssets() 的参数"
                },
                new()
                {
                    returnType = "string",
                    paramName = "Filter",
                    paramDescription = "使用 AssetDatabase.FindAssets() 的参数"
                },
                new()
                {
                    returnType = "string",
                    paramName = "Paths",
                    paramDescription = "根据相对路径进行筛选，类似 SearchInFolders 参数，可以使用 | 符号分隔多条路径"
                }
            };
        }

        protected override string SetOriginalCode()
        {
            return ReadCodeWithoutNamespace(typeof(AssetSelectorExample));
        }
    }
}