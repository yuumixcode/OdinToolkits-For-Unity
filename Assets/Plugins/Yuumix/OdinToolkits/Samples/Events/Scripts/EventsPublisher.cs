using Sirenix.OdinInspector;
using UnityEngine;
using Yuumix.OdinToolkits.Core;

namespace Yuumix.OdinToolkits.Samples.Events
{
    public struct TestBetterEvent : IEventArgs
    {
        public int Value;
    }

    public class EventsPublisher : SerializedMonoBehaviour
    {
        [ShowInInspector] EventsDebugger EventsDebugger => EventsDebugger.Instance;

        public readonly LiteEvent TestLiteEvent = new LiteEvent();

        public BetterEvent<TestBetterEvent> testBetterEventWithArgs = new BetterEvent<TestBetterEvent>();

        [Button("发布 LiteEvent 事件", ButtonSizes.Medium)]
        void PublishLiteEvent()
        {
            TestLiteEvent.Publish();
            Debug.Log("发布 " + nameof(TestLiteEvent) + "事件！");
        }

        [Button("发布 BetterEvent 事件", ButtonSizes.Medium)]
        void PublishBetterEvent()
        {
            testBetterEventWithArgs.Publish(new TestBetterEvent() { Value = 1 });
            Debug.Log("发布 " + nameof(testBetterEventWithArgs) + "事件！");
        }
    }
}
