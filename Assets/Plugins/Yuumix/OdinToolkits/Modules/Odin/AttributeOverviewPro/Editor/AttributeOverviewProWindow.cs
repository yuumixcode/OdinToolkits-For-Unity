using Sirenix.OdinInspector.Editor;
using Sirenix.Utilities;
using Sirenix.Utilities.Editor;
using UnityEditor;
using UnityEngine;

namespace Yuumix.OdinToolkits.Modules.Odin.AttributeOverviewPro.Editor
{
    public class AttributeOverviewProWindow : OdinMenuEditorWindow
    {
        [MenuItem("Tools/Odin Toolkits/Attribute Overview Pro", false, 1000)]
        private static void ShowWindow()
        {
            var window = GetWindow<AttributeOverviewProWindow>();
            window.titleContent = new GUIContent("AttributeOverview Pro");
            window.position = GUIHelper.GetEditorWindowRect().AlignCenter(800, 700);
            window.Show();
        }

        protected override OdinMenuTree BuildMenuTree() => new OdinMenuTree();
    }
}
