using Sirenix.OdinInspector.Editor;
using Sirenix.Utilities;
using Sirenix.Utilities.Editor;
using UnityEditor;
using Yuumix.OdinToolkits.Core.Editor;
using Vector4 = UnityEngine.Vector4;

namespace Yuumix.OdinToolkits.AttributeOverviewPro.Editor
{
    public class AttributeOverviewProWindow : OdinMenuEditorWindow
    {
        static AttributeOverviewProWindow _window;
        AttributeOverviewProDatabaseSO _databaseSO;
        OdinMenuTree _tree;

        #region Event Functions

        protected override void OnDestroy()
        {
            EditorApplication.delayCall -= EditorApplication_DelayCall;
            base.OnDestroy();
        }

        #endregion

        [MenuItem(OdinToolkitsMenuItems.OVERVIEW_PRO, false, OdinToolkitsMenuItems.OVERVIEW_PRO_PRIORITY)]
        public static void OpenWindow()
        {
            _window = GetWindow<AttributeOverviewProWindow>(OdinToolkitsMenuItems.OVERVIEW_PRO_WINDOW_NAME);
            _window.position = GUIHelper.GetEditorWindowRect().AlignCenter(1050, 750);
            _window.Show();
        }

        protected override void Initialize()
        {
            _databaseSO = AttributeOverviewProDatabaseSO.Instance;
            WindowPadding = new Vector4(15, 15, 15, 5);
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
                    Height = 24
                }
            };
            EditorApplication.delayCall -= EditorApplication_DelayCall;
            EditorApplication.delayCall += EditorApplication_DelayCall;
        }

        protected override OdinMenuTree BuildMenuTree()
        {
            foreach (var pair in _databaseSO.VisualPanelMap)
            {
                foreach (var visualPanelSO in pair.Value)
                {
                    var menuName = visualPanelSO.headerWidget.headerName.ChineseDisplay;
                    _tree.AddObjectAtPath(pair.Key + "/" + menuName, visualPanelSO);
                }
            }

            return _tree;
        }

        static void EditorApplication_DelayCall()
        {
            _window?.Repaint();
        }
    }
}
