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
        public event Action PublicEvent;
        event Action PrivateEvent;

        // 显式接口实现的事件（访问器默认为 private）
        event ITestInterfaceC.InterfaceEvent ITestInterfaceC.InterfaceEventA
        {
            add { }
            remove { }
        }

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
