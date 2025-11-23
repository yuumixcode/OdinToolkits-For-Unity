using Yuumix.OdinToolkits.AttributeOverviewPro.Shared;
using YuumixEditor;

namespace Yuumix.OdinToolkits.AttributeOverviewPro.Editor
{
    internal class AssetsOnlyAttributeModel : AbstractAttributeModel
    {
        public override void Initialize()
        {
            HeaderWidget = new BilingualHeaderWidget("AssetsOnly", "AssetsOnly",
                "AssetsOnly 用于 UnityEngine.Object 类型，并将 Property 限制为项目 Asset，而不是场景对象。\n" +
                "当您想要确保对象来自项目而不是场景时，请使用此项。",
                "AssetsOnly is used on object properties, and restricts the property to project assets, and not scene objects.\n" +
                "Use this when you want to ensure an object is from the project, and not from the scene.",
                OdinInspectorDocumentationLinks.ASSETS_ONLY_URL);
            ExamplePreviewItems = new[]
            {
                new AttributeExamplePreviewItem().InitializeUnitySerializedExample("AssetsOnly Example",
                    AssetsOnlyExampleSO.Instance)
            };
        }
    }
}
