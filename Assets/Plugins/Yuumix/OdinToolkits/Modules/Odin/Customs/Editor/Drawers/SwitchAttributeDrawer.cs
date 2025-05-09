using Sirenix.OdinInspector;
using Sirenix.OdinInspector.Editor;
using Sirenix.OdinInspector.Editor.ValueResolvers;
using Sirenix.Utilities;
using Sirenix.Utilities.Editor;
using System;
using UnityEditor;
using UnityEngine;
using YOGA.OdinToolkits.Modules.CustomExtensions.Runtime.Attributes;

namespace YOGA.OdinToolkits.Modules.CustomExtensions.Editor.Drawers
{
    public class SwitchAttributeDrawer : OdinAttributeDrawer<SwitchAttribute, bool>
    {
        private const float AnimationSpeedMultiplier = 6f;
        private const float SwitchWidth = 28f;
        private static readonly int ControlHint = "SwitchControlHint".GetHashCode();

        private ValueResolver<Color> _backgroundColorOnResolver;
        private ValueResolver<Color> _backgroundColorOffResolver;
        private ValueResolver<Color> _switchColorOnResolver;
        private ValueResolver<Color> _switchColorOffResolver;
        private Color backgroundColor;
        private Color switchColor;
        private float switchPosition;
        private Texture whiteTexture;
        private bool animating;
        private bool hasToggleLeftAttribute;

        protected override void Initialize()
        {
            _backgroundColorOnResolver = ValueResolver.Get<Color>(Property, Attribute.BackgroundColorOn);
            _backgroundColorOffResolver = ValueResolver.Get<Color>(Property, Attribute.BackgroundColorOff);
            _switchColorOnResolver = ValueResolver.Get<Color>(Property, Attribute.SwitchColorOn);
            _switchColorOffResolver = ValueResolver.Get<Color>(Property, Attribute.SwitchColorOff);

            var isOn = ValueEntry.SmartValue;
            backgroundColor = isOn ? _backgroundColorOnResolver.GetValue() : _backgroundColorOffResolver.GetValue();
            switchColor = isOn ? _switchColorOnResolver.GetValue() : _switchColorOffResolver.GetValue();
            switchPosition = isOn ? SwitchWidth * 0.5f : 0f;

            whiteTexture = Texture2D.whiteTexture;
            hasToggleLeftAttribute = Property.Attributes.HasAttribute<ToggleLeftAttribute>();
        }

        protected override void DrawPropertyLayout(GUIContent label)
        {
            ValueResolver.DrawErrors(
                _backgroundColorOnResolver,
                _backgroundColorOffResolver,
                _switchColorOnResolver,
                _switchColorOffResolver);

            var backgroundColorOn = _backgroundColorOnResolver.GetValue();
            var backgroundColorOff = _backgroundColorOffResolver.GetValue();
            var switchColorOn = _switchColorOnResolver.GetValue();
            var switchColorOff = _switchColorOffResolver.GetValue();

            var totalRect = EditorGUILayout.GetControlRect(label != null, EditorGUIUtility.singleLineHeight);

            if (label != null && !hasToggleLeftAttribute)
            {
                totalRect = EditorGUI.PrefixLabel(totalRect, label);
            }

            var switchBackgroundRect = Attribute.Alignment switch
            {
                SwitchAlignment.Left => totalRect.AlignLeft(SwitchWidth).AlignCenterY(SwitchWidth * 0.5f),
                SwitchAlignment.Right => totalRect.AlignRight(SwitchWidth).AlignCenterY(SwitchWidth * 0.5f),
                SwitchAlignment.Center => totalRect.AlignCenterX(SwitchWidth).AlignCenterY(SwitchWidth * 0.5f),
                _ => throw new ArgumentException(
                    "Invalid enum argument possible values are:\n- SwitchAlignment.Left\n- SwitchAlignment.Right\n- SwitchAlignment.Center")
            };

            var evt = Event.current;
            var isOn = ValueEntry.SmartValue;
            var controlID = GUIUtility.GetControlID(ControlHint, FocusType.Keyboard, switchBackgroundRect);
            var hasKeyboardFocus = GUIUtility.keyboardControl == controlID;
            var targetBackgroundColor = isOn ? backgroundColorOn : backgroundColorOff;
            var targetSwitchColor = isOn ? switchColorOn : switchColorOff;

            if (ColorHasChanged(targetBackgroundColor, targetSwitchColor))
            {
                animating = true;
            }

            if (evt.type == EventType.Layout && animating)
            {
                backgroundColor = backgroundColor.MoveTowards(
                    targetBackgroundColor,
                    GUITimeHelper.LayoutDeltaTime * AnimationSpeedMultiplier);

                switchColor = switchColor.MoveTowards(
                    targetSwitchColor,
                    GUITimeHelper.LayoutDeltaTime * AnimationSpeedMultiplier);

                var targetSwitchPosition = isOn ? SwitchWidth * 0.5f : 0f;

                switchPosition = Mathf.MoveTowards(
                    switchPosition,
                    targetSwitchPosition,
                    GUITimeHelper.LayoutDeltaTime * AnimationSpeedMultiplier * SwitchWidth * 0.5f);

                if (backgroundColor == targetBackgroundColor
                    && switchColor == targetSwitchColor
                    && Mathf.Approximately(switchPosition, targetSwitchPosition))
                {
                    animating = false;
                }

                GUIHelper.RequestRepaint();
            }
            else if (evt.OnMouseDown(switchBackgroundRect, 0))
            {
                GUIUtility.hotControl = controlID;
                GUIUtility.keyboardControl = controlID;
            }
            else if (evt.OnMouseUp(switchBackgroundRect, 0))
            {
                GUIUtility.hotControl = 0;
                GUIUtility.keyboardControl = 0;
                ChangeValueTo(!isOn);
            }
            else if (hasKeyboardFocus && evt.type == EventType.KeyDown)
            {
                switch (evt.keyCode)
                {
                    case KeyCode.Return:
                    case KeyCode.Space:
                        ChangeValueTo(!isOn);
                        break;
                    case KeyCode.LeftArrow:
                        ChangeValueTo(false);
                        break;
                    case KeyCode.RightArrow:
                        ChangeValueTo(true);
                        break;
                }
            }

            var finalBackgroundColor = hasKeyboardFocus ? Darken(backgroundColor, 1.5f) : backgroundColor;
            var borderRadius = Attribute.Rounded ? 99f : 0f;
            GUI.DrawTexture(switchBackgroundRect, whiteTexture, ScaleMode.StretchToFill, true, 0f, finalBackgroundColor, 0f,
                borderRadius);

            var finalSwitchColor = hasKeyboardFocus ? Darken(switchColor, 1.5f) : switchColor;
            var switchRect = switchBackgroundRect.SetWidth(SwitchWidth * 0.5f).Padding(SwitchWidth * 0.07f).AddX(switchPosition);
            GUI.DrawTexture(switchRect, whiteTexture, ScaleMode.StretchToFill, true, 0f, finalSwitchColor, 0f, borderRadius);

            if (hasToggleLeftAttribute)
            {
                EditorGUI.LabelField(totalRect.AddX(SwitchWidth + 4f), label);
            }
        }

        private void ChangeValueTo(bool newValue)
        {
            ValueEntry.SmartValue = newValue;
            animating = true;
        }

        private bool ColorHasChanged(Color targetBackgroundColor, Color targetSwitchColor)
        {
            return backgroundColor != targetBackgroundColor || switchColor != targetSwitchColor;
        }

        private Color Darken(Color color, float factor)
        {
            return new Color(color.r / factor, color.g / factor, color.b / factor);
        }
    }
}
