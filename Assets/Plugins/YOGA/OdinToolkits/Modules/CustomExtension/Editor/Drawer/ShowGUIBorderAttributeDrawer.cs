using Sirenix.OdinInspector.Editor;
using UnityEngine;
using YOGA.OdinToolkits.CustomExtension.Attributes;
using Yoga.Shared.Utility;
using Yoga.Shared.Utility.YuumiEditor;

namespace YOGA.Modules.OdinToolkits.Attributes.Editor
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