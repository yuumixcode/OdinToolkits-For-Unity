using Sirenix.OdinInspector;
using Sirenix.OdinInspector.Editor;
using Sirenix.Utilities;
using Sirenix.Utilities.Editor;
using UnityEditor;
using UnityEngine;
using Yuumix.OdinToolkits.Common.Editor;

namespace Yuumix.OdinToolkits.Modules.LearnArchive.OdinEditorWindow.Editor
{
    [TypeInfoBox("在 InspectObject 状态下的 Window 不能生效此方法，ShowToast 是 public 公共方法")]
    public class ShowToastLearnWindow : Sirenix.OdinInspector.Editor.OdinEditorWindow
    {
        [TitleGroup("ShowToast 方法参数配置")]
        [ShowInInspector]
        [LabelText("显示文本: ")]
        public const string Text = "这是一个 toast 消息";

        [TitleGroup("ShowToast 方法参数配置")]
        [LabelText("Toast 相对位置: ")]
        [EnumPaging]
        public ToastPosition toastPosition = ToastPosition.BottomLeft;

        [TitleGroup("ShowToast 方法参数配置")]
        [LabelText("前置图标: ")]
        public SdfIconType icon;

        [TitleGroup("ShowToast 方法参数配置")]
        [LabelText("背景颜色: ")]
        [InfoBox("初始透明度默认修正为 1")]
        public Color backGroundColor;

        [TitleGroup("ShowToast 方法参数配置")]
        [LabelText("显示时长: ")]
        [InfoBox("初始时间默认修正为 3 秒")]
        public float duration;

        protected override void OnEnable()
        {
            base.OnEnable();
            if (icon == SdfIconType.None)
            {
                icon = SdfIconType.Info;
            }

            if (duration == 0)
            {
                duration = 3f;
            }

            if (backGroundColor.a == 0)
            {
                backGroundColor.a = 1;
            }
        }

        [MenuItem(MenuItemSettings.ShowToastMethodMenuItemName,
            priority = MenuItemSettings.ShowToastMethodPriority)]
        static void OpenWindow()
        {
            var win = GetWindow<ShowToastLearnWindow>();
            win.titleContent = new GUIContent(MenuItemSettings.ShowToastMethodWindowName);
            win.position = GUIHelper.GetEditorWindowRect().AlignCenter(800, 600);
            win.Show();
        }

        [Button("根据配置触发 ShowToast 方法")]
        void Invoke()
        {
            ShowToast(toastPosition, icon, Text, backGroundColor, duration);
        }
    }
}
