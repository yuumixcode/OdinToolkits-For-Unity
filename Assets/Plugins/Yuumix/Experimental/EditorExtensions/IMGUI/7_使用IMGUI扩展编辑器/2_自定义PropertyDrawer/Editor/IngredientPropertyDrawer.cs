using UnityEditor;
using UnityEngine;

namespace Yuumix.OdinToolkits.Modules.LearnArchive.EditorExtensions.IMGUI._7_使用IMGUI扩展编辑器._2_自定义PropertyDrawer.Editor
{
    [CustomPropertyDrawer(typeof(Ingredient))]
    public class IngredientPropertyDrawer : PropertyDrawer
    {
        public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
        {
            // Using BeginProperty / EndProperty on the parent property means that
            // prefab override logic works on the entire property.
            // 开启一个属性，表示这个类的字段作为一个整体，
            // 也表示这个类，作为其他类的字段时应该怎么绘制
            EditorGUI.BeginProperty(position, label, property);
            {
                // EditorGUI.PrefixLabel(position, GUIUtility.GetControlID(FocusType.Passive), label);
                // 最好获取一个控件ID，这样在绘制的时候，可以知道当前绘制的是哪个控件
                // AI 说不获取 ID 可能影响稳定性...
                position = EditorGUI.PrefixLabel(position, GUIUtility.GetControlID(FocusType.Passive), label);
                // Don't make child fields be indented
                var indent = EditorGUI.indentLevel;
                EditorGUI.indentLevel = 0;
                // Calculate rects
                var amountRect = new Rect(position.x, position.y, 30, position.height);
                var unitRect = new Rect(position.x + 35, position.y, 50, position.height);
                var nameRect = new Rect(position.x + 90, position.y, position.width - 90, position.height);
                // Draw fields - pass GUIContent.none to each so they are drawn without labels
                EditorGUI.PropertyField(amountRect, property.FindPropertyRelative("amount"), GUIContent.none);
                EditorGUI.PropertyField(unitRect, property.FindPropertyRelative("unit"), GUIContent.none);
                EditorGUI.PropertyField(nameRect, property.FindPropertyRelative("name"), GUIContent.none);
                // Set indent back to what it was
                EditorGUI.indentLevel = indent;
            }
            EditorGUI.EndProperty();
        }
    }
}