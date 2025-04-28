using System.Collections.Generic;
using Sirenix.OdinInspector;
using Sirenix.OdinInspector.Editor;
using YOGA.OdinToolkits.Editor;

namespace YOGA.OdinToolkits.AnalysisManual.OdinAttributes.Editor
{
    [IsChineseAttributeExample]
    public class OnStateUpdateExample : ExampleScriptableObject
    {
        // @ 表示解析表达式
        // # 表示 Property 的路径，使用 () 包裹
        // $ 表示引用，$value 表示自身 Property 的值，
        // $property 表示 Odin 提供的 InspectorProperty 类型实例对象，代表自身 Property
        [TabGroup("控制其他 property")]
        [OnStateUpdate("@#(#_DefaultTabGroup.#控制其他 property.list1).State.Expanded = $value")]
        [OnInspectorGUI(nameof(DebugProperty))]
        public bool controlList1;

        [TabGroup("控制其他 property")] [OnInspectorGUI(nameof(DebugProperty))]
        public List<string> list1;

        [TabGroup("展开依赖其他 property")] [OnInspectorGUI(nameof(DebugProperty))]
        public bool controlList2;

        [TabGroup("展开依赖其他 property")]
        [OnStateUpdate("@$property.State.Expanded = controlList2")]
        [OnInspectorGUI(nameof(DebugProperty))]
        public List<string> list2;

        [TabGroup("显示依赖其他 property")] [OnInspectorGUI(nameof(DebugProperty))]
        public bool controlList3;

        [TabGroup("显示依赖其他 property")] [OnStateUpdate(nameof(Visible))] [OnInspectorGUI(nameof(DebugProperty))]
        public List<string> list3;

#if UNITY_EDITOR // 如果在运行时使用 InspectorProperty 要注意宏定义，它是一个编辑器类型
        // 使用方法引用相比于直接使用表达式，它可以代码高亮，避免细节出错
        private void Visible(InspectorProperty property)
        {
            property.State.Visible = controlList3;
        }

        private void DebugProperty(InspectorProperty property)
        {
            OdinDebugEditor.ShowInspectorProperty(property);
        }
#endif
    }
}