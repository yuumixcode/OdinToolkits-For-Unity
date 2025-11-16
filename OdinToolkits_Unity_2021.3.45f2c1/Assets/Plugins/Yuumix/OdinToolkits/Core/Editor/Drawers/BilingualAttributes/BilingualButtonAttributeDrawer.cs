using Sirenix.OdinInspector;
using Sirenix.OdinInspector.Editor;
using Sirenix.OdinInspector.Editor.ValueResolvers;
using System;
using UnityEngine;

namespace Yuumix.OdinToolkits.Core.Editor
{
    [DrawerPriority(DrawerPriorityLevel.WrapperPriority)]
    public class BilingualButtonAttributeDrawer : OdinAttributeDrawer<BilingualButtonAttribute>
    {
        ButtonAttribute _buttonAttribute;
        ValueResolver<string> _chineseGetter;
        ValueResolver<string> _englishGetter;

        protected override void Initialize()
        {
            _buttonAttribute = Property.GetAttribute<ButtonAttribute>();
            _chineseGetter = ValueResolver.GetForString(Property, Attribute.ChineseName);
            _englishGetter = ValueResolver.GetForString(Property, Attribute.EnglishName);
            _buttonAttribute.Name =
                $"@{nameof(InspectorBilingualismConfigSO)}.IsChinese ? \"{_chineseGetter.GetValue()}\" : \"{_englishGetter.GetValue()}\"";
        }

        protected override void DrawPropertyLayout(GUIContent label)
        {
            CallNextDrawer(label);
        }

        [Obsolete]
        void InvalidArchive()
        {
            if (InspectorBilingualismConfigSO.IsChinese)
            {
                _buttonAttribute.Name = "测试";
            }
            else
            {
                _buttonAttribute.Name = "Test";
            }
        }
    }
}
