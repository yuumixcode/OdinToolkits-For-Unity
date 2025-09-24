using System;
using UnityEngine;

namespace Yuumix.OdinToolkits.Modules.ScriptDocGen.Editor.Test.TestChineseSummaryAttribute
{
    /// <summary>
    /// 测试成员的 Summary 注释 “”
    /// </summary>
    public class TestMemberSummary : MonoBehaviour
    {
        /// <summary>
        /// 成员 “” Summary 注释 ????
        /// &lt;para&gt;aaa&lt;/para&gt;
        /// <para>aaa</para>
        /// </summary>
        /// <param name="filePath">以 Assets 开头的相对路径即可</param>
        [Obsolete("临时方法")]
        public static void MethodA(string filePath)
        {
            // 方法体
            Debug.Log("测试成员Summary注释");
        }
    }
}
