using System.Collections.Generic;
using Yuumix.OdinToolkits.AttributeOverviewPro.Editor;
using Yuumix.OdinToolkits.AttributeOverviewPro.Shared;
using YuumixEditor;

namespace Yuumix.OdinToolkits.Modules.Editor
{
    public class AssetsOnlyContainer : OdinAttributeContainerSO
    {
        protected override string GetHeader() => "AssetsOnly";

        protected override BilingualHeaderWidget GetHeaderWidget() => GlobalTempHeader.ModifyWidget(
            "AssetsOnly",
            "AssetsOnly",
            "AssetsOnly 用于 Object 对象，确保被标记的字段或者属性引用项目中的资源，而不是场景中的物体。",
            "AssetsOnly is used on object properties, and restricts the property to project assets, and not scene objects." +
            "\nUse this when you want to ensure an object is from the project, and not from the scene.");

        protected override string GetIntroduction() => "确保被标记的字段或者属性引用项目中的资源，而不是场景中的物体";

        protected override List<string> GetTips() =>
            new List<string>
            {
                "可以用于确保引用项目资源"
            };

        protected override List<ParameterValue> GetParamValues() => new List<ParameterValue>();

        protected override string GetOriginalCode() => ReadCodeWithoutNamespace(typeof(AssetsOnlyExample));
    }
}
