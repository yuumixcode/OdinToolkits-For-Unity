using UnityEngine;
using Yuumix.OdinToolkits.Modules.Timer.Runtime;
using Yuumix.OdinToolkits.Modules.Timer.Runtime.CustomTimers;

namespace Yuumix.OdinToolkits.Modules.Timer.Test
{
    public class TimerBasic : MonoBehaviour
    {
        [SerializeField] float duration = 5;
        [SerializeField] float duration2 = 10;
        CountdownTimer _timer, _timer2;

        void Start()
        {
            _timer = new CountdownTimer(duration);
            _timer.OnTimerStart += () => Debug.Log("Timer Started");
            _timer.OnTimerEnd += () => Debug.Log("Timer Ended");
            _timer.Start();

            _timer2 = new CountdownTimer(duration2);
            _timer2.OnTimerStart += () => Debug.Log("Timer2 Started");
            _timer2.OnTimerEnd += () => Debug.Log("Timer2 Ended");
            _timer2.Start();
        }

        void Update()
        {
            Debug.Log("Timer 进度条百分比: " + _timer.ProgressValue);
            TimerManager.UpdateTimer();
        }

        void OnDestroy()
        {
            _timer.Dispose();
            _timer2.Dispose();
        }
    }
}