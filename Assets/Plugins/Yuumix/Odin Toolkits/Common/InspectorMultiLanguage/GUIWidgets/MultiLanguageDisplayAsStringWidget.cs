using Sirenix.OdinInspector;
using System;
using System.Collections.Generic;
using System.Reflection;
#if UNITY_EDITOR
using Sirenix.OdinInspector.Editor;
#endif

namespace Yuumix.OdinToolkits.Common.InspectorMultiLanguage
{
    /// <summary>
    /// 多语言字符串显示部件，以字段的形式支持多语言
    /// </summary>
    /// <remarks>
    /// 支持兼容 Odin Inspector 绘制系统<br />
    /// 支持实时语言切换
    /// </remarks>
    [Serializable]
    [HideLabel]
    [InlineProperty]
    public class MultiLanguageDisplayAsStringWidget
    {
        public MultiLanguageDisplayAsStringWidget(string chinese, string english = null)
        {
            ChineseDisplay = chinese;
            EnglishDisplay = english ?? chinese;
        }

        [ShowIfChinese]
        [HideLabel]
        [ShowInInspector]
        [EnableGUI]
        public string ChineseDisplay { get; }

        [ShowIfEnglish]
        [HideLabel]
        [ShowInInspector]
        [EnableGUI]
        public string EnglishDisplay { get; }
    }

#if UNITY_EDITOR
    internal class MultiLanguageDisplayAsStringProcessor : OdinAttributeProcessor<MultiLanguageDisplayAsStringWidget>
    {
        public override void ProcessChildMemberAttributes(InspectorProperty parentProperty, MemberInfo member,
            List<Attribute> attributes)
        {
            switch (member.Name)
            {
                case nameof(MultiLanguageDisplayAsStringWidget.ChineseDisplay)
                    or nameof(MultiLanguageDisplayAsStringWidget.EnglishDisplay):
                    var config = parentProperty.GetAttribute<MultiLanguageDisplayWidgetConfigAttribute>();
                    if (config == null)
                    {
                        attributes.Add(new MultiLanguageInfoBoxAttribute(
                            "MultiLanguageDisplayAsStringWidget 字段必须添加 MultiLanguageDisplayAsStringWidgetConfigAttribute 才能生效",
                            "MultiLanguageDisplayAsStringWidget field must add MultiLanguageDisplayAsStringWidgetConfigAttribute to take effect"));
                        break;
                    }

                    attributes.Add(config.CreateDisplayAsStringAttribute());
                    break;
            }
        }
    }

#endif
}
