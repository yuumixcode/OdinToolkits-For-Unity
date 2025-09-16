using System;
using Sirenix.OdinInspector;
using UnityEngine;
using Yuumix.OdinToolkits.Core;

namespace Yuumix.OdinToolkits.Samples.Events
{
    public class TestBetterEventNoArgsListener : MonoBehaviour
    {
        public EventsPublisher publisher;

        public IEventBinding<TestBetterEvent> Binding;

        void Awake()
        {
            Binding = new EventBinding<TestBetterEvent>(OnBindingEventOnDestroy);
        }

        void Start()
        {
            publisher.testBetterEventWithArgs.Register(Binding).UnregisterOnDestroy(this);
        }

        void OnBindingEventOnDestroy(TestBetterEvent e)
        {
            Debug.Log("发布了 BetterEvent 事件。 " + nameof(TestBetterEventNoArgsListener) + " 执行 " +
                      nameof(OnBindingEventOnDestroy) + " 方法 ");
        }

        [Button("销毁自身", ButtonSizes.Medium)]
        void DestroySelf()
        {
            Destroy(gameObject);
        }
    }
}
