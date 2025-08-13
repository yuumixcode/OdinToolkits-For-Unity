using System;
using System.Collections.Generic;
using Sirenix.OdinInspector.Editor;
using Sirenix.Utilities;
using Sirenix.Utilities.Editor;
using UnityEditor;
using UnityEngine;
using Yuumix.OdinToolkits.Modules.Editor;

namespace Yuumix.OdinToolkits.Core.Editor
{
    public class OdinAttributeOverviewProWindow : OdinMenuEditorWindow
    {
        static OdinAttributeOverviewProWindow _window;
        public static Action OnWindowResized;
        float _previousMenuTreeWidth;
        float _previousWindowWidth;
        OdinMenuTree _tree;

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
                        string str = menuItem.Name.ToLower().Replace(" ", "");
                        string searchStr = _tree.Config.SearchTerm.ToLower().Replace(" ", "");
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
            foreach (KeyValuePair<AttributeType, List<OdinAttributeContainerSO>> map in AttributeChineseDatabase
                         .Instance.ContainerMaps)
            foreach (OdinAttributeContainerSO container in map.Value)
            {
                _tree.AddObjectAtPath(map.Key + "/" + container.GetType().Name.Replace("Container", ""), container);
            }

            return _tree;
        }

        protected override void OnImGUI()
        {
            base.OnImGUI();
            float currentWindowWidth = position.width;
            float currentMenuTreeWidth = MenuWidth;
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

        protected override void OnDestroy()
        {
            EditorApplication.delayCall -= RepaintWindow;
            base.OnDestroy();
        }

        [MenuItem(OdinToolkitsWindowMenuItems.OVERVIEW_PRO, false, OdinToolkitsWindowMenuItems.OVERVIEW_PRO_PRIORITY)]
        public static void ShowWindow()
        {
            _window = GetWindow<OdinAttributeOverviewProWindow>();
            _window.titleContent = new GUIContent(OdinToolkitsWindowMenuItems.OVERVIEW_PRO_WINDOW_NAME);
            _window.position = GUIHelper.GetEditorWindowRect().AlignCenter(1000, 750);
            _window.Show();
        }

        static void RepaintWindow()
        {
            _window?.Repaint();
        }
    }
}
