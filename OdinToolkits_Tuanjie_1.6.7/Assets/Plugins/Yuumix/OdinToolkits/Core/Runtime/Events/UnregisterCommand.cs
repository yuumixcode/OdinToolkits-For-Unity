using System;

namespace Yuumix.OdinToolkits.Core
{
    public readonly struct UnregisterCommand : IAutoUnregister
    {
        readonly Action _unregister;

        public UnregisterCommand(Action unregister) => _unregister = unregister;

        public void Unregister()
        {
            _unregister?.Invoke();
        }
    }
}
