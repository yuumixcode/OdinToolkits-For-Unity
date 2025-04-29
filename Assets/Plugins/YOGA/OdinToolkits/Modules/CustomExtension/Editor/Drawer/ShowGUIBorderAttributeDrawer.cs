using Plugins.YOGA.Common.Utility.YuumiEditor;
using Plugins.YOGA.OdinToolkits.Modules.CustomExtension.Runtime.Attributes.Custom;
using Sirenix.OdinInspector.Editor;
using UnityEngine;

namespace Plugins.YOGA.OdinToolkits.Modules.CustomExtension.Editor.Drawer
{
    [DrawerPriority(0, 10)]
    public class ShowGUIBorderAttributeDrawer : OdinAttributeDrawer<ShowGUIBorderAttribute>
    {
        protected override void DrawPropertyLayout(GUIContent label)
        {
            CallNextDrawer(label);
            YuumiEditorGUIUtility.DrawRectOutlineWithBorder(GUILayoutUtility.GetLastRect(), Color.green);
        }
    }
}