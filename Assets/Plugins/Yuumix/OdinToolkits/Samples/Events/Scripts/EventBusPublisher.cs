using Sirenix.OdinInspector;
using UnityEngine;
using Yuumix.OdinToolkits.Samples.Events;

namespace Yuumix.OdinToolkits.Core.Runtime
{
    public class EventBusPublisher : SerializedMonoBehaviour
    {
        [ShowInInspector] EventCenter EventCenter => EventCenter.Instance;

        public BetterEvent<EventArgsBindingNoArgsTest> NoArgsTestBetterEvent =
            new BetterEvent<EventArgsBindingNoArgsTest>();

        [Button("发布无参事件")]
        public void PublishNoArgsTestEvent()
        {
            NoArgsTestBetterEvent.Publish(this, new EventArgsBindingNoArgsTest());
        }

        public BetterEvent TestBetterEvent = new BetterEvent();

        [Button("发布 BetterEvent 无参事件")]
        public void PublishNoArgsBetterEvent()
        {
            TestBetterEvent.Publish(this);
        }
    }
}
