using Yuumix.OdinToolkits.Core;
using Sirenix.OdinInspector;
using Sirenix.OdinInspector.Editor;
using Sirenix.Utilities;
using Sirenix.Utilities.Editor;
using UnityEditor;
using UnityEngine;
using Yuumix.OdinToolkits.Shared;
using Yuumix.OdinToolkits.Editor.Core;
using Yuumix.OdinToolkits.Modules.Tools.ScriptDocGen.Editor;
using Yuumix.OdinToolkits.Editor;

namespace Yuumix.OdinToolkits.Editor.Shared
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
            DrawMenuSearchBar = true;
            InspectorMultiLanguageManagerSO.OnLanguageChange -= ReBuild;
            InspectorMultiLanguageManagerSO.OnLanguageChange += ReBuild;
            OnClose -= ClearEventListener;
            OnClose += ClearEventListener;
            ScriptDocGenToolSO.OnToastEvent -= ShowToast;
            ScriptDocGenToolSO.OnToastEvent += ShowToast;
            TemplateCodeGenToolSO.ToastEvent -= ShowToast;
            TemplateCodeGenToolSO.ToastEvent += ShowToast;
        }

        protected override void OnImGUI()
        {
            if (!_hasAddListener)
            {
                InspectorMultiLanguageManagerSO.OnLanguageChange -= ReBuild;
                InspectorMultiLanguageManagerSO.OnLanguageChange += ReBuild;
                _hasAddListener = true;
            }

            base.OnImGUI();
        }

        void ClearEventListener()
        {
            ScriptDocGenToolSO.OnToastEvent -= ShowToast;
        }

        void ReBuild()
        {
            _selectionInstance = MenuTree.Selection.SelectedValue;
            ShowToast(ToastPosition.BottomRight, SdfIconType.InfoSquareFill,
                "请勿连续点击！稍等，正在重建面板！", Color.white, 3f);
            BuildMenuTree();
            ForceMenuTreeRebuild();
            TrySelectMenuItemWithObject(_selectionInstance);
        }

        [MenuItem(OdinToolkitsWindowMenuItems.ToolsPackageMenuItemName, false,
            OdinToolkitsWindowMenuItems.ToolsPackagePriority)]
        public static void ShowWindow()
        {
            var window = GetWindow<ToolsPackageWindow>();
            window.titleContent = new GUIContent(OdinToolkitsWindowMenuItems.ToolsPackageWindowEnglishName);
            window.position = GUIHelper.GetEditorWindowRect().AlignCenter(1050, 750);
            window.minSize = new Vector2(500, 500);
            window.Show();
        }

        protected override OdinMenuTree BuildMenuTree()
        {
            var tree = new OdinMenuTree(false);
            string path1 = TemplateCodeGenToolSO.GenerateTemplateToolMenuPathData.GetCurrentOrFallback();
            string path2 = ScriptDocGenToolSO.ScriptDocGenToolMenuPathData.GetCurrentOrFallback();
            // 添加 Object
            tree.AddObjectAtPath(path1, TemplateCodeGenToolSO.Instance);
            tree.AddObjectAtPath(path2, ScriptDocGenToolSO.Instance);
            // 获取 MenuItem
            OdinMenuItem generateTemplateCodeToolMenuItem = tree.GetMenuItem(path1);
            OdinMenuItem scriptDocGenToolMenuItem = tree.GetMenuItem(path2);
            // 图标
            generateTemplateCodeToolMenuItem.AddThumbnailIcon(true);
            scriptDocGenToolMenuItem.AddThumbnailIcon(true);
            // Debug.Log("执行 BuildMenuTree");
            return tree;
        }
    }
}
