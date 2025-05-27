using Sirenix.OdinInspector.Editor;
using Sirenix.Utilities;
using Sirenix.Utilities.Editor;
using UnityEditor;
using UnityEngine;
using Yuumix.OdinToolkits.Modules.Tools.GenerateTemplateCode.Editor;
using Yuumix.OdinToolkits.YuumiEditor;

namespace Yuumix.OdinToolkits.Common.Editor.Windows
{
    public class ToolsPackageWindow : OdinMenuEditorWindow
    {
        #region MenuItemPath 菜单路径

        const string GenerateTemplateToolMenuPath = "模板代码生成工具";

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
            window.position = GUIHelper.GetEditorWindowRect().AlignCenter(800, 600);
            window.minSize = new Vector2(500, 500);
            window.Show();
        }

        protected override OdinMenuTree BuildMenuTree()
        {
            var tree = new OdinMenuTree(false);
            // 构建 Tree
            tree.AddObjectAtPath(GenerateTemplateToolMenuPath,
                ScriptableObjectEditorUtil.GetAssetDeleteExtra<GenerateTemplateCodeToolSO>());
            // tree.AddObjectAtPath("特殊定制/特性解析代码生成工具",
            //     ProjectEditorUtility.SO.GetScriptableObjectDeleteExtra<AttributeAnalysisGenCodeTool>());
            // 添加图标
            var generateTemplateCodeToolMenuItem = tree.GetMenuItem(GenerateTemplateToolMenuPath);
            generateTemplateCodeToolMenuItem.AddThumbnailIcon(true);
            return tree;
        }
    }
}
