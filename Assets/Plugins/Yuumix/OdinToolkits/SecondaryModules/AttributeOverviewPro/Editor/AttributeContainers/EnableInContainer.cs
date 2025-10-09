using System.Collections.Generic;

namespace Yuumix.OdinToolkits.Modules.Editor
{
    public class EnableInContainer : OdinAttributeContainerSO
    {
        protected override string GetHeader() => "EnableIn";

        protected override string GetIntroduction() => "针对 Prefab 物体对象中的 Property 的 EnableIf 的特殊版本";

        protected override List<string> GetTips() =>
            new List<string>
            {
                "使用了 EnableIn 特性的 Property 所在的脚本，需要是和 Prefab 有关的，可以是预制体物体或者子物体。",
                "当脚本所在的预制体，是某一种特定类型(PrefabKind)时，被标记的 Property 才可以获取焦点"
            };

        protected override List<ParamValue> GetParamValues() =>
            new List<ParamValue>
            {
                new ParamValue
                {
                    returnType = "PrefabKind",
                    paramName = "kind",
                    paramDescription =
                        "Prefab 当前的类型，可以同时为多种类型，用 | 分隔，如: PrefabKind.InstanceInScene | PrefabKind.InstanceInPrefab"
                },
                new ParamValue
                {
                    returnType = ">>> PrefabKind",
                    paramName = "PrefabKind.None",
                    paramDescription = "此时因为没有可以满足这个条件的情况，所以默认会无法获取焦点"
                },
                new ParamValue
                {
                    returnType = ">>> PrefabKind",
                    paramName = "PrefabKind.InstanceInScene",
                    paramDescription = "表示当前脚本挂载的物体是 Prefab，并且是场景中的实例时生效"
                },
                new ParamValue
                {
                    returnType = ">>> PrefabKind",
                    paramName = "PrefabKind.InstanceInPrefab",
                    paramDescription = "表示当前脚本挂载的物体是 Prefab，并且是嵌套在其他预制体中的物体时生效"
                },
                new ParamValue
                {
                    returnType = ">>> PrefabKind",
                    paramName = "PrefabKind.Regular",
                    paramDescription = "表示当前脚本挂载的物体是 Regular Prefab 时生效"
                },
                new ParamValue
                {
                    returnType = ">>> PrefabKind",
                    paramName = "PrefabKind.Variant",
                    paramDescription = "表示当前脚本挂载的物体是 Prefab Variant (变体) 时生效"
                },
                new ParamValue
                {
                    returnType = ">>> PrefabKind",
                    paramName = "PrefabKind.NonPrefabInstance",
                    paramDescription = "表示当前脚本挂载的物体是场景中的非 Prefab 实例时生效"
                },
                new ParamValue
                {
                    returnType = ">>> PrefabKind",
                    paramName = "PrefabKind.PrefabInstance",
                    paramDescription = "PrefabInstance = InstanceInPrefab | InstanceInScene"
                },
                new ParamValue
                {
                    returnType = ">>> PrefabKind",
                    paramName = "PrefabKind.PrefabAsset",
                    paramDescription = "PrefabAsset = Variant | Regular"
                },
                new ParamValue
                {
                    returnType = ">>> PrefabKind",
                    paramName = "PrefabKind.PrefabInstanceAndNonPrefabInstance",
                    paramDescription =
                        "PrefabInstanceAndNonPrefabInstance = InstanceInPrefab | InstanceInScene | NonPrefabInstance"
                },
                new ParamValue
                {
                    returnType = ">>> PrefabKind",
                    paramName = "PrefabKind.All",
                    paramDescription =
                        "All = PrefabInstanceAndNonPrefabInstance | PrefabAsset"
                }
            };

        protected override string GetOriginalCode() => ReadCodeWithoutNamespace(typeof(EnableInExample));
    }
}
