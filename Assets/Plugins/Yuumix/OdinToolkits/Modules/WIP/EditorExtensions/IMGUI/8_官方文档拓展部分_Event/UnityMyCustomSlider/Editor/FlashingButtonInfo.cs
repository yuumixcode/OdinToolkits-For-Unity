#if UNITY_EDITOR
using UnityEditor;
using UnityEngine;

namespace Yuumix.OdinToolkits.Modules.LearnArchive.EditorExtensions.IMGUI._8_官方文档拓展部分_Event.ControlState
{
    public class FlashingButtonInfo
    {
        double _mouseDownAt;
        bool _isFlashing = false;

        public void MouseDownNow()
        {
#if UNITY_EDITOR

            _mouseDownAt = EditorApplication.timeSinceStartup;
#endif
            _isFlashing = false;
        }

        public bool IsFlashing(int controlId)
        {
            if (GUIUtility.hotControl != controlId)
            {
                return false;
            }
#if UNITY_EDITOR
            var elapsedTime = EditorApplication.timeSinceStartup - _mouseDownAt;
#endif

            if (elapsedTime < 2f)
            {
                return false;
            }

            _isFlashing = (int)((elapsedTime - 2f) / 0.1f) % 2 == 0;
            return _isFlashing;
        }
    }
}
#endif
