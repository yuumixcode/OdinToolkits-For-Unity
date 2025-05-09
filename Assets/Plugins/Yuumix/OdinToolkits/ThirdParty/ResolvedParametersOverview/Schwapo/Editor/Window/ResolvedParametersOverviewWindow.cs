// ------------------------------------------------------------------
// * 第三方引用 - 开源库
// * 原作者: Schwapo
// * https://github.com/Schwapo/Odin-Resolved-Parameters-Overview
// ------------------------------------------------------------------
// * 整理收录: Yuumi Zeus
// * https://github.com/Yuumi-Zeus
// ------------------------------------------------------------------

using System;
using System.Linq;
using Sirenix.OdinInspector.Editor;
using UnityEditor;
using UnityEngine;
using Yuumix.OdinToolkits.Common.Editor;

namespace YOGA.Modules.OdinToolkits.Schwapo.Editor.Window
{
    public class ResolvedParametersOverviewWindow : OdinMenuEditorWindow
    {
        public static Action OnWindowResized;
        private static readonly OdinMenuTreeDrawingConfig Config = new();
        private static ResolvedParametersOverviewWindow window;
        private float previousMenuTreeWidth;
        private float previousWindowWidth;

        private static ResolvedParametersOverviewWindow Window
        {
            get
            {
                if (window == null) window = GetWindow<ResolvedParametersOverviewWindow>();

                return window;
            }
        }

        // [MenuItem("Tools/Odin Inspector/Resolved Parameters Overview")]
        [MenuItem(OdinToolkitsMenuPaths.ResolvedParametersPath, false, OdinToolkitsMenuPaths.ThirdPartyPriority)]
        public static void Open()
        {
            window = GetWindow<ResolvedParametersOverviewWindow>("Resolved Parameters Overview");
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

            if (!Mathf.Approximately(currentWindowWidth, previousWindowWidth) ||
                !Mathf.Approximately(currentMenuTreeWidth, previousMenuTreeWidth))
            {
                previousWindowWidth = currentWindowWidth;
                previousMenuTreeWidth = currentMenuTreeWidth;
                OnWindowResized?.Invoke();
            }
        }

        protected override OdinMenuTree BuildMenuTree()
        {
            var tree = new OdinMenuTree();
            tree.Config = Config;
            tree.Config.SearchTerm = "";
            tree.Config.DrawSearchToolbar = true;
            tree.Config.SearchFunction = menuItem =>
            {
                if (SearchedFor(menuItem.Name)) return true;

                var attribute = (AttributeWithResolvedParameters)menuItem.Value;

                return attribute.ResolvedParameters.Any(p => SearchedFor(p.Name));
            };

            foreach (var (_, attribute) in AttributeWithResolvedParametersMap.Map)
                tree.AddObjectAtPath(attribute.Name, attribute);

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