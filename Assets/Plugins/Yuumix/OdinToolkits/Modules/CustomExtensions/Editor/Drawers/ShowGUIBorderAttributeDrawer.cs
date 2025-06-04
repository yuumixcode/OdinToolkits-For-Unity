using Sirenix.OdinInspector.Editor;
using UnityEngine;
using Yuumix.OdinToolkits.Common.YuumixEditor;
using Yuumix.OdinToolkits.Modules.CustomExtensions.Attributes;

namespace Yuumix.OdinToolkits.Modules.CustomExtensions.Editor.Drawers
{
    [DrawerPriority(0, 10)]
    public class ShowGUIBorderAttributeDrawer : OdinAttributeDrawer<ShowGUIBorderAttribute>
    {
        protected override void DrawPropertyLayout(GUIContent label)
        {
            CallNextDrawer(label);
            YuumixEditorGUIUtil.DrawRectOutlineWithBorder(GUILayoutUtility.GetLastRect(), Color.green);
        }
    }
}
