using Sirenix.OdinInspector;
using Sirenix.OdinInspector.Editor;
using Sirenix.Utilities;
using Sirenix.Utilities.Editor;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using Yuumix.OdinToolkits.AttributeOverviewPro.Shared;

namespace Yuumix.OdinToolkits.AttributeOverviewPro.Deprecated.Editor
{
    [AttributeOverviewProExample]
    public class OnStateUpdateExample : ExampleSO
    {
        #region Serialized Fields

        // @ 表示解析表达式
        // # 表示 Property 的路径，使用 () 包裹
        // $ 表示引用，$value 表示自身 Property 的值，
        // $property 表示 Odin 提供的 InspectorProperty 类型实例对象，代表自身 Property
        [TabGroup("控制其他 property")]
        [OnStateUpdate("@#(#_DefaultTabGroup.#控制其他 property.list1).State.Expanded = $value")]
        [OnInspectorGUI(nameof(DebugProperty))]
        public bool controlList1;

        [TabGroup("展开依赖其他 property")]
        [OnInspectorGUI(nameof(DebugProperty))]
        public bool controlList2;

        [TabGroup("显示依赖其他 property")]
        [OnInspectorGUI(nameof(DebugProperty))]
        public bool controlList3;

        [TabGroup("控制其他 property")]
        [OnInspectorGUI(nameof(DebugProperty))]
        public List<string> list1;

        [TabGroup("展开依赖其他 property")]
        [OnStateUpdate("@$property.State.Expanded = controlList2")]
        [OnInspectorGUI(nameof(DebugProperty))]
        public List<string> list2;

        [TabGroup("显示依赖其他 property")]
        [OnStateUpdate(nameof(Visible))]
        [OnInspectorGUI(nameof(DebugProperty))]
        public List<string> list3;

        #endregion

#if UNITY_EDITOR // 如果在运行时使用 InspectorProperty 要注意宏定义，它是一个编辑器类型
        // 使用方法引用相比于直接使用表达式，它可以代码高亮，避免细节出错
        void Visible(InspectorProperty property)
        {
            property.State.Visible = controlList3;
        }

        void DebugProperty(InspectorProperty property)
        {
            ShowInspectorProperty(property);
        }

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
            if (border)
            {
                SirenixEditorGUI.DrawBorders(GUILayoutUtility.GetLastRect(), 1, Color.green);
            }
        }

        public static void DoAttributes(InspectorProperty property, bool border = false)
        {
            var rect = SirenixEditorGUI.BeginLegendBox(GUIHelper.TempContent(property.Label.text + " 特性列表"));
            for (var i = 0; i < property.Attributes.Count; i++)
            {
                EditorGUILayout.SelectableLabel(i + ": " + property.Attributes[i].GetType().Name,
                    GUILayoutOptions.Height(20));
            }

            SirenixEditorGUI.EndLegendBox();
            if (border)
            {
                SirenixEditorGUI.DrawBorders(rect.Padding(-1), 1, Color.green);
            }
        }
#endif
    }
}
