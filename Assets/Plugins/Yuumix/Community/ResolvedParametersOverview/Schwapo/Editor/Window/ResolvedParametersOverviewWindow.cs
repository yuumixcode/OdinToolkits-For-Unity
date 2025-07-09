using Sirenix.OdinInspector.Editor;
using Sirenix.Utilities;
using Sirenix.Utilities.Editor;
using System;
using System.Linq;
using UnityEngine;
using Yuumix.OdinToolkits.Editor.Core;

namespace Yuumix.OdinToolkits.ThirdParty.ResolvedParametersOverview.Schwapo.Editor.Window
{
    public class ResolvedParametersOverviewWindow : OdinMenuEditorWindow
    {
        public static Action OnWindowResized;
        static readonly OdinMenuTreeDrawingConfig Config = new OdinMenuTreeDrawingConfig();
        static ResolvedParametersOverviewWindow _window;
        float _previousMenuTreeWidth;
        float _previousWindowWidth;

        static ResolvedParametersOverviewWindow Window
        {
            get
            {
                if (_window == null)
                {
                    _window = GetWindow<ResolvedParametersOverviewWindow>();
                }

                return _window;
            }
        }

        // [MenuItem("Tools/Odin Inspector/Resolved Parameters Overview")]
        // [MenuItem(MenuItemGlobalSettings.ResolvedParametersMenuItemName, false, MenuItemGlobalSettings.ResolvedParametersPriority)]
        public static void Open()
        {
            _window = GetWindow<ResolvedParametersOverviewWindow>(OdinToolkitsWindowMenuItems.ResolvedParametersOverviewWindowName);
            _window.position = GUIHelper.GetEditorWindowRect().AlignCenter(800, 600);
            _window.minSize = new Vector2(500, 500);
            _window.ShowUtility();
        }

        protected override void Initialize()
        {
            WindowPadding = Vector4.zero;
        }

        protected override void OnImGUI()
        {
            base.OnImGUI();

            var currentWindowWidth = Window.position.width;
            var currentMenuTreeWidth = Window.MenuWidth;

            if (!Mathf.Approximately(currentWindowWidth, _previousWindowWidth) ||
                !Mathf.Approximately(currentMenuTreeWidth, _previousMenuTreeWidth))
            {
                _previousWindowWidth = currentWindowWidth;
                _previousMenuTreeWidth = currentMenuTreeWidth;
                OnWindowResized?.Invoke();
            }
        }

        protected override OdinMenuTree BuildMenuTree()
        {
            var tree = new OdinMenuTree
            {
                Config = Config
            };
            tree.Config.SearchTerm = "";
            tree.Config.DrawSearchToolbar = true;
            tree.Config.SearchFunction = menuItem =>
            {
                if (SearchedFor(menuItem.Name))
                {
                    return true;
                }

                var attribute = (AttributeWithResolvedParameters)menuItem.Value;

                return attribute.ResolvedParameters.Any(p => SearchedFor(p.Name));
            };

            foreach (var (_, attribute) in AttributeWithResolvedParametersMap.Map)
            {
                tree.AddObjectAtPath(attribute.Name, attribute);
            }

            return tree;
        }

        public static bool SearchedFor(string str)
        {
            var unifiedSearchStr = str.ToLower().Replace(" ", "");
            var unifiedSearchTerm = Config.SearchTerm.ToLower().Replace(" ", "");
            return unifiedSearchStr.Contains(unifiedSearchTerm);
        }

        public static void RepaintWindow()
        {
            Window.Repaint();
        }
    }
}
