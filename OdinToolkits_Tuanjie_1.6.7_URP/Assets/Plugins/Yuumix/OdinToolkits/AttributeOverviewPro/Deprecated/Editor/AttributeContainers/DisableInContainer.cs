using System.Collections.Generic;
using Yuumix.OdinToolkits.AttributeOverviewPro.Shared;

namespace Yuumix.OdinToolkits.AttributeOverviewPro.Deprecated.Editor
{
    public class DisableInContainer : OdinAttributeContainerSO
    {
        protected override string GetHeader() => "DisableIn";

        protected override string GetIntroduction() => "针对 Prefab 物体对象中的 Property 的 DisableIf 的特殊版本";

        protected override List<string> GetTips() =>
            new List<string>
            {
                "使用了 DisableIn 特性的 Property 所在的脚本，需要是和 Prefab 有关的，可以是预制体物体或者子物体。",
                "当脚本所在的预制体，是某一种特定类型(PrefabKind)时，被标记的 Property 将失去焦点，无法选中"
            };

        protected override List<ParameterValue> GetParamValues() =>
            new List<ParameterValue>
            {
                new ParameterValue
                {
                    ReturnType = "PrefabKind",
                    ParameterName = "kind",
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
                    ParameterDescription = "表示当前脚本挂载的物体是 Prefab，并且是场景中的实例时生效"
                },
                new ParameterValue
                {
                    ReturnType = ">>> PrefabKind",
                    ParameterName = "PrefabKind.InstanceInPrefab",
                    ParameterDescription = "表示当前脚本挂载的物体是 Prefab，并且是嵌套在其他预制体中的物体时生效"
                },
                new ParameterValue
                {
                    ReturnType = ">>> PrefabKind",
                    ParameterName = "PrefabKind.Regular",
                    ParameterDescription = "表示当前脚本挂载的物体是 Regular Prefab 时生效"
                },
                new ParameterValue
                {
                    ReturnType = ">>> PrefabKind",
                    ParameterName = "PrefabKind.Variant",
                    ParameterDescription = "表示当前脚本挂载的物体是 Prefab Variant (变体) 时生效"
                },
                new ParameterValue
                {
                    ReturnType = ">>> PrefabKind",
                    ParameterName = "PrefabKind.NonPrefabInstance",
                    ParameterDescription = "表示当前脚本挂载的物体是场景中的非 Prefab 实例时生效"
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
                    ParameterDescription = "All = PrefabInstanceAndNonPrefabInstance | PrefabAsset"
                }
            };

        protected override string GetOriginalCode() => ReadCodeWithoutNamespace(typeof(DisableInExample));
    }
}
