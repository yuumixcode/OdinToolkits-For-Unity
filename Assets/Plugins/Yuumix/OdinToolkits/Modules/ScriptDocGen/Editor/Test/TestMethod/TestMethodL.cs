using System;
using UnityEngine;

namespace Yuumix.OdinToolkits.Modules.Editor
{
    /// <summary>
    /// 测试匿名方法
    /// </summary>
    public class TestMethodL
    {
        readonly Action _x = () => { Debug.Log("VAR"); };

        public void Method()
        {
            _x();
        }
    }
}
