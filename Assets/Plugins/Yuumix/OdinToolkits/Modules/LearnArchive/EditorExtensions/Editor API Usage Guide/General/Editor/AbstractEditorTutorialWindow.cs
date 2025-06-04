using Sirenix.OdinInspector;
using Sirenix.OdinInspector.Editor;
using Sirenix.Utilities.Editor;
using UnityEditor;
using UnityEngine;
using Yuumix.OdinToolkits.Common.YuumixEditor;

namespace Yuumix.OdinToolkits.Modules.LearnArchive.EditorExtensions.Editor_API_Usage_Guide.General.Editor
{
    public abstract class AbstractEditorTutorialWindow<T> : Sirenix.OdinInspector.Editor.OdinEditorWindow where T : AbstractEditorTutorialWindow<T>
    {
        public OdinMenuEditorWindow OwnerWindow { get; set; }

        [PropertyOrder(-1)]
        [TitleGroup("案例介绍")]
        [ShowInInspector, EnableGUI, HideLabel]
        [DisplayAsString(TextAlignment.Left, Overflow = false, FontSize = 13, EnableRichText = true)]
        public string UsageTip
        {
            get
            {
                GUIHelper.RequestRepaint();
                return SetUsageTip();
            }
        }

        [PropertyOrder(1000)]
        [TitleGroup("锁定脚本工具")]
        [Button("锁定脚本", ButtonSizes.Medium, Icon = SdfIconType.EyeFill)]
        protected void PingScript()
        {
#if UNITY_EDITOR
            EditorGUIUtility.PingObject(MonoScriptEditorUtil.GetMonoScript(typeof(T).Name));
#endif
        }

        protected abstract string SetUsageTip();
    }
}
