using Sirenix.OdinInspector;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Reflection;
#if UNITY_EDITOR
using Sirenix.OdinInspector.Editor;
#endif

namespace Yuumix.OdinToolkits.Core
{
    /// <summary>
    /// 多语言按钮，以字段形式实现多语言按钮支持<br />
    /// 必须添加 <c>MultiLanguageButtonWidgetConfigAttribute</c> 特性去设置按钮样式。<br />
    /// 构造函数中赋值无参方法，推荐静态方法。<br />
    /// </summary>
    /// <remarks>
    /// 支持兼容 Odin Inspector 的绘制系统<br />
    /// 支持实时语言切换
    /// </remarks>
    [Serializable]
    [InlineProperty]
    [HideLabel]
    [MultiLanguageComment("多语言按钮控件，以字段形式实现多语言按钮支持",
        "Multilingual button widget, which implements multilingual button support in the form of fields. Compatible with Odin Inspector")]
    public class MultiLanguageButtonProperty
    {
        Action _targetMethod;

        [MultiLanguageComment("按钮触发时执行的方法，推荐静态方法",
            "Methods to execute when the button is triggered, static methods are recommended")]
        public MultiLanguageButtonProperty(Action action) => _targetMethod = action;

        [ShowIfChinese]
        [Conditional("UNITY_EDITOR")]
        public void ChineseButton()
        {
            _targetMethod.Invoke();
        }

        [ShowIfEnglish]
        [Conditional("UNITY_EDITOR")]
        public void EnglishButton()
        {
            _targetMethod.Invoke();
        }
    }

#if UNITY_EDITOR
    internal class MultiLanguageButtonProcessor : OdinAttributeProcessor<MultiLanguageButtonProperty>
    {
        public override void ProcessChildMemberAttributes(InspectorProperty parentProperty, MemberInfo member,
            List<Attribute> attributes)
        {
            switch (member.Name)
            {
                case nameof(MultiLanguageButtonProperty.ChineseButton):
                {
                    var config = parentProperty.GetAttribute<MultiLanguageButtonWidgetConfigAttribute>();
                    if (config == null)
                    {
                        attributes.Add(new MultiLanguageInfoBoxAttribute(
                            "MultiLanguageButtonWidget 字段必须添加 MultiLanguageButtonWidgetConfigAttribute 才能生效",
                            "MultiLanguageButtonWidget field must add MultiLanguageButtonWidgetConfigAttribute to take effect"));
                        break;
                    }

                    attributes.Add(config.CreateChineseButton());
                    break;
                }
                case nameof(MultiLanguageButtonProperty.EnglishButton):
                {
                    var config = parentProperty.GetAttribute<MultiLanguageButtonWidgetConfigAttribute>();
                    if (config == null)
                    {
                        attributes.Add(new MultiLanguageInfoBoxAttribute(
                            "MultiLanguageButtonWidget 字段必须添加 MultiLanguageButtonWidgetConfigAttribute 才能生效",
                            "MultiLanguageButtonWidget field must add MultiLanguageButtonWidgetConfigAttribute to take effect"));
                        break;
                    }

                    attributes.Add(config.CreateEnglishButton());
                    break;
                }
            }
        }
    }
#endif
}
