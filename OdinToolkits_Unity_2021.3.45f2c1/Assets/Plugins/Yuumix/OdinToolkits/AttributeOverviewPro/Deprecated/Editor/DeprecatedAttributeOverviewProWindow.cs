using Sirenix.OdinInspector.Editor;
using Sirenix.Utilities;
using Sirenix.Utilities.Editor;
using System;
using UnityEditor;
using UnityEngine;
using Yuumix.OdinToolkits.Core.Editor;

namespace Yuumix.OdinToolkits.AttributeOverviewPro.Deprecated.Editor
{
    public class DeprecatedAttributeOverviewProWindow : OdinMenuEditorWindow
    {
        static DeprecatedAttributeOverviewProWindow _window;
        public static Action OnWindowResized;
        float _previousMenuTreeWidth;
        float _previousWindowWidth;
        OdinMenuTree _tree;

        #region Event Functions

        protected override void OnDestroy()
        {
            EditorApplication.delayCall -= RepaintWindow;
            base.OnDestroy();
        }

        #endregion

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
                    Height = 25
                }
            };

            EditorApplication.delayCall += RepaintWindow;
        }

        protected override OdinMenuTree BuildMenuTree()
        {
            foreach (var map in AttributeChineseDatabase
                         .Instance.ContainerMaps)
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

        [MenuItem(OdinToolkitsMenuItems.DEPRECATED_OVERVIEW_PRO, false,
            OdinToolkitsMenuItems.DEPRECATED_OVERVIEW_PRO_PRIORITY)]
        public static void ShowWindow()
        {
            _window = GetWindow<DeprecatedAttributeOverviewProWindow>();
            _window.titleContent = new GUIContent(OdinToolkitsMenuItems.DEPRECATED_OVERVIEW_PRO_WINDOW_NAME);
            _window.position = GUIHelper.GetEditorWindowRect().AlignCenter(1000, 750);
            _window.Show();
        }

        static void RepaintWindow()
        {
            _window?.Repaint();
        }
    }
}
