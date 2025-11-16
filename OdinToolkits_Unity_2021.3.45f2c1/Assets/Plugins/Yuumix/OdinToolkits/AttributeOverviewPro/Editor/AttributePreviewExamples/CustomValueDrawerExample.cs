using Sirenix.OdinInspector;
using Sirenix.OdinInspector.Editor;
using Sirenix.Utilities.Editor;
using System;
using UnityEditor;
using UnityEngine;

namespace Yuumix.OdinToolkits.Modules.Editor
{
    [OdinToolkitsAttributeExample]
    public class CustomValueDrawerExample : ExampleSO
    {
        #region Serialized Fields

        [PropertyOrder(-5)]
        [LabelText("左边最小值: ")]
        public float from = 2;

        [PropertyOrder(-5)]
        [LabelText("右边最大值: ")]
        public float to = 7;

        [PropertyOrder(1)]
        [FoldoutGroup("Action 方法签名: float Method (float value)")]
        [InfoBox("范围编写在代码中")]
        [CustomValueDrawer("MyCustomDrawerStatic")]
        public float customDrawerStatic;

        [PropertyOrder(20)]
        [FoldoutGroup("Action 方法签名: float Method (float value)")]
        [InfoBox("对集合类型使用，实际作用的是集合中的元素")]
        [CustomValueDrawer("MyCustomDrawerArrayNoLabel")]
        public float[] customDrawerArrayNoLabel = { 3, 5, 6 };

        [PropertyOrder(20)]
        [FoldoutGroup("Action 方法签名: float Method (float value, GUIContent label)")]
        [InfoBox("绘制方法内部还可以调用其他字段，使用字段动态设置范围")]
        [CustomValueDrawer("MyCustomDrawerInstance")]
        public float customDrawerInstance;

        [PropertyOrder(40)]
        [FoldoutGroup("Action 方法签名: float Method (float value, GUIContent label, " +
                      "Func<GUIContent, bool> callNextDrawer)")]
        [InfoBox("接入 Odin 绘制链")]
        [CustomValueDrawer("CustomDrawerAppendRange")]
        public float appendRange;

        [PropertyOrder(50)]
        [FoldoutGroup("Action 方法签名: float Method ( xxx ) 过长")]
        [InfoBox("Action 方法签名: float Method(float value, GUIContent label," +
                 " Func<GUIContent, bool> callNextDrawer, InspectorProperty property)")]
        [InfoBox("获取 InspectorProperty，绿色边框代表 Property 的范围")]
        [CustomValueDrawer("MyCustomDrawSpecial")]
        public float specialFloat;

        #endregion

        public override void SetDefaultValue()
        {
            customDrawerStatic = 0;
            customDrawerInstance = 0;
            customDrawerArrayNoLabel = new float[] { 0, 0, 0 };
            appendRange = 0;
            specialFloat = 0;
            from = 2;
            to = 7;
        }

#if UNITY_EDITOR // Editor 相关需要宏定义
        // 绘制方法可选参数有四种
        // 1. float value字段值
        float MyCustomDrawerStatic(float value) => EditorGUILayout.Slider(value, 0, 10);

        // 2. GUIContent label 用于设置 label 样式
        float MyCustomDrawerInstance(float value, GUIContent label) => EditorGUILayout.Slider(label, value, from, to);

        float MyCustomDrawerArrayNoLabel(float value) => EditorGUILayout.Slider(value, from, to);

        // 3. Func<GUIContent, bool> callNextDrawer 用于绘制下一层
        float CustomDrawerAppendRange(float value, GUIContent label, Func<GUIContent, bool> callNextDrawer)
        {
            SirenixEditorGUI.BeginBox();
            // Odin 的绘制链，调用 callNextDrawer 方法，进入下一层绘制
            callNextDrawer(label);
            var result = EditorGUILayout.Slider(label, value, from, to);
            SirenixEditorGUI.EndBox();
            return result;
        }

        // 4. InspectorProperty property Odin 封装的类，类似于 Unity 的 SerializedProperty 
        float MyCustomDrawSpecial(float value, GUIContent label, Func<GUIContent, bool> callNextDrawer,
            InspectorProperty property)
        {
            var rect = EditorGUILayout.GetControlRect();
            SirenixEditorGUI.DrawHorizontalLineSeperator(rect.x, rect.center.y, rect.width, 1);
            SirenixEditorGUI.BeginBox(label);
            EditorGUILayout.LabelField("Property 的 Odin 路径: (可以看到属于一个 Group) " + property.Path);
            EditorGUILayout.LabelField("Property 的 Unity 路径: " + property.UnityPropertyPath);
            EditorGUILayout.LabelField("Property State Enabled 状态: " + property.State.Enabled);
            SirenixEditorGUI.EndBox();
            SirenixEditorGUI.BeginBox();
            EditorGUILayout.LabelField("Property Attributes 这个字段上标记的特性列表: ");
            var list = property.Attributes;
            foreach (var item in list)
            {
                EditorGUILayout.LabelField(item.GetType().Name);
            }

            SirenixEditorGUI.EndBox();
            SirenixEditorGUI.BeginBox();
            callNextDrawer(label);
            var result = EditorGUILayout.Slider(label, value, from, to);
            SirenixEditorGUI.EndBox();
            // 绘制边框 property.LastDrawnValueRect 绘制此属性的最后一个区域
            SirenixEditorGUI.DrawBorders(new Rect(property.LastDrawnValueRect)
            {
                x = property.LastDrawnValueRect.x - 1,
                y = property.LastDrawnValueRect.y - 1,
                width = property.LastDrawnValueRect.width + 2,
                height = property.LastDrawnValueRect.height + 2
            }, 1, Color.green);
            return result;
        }
#endif
    }
}
