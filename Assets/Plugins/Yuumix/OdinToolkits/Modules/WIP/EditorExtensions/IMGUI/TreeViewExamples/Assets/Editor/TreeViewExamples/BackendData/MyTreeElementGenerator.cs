using System.Collections.Generic;
using Yuumix.OdinToolkits.Modules.LearnArchive.EditorExtensions.IMGUI.TreeViewExamples.Assets.Editor.TreeDataModel;
using Random = UnityEngine.Random;

namespace Yuumix.OdinToolkits.Modules.LearnArchive.EditorExtensions.IMGUI.TreeViewExamples.Assets.Editor.TreeViewExamples.
    BackendData
{
    internal static class MyTreeElementGenerator
    {
        static int IDCounter;
        static readonly int minNumChildren = 5;
        static readonly int maxNumChildren = 10;
        static readonly float probabilityOfBeingLeaf = 0.5f;

        public static List<MyTreeElement> GenerateRandomTree(int numTotalElements)
        {
            var numRootChildren = numTotalElements / 4;
            IDCounter = 0;
            var treeElements = new List<MyTreeElement>(numTotalElements);

            var root = new MyTreeElement("Root", -1, IDCounter);
            treeElements.Add(root);
            for (var i = 0; i < numRootChildren; ++i)
            {
                var allowedDepth = 6;
                AddChildrenRecursive(root, Random.Range(minNumChildren, maxNumChildren), true, numTotalElements, ref allowedDepth,
                    treeElements);
            }

            return treeElements;
        }

        static void AddChildrenRecursive(TreeElement element, int numChildren, bool force, int numTotalElements,
            ref int allowedDepth, List<MyTreeElement> treeElements)
        {
            if (element.depth >= allowedDepth)
            {
                allowedDepth = 0;
                return;
            }

            for (var i = 0; i < numChildren; ++i)
            {
                if (IDCounter > numTotalElements)
                {
                    return;
                }

                var child = new MyTreeElement("Element " + IDCounter, element.depth + 1, ++IDCounter);
                treeElements.Add(child);

                if (!force && Random.value < probabilityOfBeingLeaf)
                {
                    continue;
                }

                AddChildrenRecursive(child, Random.Range(minNumChildren, maxNumChildren), false, numTotalElements,
                    ref allowedDepth, treeElements);
            }
        }
    }
}
