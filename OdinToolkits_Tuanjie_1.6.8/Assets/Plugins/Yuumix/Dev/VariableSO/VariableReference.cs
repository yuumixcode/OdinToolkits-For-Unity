using System;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Dev
{
    /// <summary>
    /// 泛型引用类，允许在常量值或 ScriptableObject 变量之间进行选择。
    /// 为游戏系统提供统一的值访问方式。
    /// </summary>
    /// <typeparam name="T">值的类型</typeparam>
    /// <typeparam name="TSO">ScriptableObject 变量类型</typeparam>
    [Serializable]
    public class VariableReference<T, TSO> where TSO : BaseVariableSO<T>
    {
        [HorizontalGroup("Reference")]
        [HideLabel]
        [ToggleLeft]
        public bool useConstant = true;

        [HorizontalGroup("Reference")]
        [HideLabel]
        [ShowIf(nameof(useConstant))]
        public T constantValue;

        [HorizontalGroup("Reference")]
        [HideLabel]
        [HideIf(nameof(useConstant))]
        [InlineEditor(InlineEditorModes.SmallPreview)]
        public TSO variable;

        /// <summary>
        /// 获取当前值，可以是常量或来自 ScriptableObject 变量。
        /// </summary>
        public T Value
        {
            get => useConstant ? constantValue : (variable != null ? variable.Value : default);
            set
            {
                if (useConstant)
                {
                    constantValue = value;
                }
                else if (variable != null)
                {
                    variable.SetValue(value);
                }
            }
        }

        /// <summary>
        /// 如果使用 ScriptableObject 变量，则订阅 OnValueChanged 事件。
        /// </summary>
        public void SubscribeToChange(Action<T> callback)
        {
            if (!useConstant && variable != null)
            {
                variable.OnValueChanged += callback;
            }
        }

        /// <summary>
        /// 如果使用 ScriptableObject 变量，则取消订阅 OnValueChanged 事件。
        /// </summary>
        public void UnsubscribeFromChange(Action<T> callback)
        {
            if (!useConstant && variable != null)
            {
                variable.OnValueChanged -= callback;
            }
        }

        public static implicit operator T(VariableReference<T, TSO> reference)
        {
            return reference.Value;
        }
    }

    // 常用类型的具体实现
    [Serializable]
    public class IntReference : VariableReference<int, IntVariableSO> { }

    [Serializable]
    public class FloatReference : VariableReference<float, FloatVariableSO> { }

    [Serializable]
    public class BoolReference : VariableReference<bool, BoolVariableSO> { }

    [Serializable]
    public class StringReference : VariableReference<string, StringVariableSO> { }

    [Serializable]
    public class Vector2Reference : VariableReference<Vector2, Vector2VariableSO> { }

    [Serializable]
    public class Vector3Reference : VariableReference<Vector3, Vector3VariableSO> { }

    [Serializable]
    public class ColorReference : VariableReference<Color, ColorVariableSO> { }

    [Serializable]
    public class GameObjectReference : VariableReference<GameObject, GameObjectVariableSO> { }
}
