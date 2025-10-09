using System;

namespace Yuumix.OdinToolkits.Modules.ScriptDocGen.Editor.Test.TestEvent
{
    /// <summary>
    /// 有参数的事件
    /// </summary>
    public class TestEventC
    {
        public static event Action<int> OnEvent;

        static void OnOnEvent(int obj)
        {
            OnEvent?.Invoke(obj);
        }
    }
}
