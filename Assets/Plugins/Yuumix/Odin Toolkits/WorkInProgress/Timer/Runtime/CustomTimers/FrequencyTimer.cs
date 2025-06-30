using System;
using UnityEngine;

namespace Yuumix.OdinToolkits.Modules.Timer.Runtime.CustomTimers
{
    /// <summary>
    /// Timer that ticks at a specific frequency. (N times per second)
    /// </summary>
    public class FrequencyTimer : AbsTimer
    {
        public int TicksPerSecond { get; private set; }

        public Action OnTick = delegate { };

        float _timeThreshold;

        public FrequencyTimer(int ticksPerSecond) : base(0)
        {
            CalculateTimeThreshold(ticksPerSecond);
        }

        public override void Tick()
        {
            if (IsRunning && CurrentTime >= _timeThreshold)
            {
                CurrentTime -= _timeThreshold;
                OnTick.Invoke();
            }

            if (IsRunning && CurrentTime < _timeThreshold)
            {
                CurrentTime += Time.deltaTime;
            }
        }

        public override bool IsFinished => !IsRunning;

        public override void Reset()
        {
            CurrentTime = 0;
        }

        public void Reset(int newTicksPerSecond)
        {
            CalculateTimeThreshold(newTicksPerSecond);
            Reset();
        }

        void CalculateTimeThreshold(int ticksPerSecond)
        {
            TicksPerSecond = ticksPerSecond;
            _timeThreshold = 1f / TicksPerSecond;
        }
    }
}