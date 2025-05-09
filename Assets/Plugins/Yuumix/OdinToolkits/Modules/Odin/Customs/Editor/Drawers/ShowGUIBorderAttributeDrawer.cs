using Sirenix.OdinInspector.Editor;
using UnityEngine;
using YOGA.OdinToolkits.Modules.CustomExtensions.Runtime.Attributes;
using Yuumix.OdinToolkits.Modules.Utilities.YuumiEditor;

namespace YOGA.OdinToolkits.Modules.CustomExtensions.Editor.Drawers
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