using UnityEngine;

namespace Yuumix.OdinToolkits.Modules.Timer.Runtime.CustomTimers
{
    public class CountdownTimer : AbsTimer
    {
        public CountdownTimer(float initialTime) : base(initialTime) { }

        public override void Tick()
        {
            if (IsRunning && CurrentTime > 0)
            {
                CurrentTime -= Time.deltaTime;
            }

            if (IsRunning && CurrentTime <= 0)
            {
                Stop();
            }
        }

        public override bool IsFinished => CurrentTime <= 0;
    }
}