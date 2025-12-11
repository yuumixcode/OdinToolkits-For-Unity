using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;
using Yuumix.OdinToolkits.Core;
using Yuumix.OdinToolkits.Core.Editor;
using Yuumix.OdinToolkits.Core.SafeEditor;

namespace Yuumix.OdinToolkits.Module.Editor
{
    /// <summary>
    /// MenuItemViewer 可视化面板
    /// </summary>
    public class MenuItemViewerVisualPanelSO : OdinEditorScriptableSingleton<MenuItemViewerVisualPanelSO>,
        IOdinToolkitsEditorReset
    {
        public static BilingualData ToolMenuPath = new BilingualData("菜单项检查器", "MenuItemViewer");

        #region Event Functions

        void OnEnable()
        {
            headerWidget = new BilingualHeaderWidget("MenuItem 查看器", "MenuItem Viewer",
                "查看项目内的 MenuItem 的信息，便于规划菜单项",
                "View the information of MenuItems within the project to facilitate menu item planning",
                OdinToolkitsWebLinks.OFFICIAL_WEBSITE);
        }

        #endregion

        #region IOdinToolkitsEditorReset Members

        public void EditorReset()
        {
            assemblyFilter = null;
            menuItemInfos = null;
        }

        #endregion

        [PropertySpace(8, 8)]
        [BilingualButton("搜集项目所有菜单项，排除筛选项", "Collect MenuItems Exclude Filter", ButtonSizes.Large)]
        public void CollectMenuItems()
        {
            menuItemInfos = MenuItemViewerController.GetAllMenuItems(assemblyFilter);
        }

        #region Serialized Fields

        public BilingualHeaderWidget headerWidget;

        [PropertySpace]
        [SerializeReference]
        public IAssemblyFilter assemblyFilter;

        [PropertyOrder(10)]
        [Searchable(FilterOptions = SearchFilterOptions.ISearchFilterableInterface)]
        public List<MenuItemInfo> menuItemInfos;

        #endregion
    }
}
