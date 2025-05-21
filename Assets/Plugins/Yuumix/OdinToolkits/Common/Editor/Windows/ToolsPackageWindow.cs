using Sirenix.OdinInspector;
using Sirenix.OdinInspector.Editor;
using Sirenix.Utilities;
using Sirenix.Utilities.Editor;
using UnityEditor;
using UnityEngine;
using Yuumix.OdinToolkits.Modules.Tools.GenerateTemplateCode.Editor;
using Yuumix.OdinToolkits.Modules.Utilities.YuumiEditor;

namespace Yuumix.OdinToolkits.Common.Editor.Windows
{
    public class ToolsPackageWindow : OdinMenuEditorWindow
    {
        protected override void OnEnable()
        {
            base.OnEnable();
            WindowPadding = new Vector4(10, 10, 10, 10);
        }

        [MenuItem(OdinToolkitsMenuPaths.ToolsPackagePath, false, OdinToolkitsMenuPaths.ToolsPackagePriority)]
        public static void ShowWindow()
        {
            var window = GetWindow<ToolsPackageWindow>();
            window.titleContent = new GUIContent("Odin Toolkits 工具包");
            window.position = GUIHelper.GetEditorWindowRect().AlignCenter(800, 600);
            window.minSize = new Vector2(500, 500);
            window.Show();
        }

        protected override OdinMenuTree BuildMenuTree()
        {
            var tree = new OdinMenuTree(false);
            // 构建 Tree
            tree.AddObjectAtPath(GenerateTemplateToolMenuPath,
                ProjectEditorUtility.SO.GetScriptableObjectDeleteExtra<GenerateTemplateCodeToolSO>());
            // tree.AddObjectAtPath("特殊定制/特性解析代码生成工具",
            //     ProjectEditorUtility.SO.GetScriptableObjectDeleteExtra<AttributeAnalysisGenCodeTool>());
            // 添加图标
            var generateTemplateCodeToolMenuItem = tree.GetMenuItem(GenerateTemplateToolMenuPath);
            generateTemplateCodeToolMenuItem.AddThumbnailIcon(true);
            return tree;
        }

        #region MenuItemPath 菜单路径

        private const string GenerateTemplateToolMenuPath = "模板代码生成工具";

        #endregion
    }
}
