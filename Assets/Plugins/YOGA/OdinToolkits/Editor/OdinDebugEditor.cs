using Sirenix.OdinInspector.Editor;
using Sirenix.Utilities;
using Sirenix.Utilities.Editor;
using UnityEditor;
using UnityEngine;

namespace YOGA.OdinToolkits.Editor
{
    public static class OdinDebugEditor
    {
        public static void ShowInspectorProperty(InspectorProperty property, bool border = false)
        {
            SirenixEditorGUI.BeginBox(GUIHelper.TempContent(property.Label.text + " InspectorProperty 调试信息"));
            DoPropertyLabel("Odin 绘制系统的路径", property.Path, border);
            DoPropertyLabel("Unity 系统的路径", property.UnityPropertyPath, border);
            DoPropertyLabel("InspectorProperty Index", property.Index.ToString(), border);
            DoPropertyLabel("InspectorProperty 内部的 InspectorPropertyInfo 实例对象",
                property.Info.PropertyName, border);
            DoPropertyLabel("InspectorPropertyInfo 实例对象是否有任何后备成员",
                property.Info.HasBackingMembers.ToString(), border);
            DoPropertyLabel("InspectorPropertyInfo 实例对象是否仅有一个后备成员",
                property.Info.HasSingleBackingMember.ToString(), border);
            DoAttributes(property, border);
            SirenixEditorGUI.EndBox();
        }

        public static void DoPropertyLabel(string name, string text, bool border = false)
        {
            EditorGUILayout.SelectableLabel(name + ": " + text, GUILayoutOptions.Height(20));
            if (border) SirenixEditorGUI.DrawBorders(GUILayoutUtility.GetLastRect(), 1, Color.green);
        }

        public static void DoAttributes(InspectorProperty property, bool border = false)
        {
            var rect = SirenixEditorGUI.BeginLegendBox(GUIHelper.TempContent(property.Label.text + " 特性列表"));
            for (var i = 0; i < property.Attributes.Count; i++)
                EditorGUILayout.SelectableLabel(i + ": " + property.Attributes[i].GetType().Name,
                    GUILayoutOptions.Height(20));

            SirenixEditorGUI.EndLegendBox();
            if (border) SirenixEditorGUI.DrawBorders(rect.Padding(-1), 1, Color.green);
        }
    }
}