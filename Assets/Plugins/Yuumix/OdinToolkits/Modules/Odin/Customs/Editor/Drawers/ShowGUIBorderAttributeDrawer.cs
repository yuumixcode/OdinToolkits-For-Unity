using Sirenix.OdinInspector.Editor;
using UnityEngine;
using Yuumix.OdinToolkits.Modules.Odin.Customs.Runtime.Attributes;
using Yuumix.OdinToolkits.Modules.Utilities.YuumiEditor;

namespace Yuumix.OdinToolkits.Modules.Odin.Customs.Editor.Drawers
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