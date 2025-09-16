using Sirenix.OdinInspector.Editor;
using Sirenix.Utilities;
using Sirenix.Utilities.Editor;
using UnityEditor;
using UnityEngine;
using Yuumix.OdinToolkits.Core.Editor;
using Yuumix.OdinToolkits.Modules.ScriptDocGen.Editor.Settings;

namespace Yuumix.OdinToolkits.Modules.Editor
{
    public class ScriptDocGenWindow : OdinMenuEditorWindow
    {
        protected override void OnEnable()
        {
            base.OnEnable();
            WindowPadding = new Vector4(10, 10, 10, 10);
            MenuWidth = 230;
            DrawMenuSearchBar = true;
        }

        [MenuItem(OdinToolkitsWindowMenuItems.SCRIPT_DOC_GEN,
            false,
            OdinToolkitsWindowMenuItems.SCRIPT_DOC_GEN_PRIORITY)]
        public static void Open()
        {
            var window = GetWindow<ScriptDocGenWindow>();
            window.titleContent = new GUIContent(OdinToolkitsWindowMenuItems.SCRIPT_DOC_GEN_WINDOW_NAME);
            window.position = GUIHelper.GetEditorWindowRect().AlignCenter(1000, 750);
            window.Show();
        }

        protected override OdinMenuTree BuildMenuTree()
        {
            var tree = new OdinMenuTree(false);
            const string path1 = ScriptDocGenSettingSO.MENU_PATH;
            tree.AddObjectAtPath(path1, ScriptDocGenSettingSO.Instance);
            // 获取 MenuItem
            OdinMenuItem scriptDocGenSettingMenuItem = tree.GetMenuItem(path1);
            // 图标
            scriptDocGenSettingMenuItem.AddThumbnailIcon(true);
            return tree;
        }
    }
}
