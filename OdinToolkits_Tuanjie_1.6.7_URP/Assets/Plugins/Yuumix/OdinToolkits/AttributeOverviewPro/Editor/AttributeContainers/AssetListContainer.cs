using System.Collections.Generic;
using Yuumix.OdinToolkits.AttributeOverviewPro.Editor;
using Yuumix.OdinToolkits.AttributeOverviewPro.Shared;

namespace Yuumix.OdinToolkits.Modules.Editor
{
    public class AssetListContainer : OdinAttributeContainerSO
    {
        protected override string GetHeader() => "AssetList";

        protected override string GetIntroduction() =>
            "对继承自 UnityEngine.Object 的类型以及使用此类型的列表或数组使用，" +
            "根据要求得出所有可能的结果，然后从此结果中选择需要的对象，不需要在 ProjectWindow 中选择";

        protected override List<string> GetTips() => new List<string>();

        protected override List<ParameterValue> GetParamValues() =>
            new List<ParameterValue>
            {
                new ParameterValue
                {
                    ReturnType = "bool",
                    ParameterName = "AutoPopulate",
                    ParameterDescription = "如果为 true，所有在资产列表中找到并显示的资产，将在检查时自动添加到列表中。"
                },
                new ParameterValue
                {
                    ReturnType = "bool",
                    ParameterName = "Tags",
                    ParameterDescription = "根据 Asset 的 Tag 进行筛选"
                },
                new ParameterValue
                {
                    ReturnType = "string",
                    ParameterName = "LayerNames",
                    ParameterDescription = "根据 Asset 的 LayerName 进行筛选"
                },
                new ParameterValue
                {
                    ReturnType = "string",
                    ParameterName = "AssetNamePrefix",
                    ParameterDescription = "根据 Asset 的文件名前缀筛选"
                },
                new ParameterValue
                {
                    ReturnType = "string",
                    ParameterName = "Path",
                    ParameterDescription = "根据相对路径进行筛选"
                },
                new ParameterValue
                {
                    ReturnType = "string",
                    ParameterName = "CustomFilterMethod",
                    ParameterDescription = "自定义筛选方法，参数可选 T element,IList<T> value,InspectorProperty property"
                }
            };

        protected override string GetOriginalCode() => ReadCodeWithoutNamespace(typeof(AssetListExample));
    }
}
