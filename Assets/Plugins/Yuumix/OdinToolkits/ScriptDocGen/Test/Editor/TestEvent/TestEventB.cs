using System;

namespace Yuumix.OdinToolkits.ScriptDocGen.Editor.Test.TestEvent
{
    /// <summary>
    /// 测试静态事件
    /// </summary>
    public class TestEventB
    {
        public static event Action OnEvent;

        static void OnOnEvent()
        {
            OnEvent?.Invoke();
        }
    }
}
