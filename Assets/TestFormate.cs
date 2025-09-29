using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Yuumix.OdinToolkits.AdvancedTypeAnalyzer;

public class TestFormate : MonoBehaviour
{
    public MyEnum testEnum;

    void Start()
    {
        var formate = FieldData.GetFormattedDefaultValue(typeof(MyEnum), testEnum);
        Debug.Log(formate);
    }

    public enum MyEnum
    {
        Value1,
        Value2,
        Value3
    }
}
