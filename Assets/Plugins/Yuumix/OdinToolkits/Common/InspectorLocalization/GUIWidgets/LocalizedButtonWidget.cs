using Sirenix.OdinInspector;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Reflection;
using Yuumix.OdinToolkits.Common.InspectorLocalization;
#if UNITY_EDITOR
using Sirenix.OdinInspector.Editor;
#endif

namespace Yuumix.OdinToolkits.Common.InspectorLocalization
{
    /// <summary>
    /// 多语言按钮部件，以字段形式实现多语言按钮支持<br />
    /// 构造函数中赋值无参方法，推荐静态方法。<br />
    /// 添加 LocalizedButtonConfigAttribute 进行 Button 样式设置。<br />
    /// 支持兼容 Odin Inspector 的绘制系统<br />
    /// 支持实时语言切换
    /// </summary>
    /// <remarks>
    ///     <c>2025/06/15 Yuumix Zeus 确认注释</c><br />
    /// </remarks>
    [Serializable]
    [InlineProperty]
    [HideLabel]
    [LocalizedComment("本地化按钮组件，用于多语言显示按钮，支持 Odin Inspector 的绘制系统",
        "Localized button widget, used to display buttons in multiple languages，Supports Odin Inspector Drawing Systems")]
    public class LocalizedButtonWidget
    {
        Action _targetMethod;

        [LocalizedComment("按钮触发时执行的方法，推荐静态方法",
            "Methods to execute when the button is triggered, static methods are recommended")]
        public LocalizedButtonWidget(Action action) => _targetMethod = action;
        
        bool IsChinese => InspectorLocalizationManagerSO.IsChinese;
        bool IsEnglish => InspectorLocalizationManagerSO.IsEnglish;

        [ShowIf(nameof(IsChinese), false)]
        [Conditional("UNITY_EDITOR")]
        public void ChineseButton()
        {
            _targetMethod.Invoke();
        }

        [ShowIf(nameof(IsEnglish), false)]
        [Conditional("UNITY_EDITOR")]
        public void EnglishButton()
        {
            _targetMethod.Invoke();
        }
    }

#if UNITY_EDITOR
    internal class LocalizedButtonProcessor : OdinAttributeProcessor<LocalizedButtonWidget>
    {
        public override void ProcessChildMemberAttributes(InspectorProperty parentProperty, MemberInfo member,
            List<Attribute> attributes)
        {
            switch (member.Name)
            {
                case nameof(LocalizedButtonWidget.ChineseButton):
                {
                    var config = parentProperty.GetAttribute<LocalizedButtonWidgetConfigAttribute>().Data;
                    attributes.Add(ButtonAttributeData.CreateChineseButtonAttribute(config));
                    break;
                }
                case nameof(LocalizedButtonWidget.EnglishButton):
                {
                    var config = parentProperty.GetAttribute<LocalizedButtonWidgetConfigAttribute>().Data;
                    attributes.Add(ButtonAttributeData.CreateEnglishButtonAttribute(config));
                    break;
                }
            }
        }
    }
#endif
}
