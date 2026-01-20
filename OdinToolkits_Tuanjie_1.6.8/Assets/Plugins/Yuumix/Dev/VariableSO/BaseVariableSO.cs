using System;
using Sirenix.OdinInspector;
using UnityEngine;
using Object = UnityEngine.Object;
#if UNITY_EDITOR
using UnityEditor;
using Sirenix.Utilities.Editor;
#endif

namespace Dev
{
    /// <summary>
    /// ScriptableObject 变量的泛型抽象类
    /// </summary>
    public abstract class BaseVariableSO<T> : ScriptableObject
    {
        [SerializeField]
        [OnValueChanged(nameof(RaiseOnValueChanged))]
        [DelayedProperty]
        [LabelText("值")]
        protected T value;

        [SerializeField]
        [BoxGroup("调试选项")]
        [LabelText("启用 Console 日志")]
        [Tooltip("当值发生变化时，是否在 Console 中输出日志")]
        protected bool enableConsoleLog;

        /// <summary>
        /// 获取此变量的当前值。
        /// </summary>
        public T Value => value;

        /// <summary>
        /// 获取 OnValueChanged 事件的订阅者数量。
        /// </summary>
        [ShowInInspector]
        [ReadOnly]
        [PropertyOrder(101)]
        [LabelText("事件订阅者数量")]
        public int SubscriberCount => OnValueChanged?.GetInvocationList().Length ?? 0;

        /// <summary>
        /// 当值发生变化时触发的事件。
        /// </summary>
        public event Action<T> OnValueChanged;

        /// <summary>
        /// 设置新值，如果值不同则触发 OnValueChanged 事件。
        /// </summary>
        public virtual void SetValue(T newValue)
        {
            if (Equals(value, newValue))
            {
                return;
            }

            value = newValue;
            RaiseOnValueChanged();
        }

        /// <summary>
        /// 设置值但不触发 OnValueChanged 事件。
        /// </summary>
        public void SetValueSilent(T newValue)
        {
            value = newValue;
        }

        /// <summary>
        /// 使用当前值手动触发 OnValueChanged 事件。
        /// </summary>
        [Button("手动触发事件")]
        [PropertyOrder(100)]
        public void RaiseOnValueChanged()
        {
            OnValueChanged?.Invoke(value);

#if UNITY_EDITOR
            if (enableConsoleLog)
            {
                LogVariableValueChange(value);
            }
#endif
        }

#if UNITY_EDITOR
        [OnInspectorGUI]
        [PropertyOrder(102)]
        void DrawEventSubscribers()
        {
            if (!Application.isPlaying)
            {
                return;
            }

            if (OnValueChanged == null || OnValueChanged.GetInvocationList().Length == 0)
            {
                return;
            }

            SirenixEditorGUI.BeginBox("事件订阅者列表 (仅运行时)");

            var delegates = OnValueChanged.GetInvocationList();
            for (var i = 0; i < delegates.Length; i++)
            {
                var del = delegates[i];
                var target = del.Target;
                var method = del.Method;

                GUILayout.BeginHorizontal();

                // 绘制索引
                GUILayout.Label($"[{i}]", GUILayout.Width(30));

                if (target == null)
                {
                    // 静态方法
                    EditorGUILayout.LabelField($"静态方法: {method.DeclaringType?.Name}.{method.Name}",
                        EditorStyles.label);
                }
                else if (target is Object unityObj)
                {
                    // Unity 对象（可点击跳转）
                    EditorGUILayout.ObjectField(unityObj, typeof(Object), true);

                    // 显示方法名
                    GUILayout.Label($"→ {method.Name}", GUILayout.Width(150));

                    // 如果是场景对象，添加 Ping 按钮
                    if (unityObj is MonoBehaviour || unityObj is Component)
                    {
                        if (GUILayout.Button("定位", GUILayout.Width(50)))
                        {
                            EditorGUIUtility.PingObject(unityObj);
                            Selection.activeObject = unityObj;
                        }
                    }
                }
                else
                {
                    // 纯 C# 对象
                    var targetType = target.GetType();
                    EditorGUILayout.LabelField($"[C# 对象] {targetType.Name}.{method.Name}",
                        EditorStyles.label);

                    // 添加标记
                    GUILayout.Label("(非Unity对象)", EditorStyles.miniLabel, GUILayout.Width(80));
                }

                GUILayout.EndHorizontal();

                // 添加分隔线
                if (i < delegates.Length - 1)
                {
                    SirenixEditorGUI.DrawSolidRect(GUILayoutUtility.GetRect(0, 1),
                        new Color(0.3f, 0.3f, 0.3f, 0.5f));
                }
            }

            SirenixEditorGUI.EndBox();
        }

        void LogVariableValueChange(T newValue)
        {
            Debug.Log($"'{name}' 值变更为: {newValue}");
        }
#endif
    }
}
