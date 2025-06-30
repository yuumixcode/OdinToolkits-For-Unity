using UnityEngine;

namespace Yuumix.OdinToolkits.Modules.LearnArchive.EditorExtensions.IMGUI._7_使用IMGUI扩展编辑器._2_自定义PropertyDrawer
{
    public class UnityDocRangeAttribute : PropertyAttribute
    {
        public readonly float Min;
        public readonly float Max;

        public UnityDocRangeAttribute(float min, float max)
        {
            Min = min;
            Max = max;
        }
    }
}