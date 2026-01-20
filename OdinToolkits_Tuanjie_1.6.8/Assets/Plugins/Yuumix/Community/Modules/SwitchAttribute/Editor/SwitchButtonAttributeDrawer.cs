using Sirenix.OdinInspector;
using Sirenix.OdinInspector.Editor;
using Sirenix.OdinInspector.Editor.ValueResolvers;
using Sirenix.Utilities;
using Sirenix.Utilities.Editor;
using System;
using UnityEditor;
using UnityEngine;

namespace Yuumix.Community.SwitchAttribute.Editor
{
    public class SwitchButtonAttributeDrawer : OdinAttributeDrawer<SwitchButtonAttribute, bool>
    {
        const float ANIMATION_SPEED_MULTIPLIER = 6f;

        const int SWITCH_WIDTH = 28;

        // 控件唯一标识哈希值（用于GUIUtility.hotControl）
        static readonly int ControlHint = "SwitchControlHint".GetHashCode();

        bool _animating;

        Color _backgroundColor;

        bool _hasToggleLeftAttribute;

        Color _switchColor;

        float _switchPosition;

        Texture _whiteTexture;

        protected override void Initialize()
        {
            _backgroundColorOnResolver = ValueResolver.Get<Color>(Property, Attribute.BackgroundColorOn);
            _backgroundColorOffResolver = ValueResolver.Get<Color>(Property, Attribute.BackgroundColorOff);
            _switchColorOnResolver = ValueResolver.Get<Color>(Property, Attribute.SwitchColorOn);
            _switchColorOffResolver = ValueResolver.Get<Color>(Property, Attribute.SwitchColorOff);
            var isOn = ValueEntry.SmartValue;
            _backgroundColor = isOn ? _backgroundColorOnResolver.GetValue() : _backgroundColorOffResolver.GetValue();
            _switchColor = isOn ? _switchColorOnResolver.GetValue() : _switchColorOffResolver.GetValue();
            _switchPosition = isOn ? SWITCH_WIDTH * 0.5f : 0f;
            _whiteTexture = Texture2D.whiteTexture;
            _hasToggleLeftAttribute = Property.Attributes.HasAttribute<ToggleLeftAttribute>();
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
            if (label != null && !_hasToggleLeftAttribute)
            {
                totalRect = EditorGUI.PrefixLabel(totalRect, label);
            }

            const float preHeight = SWITCH_WIDTH * 0.5f;
            var height = preHeight < totalRect.height ? preHeight : totalRect.height;
            // 根据对齐方式计算开关背景区域
            var switchBackgroundRect = Attribute.Alignment switch
            {
                SwitchAlignment.Left => totalRect.AlignLeft(SWITCH_WIDTH).AlignCenterY(height),
                SwitchAlignment.Right => totalRect.AlignRight(SWITCH_WIDTH).AlignCenterY(height),
                SwitchAlignment.Center => totalRect.AlignCenterX(SWITCH_WIDTH).AlignCenterY(height),
                _ => throw new ArgumentException("Invalid alignment")
            };

            var evt = Event.current;
            var controlID = GUIUtility.GetControlID(ControlHint, FocusType.Passive, switchBackgroundRect);
            // 当前开关状态
            var isOn = ValueEntry.SmartValue;
            var targetBackgroundColor = isOn ? backgroundColorOn : backgroundColorOff;
            var targetSwitchColor = isOn ? switchColorOn : switchColorOff;
            if (ColorNeedChanged(targetBackgroundColor, targetSwitchColor))
            {
                _animating = true;
            }

            // 布局阶段处理动画
            if (evt.type == EventType.Layout && _animating)
            {
                _backgroundColor = _backgroundColor.MoveTowards(
                    targetBackgroundColor,
                    GUITimeHelper.LayoutDeltaTime * ANIMATION_SPEED_MULTIPLIER);
                _switchColor = _switchColor.MoveTowards(
                    targetSwitchColor,
                    GUITimeHelper.LayoutDeltaTime * ANIMATION_SPEED_MULTIPLIER);
                var targetSwitchPosition = isOn ? SWITCH_WIDTH * 0.5f : 0f;
                _switchPosition = Mathf.MoveTowards(
                    _switchPosition,
                    targetSwitchPosition,
                    GUITimeHelper.LayoutDeltaTime * ANIMATION_SPEED_MULTIPLIER * SWITCH_WIDTH * 0.5f);

                // 动画结束条件检查
                if (_backgroundColor == targetBackgroundColor
                    && _switchColor == targetSwitchColor
                    && Mathf.Approximately(_switchPosition, targetSwitchPosition))
                {
                    _animating = false;
                }

                GUIHelper.RequestRepaint();
            }
            else if (evt.OnMouseDown(switchBackgroundRect, 0)) // 鼠标按下事件
            {
                // 获取焦点并标记为脏数据
                GUIUtility.hotControl = controlID;
            }
            else if (evt.OnMouseUp(switchBackgroundRect, 0) && GUIUtility.hotControl == controlID) // 鼠标抬起事件
            {
                // 移除焦点并触发状态切换
                GUIUtility.hotControl = 0;
                ChangeValueTo(!isOn);
            }

            // 计算最终背景颜色（考虑焦点状态）
            var finalBackgroundColor = _backgroundColor;
            var borderRadius = Attribute.Rounded ? 99f : 0f;
            GUI.DrawTexture(switchBackgroundRect, _whiteTexture, ScaleMode.StretchToFill, true, 0f,
                finalBackgroundColor, 0f, borderRadius);
            var finalSwitchColor = _switchColor;
            // 计算开关滑块矩形区域
            var switchRect = switchBackgroundRect
                .SetWidth(SWITCH_WIDTH * 0.5f)
                .Padding(SWITCH_WIDTH * 0.07f)
                .AddX(_switchPosition);
            GUI.DrawTexture(switchRect, _whiteTexture, ScaleMode.StretchToFill, true, 0f,
                finalSwitchColor, 0f, borderRadius);
            if (_hasToggleLeftAttribute)
            {
                EditorGUI.LabelField(totalRect.AddX(SWITCH_WIDTH + 4f), label);
            }
        }

        void ChangeValueTo(bool newValue)
        {
            ValueEntry.SmartValue = newValue;
            _animating = true;
        }

        bool ColorNeedChanged(Color targetBackgroundColor, Color targetSwitchColor) =>
            _backgroundColor != targetBackgroundColor || _switchColor != targetSwitchColor;

        #region Resolvers

        ValueResolver<Color> _backgroundColorOffResolver;
        ValueResolver<Color> _backgroundColorOnResolver;
        ValueResolver<Color> _switchColorOffResolver;
        ValueResolver<Color> _switchColorOnResolver;

        #endregion
    }
}
