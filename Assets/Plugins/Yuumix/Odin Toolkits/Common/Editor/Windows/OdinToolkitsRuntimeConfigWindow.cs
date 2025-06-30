using Sirenix.OdinInspector;
using Sirenix.OdinInspector.Editor;
using Sirenix.Utilities;
using Sirenix.Utilities.Editor;
using UnityEditor;
using UnityEngine;
using Yuumix.OdinToolkits.Common.InspectorMultiLanguage;

namespace Yuumix.OdinToolkits.Common.Editor
{
    public class OdinToolkitsRuntimeConfigWindow : OdinEditorWindow
    {
        [InlineEditor(InlineEditorObjectFieldModes.CompletelyHidden)]
        public OdinToolkitsRuntimeConfig odinToolkitsRuntimeConfig;

        protected override void OnEnable()
        {
            base.OnEnable();
            WindowPadding = new Vector4(10, 10, 10, 10);
            odinToolkitsRuntimeConfig = OdinToolkitsRuntimeConfig.Instance;
        }

        [MenuItem(MenuItemGlobalSettings.RuntimeConfigMenuItemName, false,
            MenuItemGlobalSettings.RuntimeConfigPriority)]
        public static void ShowWindow()
        {
            var window = GetWindow<OdinToolkitsRuntimeConfigWindow>();
            if (InspectorMultiLanguageManagerSO.IsChinese)
            {
                window.titleContent = new GUIContent(MenuItemGlobalSettings.RuntimeConfigWindowNameCn);
            }
            else if (InspectorMultiLanguageManagerSO.IsEnglish)
            {
                window.titleContent = new GUIContent(MenuItemGlobalSettings.RuntimeConfigWindowNameEn);
            }

            window.position = GUIHelper.GetEditorWindowRect().AlignCenter(800, 600);
            window.minSize = new Vector2(500, 500);
            window.ShowUtility();
        }
    }
}
