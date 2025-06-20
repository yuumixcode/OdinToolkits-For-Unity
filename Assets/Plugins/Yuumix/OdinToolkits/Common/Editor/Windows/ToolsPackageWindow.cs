using Sirenix.OdinInspector;
using Sirenix.OdinInspector.Editor;
using Sirenix.Utilities;
using Sirenix.Utilities.Editor;
using UnityEditor;
using UnityEngine;
using Yuumix.OdinToolkits.Common.InspectorLocalization;
using Yuumix.OdinToolkits.Common.YuumixEditor;
using Yuumix.OdinToolkits.Modules.Tools.GenerateTemplateCode.Editor;
using Yuumix.OdinToolkits.Modules.Tools.MemberInfoBrowseExportTool.Editor;
using Yuumix.OdinToolkits.Modules.Tools.ScriptDocGen.Editor;

namespace Yuumix.OdinToolkits.Common.Editor
{
    public class ToolsPackageWindow : OdinMenuEditorWindow
    {
        static object _selectionInstance;
        static bool _hasAddListener;

        protected override void OnEnable()
        {
            base.OnEnable();
            WindowPadding = new Vector4(10, 10, 10, 10);
            MenuWidth = 230;
            InspectorLocalizationManagerSO.OnLanguageChange -= ReBuild;
            InspectorLocalizationManagerSO.OnLanguageChange += ReBuild;
            // 设置 window
            ScriptDocGenToolSO.Instance.SetWindow(this);
            GenerateTemplateCodeToolSO.Instance.SetWindow(this);
            OnClose -= ClearWindowReference;
            OnClose += ClearWindowReference;
        }

        protected override void OnImGUI()
        {
            if (!_hasAddListener)
            {
                InspectorLocalizationManagerSO.OnLanguageChange -= ReBuild;
                InspectorLocalizationManagerSO.OnLanguageChange += ReBuild;
                _hasAddListener = true;
            }

            base.OnImGUI();
        }

        static void ClearWindowReference()
        {
            ScriptDocGenToolSO.Instance.ClearWindow();
            GenerateTemplateCodeToolSO.Instance.ClearWindow();
        }

        void ReBuild()
        {
            // Debug.Log("ToolPackage Rebuild");
            _selectionInstance = MenuTree.Selection.SelectedValue;
            ShowToast(ToastPosition.BottomRight, SdfIconType.InfoSquareFill,
                "触发切换语言，正在重建面板，稍等，请勿连续点击!", new Color(0.996f, 0.906f, 0.459f, 1f), 5f);
            BuildMenuTree();
            ForceMenuTreeRebuild();
            TrySelectMenuItemWithObject(_selectionInstance);
        }

        [MenuItem(MenuItemGlobalSettings.ToolsPackageMenuItemName, false, MenuItemGlobalSettings.ToolsPackagePriority)]
        public static void ShowWindow()
        {
            var window = GetWindow<ToolsPackageWindow>();
            window.titleContent = new GUIContent(MenuItemGlobalSettings.ToolsPackageWindowEnglishName);
            window.position = GUIHelper.GetEditorWindowRect().AlignCenter(1050, 750);
            window.minSize = new Vector2(500, 500);
            window.Show();
        }

        protected override OdinMenuTree BuildMenuTree()
        {
            var tree = new OdinMenuTree(false);
            // 添加 Object
            tree.AddObjectAtPath(_generateTemplateToolMenuPathData.GetCurrentOrFallback(),
                ScriptableObjectEditorUtil.GetAssetDeleteExtra<GenerateTemplateCodeToolSO>());
            tree.AddObjectAtPath(_memberInfoBrowseExportToolMenuPathData.GetCurrentOrFallback(),
                MemberBrowseExportToolSO.Instance);
            tree.AddObjectAtPath(_scriptDocGenToolMenuPathData.GetCurrentOrFallback(),
                ScriptableObjectEditorUtil.GetAssetDeleteExtra<ScriptDocGenToolSO>());
            // 获取 MenuItem
            var generateTemplateCodeToolMenuItem =
                tree.GetMenuItem(_generateTemplateToolMenuPathData.GetCurrentOrFallback());
            var apiBrowserMenuItem = tree.GetMenuItem(_memberInfoBrowseExportToolMenuPathData.GetCurrentOrFallback());
            var scriptDocGenToolMenuItem = tree.GetMenuItem(_scriptDocGenToolMenuPathData.GetCurrentOrFallback());
            // 图标
            generateTemplateCodeToolMenuItem.AddThumbnailIcon(true);
            apiBrowserMenuItem.AddThumbnailIcon(true);
            scriptDocGenToolMenuItem.AddThumbnailIcon(true);

            // Debug.Log("执行 BuildMenuTree");
            return tree;
        }

        #region MenuItemPath 菜单路径

        readonly MultipleLanguageData _generateTemplateToolMenuPathData =
            new MultipleLanguageData("模板代码生成工具", "Generate Template Tool");

        readonly MultipleLanguageData _memberInfoBrowseExportToolMenuPathData =
            new MultipleLanguageData("成员信息浏览导出工具", "MemberInfo Browser & Exporter");

        readonly MultipleLanguageData _scriptDocGenToolMenuPathData =
            new MultipleLanguageData("脚本文档生成工具", "Script Doc Generate Tool");

        #endregion
    }
}
