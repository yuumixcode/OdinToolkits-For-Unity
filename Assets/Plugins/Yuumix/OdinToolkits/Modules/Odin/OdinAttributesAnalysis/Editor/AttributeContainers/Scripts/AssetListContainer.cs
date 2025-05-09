using Plugins.YOGA.OdinToolkits.Modules.OdinAttributesAnalysis.Common.Editor;
using Plugins.YOGA.OdinToolkits.Modules.OdinAttributesAnalysis.Editor.AttributePreviewExamples.Scripts;
using System.Collections.Generic;

namespace Plugins.YOGA.OdinToolkits.Modules.OdinAttributesAnalysis.Editor.AttributeContainers.Scripts
{
    public class AssetListContainer : AbsContainer
    {
        protected override string SetHeader()
        {
            return "AssetList";
        }

        protected override string SetBrief()
        {
            return "对继承自 UnityEngine.Object 的类型以及使用此类型的列表或数组使用，" +
                   "根据要求得出所有可能的结果，然后从此结果中选择需要的对象，不需要在 ProjectWindow 中选择";
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
                    paramName = "AutoPopulate",
                    paramDescription = "如果为 true，所有在资产列表中找到并显示的资产，将在检查时自动添加到列表中。"
                },
                new()
                {
                    returnType = "bool",
                    paramName = "Tags",
                    paramDescription = "根据 Asset 的 Tag 进行筛选"
                },
                new()
                {
                    returnType = "string",
                    paramName = "LayerNames",
                    paramDescription = "根据 Asset 的 LayerName 进行筛选"
                },
                new()
                {
                    returnType = "string",
                    paramName = "AssetNamePrefix",
                    paramDescription = "根据 Asset 的文件名前缀筛选"
                },
                new()
                {
                    returnType = "string",
                    paramName = "Path",
                    paramDescription = "根据相对路径进行筛选"
                },
                new()
                {
                    returnType = "string",
                    paramName = "CustomFilterMethod",
                    paramDescription = "自定义筛选方法，参数可选 T element,IList<T> value,InspectorProperty property"
                }
            };
        }

        protected override string SetOriginalCode()
        {
            return ReadCodeWithoutNamespace(typeof(AssetListExample));
        }
    }
}