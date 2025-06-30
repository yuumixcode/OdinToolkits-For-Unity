using Sirenix.OdinInspector.Editor;
using Sirenix.Utilities;
using Sirenix.Utilities.Editor;
using System;
using UnityEditor;
using UnityEngine;
using Yuumix.OdinToolkits.Common.Editor;
using Yuumix.OdinToolkits.Modules.Odin.OdinAttributesAnalysis.Editor.Database;

namespace Yuumix.OdinToolkits.Modules.Odin.OdinAttributesAnalysis.Editor.Window
{
    public class ChineseAttributeEditorWindow : OdinMenuEditorWindow
    {
        static ChineseAttributeEditorWindow _window;
        public static Action OnWindowResized;
        float _previousMenuTreeWidth;
        float _previousWindowWidth;
        OdinMenuTree _tree;

        protected override void OnDestroy()
        {
            EditorApplication.delayCall -= RepaintWindow;
            base.OnDestroy();
        }

        [MenuItem(MenuItemGlobalSettings.AttributeChineseMenuItemName, false, MenuItemGlobalSettings.AttributeChinesePriority)]
        public static void ShowWindow()
        {
            _window = GetWindow<ChineseAttributeEditorWindow>();
            _window.titleContent = new GUIContent(MenuItemGlobalSettings.AttributeChineseWindowName);
            _window.position = GUIHelper.GetEditorWindowRect().AlignCenter(1000, 750);
            _window.Show();
        }

        protected override void Initialize()
        {
            WindowPadding = new Vector4(10, 10, 10, 10);
            MenuWidth = 230f;
            _tree = new OdinMenuTree
            {
                Config =
                {
                    DrawSearchToolbar = true,
                    SearchTerm = "",
                    SearchFunction = menuItem =>
                    {
                        var str = menuItem.Name.ToLower().Replace(" ", "");
                        var searchStr = _tree.Config.SearchTerm.ToLower().Replace(" ", "");
                        return str.Contains(searchStr);
                    }
                },
                DefaultMenuStyle = new OdinMenuStyle
                {
                    Height = 27
                }
            };
            // 只会执行一次
            EditorApplication.delayCall += RepaintWindow;
        }

        protected override OdinMenuTree BuildMenuTree()
        {
            foreach (var map in AttributeChineseDatabase.Instance.ContainerMaps)
            foreach (var container in map.Value)
            {
                _tree.AddObjectAtPath(map.Key + "/" + container.GetType().Name.Replace("Container", ""), container);
            }

            return _tree;
        }

        protected override void OnImGUI()
        {
            base.OnImGUI();
            var currentWindowWidth = position.width;
            var currentMenuTreeWidth = MenuWidth;
            if (EditorApplication.timeSinceStartup % 0.5f <= 0.01f)
            {
                OnWindowResized?.Invoke();
            }

            if (Mathf.Approximately(currentWindowWidth, _previousWindowWidth) &&
                Mathf.Approximately(currentMenuTreeWidth, _previousMenuTreeWidth))
            {
                return;
            }

            _previousWindowWidth = currentWindowWidth;
            _previousMenuTreeWidth = currentMenuTreeWidth;
            OnWindowResized?.Invoke();
        }

        static void RepaintWindow()
        {
            _window?.Repaint();
        }
    }
}
