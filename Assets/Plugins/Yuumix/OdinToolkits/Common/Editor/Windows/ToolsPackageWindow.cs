using Sirenix.OdinInspector.Editor;
using Sirenix.Utilities;
using Sirenix.Utilities.Editor;
using UnityEditor;
using UnityEngine;
using Yuumix.OdinToolkits.Common.YuumixEditor;
using Yuumix.OdinToolkits.Modules.APIBrowser;
using Yuumix.OdinToolkits.Modules.Tools.GenerateTemplateCode.Editor;

namespace Yuumix.OdinToolkits.Common.Editor.Windows
{
    public class ToolsPackageWindow : OdinMenuEditorWindow
    {
        #region MenuItemPath 菜单路径

        const string GenerateTemplateToolMenuPath = "模板代码生成工具";
        const string APIBrowser = "API 浏览器";

        #endregion

        protected override void OnEnable()
        {
            base.OnEnable();
            WindowPadding = new Vector4(10, 10, 10, 10);
        }

        [MenuItem(MenuItemSettings.ToolsPackageMenuItemName, false, MenuItemSettings.ToolsPackagePriority)]
        public static void ShowWindow()
        {
            var window = GetWindow<ToolsPackageWindow>();
            window.titleContent = new GUIContent(MenuItemSettings.ToolsPackageWindowName);
            window.position = GUIHelper.GetEditorWindowRect().AlignCenter(950, 700);
            window.minSize = new Vector2(500, 500);
            window.Show();
        }

        protected override OdinMenuTree BuildMenuTree()
        {
            var tree = new OdinMenuTree(false);
            // 构建 Tree
            tree.AddObjectAtPath(GenerateTemplateToolMenuPath,
                ScriptableObjectEditorUtil.GetAssetDeleteExtra<GenerateTemplateCodeToolSO>());
            tree.AddObjectAtPath(APIBrowser, ScriptableObjectEditorUtil.GetAssetDeleteExtra<ApiBrowserSO>());
            // 添加图标
            var generateTemplateCodeToolMenuItem = tree.GetMenuItem(GenerateTemplateToolMenuPath);
            var apiBrowserMenuItem = tree.GetMenuItem(APIBrowser);
            generateTemplateCodeToolMenuItem.AddThumbnailIcon(true);
            apiBrowserMenuItem.AddThumbnailIcon(true);
            return tree;
        }
    }
}
