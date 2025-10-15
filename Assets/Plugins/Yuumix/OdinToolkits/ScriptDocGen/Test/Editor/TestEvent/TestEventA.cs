using System;

namespace Yuumix.OdinToolkits.ScriptDocGen.Editor.Test.TestEvent
{
    public interface ITestInterfaceC
    {
        #region Delegates

        delegate void InterfaceEvent();

        #endregion

        event InterfaceEvent InterfaceEventA;
    }

    /// <summary>
    /// 测试解析事件
    /// </summary>
    public class TestEventA : ITestInterfaceC
    {
        #region ITestInterfaceC Members

        // 显式接口实现的事件（访问器默认为 private）
        event ITestInterfaceC.InterfaceEvent ITestInterfaceC.InterfaceEventA
        {
            add { }
            remove { }
        }

        #endregion

        public event Action PublicEvent;
        event Action PrivateEvent;

        protected virtual void OnPublicEvent()
        {
            PublicEvent?.Invoke();
        }

        protected virtual void OnPrivateEvent()
        {
            PrivateEvent?.Invoke();
        }
    }
}
