using UnityEngine;
using Yuumix.OdinToolkits.Modules.LearnArchive.EditorExtensions.IMGUI._8_官方文档拓展部分_Event.ControlState;

namespace Yuumix.OdinToolkits.Modules.LearnArchive.EditorExtensions.IMGUI._8_官方文档拓展部分_Event.UnityMyCustomSlider.Editor
{
    /// <summary>
    /// 自定义封装控件，可以用于在其他地方，使用过程类似于 GUI.Label(); GUI.HorizontalSlider();
    /// </summary>
    public static class UnityCustomControls
    {
        /// <summary>
        /// 这是一个特殊的自定义控件，类似于 Slider，但是带有一个自定义的纹理，并且可以设置颜色。
        /// </summary>
        public static float MyCustomSlider(Rect controlRect, float value, GUIStyle style)
        {
            if (style == null)
            {
                style = GUI.skin.horizontalSlider; // 使用内置的 horizontalSlider 样式
            }

            // 1. 获取控件ID
            // 这个图形用于绘制一个特殊滑块，但是它主要是用于表示，所以不需要焦点。
            var controlID = GUIUtility.GetControlID(FocusType.Keyboard);
            // EditorGUI.LabelField(controlRect, "My Custom Slider");
            // 2. 获取当前事件
            // 通过控件 ID ，可以知道当前事件类型，可以让 Unity 帮我们筛选掉一些事件
            switch (Event.current.GetTypeForControl(controlID))
            {
                case EventType.Repaint:
                {
                    // Work out the width of the bar in pixels by lerping
                    int pixelWidth = (int)Mathf.Lerp(1f, controlRect.width, value);
                    // Build up the rectangle that the bar will cover
                    // by copying the whole control rect, and then setting the width
                    Rect targetRect = new Rect(controlRect)
                    {
                        width = pixelWidth
                    };
                    // Tint (色彩) whatever we draw to be red/green depending on value
                    GUI.color = Color.Lerp(Color.red, Color.green, value);
                    // 绘制一个色块
                    // Draw the texture from the GUIStyle, applying the tint
                    GUI.DrawTexture(targetRect, style.normal.background);
                    // Reset the tint back to white, i.e. untinted
                    GUI.color = Color.white;
                    break;
                }
                case EventType.MouseDown:
                {
                    // If the click is actually on us...
                    // ...and the click is with the left mouse button (button 0)...
                    if (controlRect.Contains(Event.current.mousePosition) && Event.current.button == 0)
                    {
                        // ...then capture the mouse by setting the hotControl.
                        // This is the control that will receive all mouse events until the mouse is released.
                        // hotControl 如果不等于 0 ，则接下来的鼠标事件都会被这个控件接收，直到鼠标被释放。
                        // 其他控件会过滤掉鼠标事件信息，不会响应
                        // when it’s not 0, then mouse events get filtered out unless the control ID being passed in is the hotControl.
                        GUIUtility.hotControl = controlID;
                    }

                    break;
                }
                case EventType.MouseUp:
                {
                    // 请注意，当其他控件是热控件时 - 即 GUIUtility.hotControl 不是 0 和我们自己的控件 ID
                    // - 那么这些情况根本不会执行，因为 GetTypeForControl（） 将返回 'ignore' 而不是 mouseUp/mouseDown 事件。
                    // If we were the hotControl, we aren't any more.
                    if (GUIUtility.hotControl == controlID)
                        GUIUtility.hotControl = 0;
                    break;
                }
            }

            if (Event.current.isMouse && GUIUtility.hotControl == controlID)
            {
                // 3. 获取鼠标位置
                Vector2 mousePos = Event.current.mousePosition;
                float relativeX = mousePos.x - controlRect.x;
                float percent = relativeX / controlRect.width;
                value = Mathf.Clamp01(percent);
                GUI.changed = true;
                // Mark event as 'used' so other controls don't respond to it, and to
                // trigger an automatic repaint.
                // 鼠标事件被使用，其他控件将不会响应，并且会触发一个自动重绘。
                Event.current.Use();
            }

            return value;
        }

        public static bool FlashingButton(Rect controlRect, GUIContent content, GUIStyle style)
        {
            var controlID = GUIUtility.GetControlID(FocusType.Passive);
            // --- 也可以直接使用 ScriptableObject 作为持久性数据
            // IMGUI 为与控件关联的 “state objects” 提供了一个简单的存储系统。
            // 定义自己的类来存储值，然后要求 IMGUI 管理与控件的 ID 关联的实例。每个控件 ID 只允许一个状态对象，并且你自己不能实例化它
            // - IMGUI 使用状态对象的默认构造函数为你执行此操作。在重新加载编辑器代码时，State 对象也不会被序列化
            // - 每次重新编译代码时都会发生这种情况 - 因此您应该只将它们用于短期内容。
            // （请注意，即使你将 state 对象标记为 [Serializable] 也是如此 - 序列化程序根本不访问堆的这个特定角落）。
            var state = (FlashingButtonInfo)GUIUtility.GetStateObject(typeof(FlashingButtonInfo), controlID);
            switch (Event.current.GetTypeForControl(controlID))
            {
                case EventType.Repaint:
                {
                    GUI.color = state.IsFlashing(controlID)
                        ? Color.red
                        : Color.white;
                    style.Draw(controlRect, content, controlID);
                    // Debug.Log(">>> Repaint");
                    break;
                }
                case EventType.MouseDown:
                {
                    if (controlRect.Contains(Event.current.mousePosition)
                        && Event.current.button == 0
                        && GUIUtility.hotControl == 0)
                    {
                        GUIUtility.hotControl = controlID;
                        state.MouseDownNow();
                    }

                    Event.current.Use();
                    // Debug.Log(">>> Event Used >>> MouseDown");
                    break;
                }
                case EventType.MouseUp:
                {
                    if (GUIUtility.hotControl == controlID)
                    {
                        GUIUtility.hotControl = 0;
                    }

                    Event.current.Use();
                    // Debug.Log(">>> Event Used >>> MouseUp");
                    break;
                }
            }

            if (Event.current.isMouse && GUIUtility.hotControl == controlID)
            {
                GUI.changed = true;
                // Mark event as 'used' so other controls don't respond to it, and to
                // trigger an automatic repaint.
                // 鼠标事件被使用，其他控件将不会响应，并且会触发一个自动重绘。
                Event.current.Use();
                Debug.Log(">>> Event Used");
            }

            return GUIUtility.hotControl == controlID && controlRect.Contains(Event.current.mousePosition);
        }
    }
}