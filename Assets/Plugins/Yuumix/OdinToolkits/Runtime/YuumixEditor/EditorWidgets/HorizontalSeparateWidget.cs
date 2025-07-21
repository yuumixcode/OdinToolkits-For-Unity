#if UNITY_EDITOR
using System;
using Sirenix.OdinInspector;
using Sirenix.Utilities.Editor;

namespace YuumixEditor
{
    [Serializable]
    [HideLabel]
    [InlineProperty]
    public class HorizontalSeparateWidget
    {
        float _height;
        float _topPadding;
        float _bottomPadding;

        [OnInspectorGUI]
        public void Separate()
        {
            SirenixEditorGUI.DrawThickHorizontalSeperator(_height, _topPadding, _bottomPadding);
        }

        public HorizontalSeparateWidget()
        {
            _height = 4;
            _topPadding = 10;
            _bottomPadding = 5;
        }

        public HorizontalSeparateWidget(float height, float topPadding, float bottomPadding)
        {
            _height = height;
            _topPadding = topPadding;
            _bottomPadding = bottomPadding;
        }
    }
}
#endif
