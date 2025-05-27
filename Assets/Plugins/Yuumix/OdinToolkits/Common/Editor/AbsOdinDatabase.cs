using Sirenix.OdinInspector;
using UnityEditor;
using Yuumix.OdinToolkits.YuumiEditor;

namespace Yuumix.OdinToolkits.Common.Editor
{
    public abstract class AbsOdinDatabase<T> : SerializedScriptableObject where T : AbsOdinDatabase<T>
    {
        [PropertyOrder(980)]
        [TitleGroup("工具数据库通用操作")]
        [ButtonGroup("工具数据库通用操作/Split")]
        [Button("清空工具数据库",
            ButtonSizes.Medium, Icon = SdfIconType.ArrowCounterclockwise,
            IconAlignment = IconAlignment.LeftOfText)]
        protected virtual void ClearDatabase() { }

        [PropertyOrder(990)]
        [TitleGroup("工具数据库通用操作")]
        [ButtonGroup("工具数据库通用操作/Split")]
        [Button("初始化数据库",
            ButtonSizes.Medium, Icon = SdfIconType.App,
            IconAlignment = IconAlignment.LeftOfText)]
        protected virtual void InitializeData() { }

        [PropertyOrder(1000)]
        [TitleGroup("工具数据库通用操作")]
        [ButtonGroup("工具数据库通用操作/Split")]
        [Button("锁定数据库原始脚本", ButtonSizes.Medium, Icon = SdfIconType.EyeFill)]
        protected void PingScript()
        {
#if UNITY_EDITOR
            EditorGUIUtility.PingObject(MonoScriptEditorUtil.GetMonoScript(typeof(T).Name));
#endif
        }
    }
}
