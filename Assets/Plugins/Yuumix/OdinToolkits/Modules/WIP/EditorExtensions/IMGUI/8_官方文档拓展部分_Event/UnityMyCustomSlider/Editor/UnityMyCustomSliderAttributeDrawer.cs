using UnityEditor;
using UnityEngine;

namespace Yuumix.OdinToolkits.Modules.LearnArchive.EditorExtensions.IMGUI._8_官方文档拓展部分_Event.UnityMyCustomSlider.Editor
{
    [CustomPropertyDrawer(typeof(UnityMyCustomSliderAttribute))]
    public class UnityMyCustomSliderAttributeDrawer : PropertyDrawer
    {
        public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
        {
            if (property.propertyType == SerializedPropertyType.Float)
            {
                property.floatValue = EditorGUI.FloatField(position, label, property.floatValue);

                // 自定义滑块控件的高度
                float customHeight = 50f;
                Rect customRect = GUILayoutUtility.GetRect(position.width, customHeight); // 获取当前行的宽度和自定义的高度
                EditorGUI.DrawRect(customRect, new Color(0, 0, 0, 0)); // 清除背景，以便自定义控件可以绘制在其上

                // 绘制自定义滑块控件
                property.floatValue = UnityCustomControls.MyCustomSlider(customRect, property.floatValue, new GUIStyle()
                {
                    normal = new GUIStyleState()
                    {
                        background = Texture2D.whiteTexture // 这里使用一个白色纹理作为背景
                    }
                });

                // 计算下一个LabelField的位置，使其紧随自定义控件之后
                Rect labelRect = GUILayoutUtility.GetRect(position.width, EditorGUIUtility.singleLineHeight);
                EditorGUI.LabelField(labelRect, label.text, "This is New Control Rect");
            }
            else
            {
                EditorGUI.LabelField(position, label.text, "UnityMyCustomSlider only works on float.");
            }
        }
    }
}