using Plugins.YOGA.OdinToolkits.Modules.OdinAttributesAnalysis.Common.Editor;
using Sirenix.OdinInspector;
using Sirenix.Utilities.Editor;
using System;
using System.Globalization;
using UnityEngine;

namespace Plugins.YOGA.OdinToolkits.Modules.OdinAttributesAnalysis.Editor.AttributePreviewExamples.Scripts
{
    [IsChineseAttributeExample]
    public class OnInspectorInitExample : ExampleScriptableObject
    {
        [OnInspectorInit(nameof(First))] public string first;

        [OnInspectorInit(nameof(Second))] public string second;

        // OnInspectorInit executes the first time this string is about to be drawn in the inspector.
        // It will execute again when the example is reselected.
        [OnInspectorInit("@timeWhenExampleWasOpened = DateTime.Now.ToString()")]
        public string timeWhenExampleWasOpened;

        // OnInspectorInit will not execute before the property is actually "resolved" in the inspector.
        // Remember, Odin's property system is lazily evaluated, and so a property does not actually exist
        // and is not initialized before something is actually asking for it.
        // 
        // Therefore, this OnInspectorInit attribute won't execute until the foldout is expanded.
        [FoldoutGroup("Delayed Initialization", Expanded = false, HideWhenChildrenAreInvisible = false)]
        [OnInspectorInit("@timeFoldoutWasOpened = DateTime.Now.ToString()")]
        public string timeFoldoutWasOpened;

        [ShowInInspector]
        [DisplayAsString]
        [PropertyOrder(-1)]
        public string CurrentTime
        {
            get
            {
                GUIHelper.RequestRepaint();
                return DateTime.Now.ToString(CultureInfo.CurrentCulture);
            }
        }

        private void First()
        {
            Debug.Log("first 字段进行初始化");
        }

        private void Second()
        {
            Debug.Log("second 字段进行初始化");
        }
    }
}