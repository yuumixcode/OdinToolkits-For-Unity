using System.Collections.Generic;
using Yuumix.OdinToolkits.AttributeOverviewPro.Shared;

namespace Yuumix.OdinToolkits.AttributeOverviewPro.Deprecated.Editor
{
    public class RequiredListLengthContainer : OdinAttributeContainerSO
    {
        protected override string GetHeader() => "RequiredListLength";

        protected override string GetIntroduction() => "限制 List 元素的个数";

        protected override List<string> GetTips() => new List<string>();

        protected override List<ParameterValue> GetParamValues() =>
            new List<ParameterValue>
            {
                new ParameterValue
                {
                    ReturnType = "int",
                    ParameterName = "fixedLength",
                    ParameterDescription = "设置固定长度"
                },
                new ParameterValue
                {
                    ReturnType = "int",
                    ParameterName = "minLength",
                    ParameterDescription = "设置最小长度，默认为 0"
                },
                new ParameterValue
                {
                    ReturnType = "int",
                    ParameterName = "maxLength",
                    ParameterDescription = "设置最大长度，默认为 int.MaxValue"
                },
                new ParameterValue
                {
                    ReturnType = "string",
                    ParameterName = "minLengthGetter",
                    ParameterDescription = "设置最小长度，默认为 0，" + DescriptionConfigs.SupportAllResolver
                },
                new ParameterValue
                {
                    ReturnType = "string",
                    ParameterName = "maxLengthGetter",
                    ParameterDescription = "设置最大长度，默认为 int.MaxValue，" + DescriptionConfigs.SupportAllResolver
                },
                new ParameterValue
                {
                    ReturnType = "PrefabKind 枚举",
                    ParameterName = "prefabKind",
                    ParameterDescription =
                        "Prefab 当前的类型，可以同时为多种类型，用 | 分隔，如: PrefabKind.InstanceInScene | PrefabKind.InstanceInPrefab"
                },
                new ParameterValue
                {
                    ReturnType = ">>> PrefabKind",
                    ParameterName = "PrefabKind.None",
                    ParameterDescription = "无意义，枚举占位符"
                },
                new ParameterValue
                {
                    ReturnType = ">>> PrefabKind",
                    ParameterName = "PrefabKind.InstanceInScene",
                    ParameterDescription = "表示当前脚本挂载的物体是 Prefab，并且是场景中的实例时生效，判断标记的 Property 是否为空"
                },
                new ParameterValue
                {
                    ReturnType = ">>> PrefabKind",
                    ParameterName = "PrefabKind.InstanceInPrefab",
                    ParameterDescription = "表示当前脚本挂载的物体是 Prefab，并且是嵌套在其他预制体中的物体时生效，判断标记的 Property 是否为空"
                },
                new ParameterValue
                {
                    ReturnType = ">>> PrefabKind",
                    ParameterName = "PrefabKind.Regular",
                    ParameterDescription = "表示当前脚本挂载的物体是 Regular Prefab 时生效，判断标记的 Property 是否为空"
                },
                new ParameterValue
                {
                    ReturnType = ">>> PrefabKind",
                    ParameterName = "PrefabKind.Variant",
                    ParameterDescription = "表示当前脚本挂载的物体是 Prefab Variant (变体) 时生效，判断标记的 Property 是否为空"
                },
                new ParameterValue
                {
                    ReturnType = ">>> PrefabKind",
                    ParameterName = "PrefabKind.NonPrefabInstance",
                    ParameterDescription = "表示当前脚本挂载的物体是场景中的非 Prefab 实例时生效，判断标记的 Property 是否为空"
                },
                new ParameterValue
                {
                    ReturnType = ">>> PrefabKind",
                    ParameterName = "PrefabKind.PrefabInstance",
                    ParameterDescription = "PrefabInstance = InstanceInPrefab | InstanceInScene"
                },
                new ParameterValue
                {
                    ReturnType = ">>> PrefabKind",
                    ParameterName = "PrefabKind.PrefabAsset",
                    ParameterDescription = "PrefabAsset = Variant | Regular"
                },
                new ParameterValue
                {
                    ReturnType = ">>> PrefabKind",
                    ParameterName = "PrefabKind.PrefabInstanceAndNonPrefabInstance",
                    ParameterDescription =
                        "PrefabInstanceAndNonPrefabInstance = InstanceInPrefab | InstanceInScene | NonPrefabInstance"
                },
                new ParameterValue
                {
                    ReturnType = ">>> PrefabKind",
                    ParameterName = "PrefabKind.All",
                    ParameterDescription =
                        "All = PrefabInstanceAndNonPrefabInstance | PrefabAsset"
                }
            };

        protected override string GetOriginalCode() => ReadCodeWithoutNamespace(typeof(RequiredListLengthExample));
    }
}
