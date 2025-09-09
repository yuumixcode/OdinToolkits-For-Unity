using UnityEngine;

namespace Yuumix.OdinToolkits.Core.Runtime
{
    public interface IAutoUnregister
    {
        void Unregister();
    }

    public static class AutoUnregisterExtensions
    {
        public static void UnregisterOnDisable(this IAutoUnregister autoUnregister, MonoBehaviour mono)
        {
            if (mono.TryGetComponent(out UnregisterTriggerOnDisable trigger))
            {
                trigger.Register(autoUnregister);
            }
            else
            {
                trigger = mono.gameObject.AddComponent<UnregisterTriggerOnDisable>();
                trigger.Register(autoUnregister);
            }
        }

        public static void UnregisterOnDestroy(this IAutoUnregister autoUnregister, MonoBehaviour mono)
        {
            if (mono.TryGetComponent(out UnregisterTriggerOnDestroy trigger))
            {
                trigger.Register(autoUnregister);
            }
            else
            {
                trigger = mono.gameObject.AddComponent<UnregisterTriggerOnDestroy>();
                trigger.Register(autoUnregister);
            }
        }
    }
}
