using Sirenix.OdinInspector;
using Sirenix.OdinInspector.Editor;
using Sirenix.Utilities;
using Sirenix.Utilities.Editor;
using UnityEditor;
using UnityEngine;
using Yuumix.OdinToolkits.Common.InspectorMultiLanguage;

namespace Yuumix.OdinToolkits.Common.Editor
{
    public class OdinToolkitsEditorSettingsWindow : OdinEditorWindow
    {
        public MultiLanguageHeaderWidget header = new MultiLanguageHeaderWidget(
            "Odin Toolkits 编辑器设置",
            "Odin Toolkits Editor Settings",
            "仅在编辑器阶段可以读取的设置文件",
            "Settings file that can only be read during the editor stage");

        [ShowInInspector]
        [InlineEditor(InlineEditorObjectFieldModes.CompletelyHidden)]
        [EnableGUI]
        public InspectorMultiLanguageManagerSO InspectorMultiLanguageManager =>
            InspectorMultiLanguageManagerSO.Instance;

        protected override void OnEnable()
        {
            base.OnEnable();
            WindowPadding = new Vector4(10, 10, 10, 10);
        }

        [MenuItem(MenuItemGlobalSettings.EditorSettingsMenuItemName, false,
            MenuItemGlobalSettings.EditorSettingsWindowPriority)]
        public static void ShowWindow()
        {
            var window = GetWindow<OdinToolkitsEditorSettingsWindow>();
            if (InspectorMultiLanguageManagerSO.IsChinese)
            {
                window.titleContent = new GUIContent(MenuItemGlobalSettings.EditorSettingsWindowNameCn);
            }
            else if (InspectorMultiLanguageManagerSO.IsEnglish)
            {
                window.titleContent = new GUIContent(MenuItemGlobalSettings.EditorSettingsWindowNameEn);
            }

            window.position = GUIHelper.GetEditorWindowRect().AlignCenter(800, 600);
            window.minSize = new Vector2(500, 500);
            window.ShowUtility();
        }
    }
}
