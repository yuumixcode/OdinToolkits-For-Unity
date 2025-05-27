using System;
using System.Collections.Generic;
using System.Linq;
using UnityEditor;
using UnityEditor.IMGUI.Controls;
using UnityEngine;
using Yuumix.OdinToolkits.Modules.LearnArchive.EditorExtensions.IMGUI.TreeViewExamples.Assets.Editor.TreeDataModel;
using Object = UnityEngine.Object;

namespace Yuumix.OdinToolkits.Modules.LearnArchive.EditorExtensions.IMGUI.TreeViewExamples.Assets.Editor.TreeViewExamples.
    BackendData
{
    internal class TreeViewItem<T> : TreeViewItem where T : TreeElement
    {
        public TreeViewItem(int id, int depth, string displayName, T data) : base(id, depth, displayName) => this.data = data;

        public T data { get; set; }
    }

    internal class TreeViewWithTreeModel<T> : TreeView where T : TreeElement
    {
        // Dragging
        //-----------

        const string k_GenericDragID = "GenericDragColumnDragging";
        readonly List<TreeViewItem> m_Rows = new List<TreeViewItem>(100);

        public TreeViewWithTreeModel(TreeViewState state, TreeModel<T> model) : base(state)
        {
            Init(model);
        }

        public TreeViewWithTreeModel(TreeViewState state, MultiColumnHeader multiColumnHeader, TreeModel<T> model)
            : base(state, multiColumnHeader)
        {
            Init(model);
        }

        public TreeModel<T> treeModel { get; private set; }

        public event Action treeChanged;
        public event Action<IList<TreeViewItem>> beforeDroppingDraggedItems;

        void Init(TreeModel<T> model)
        {
            treeModel = model;
            treeModel.modelChanged += ModelChanged;
        }

        void ModelChanged()
        {
            if (treeChanged != null)
            {
                treeChanged();
            }

            Reload();
        }

        protected override TreeViewItem BuildRoot()
        {
            var depthForHiddenRoot = -1;
            return new TreeViewItem<T>(treeModel.root.id, depthForHiddenRoot, treeModel.root.name, treeModel.root);
        }

        protected override IList<TreeViewItem> BuildRows(TreeViewItem root)
        {
            if (treeModel.root == null)
            {
                Debug.LogError("tree model root is null. did you call SetData()?");
            }

            m_Rows.Clear();
            if (!string.IsNullOrEmpty(searchString))
            {
                Search(treeModel.root, searchString, m_Rows);
            }
            else
            {
                if (treeModel.root.hasChildren)
                {
                    AddChildrenRecursive(treeModel.root, 0, m_Rows);
                }
            }

            // We still need to setup the child parent information for the rows since this 
            // information is used by the TreeView internal logic (navigation, dragging etc)
            SetupParentsAndChildrenFromDepths(root, m_Rows);

            return m_Rows;
        }

        void AddChildrenRecursive(T parent, int depth, IList<TreeViewItem> newRows)
        {
            foreach (T child in parent.children)
            {
                var item = new TreeViewItem<T>(child.id, depth, child.name, child);
                newRows.Add(item);

                if (child.hasChildren)
                {
                    if (IsExpanded(child.id))
                    {
                        AddChildrenRecursive(child, depth + 1, newRows);
                    }
                    else
                    {
                        item.children = CreateChildListForCollapsedParent();
                    }
                }
            }
        }

        void Search(T searchFromThis, string search, List<TreeViewItem> result)
        {
            if (string.IsNullOrEmpty(search))
            {
                throw new ArgumentException("Invalid search: cannot be null or empty", "search");
            }

            const int kItemDepth = 0; // tree is flattened when searching

            var stack = new Stack<T>();
            foreach (var element in searchFromThis.children)
            {
                stack.Push((T)element);
            }

            while (stack.Count > 0)
            {
                var current = stack.Pop();
                // Matches search?
                if (current.name.IndexOf(search, StringComparison.OrdinalIgnoreCase) >= 0)
                {
                    result.Add(new TreeViewItem<T>(current.id, kItemDepth, current.name, current));
                }

                if (current.children != null && current.children.Count > 0)
                {
                    foreach (var element in current.children)
                    {
                        stack.Push((T)element);
                    }
                }
            }

            SortSearchResult(result);
        }

        protected virtual void SortSearchResult(List<TreeViewItem> rows)
        {
            rows.Sort((x, y) =>
                EditorUtility.NaturalCompare(x.displayName,
                    y.displayName)); // sort by displayName by default, can be overriden for multicolumn solutions
        }

        protected override IList<int> GetAncestors(int id) => treeModel.GetAncestors(id);

        protected override IList<int> GetDescendantsThatHaveChildren(int id) => treeModel.GetDescendantsThatHaveChildren(id);

        protected override bool CanStartDrag(CanStartDragArgs args) => true;

        protected override void SetupDragAndDrop(SetupDragAndDropArgs args)
        {
            if (hasSearch)
            {
                return;
            }

            DragAndDrop.PrepareStartDrag();
            var draggedRows = GetRows().Where(item => args.draggedItemIDs.Contains(item.id)).ToList();
            DragAndDrop.SetGenericData(k_GenericDragID, draggedRows);
            DragAndDrop.objectReferences = new Object[] { }; // this IS required for dragging to work
            var title = draggedRows.Count == 1 ? draggedRows[0].displayName : "< Multiple >";
            DragAndDrop.StartDrag(title);
        }

        protected override DragAndDropVisualMode HandleDragAndDrop(DragAndDropArgs args)
        {
            // Check if we can handle the current drag data (could be dragged in from other areas/windows in the editor)
            var draggedRows = DragAndDrop.GetGenericData(k_GenericDragID) as List<TreeViewItem>;
            if (draggedRows == null)
            {
                return DragAndDropVisualMode.None;
            }

            // Parent item is null when dragging outside any tree view items.
            switch (args.dragAndDropPosition)
            {
                case DragAndDropPosition.UponItem:
                case DragAndDropPosition.BetweenItems:
                {
                    var validDrag = ValidDrag(args.parentItem, draggedRows);
                    if (args.performDrop && validDrag)
                    {
                        var parentData = ((TreeViewItem<T>)args.parentItem).data;
                        OnDropDraggedElementsAtIndex(draggedRows, parentData, args.insertAtIndex == -1 ? 0 : args.insertAtIndex);
                    }

                    return validDrag ? DragAndDropVisualMode.Move : DragAndDropVisualMode.None;
                }

                case DragAndDropPosition.OutsideItems:
                {
                    if (args.performDrop)
                    {
                        OnDropDraggedElementsAtIndex(draggedRows, treeModel.root, treeModel.root.children.Count);
                    }

                    return DragAndDropVisualMode.Move;
                }
                default:
                    Debug.LogError("Unhandled enum " + args.dragAndDropPosition);
                    return DragAndDropVisualMode.None;
            }
        }

        public virtual void OnDropDraggedElementsAtIndex(List<TreeViewItem> draggedRows, T parent, int insertIndex)
        {
            if (beforeDroppingDraggedItems != null)
            {
                beforeDroppingDraggedItems(draggedRows);
            }

            var draggedElements = new List<TreeElement>();
            foreach (var x in draggedRows)
            {
                draggedElements.Add(((TreeViewItem<T>)x).data);
            }

            var selectedIDs = draggedElements.Select(x => x.id).ToArray();
            treeModel.MoveElements(parent, insertIndex, draggedElements);
            SetSelection(selectedIDs, TreeViewSelectionOptions.RevealAndFrame);
        }

        bool ValidDrag(TreeViewItem parent, List<TreeViewItem> draggedItems)
        {
            var currentParent = parent;
            while (currentParent != null)
            {
                if (draggedItems.Contains(currentParent))
                {
                    return false;
                }

                currentParent = currentParent.parent;
            }

            return true;
        }
    }
}
