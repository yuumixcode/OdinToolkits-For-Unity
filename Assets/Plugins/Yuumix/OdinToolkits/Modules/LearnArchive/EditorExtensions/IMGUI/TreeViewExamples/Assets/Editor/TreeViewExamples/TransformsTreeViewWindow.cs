using UnityEditor;
using UnityEditor.IMGUI.Controls;
using UnityEngine;

namespace Yuumix.OdinToolkits.Modules.LearnArchive.EditorExtensions.IMGUI.TreeViewExamples.Assets.Editor.TreeViewExamples
{
    internal class TransformTreeWindow : EditorWindow
    {
        [SerializeField]
        TreeViewState m_TreeViewState;

        TreeView m_TreeView;

        void OnEnable()
        {
            if (m_TreeViewState == null)
            {
                m_TreeViewState = new TreeViewState();
            }

            m_TreeView = new TransformTreeView(m_TreeViewState);
        }

        void OnGUI()
        {
            DoToolbar();
            DoTreeView();
        }

        void OnHierarchyChange()
        {
            if (m_TreeView != null)
            {
                m_TreeView.Reload();
            }

            Repaint();
        }

        void OnSelectionChange()
        {
            if (m_TreeView != null)
            {
                m_TreeView.SetSelection(Selection.instanceIDs);
            }

            Repaint();
        }

        // [MenuItem("TreeView Examples/Transform Hierarchy")]
        static void ShowWindow()
        {
            var window = GetWindow<TransformTreeWindow>();
            window.titleContent = new GUIContent("My Hierarchy");
            window.Show();
        }

        void DoTreeView()
        {
            var rect = GUILayoutUtility.GetRect(0, 100000, 0, 100000);
            m_TreeView.OnGUI(rect);
        }

        void DoToolbar()
        {
            GUILayout.BeginHorizontal(EditorStyles.toolbar);
            GUILayout.FlexibleSpace();
            GUILayout.EndHorizontal();
        }
    }
}
