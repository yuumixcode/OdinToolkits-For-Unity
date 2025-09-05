using System;
using UnityEngine;
using Yuumix.OdinToolkits.Core.Runtime;

namespace Yuumix.OdinToolkits.Samples.Events
{
    public class AAA : EventArgs { }

    public struct EventArgsBindingNoArgsTest : IEventArgs { }

    public class EventBindingListener : MonoBehaviour
    {
        public event EventHandler OnDefaultEvent;
        public event EventHandler<EventArgsBindingNoArgsTest> OnNoArgsTestEvent2;
        public EventBusPublisher publisher;

        readonly EventBinding<EventArgsBindingNoArgsTest> _eventBindingNoArgs =
            new EventBinding<EventArgsBindingNoArgsTest>(OnNoArgsTestEvent);

        void Start()
        {
            publisher.NoArgsTestBetterEvent.Register(_eventBindingNoArgs);
            publisher.TestBetterEvent.Register(Publisher_TestBetterEvent);
            OnDefaultEvent += OnOnDefaultEvent;
        }

        void Publisher_TestBetterEvent(object sender)
        {
            Debug.Log(sender + "发布了 " + nameof(EventBusPublisher.TestBetterEvent) + " 事件");
        }

        void OnOnDefaultEvent(object sender, EventArgs e) { }

        static void OnNoArgsTestEvent()
        {
            Debug.Log("触发 " + nameof(EventArgsBindingNoArgsTest) + " 事件");
        }

        protected virtual void OnOnDefaultEvent()
        {
            OnDefaultEvent?.Invoke(this, EventArgs.Empty);
        }
    }
}
