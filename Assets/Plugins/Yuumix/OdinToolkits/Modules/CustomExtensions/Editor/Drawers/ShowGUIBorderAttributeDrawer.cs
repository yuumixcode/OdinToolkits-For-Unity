using Sirenix.OdinInspector.Editor;
using UnityEngine;
using Yuumix.OdinToolkits.Modules.CustomExtensions.Attributes;
using Yuumix.OdinToolkits.YuumiEditor;

namespace Yuumix.OdinToolkits.Modules.CustomExtensions.Editor.Drawers
{
    [DrawerPriority(0, 10)]
    public class ShowGUIBorderAttributeDrawer : OdinAttributeDrawer<ShowGUIBorderAttribute>
    {
        protected override void DrawPropertyLayout(GUIContent label)
        {
            CallNextDrawer(label);
            YuumiEditorGUIUtil.DrawRectOutlineWithBorder(GUILayoutUtility.GetLastRect(), Color.green);
        }
    }
}
