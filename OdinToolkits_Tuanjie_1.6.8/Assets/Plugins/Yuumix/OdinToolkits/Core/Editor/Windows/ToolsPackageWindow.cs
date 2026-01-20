using Sirenix.OdinInspector;
using Sirenix.OdinInspector.Editor;
using Sirenix.Utilities;
using Sirenix.Utilities.Editor;
using UnityEditor;
using UnityEngine;
using Yuumix.OdinToolkits.Module.Editor;
using Yuumix.OdinToolkits.Modules.Editor;

namespace Yuumix.OdinToolkits.Core.Editor
{
    public class ToolsPackageWindow : OdinMenuEditorWindow
    {
        static object _selectionInstance;
        static bool _hasAddListener;

        #region Event Functions

        protected override void OnEnable()
        {
            base.OnEnable();
            WindowPadding = new Vector4(10, 10, 10, 10);
            MenuWidth = 230;
            DrawMenuSearchBar = true;
            InspectorBilingualismConfigSO.OnLanguageChanged -= ReBuild;
            InspectorBilingualismConfigSO.OnLanguageChanged += ReBuild;
            OnClose -= ClearEventListener;
            OnClose += ClearEventListener;
            TemplateCodeGeneratorVisualPanelSO.ToastEvent -= ShowToast;
            TemplateCodeGeneratorVisualPanelSO.ToastEvent += ShowToast;
        }

        #endregion

        protected override void OnImGUI()
        {
            if (!_hasAddListener)
            {
                InspectorBilingualismConfigSO.OnLanguageChanged -= ReBuild;
                InspectorBilingualismConfigSO.OnLanguageChanged += ReBuild;
                _hasAddListener = true;
            }

            base.OnImGUI();
        }

        void ClearEventListener()
        {
            TemplateCodeGeneratorVisualPanelSO.ToastEvent -= ShowToast;
        }

        void ReBuild()
        {
            _selectionInstance = MenuTree.Selection.SelectedValue;
            ShowToast(ToastPosition.BottomRight, SdfIconType.InfoSquareFill, "请勿连续点击！稍等，正在重建面板！", Color.white,
                3f);
            BuildMenuTree();
            ForceMenuTreeRebuild();
            TrySelectMenuItemWithObject(_selectionInstance);
        }

        [MenuItem(OdinToolkitsMenuItems.TOOLS_PACKAGE_MENU_ITEM_NAME, false,
            OdinToolkitsMenuItems.TOOLS_PACKAGE_PRIORITY)]
        public static void ShowWindow()
        {
            var window = GetWindow<ToolsPackageWindow>();
            window.titleContent = new GUIContent(OdinToolkitsMenuItems.TOOLS_PACKAGE_WINDOW_ENGLISH_NAME);
            window.position = GUIHelper.GetEditorWindowRect()
                .AlignCenter(1050, 750);
            window.minSize = new Vector2(500, 500);
            window.Show();
        }

        protected override OdinMenuTree BuildMenuTree()
        {
            var tree = new OdinMenuTree(false);
            var path2 = TemplateCodeGeneratorVisualPanelSO.GenerateTemplateToolMenuPathData
                .GetCurrentOrFallback();
            var path3 = DirectoryTreeGenToolSO.DirectoryTreeGenToolMenuPathData.GetCurrentOrFallback();
            var path4 = ExportPackageToolVisualPanelSO.ToolMenuPathData.GetCurrentOrFallback();
            var path5 = MenuItemViewerVisualPanelSO.ToolMenuPath.GetCurrentOrFallback();
            // 添加 Object
            tree.AddObjectAtPath(path2, TemplateCodeGeneratorVisualPanelSO.Instance);
            tree.AddObjectAtPath(path3, DirectoryTreeGenToolSO.Instance);
            tree.AddObjectAtPath(path4, ExportPackageToolVisualPanelSO.Instance);
            tree.AddObjectAtPath(path5, MenuItemViewerVisualPanelSO.Instance);
            // 获取 MenuItem
            var generateTemplateCodeToolMenuItem = tree.GetMenuItem(path2);
            var directoryTreeGenToolMenuItem = tree.GetMenuItem(path3);
            var exportPackageToolMenuItem = tree.GetMenuItem(path4);
            var menuItemViewerMenuItem = tree.GetMenuItem(path5);
            // 图标
            generateTemplateCodeToolMenuItem.AddThumbnailIcon(true);
            directoryTreeGenToolMenuItem.AddThumbnailIcon(true);
            exportPackageToolMenuItem.AddThumbnailIcon(true);
            menuItemViewerMenuItem.AddThumbnailIcon(true);
            return tree;
        }
    }
}
