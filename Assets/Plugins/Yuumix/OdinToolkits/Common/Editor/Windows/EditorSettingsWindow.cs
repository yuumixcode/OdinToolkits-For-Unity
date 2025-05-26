using Sirenix.OdinInspector.Editor;
using Sirenix.Utilities;
using Sirenix.Utilities.Editor;
using UnityEditor;
using UnityEngine;
using Yuumix.OdinToolkits.Common.YuumiEditor.Localization;

namespace Yuumix.OdinToolkits.Common.Editor.Windows
{
    public class EditorSettingsWindow : OdinMenuEditorWindow
    {
        #region MenuItemPath 菜单路径

        const string InspectorLanguageManagerMenuPath = "检视面板语言设置";

        #endregion

        protected override void OnEnable()
        {
            base.OnEnable();
            WindowPadding = new Vector4(10, 10, 10, 10);
        }

        [MenuItem(OdinToolkitsMenuPaths.EditorSettingsPath, false, OdinToolkitsMenuPaths.EditorSettingsWindowPriority)]
        public static void ShowWindow()
        {
            var window = GetWindow<EditorSettingsWindow>();
            window.titleContent = new GUIContent("OdinToolkits EditorSettings");
            window.position = GUIHelper.GetEditorWindowRect().AlignCenter(800, 600);
            window.minSize = new Vector2(500, 500);
            window.Show();
        }

        protected override OdinMenuTree BuildMenuTree()
        {
            var tree = new OdinMenuTree(false);
            // 构建 Tree
            tree.AddObjectAtPath(InspectorLanguageManagerMenuPath, InspectorLanguageManagerSO.Instance);
            // 添加图标
            var inspectorLanguageManagerMenuItem = tree.GetMenuItem(InspectorLanguageManagerMenuPath);
            inspectorLanguageManagerMenuItem.AddThumbnailIcon(true);
            return tree;
        }
    }
}
