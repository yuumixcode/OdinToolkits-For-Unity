using System;
using UnityEngine;

namespace Yuumix.OdinToolkits.Modules.Timer.Runtime
{
    public abstract class AbsTimer : IDisposable
    {
        public float CurrentTime { get; protected set; }
        public bool IsRunning { get; private set; }

        float _initialTime;

        protected AbsTimer(float initialTime)
        {
            _initialTime = initialTime;
        }

        public float ProgressValue => Mathf.Clamp01(CurrentTime / _initialTime);
        public Action OnTimerStart = delegate { };
        public Action OnTimerEnd = delegate { };

        public void Start()
        {
            CurrentTime = _initialTime;
            if (!IsRunning)
            {
                IsRunning = true;
                TimerManager.RegisterTimer(this);
                OnTimerStart.Invoke();
            }
        }

        public void Stop()
        {
            if (IsRunning)
            {
                IsRunning = false;
                TimerManager.DeRegisterTimer(this);
                OnTimerEnd.Invoke();
            }
        }

        public abstract void Tick();
        public abstract bool IsFinished { get; }

        public void Resume() => IsRunning = true;
        public void Pause() => IsRunning = false;

        public virtual void Reset() => CurrentTime = _initialTime;

        public virtual void Reset(float newTime)
        {
            _initialTime = newTime;
            Reset();
        }

        bool _disposed;

        ~AbsTimer()
        {
            Dispose(false);
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (_disposed)
            {
                return;
            }

            if (disposing)
            {
                TimerManager.DeRegisterTimer(this);
            }

            _disposed = true;
        }
    }
}