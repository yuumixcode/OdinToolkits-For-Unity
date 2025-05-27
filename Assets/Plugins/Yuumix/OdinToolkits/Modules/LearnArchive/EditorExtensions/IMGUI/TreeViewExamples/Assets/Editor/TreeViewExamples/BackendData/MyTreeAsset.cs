using System.Collections.Generic;
using UnityEngine;

namespace Yuumix.OdinToolkits.Modules.LearnArchive.EditorExtensions.IMGUI.TreeViewExamples.Assets.Editor.TreeViewExamples.
    BackendData
{
    // [CreateAssetMenu(fileName = "TreeDataAsset", menuName = "Tree Asset", order = 1)]
    public class MyTreeAsset : ScriptableObject
    {
        [SerializeField]
        List<MyTreeElement> m_TreeElements = new List<MyTreeElement>();

        internal List<MyTreeElement> treeElements
        {
            get => m_TreeElements;
            set => m_TreeElements = value;
        }

        void Awake()
        {
            if (m_TreeElements.Count == 0)
            {
                m_TreeElements = MyTreeElementGenerator.GenerateRandomTree(160);
            }
        }
    }
}
