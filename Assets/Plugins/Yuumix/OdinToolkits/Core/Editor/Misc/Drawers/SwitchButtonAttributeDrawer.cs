using System;
using Sirenix.OdinInspector;
using Sirenix.OdinInspector.Editor;
using Sirenix.OdinInspector.Editor.ValueResolvers;
using Sirenix.Utilities;
using Sirenix.Utilities.Editor;
using UnityEditor;
using UnityEngine;
using Yuumix.OdinToolkits.Core;

namespace Yuumix.OdinToolkits.Core.Editor
{
    public class SwitchButtonAttributeDrawer : OdinAttributeDrawer<SwitchButtonAttribute, bool>
    {
        // 动画速度系数（影响切换速度）
        const float AnimationSpeedMultiplier = 6f;

        // 开关控件宽度
        const int SwitchWidth = 28;

        // 控件唯一标识哈希值（用于GUIUtility.hotControl）
        static readonly int ControlHint = "SwitchControlHint".GetHashCode();

        // 是否正在执行动画
        bool _animating;

        // 当前状态的颜色缓存
        Color _backgroundColor;
        ValueResolver<Color> _backgroundColorOffResolver;

        // 颜色解析器（用于动态获取属性绑定的颜色值）
        ValueResolver<Color> _backgroundColorOnResolver;

        // 是否存在ToggleLeftAttribute特性
        bool _hasToggleLeftAttribute;

        Color _switchColor;
        ValueResolver<Color> _switchColorOffResolver;
        ValueResolver<Color> _switchColorOnResolver;

        // 开关位置（0-28px范围内移动）
        float _switchPosition;

        // 白色纹理（用于绘制背景和滑块）
        Texture _whiteTexture;

        // 初始化方法（仅在属性第一次绘制时调用）
        protected override void Initialize()
        {
            // 初始化颜色解析器（根据特性参数动态获取颜色）
            _backgroundColorOnResolver = ValueResolver.Get<Color>(Property, Attribute.BackgroundColorOn);
            _backgroundColorOffResolver = ValueResolver.Get<Color>(Property, Attribute.BackgroundColorOff);
            _switchColorOnResolver = ValueResolver.Get<Color>(Property, Attribute.SwitchColorOn);
            _switchColorOffResolver = ValueResolver.Get<Color>(Property, Attribute.SwitchColorOff);
            // 获取当前开关状态对应的颜色
            bool isOn = ValueEntry.SmartValue;
            _backgroundColor = isOn ? _backgroundColorOnResolver.GetValue() : _backgroundColorOffResolver.GetValue();
            _switchColor = isOn ? _switchColorOnResolver.GetValue() : _switchColorOffResolver.GetValue();
            // 初始化滑块位置（开状态居中，关状态左对齐）
            _switchPosition = isOn ? SwitchWidth * 0.5f : 0f;
            // 获取白色纹理（Unity内置资源）
            _whiteTexture = Texture2D.whiteTexture;
            // 检测是否存在ToggleLeftAttribute特性（影响标签布局）
            _hasToggleLeftAttribute = Property.Attributes.HasAttribute<ToggleLeftAttribute>();
        }

        protected override void DrawPropertyLayout(GUIContent label)
        {
            ValueResolver.DrawErrors(
                _backgroundColorOnResolver,
                _backgroundColorOffResolver,
                _switchColorOnResolver,
                _switchColorOffResolver);
            Color backgroundColorOn = _backgroundColorOnResolver.GetValue();
            Color backgroundColorOff = _backgroundColorOffResolver.GetValue();
            Color switchColorOn = _switchColorOnResolver.GetValue();
            Color switchColorOff = _switchColorOffResolver.GetValue();
            Rect totalRect = EditorGUILayout.GetControlRect(label != null, EditorGUIUtility.singleLineHeight);
            if (label != null && !_hasToggleLeftAttribute)
            {
                totalRect = EditorGUI.PrefixLabel(totalRect, label);
            }

            const float preHeight = SwitchWidth * 0.5f;
            float height = preHeight < totalRect.height ? preHeight : totalRect.height;
            // 根据对齐方式计算开关背景区域
            Rect switchBackgroundRect = Attribute.Alignment switch
            {
                SwitchAlignment.Left => totalRect.AlignLeft(SwitchWidth).AlignCenterY(height),
                SwitchAlignment.Right => totalRect.AlignRight(SwitchWidth).AlignCenterY(height),
                SwitchAlignment.Center => totalRect.AlignCenterX(SwitchWidth).AlignCenterY(height),
                _ => throw new ArgumentException("Invalid alignment")
            };

            // 获取当前事件信息
            Event evt = Event.current;
            // 获取当前控件唯一标识
            int controlID = GUIUtility.GetControlID(ControlHint, FocusType.Passive, switchBackgroundRect);
            // 当前开关状态
            bool isOn = ValueEntry.SmartValue;
            // 目标背景颜色
            Color targetBackgroundColor = isOn ? backgroundColorOn : backgroundColorOff;
            // 目标开关颜色
            Color targetSwitchColor = isOn ? switchColorOn : switchColorOff;
            // 处理颜色变化时的动画过渡
            if (ColorNeedChanged(targetBackgroundColor, targetSwitchColor))
            {
                _animating = true;
            }

            // 布局阶段处理动画
            if (evt.type == EventType.Layout && _animating)
            {
                // 平滑过渡背景颜色
                _backgroundColor = _backgroundColor.MoveTowards(
                    targetBackgroundColor,
                    GUITimeHelper.LayoutDeltaTime * AnimationSpeedMultiplier);

                // 平滑过渡开关颜色
                _switchColor = _switchColor.MoveTowards(
                    targetSwitchColor,
                    GUITimeHelper.LayoutDeltaTime * AnimationSpeedMultiplier);

                // 平滑移动开关位置
                float targetSwitchPosition = isOn ? SwitchWidth * 0.5f : 0f;
                _switchPosition = Mathf.MoveTowards(
                    _switchPosition,
                    targetSwitchPosition,
                    GUITimeHelper.LayoutDeltaTime * AnimationSpeedMultiplier * SwitchWidth * 0.5f);

                // 动画结束条件检查
                if (_backgroundColor == targetBackgroundColor
                    && _switchColor == targetSwitchColor
                    && Mathf.Approximately(_switchPosition, targetSwitchPosition))
                {
                    _animating = false;
                }

                // 请求界面重绘
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
            Color finalBackgroundColor = _backgroundColor;
            // 圆角半径（根据是否开启圆角特性）
            float borderRadius = Attribute.Rounded ? 99f : 0f;
            // 绘制背景矩形
            GUI.DrawTexture(switchBackgroundRect, _whiteTexture, ScaleMode.StretchToFill, true, 0f,
                finalBackgroundColor, 0f, borderRadius);
            // 计算最终开关颜色
            Color finalSwitchColor = _switchColor;
            // 计算开关滑块矩形区域
            Rect switchRect = switchBackgroundRect
                .SetWidth(SwitchWidth * 0.5f) // 设置宽度为父容器一半
                .Padding(SwitchWidth * 0.07f) // 内边距
                .AddX(_switchPosition);       // X轴偏移量控制滑块位置
            // 绘制开关滑块
            GUI.DrawTexture(switchRect, _whiteTexture, ScaleMode.StretchToFill, true, 0f,
                finalSwitchColor, 0f, borderRadius);
            // 处理 ToggleLeftAttribute 特性（标签位置调整）
            if (_hasToggleLeftAttribute)
            {
                EditorGUI.LabelField(totalRect.AddX(SwitchWidth + 4f), label);
            }
        }

        // 切换开关状态的方法
        void ChangeValueTo(bool newValue)
        {
            ValueEntry.SmartValue = newValue;
            _animating = true;
        }

        bool ColorNeedChanged(Color targetBackgroundColor, Color targetSwitchColor) =>
            _backgroundColor != targetBackgroundColor || _switchColor != targetSwitchColor;
    }
}
