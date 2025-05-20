using Sirenix.OdinInspector;
using Sirenix.OdinInspector.Editor;
using Sirenix.OdinInspector.Editor.ValueResolvers;
using Sirenix.Utilities;
using Sirenix.Utilities.Editor;
using System;
using UnityEditor;
using UnityEngine;
using Yuumix.OdinToolkits.Modules.Odin.Customs.Runtime.Attributes;

namespace Yuumix.OdinToolkits.Modules.Odin.Customs.Editor.Drawers
{
    public class SwitchButtonAttributeDrawer : OdinAttributeDrawer<SwitchButtonAttribute, bool>
    {
        // 动画速度系数（影响切换速度）
        private const float AnimationSpeedMultiplier = 6f;

        // 控件唯一标识哈希值（用于GUIUtility.hotControl）
        private static readonly int ControlHint = "SwitchControlHint".GetHashCode();

        // 颜色解析器（用于动态获取属性绑定的颜色值）
        private ValueResolver<Color> _backgroundColorOnResolver;
        private ValueResolver<Color> _backgroundColorOffResolver;
        private ValueResolver<Color> _switchColorOnResolver;
        private ValueResolver<Color> _switchColorOffResolver;

        // 开关控件宽度
        private const int SwitchWidth = 28;

        // 当前状态的颜色缓存
        private Color _backgroundColor;

        private Color _switchColor;

        // 开关位置（0-28px范围内移动）
        private float _switchPosition;

        // 白色纹理（用于绘制背景和滑块）
        private Texture _whiteTexture;

        // 是否正在执行动画
        private bool _animating;

        // 是否存在ToggleLeftAttribute特性
        private bool _hasToggleLeftAttribute;

        // 初始化方法（仅在属性第一次绘制时调用）
        protected override void Initialize()
        {
            // 初始化颜色解析器（根据特性参数动态获取颜色）
            _backgroundColorOnResolver = ValueResolver.Get<Color>(Property, Attribute.BackgroundColorOn);
            _backgroundColorOffResolver = ValueResolver.Get<Color>(Property, Attribute.BackgroundColorOff);
            _switchColorOnResolver = ValueResolver.Get<Color>(Property, Attribute.SwitchColorOn);
            _switchColorOffResolver = ValueResolver.Get<Color>(Property, Attribute.SwitchColorOff);
            // 获取当前开关状态对应的颜色
            var isOn = ValueEntry.SmartValue;
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
            // 绘制错误提示（如果有颜色解析失败等情况）
            ValueResolver.DrawErrors(
                _backgroundColorOnResolver,
                _backgroundColorOffResolver,
                _switchColorOnResolver,
                _switchColorOffResolver);
            // 获取当前颜色值（可能来自动态解析）
            var backgroundColorOn = _backgroundColorOnResolver.GetValue();
            var backgroundColorOff = _backgroundColorOffResolver.GetValue();
            var switchColorOn = _switchColorOnResolver.GetValue();
            var switchColorOff = _switchColorOffResolver.GetValue();
            // 获取控件整体矩形区域
            var totalRect = EditorGUILayout.GetControlRect(label != null, EditorGUIUtility.singleLineHeight);
            // 处理标签布局（根据对齐方式调整位置）
            if (label != null && !_hasToggleLeftAttribute)
            {
                totalRect = EditorGUI.PrefixLabel(totalRect, label);
            }

            const float preHeight = SwitchWidth * 0.5f;
            var height = preHeight < totalRect.height ? preHeight : totalRect.height;
            // 根据对齐方式计算开关背景区域
            var switchBackgroundRect = Attribute.Alignment switch
            {
                SwitchAlignment.Left => totalRect.AlignLeft(SwitchWidth).AlignCenterY(height),
                SwitchAlignment.Right => totalRect.AlignRight(SwitchWidth).AlignCenterY(height),
                SwitchAlignment.Center => totalRect.AlignCenterX(SwitchWidth).AlignCenterY(height),
                _ => throw new ArgumentException("Invalid alignment")
            };

            // 获取当前事件信息
            var evt = Event.current;
            // 获取当前控件唯一标识
            var controlID = GUIUtility.GetControlID(ControlHint, FocusType.Passive, switchBackgroundRect);
            // 当前开关状态
            var isOn = ValueEntry.SmartValue;
            // 目标背景颜色
            var targetBackgroundColor = isOn ? backgroundColorOn : backgroundColorOff;
            // 目标开关颜色
            var targetSwitchColor = isOn ? switchColorOn : switchColorOff;
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
                var targetSwitchPosition = isOn ? SwitchWidth * 0.5f : 0f;
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
            var finalBackgroundColor = _backgroundColor;
            // 圆角半径（根据是否开启圆角特性）
            var borderRadius = Attribute.Rounded ? 99f : 0f;
            // 绘制背景矩形
            GUI.DrawTexture(switchBackgroundRect, _whiteTexture, ScaleMode.StretchToFill, true, 0f,
                finalBackgroundColor, 0f, borderRadius);
            // 计算最终开关颜色
            var finalSwitchColor = _switchColor;
            // 计算开关滑块矩形区域
            var switchRect = switchBackgroundRect
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
        private void ChangeValueTo(bool newValue)
        {
            ValueEntry.SmartValue = newValue;
            _animating = true;
        }

        // 颜色变化检测方法
        private bool ColorNeedChanged(Color targetBackgroundColor, Color targetSwitchColor) =>
            _backgroundColor != targetBackgroundColor || _switchColor != targetSwitchColor;

        // 颜色加深辅助方法
        private static Color Darken(Color color, float factor) => new Color(color.r / factor, color.g / factor, color.b / factor);
    }
}
