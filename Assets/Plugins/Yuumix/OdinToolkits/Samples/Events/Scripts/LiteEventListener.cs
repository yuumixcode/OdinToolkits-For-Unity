using System;
using Sirenix.OdinInspector;
using UnityEngine;
using Yuumix.OdinToolkits.Core;

namespace Yuumix.OdinToolkits.Samples.Events
{
    public class LiteEventListener : MonoBehaviour
    {
        public EventsPublisher publisher;

        void OnEnable()
        {
            publisher.TestLiteEvent.Register(OnTestLiteEventOnDisable).UnregisterOnDisable(this);
        }

        void Start()
        {
            publisher.TestLiteEvent.Register(OnTestLiteEventOnDestroy).UnregisterOnDestroy(this);
        }

        void OnTestLiteEventOnDestroy()
        {
            Debug.Log(nameof(LiteEventListener) + " 执行 " + nameof(OnTestLiteEventOnDestroy) + "方法");
        }

        void OnTestLiteEventOnDisable()
        {
            Debug.Log(nameof(LiteEventListener) + " 执行 " + nameof(OnTestLiteEventOnDisable) + "方法");
        }

        [Button("销毁自身", ButtonSizes.Medium)]
        void DestroySelf()
        {
            Destroy(gameObject);
        }
    }
}
