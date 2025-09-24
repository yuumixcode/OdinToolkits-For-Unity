using System;

namespace Yuumix.OdinToolkits.Modules.ScriptDocGen.Editor.Test.TestMethod
{
    /// <summary>
    /// 剔除一些特殊方法，比如属性的 get 和 set，事件的 add 和 remove
    /// </summary>
    public class TestMethodG
    {
        public int A { get; set; }
        public event Action Event;

        public void OnEvent()
        {
            Event?.Invoke();
        }
    }
}
