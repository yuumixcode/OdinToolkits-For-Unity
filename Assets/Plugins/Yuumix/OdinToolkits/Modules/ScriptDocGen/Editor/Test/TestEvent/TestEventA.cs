using System;

namespace Yuumix.OdinToolkits.Modules.Editor
{
    public interface ITestInterfaceC
    {
        delegate void InterfaceEvent();

        event InterfaceEvent InterfaceEventA;
    }

    /// <summary>
    /// 测试解析事件
    /// </summary>
    public class TestEventA : ITestInterfaceC
    {
        // 显式接口实现的事件（访问器默认为 private）
        event ITestInterfaceC.InterfaceEvent ITestInterfaceC.InterfaceEventA
        {
            add { }
            remove { }
        }

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
