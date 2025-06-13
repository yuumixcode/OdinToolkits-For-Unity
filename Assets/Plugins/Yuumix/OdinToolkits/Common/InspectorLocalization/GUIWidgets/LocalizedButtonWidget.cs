using Sirenix.OdinInspector;
using Sirenix.OdinInspector.Editor;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Reflection;
using Yuumix.OdinToolkits.Common.InspectorLocalization.Attributes.WidgetConfigs;
using Yuumix.OdinToolkits.Common.InspectorLocalization.Structs;

namespace Yuumix.OdinToolkits.Common.InspectorLocalization.GUIWidgets
{
    /// <summary>
    /// 本地化按钮自定义类，以字段或者属性的形式实现多语言本地化按钮支持，完美兼容 Odin Inspector 的绘制系统，支持实时语言切换。
    /// 构造函数中赋值无参方法，推荐静态方法。添加 LocalizedButtonConfigAttribute 进行 Button 样式设置。
    /// </summary>
    [Serializable]
    [InlineProperty]
    [HideLabel]
    public class LocalizedButtonWidget
    {
        InspectorLocalizationManagerSO LanguageLocalizationManager => InspectorLocalizationManagerSO.Instance;
        bool IsChinese => LanguageLocalizationManager.IsChinese;
        bool IsEnglish => LanguageLocalizationManager.IsEnglish;

        Action _targetMethod;

        public LocalizedButtonWidget(Action action) => _targetMethod = action;

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
    internal class LocalizedToolButtonProcessor : OdinAttributeProcessor<LocalizedButtonWidget>
    {
        public override void ProcessChildMemberAttributes(InspectorProperty parentProperty, MemberInfo member,
            List<Attribute> attributes)
        {
            if (member.Name == nameof(LocalizedButtonWidget.ChineseButton))
            {
                var config = parentProperty.GetAttribute<LocalizedButtonWidgetConfigAttribute>().Config;
                attributes.Add(ButtonAttributeConfig.CreateChineseButtonAttribute(config));
            }
            else if (member.Name == nameof(LocalizedButtonWidget.EnglishButton))
            {
                var config = parentProperty.GetAttribute<LocalizedButtonWidgetConfigAttribute>().Config;
                attributes.Add(ButtonAttributeConfig.CreateEnglishButtonAttribute(config));
            }
        }
    }
#endif
}
