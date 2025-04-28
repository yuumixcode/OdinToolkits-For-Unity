using System.Collections.Generic;

namespace YOGA.OdinToolkits.AnalysisManual.OdinAttributes.Editor
{
    public class ShowInContainer : AbsContainer
    {
        protected override string SetHeader()
        {
            return "ShowIn";
        }

        protected override string SetBrief()
        {
            return "针对 Prefab 物体对象中的 Property 的 ShowIf 的特殊版本";
        }

        protected override List<string> SetTip()
        {
            return new List<string>
            {
                "使用了 ShowIn 特性的 Property 所在的脚本，需要是和 Prefab 有关的，可以是预制体物体或者子物体。",
                "当脚本所在的预制体，是某一种特定类型(PrefabKind)时，被标记的 Property 将会显示"
            };
        }

        protected override List<ParamValue> SetParamValues()
        {
            return new List<ParamValue>
            {
                new()
                {
                    returnType = "PrefabKind",
                    paramName = "kind",
                    paramDescription =
                        "Prefab 当前的类型，可以同时为多种类型，用 | 分隔，如: PrefabKind.InstanceInScene | PrefabKind.InstanceInPrefab"
                },
                new()
                {
                    returnType = ">>> PrefabKind",
                    paramName = "PrefabKind.None",
                    paramDescription = "此时将不会显示，因为无法满足这个条件。"
                },
                new()
                {
                    returnType = ">>> PrefabKind",
                    paramName = "PrefabKind.InstanceInScene",
                    paramDescription = "表示当前脚本挂载的物体是 Prefab，并且是场景中的实例时生效"
                },
                new()
                {
                    returnType = ">>> PrefabKind",
                    paramName = "PrefabKind.InstanceInPrefab",
                    paramDescription = "表示当前脚本挂载的物体是 Prefab，并且是嵌套在其他预制体中的物体时生效"
                },
                new()
                {
                    returnType = ">>> PrefabKind",
                    paramName = "PrefabKind.Regular",
                    paramDescription = "表示当前脚本挂载的物体是 Regular Prefab 时生效"
                },
                new()
                {
                    returnType = ">>> PrefabKind",
                    paramName = "PrefabKind.Variant",
                    paramDescription = "表示当前脚本挂载的物体是 Prefab Variant (变体) 时生效"
                },
                new()
                {
                    returnType = ">>> PrefabKind",
                    paramName = "PrefabKind.NonPrefabInstance",
                    paramDescription = "表示当前脚本挂载的物体是场景中的非 Prefab 实例时生效"
                },
                new()
                {
                    returnType = ">>> PrefabKind",
                    paramName = "PrefabKind.PrefabInstance",
                    paramDescription = "PrefabInstance = InstanceInPrefab | InstanceInScene"
                },
                new()
                {
                    returnType = ">>> PrefabKind",
                    paramName = "PrefabKind.PrefabAsset",
                    paramDescription = "PrefabAsset = Variant | Regular"
                },
                new()
                {
                    returnType = ">>> PrefabKind",
                    paramName = "PrefabKind.PrefabInstanceAndNonPrefabInstance",
                    paramDescription =
                        "PrefabInstanceAndNonPrefabInstance = InstanceInPrefab | InstanceInScene | NonPrefabInstance"
                },
                new()
                {
                    returnType = ">>> PrefabKind",
                    paramName = "PrefabKind.All",
                    paramDescription =
                        "All = PrefabInstanceAndNonPrefabInstance | PrefabAsset"
                }
            };
        }

        protected override string SetOriginalCode()
        {
            return ReadCodeWithoutNamespace(typeof(ShowInExample));
        }
    }
}