using Sirenix.OdinInspector;
using Sirenix.OdinInspector.Editor;
using Sirenix.OdinInspector.Editor.ValueResolvers;
using Sirenix.Utilities.Editor;
using UnityEditor;
using UnityEngine;

namespace Yuumix.OdinToolkits.Core.Editor
{
    [DrawerPriority(0.0, 10001.0)]
    public class BilingualInfoBoxAttributeDrawer : OdinAttributeDrawer<BilingualInfoBoxAttribute>
    {
        bool _drawMessageBox;
        ValueResolver<Color> _iconColorResolver;
        ValueResolver<string> _messageResolver;
        MessageType _messageType;
        ValueResolver<bool> _visibleIfResolver;

        protected override void Initialize()
        {
            _visibleIfResolver = ValueResolver.Get(Property, Attribute.VisibleIf, true);
            _messageResolver =
                ValueResolver.GetForString(Property, Attribute.BilingualData.GetCurrentOrFallback());
            _iconColorResolver = ValueResolver.Get(Property, Attribute.IconColor,
                EditorStyles.label.normal.textColor);
            _drawMessageBox = _visibleIfResolver.GetValue();
            switch (Attribute.InfoMessageType)
            {
                case InfoMessageType.Info:
                    _messageType = MessageType.Info;
                    break;
                case InfoMessageType.Warning:
                    _messageType = MessageType.Warning;
                    break;
                case InfoMessageType.Error:
                    _messageType = MessageType.Error;
                    break;
                default:
                    _messageType = MessageType.None;
                    break;
            }

            InspectorBilingualismConfigSO.OnLanguageChanged -= ReloadResolver;
            InspectorBilingualismConfigSO.OnLanguageChanged += ReloadResolver;
        }

        void ReloadResolver()
        {
            _messageResolver =
                ValueResolver.GetForString(Property, Attribute.BilingualData.GetCurrentOrFallback());
        }

        protected override void DrawPropertyLayout(GUIContent label)
        {
            var flag = true;
            if (_visibleIfResolver.HasError)
            {
                SirenixEditorGUI.ErrorMessageBox(_visibleIfResolver.ErrorMessage);
                flag = false;
            }

            if (_messageResolver.HasError)
            {
                SirenixEditorGUI.ErrorMessageBox(_messageResolver.ErrorMessage);
                flag = false;
            }

            if (_iconColorResolver.HasError)
            {
                SirenixEditorGUI.ErrorMessageBox(_iconColorResolver.ErrorMessage);
                flag = false;
            }

            if (flag)
            {
                if (Attribute.GUIAlwaysEnabled)
                {
                    GUIHelper.PushGUIEnabled(true);
                }

                if (Event.current.type == EventType.Layout)
                {
                    _drawMessageBox = _visibleIfResolver.GetValue();
                }

                if (_drawMessageBox)
                {
                    var message = _messageResolver.GetValue();
                    if (Attribute.HasDefinedIcon)
                    {
                        SirenixEditorGUI.IconMessageBox(message, Attribute.Icon,
                            _iconColorResolver.GetValue());
                    }
                    else
                    {
                        SirenixEditorGUI.MessageBox(message, _messageType);
                    }
                }

                if (Attribute.GUIAlwaysEnabled)
                {
                    GUIHelper.PopGUIEnabled();
                }
            }

            CallNextDrawer(label);
        }
    }
}
