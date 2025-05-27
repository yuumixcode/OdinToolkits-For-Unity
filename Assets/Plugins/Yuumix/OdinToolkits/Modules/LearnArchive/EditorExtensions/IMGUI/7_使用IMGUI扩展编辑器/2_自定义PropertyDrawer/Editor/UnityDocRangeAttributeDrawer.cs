using UnityEditor;
using UnityEngine;

namespace Yuumix.OdinToolkits.Modules.LearnArchive.EditorExtensions.IMGUI._7_使用IMGUI扩展编辑器._2_自定义PropertyDrawer.Editor
{
    [CustomPropertyDrawer(typeof(UnityDocRangeAttribute))]
    public class UnityDocRangeAttributeDrawer : PropertyDrawer
    {
        public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
        {
            var attr = (UnityDocRangeAttribute)attribute;

            if (property.propertyType == SerializedPropertyType.Float)
            {
                property.floatValue = EditorGUI.Slider(position, label, property.floatValue, attr.Min, attr.Max);
            }
            else if (property.propertyType == SerializedPropertyType.Integer)
            {
                property.intValue =
                    EditorGUI.IntSlider(position, label, property.intValue, (int)attr.Min, (int)attr.Max);
            }
            else
            {
                EditorGUI.LabelField(position, label.text, "UnityDocRange only works on float and int.");
            }
        }
    }
}