using Sirenix.OdinInspector;
using System;
using UnityEngine;
using Yuumix.OdinToolkits.ScriptDocGenerator;

public class TestTypeData : SerializedMonoBehaviour
{
    public Type Type;
    public ITypeData TypeData;

    [Button("生成类型数据", ButtonSizes.Large)]
    public void CreateTypeData()
    {
        TypeData = new YuumixDefaultAnalysisDataFactory().CreateTypeData(Type);
    }
}
