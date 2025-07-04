using Sirenix.OdinInspector;
using Sirenix.OdinInspector.Editor;
using Sirenix.OdinInspector.Editor.ValueResolvers;
using Sirenix.Utilities.Editor;
using System;
using UnityEditor;
using UnityEngine;

namespace Yuumix.OdinToolkits.Common.InspectorMultiLanguage.Editor
{
    [DrawerPriority(DrawerPriorityLevel.WrapperPriority)]
    public class MultiLanguageButtonAttributeDrawer : OdinAttributeDrawer<MultiLanguageButtonAttribute>
    {
        ButtonAttribute _buttonAttribute;
        ValueResolver<string> _chineseGetter;
        ValueResolver<string> _englishGetter;

        protected override void Initialize()
        {
            _buttonAttribute = Property.GetAttribute<ButtonAttribute>();
            _chineseGetter = ValueResolver.GetForString(Property, Attribute.ChineseName);
            _englishGetter = ValueResolver.GetForString(Property, Attribute.EnglishName);
            // _buttonAttribute.Name =
            //     $"@InspectorMultiLanguageManagerSO.IsChinese ? \"{_chineseGetter.GetValue()}\" : \"{_englishGetter.GetValue()}\"";
            // InvalidArchive();
        }

        protected override void DrawPropertyLayout(GUIContent label)
        {
            _buttonAttribute.Name =
                $"@InspectorMultiLanguageManagerSO.IsChinese ? \"{_chineseGetter.GetValue()}\" : \"{_englishGetter.GetValue()}\"";
            CallNextDrawer(label);
        }

        [Obsolete]
        void InvalidArchive()
        {
            if (InspectorMultiLanguageManagerSO.IsChinese)
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
