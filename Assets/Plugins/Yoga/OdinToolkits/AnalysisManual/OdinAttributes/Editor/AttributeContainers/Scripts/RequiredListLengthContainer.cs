using System.Collections.Generic;

namespace YOGA.OdinToolkits.AnalysisManual.OdinAttributes.Editor
{
    public class RequiredListLengthContainer : AbsContainer
    {
        protected override string SetHeader() => "RequiredListLength";

        protected override string SetBrief() => "限制 List 元素的个数";

        protected override List<string> SetTip() => new List<string>()
            { };

        protected override List<ParamValue> SetParamValues() => new List<ParamValue>()
        {
            new ParamValue()
            {
                returnType = "int",
                paramName = "fixedLength",
                paramDescription = "设置固定长度"
            },
            new ParamValue()
            {
                returnType = "int",
                paramName = "minLength",
                paramDescription = "设置最小长度，默认为 0"
            },
            new ParamValue()
            {
                returnType = "int",
                paramName = "maxLength",
                paramDescription = "设置最大长度，默认为 int.MaxValue"
            },
            new ParamValue()
            {
                returnType = "string",
                paramName = "minLengthGetter",
                paramDescription = "设置最小长度，默认为 0，" + DescriptionConfigs.SupportAllResolver
            },
            new ParamValue()
            {
                returnType = "string",
                paramName = "maxLengthGetter",
                paramDescription = "设置最大长度，默认为 int.MaxValue，" + DescriptionConfigs.SupportAllResolver
            },
            new ParamValue()
            {
                returnType = "PrefabKind 枚举",
                paramName = "prefabKind",
                paramDescription =
                    "Prefab 当前的类型，可以同时为多种类型，用 | 分隔，如: PrefabKind.InstanceInScene | PrefabKind.InstanceInPrefab"
            },
            new ParamValue()
            {
                returnType = ">>> PrefabKind",
                paramName = "PrefabKind.None",
                paramDescription = "无意义，枚举占位符"
            },
            new ParamValue()
            {
                returnType = ">>> PrefabKind",
                paramName = "PrefabKind.InstanceInScene",
                paramDescription = "表示当前脚本挂载的物体是 Prefab，并且是场景中的实例时生效，判断标记的 Property 是否为空"
            },
            new ParamValue()
            {
                returnType = ">>> PrefabKind",
                paramName = "PrefabKind.InstanceInPrefab",
                paramDescription = "表示当前脚本挂载的物体是 Prefab，并且是嵌套在其他预制体中的物体时生效，判断标记的 Property 是否为空"
            },
            new ParamValue()
            {
                returnType = ">>> PrefabKind",
                paramName = "PrefabKind.Regular",
                paramDescription = "表示当前脚本挂载的物体是 Regular Prefab 时生效，判断标记的 Property 是否为空"
            },
            new ParamValue()
            {
                returnType = ">>> PrefabKind",
                paramName = "PrefabKind.Variant",
                paramDescription = "表示当前脚本挂载的物体是 Prefab Variant (变体) 时生效，判断标记的 Property 是否为空"
            },
            new ParamValue()
            {
                returnType = ">>> PrefabKind",
                paramName = "PrefabKind.NonPrefabInstance",
                paramDescription = "表示当前脚本挂载的物体是场景中的非 Prefab 实例时生效，判断标记的 Property 是否为空"
            },
            new ParamValue()
            {
                returnType = ">>> PrefabKind",
                paramName = "PrefabKind.PrefabInstance",
                paramDescription = "PrefabInstance = InstanceInPrefab | InstanceInScene"
            },
            new ParamValue()
            {
                returnType = ">>> PrefabKind",
                paramName = "PrefabKind.PrefabAsset",
                paramDescription = "PrefabAsset = Variant | Regular"
            },
            new ParamValue()
            {
                returnType = ">>> PrefabKind",
                paramName = "PrefabKind.PrefabInstanceAndNonPrefabInstance",
                paramDescription =
                    "PrefabInstanceAndNonPrefabInstance = InstanceInPrefab | InstanceInScene | NonPrefabInstance"
            },
            new ParamValue()
            {
                returnType = ">>> PrefabKind",
                paramName = "PrefabKind.All",
                paramDescription =
                    "All = PrefabInstanceAndNonPrefabInstance | PrefabAsset"
            }
        };

        protected override string SetOriginalCode() => ReadCodeWithoutNamespace(typeof(RequiredListLengthExample));
    }
}