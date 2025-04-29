using Plugins.YOGA.OdinToolkits.Editor;
using Plugins.YOGA.OdinToolkits.Modules.OdinAttributesAnalysis.Editor.Database;
using Sirenix.OdinInspector.Editor;
using Sirenix.Utilities;
using Sirenix.Utilities.Editor;
using System;
using UnityEditor;
using UnityEngine;

namespace Plugins.YOGA.OdinToolkits.Modules.OdinAttributesAnalysis.Editor.Window
{
    public class ChineseAttributeEditorWindow : OdinMenuEditorWindow
    {
        private static ChineseAttributeEditorWindow _window;
        public static Action OnWindowResized;
        private float _previousMenuTreeWidth;
        private float _previousWindowWidth;
        private OdinMenuTree _tree;

        protected override void OnDestroy()
        {
            EditorApplication.delayCall -= RepaintWindow;
            base.OnDestroy();
        }

        [MenuItem(OdinToolkitsMenuPaths.AttributeManualPath, false, OdinToolkitsMenuPaths.AttributeManualPriority)]
        public static void ShowWindow()
        {
            _window = GetWindow<ChineseAttributeEditorWindow>();
            _window.titleContent = new GUIContent(OdinToolkitsMenuPaths.AttributeManualWindowName);
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
                _tree.AddObjectAtPath(map.Key + "/" + container.GetType().Name.Replace("Container", ""), container);

            return _tree;
        }

        protected override void OnImGUI()
        {
            base.OnImGUI();
            var currentWindowWidth = position.width;
            var currentMenuTreeWidth = MenuWidth;
            if (EditorApplication.timeSinceStartup % 0.5f <= 0.01f) OnWindowResized?.Invoke();

            if (Mathf.Approximately(currentWindowWidth, _previousWindowWidth) &&
                Mathf.Approximately(currentMenuTreeWidth, _previousMenuTreeWidth))
                return;

            _previousWindowWidth = currentWindowWidth;
            _previousMenuTreeWidth = currentMenuTreeWidth;
            OnWindowResized?.Invoke();
        }

        private static void RepaintWindow()
        {
            _window?.Repaint();
        }
    }
}