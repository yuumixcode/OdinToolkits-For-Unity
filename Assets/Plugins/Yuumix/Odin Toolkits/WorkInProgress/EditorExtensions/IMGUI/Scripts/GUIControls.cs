using UnityEngine;

namespace Yuumix.OdinToolkits.Modules.LearnArchive.EditorExtensions.IMGUI.Scripts
{
    public class GUIControls : MonoBehaviour
    {
        public string textFieldString = "TextField";
        public string textAreaString = "TextArea";
        public bool toggleBool = false;
        public int toolbarInt = 0;
        public string[] toolbarStrings = { "Toolbar1", "Toolbar2", "Toolbar3" };
        public int selectionGridInt = 0;
        public string[] selectionStrings = { "Grid 1", "Grid 2", "Grid 3", "Grid 4" };
        public float hSliderValue = 0.0f;
        public float vSliderValue = 0.0f;
        public float hScrollbarValue;
        public float vScrollbarValue;
        public Vector2 scrollViewVector = Vector2.zero;
        public string innerText = "I am inside the ScrollView";
        public Rect windowRect = new Rect(10, 650, 200, 100);
        public bool windowChanged = false; // 用于检测窗口中的控件是否被操作过

        void OnGUI()
        {
            // windowChanged = false;
            // Label
            GUI.Label(new Rect(10, 10, 100, 20), "Label");
            // Button
            if (GUI.Button(new Rect(10, 40, 100, 30), "Button"))
            {
                print("Button is clicked");
            }

            // RepeatButton
            if (GUI.RepeatButton(new Rect(10, 80, 100, 30), "RepeatButton"))
            {
                print("RepeatButton is clicked");
            }

            // TextField
            textFieldString = GUI.TextField(new Rect(10, 120, 100, 30), textFieldString);
            // TextArea
            textAreaString = GUI.TextArea(new Rect(10, 160, 100, 50), textAreaString);
            // Toggle
            toggleBool = GUI.Toggle(new Rect(10, 220, 100, 30), toggleBool, "Toggle");
            // Toolbar
            toolbarInt = GUI.Toolbar(new Rect(10, 260, 200, 30), toolbarInt, toolbarStrings);
            // SelectionGrid
            // xCount 是一行几个的意思
            selectionGridInt = GUI.SelectionGrid(new Rect(10, 300, 200, 100), selectionGridInt, selectionStrings, 3);
            // HorizontalSlider
            hSliderValue = GUI.HorizontalSlider(new Rect(10, 420, 200, 30), hSliderValue, 0.0f, 1.0f);
            // VerticalSlider
            vSliderValue = GUI.VerticalSlider(new Rect(220, 420, 30, 100), vSliderValue, 0.0f, 1.0f);
            // HorizontalScrollbar
            hScrollbarValue = GUI.HorizontalScrollbar(new Rect(250, 420, 200, 30), hScrollbarValue, 10f, 1.0f, 50.0f);
            // VerticalScrollbar
            vScrollbarValue = GUI.VerticalScrollbar(new Rect(450, 420, 30, 100), vScrollbarValue, 10f, 1.0f, 50.0f);
            // ScrollView
            // 需要开始和关闭
            scrollViewVector =
                GUI.BeginScrollView(new Rect(10, 430, 200, 200), scrollViewVector, new Rect(0, 0, 500, 500));
            innerText = GUI.TextArea(new Rect(0, 0, 500, 500), innerText);
            GUI.EndScrollView();
            // Window
            // Windows 是唯一需要附加功能才能正常工作的 Control。您必须提供 ID 号和要为 Window 执行的函数名称。在 Window 函数中，您可以创建实际行为或包含的 Controls。
            // 第三个参数是一个委托，形参为 int id
            // WindowFunction 这个方法中的 Rect 要重新从左上角开始计算，是一个相对位置
            windowRect = GUI.Window(0, windowRect, WindowFunction, "My First Windows");
            // GUI.changed
            // GUI.changed will return true if any GUI Control placed before it was manipulated by the user.
            // 检测之前的控件是否有被修改，通常这个需要放在 OnGUI 的最后位置
            // 顺序依赖：GUI.changed 在每次 OnGUI 调用开始时都会重置为 false。当用户与某个控件交互时，GUI.changed 会被设置为 true。
            // 因此，只有在所有控件都绘制完毕后，GUI.changed 才能准确地反映出是否有任何控件被修改。
            if (GUI.changed || windowChanged)
            {
                print("GUI.changed Or windowChanged is true");
                // 需要在内部，对想要监测的值，再次判断
                if (0 == toolbarInt)
                {
                    Debug.Log("First button was clicked");
                }
                else
                {
                    Debug.Log("Second button was clicked");
                }

                // 只会对 OnGUI 这一层的控件生效，对于 Window 里面的，Button 点击不会生效
                // 除非人为加一个参数，和 GUI.changed 同时检测
            }

            // OnGUI 图形是从上往下绘制的
            // 点击按钮这个行为，在检测到之后，立刻修改 windowChanged，但是图形将会在下次 OnGUI 中生效，绘制点击按钮的 GUI
            // 如果在 OnGUI 的开始就重置 windowChanged ，那么值就保留不到下一次检测
            // 一定要放在后面重置
            windowChanged = false;
        }

        void WindowFunction(int windowID)
        {
            if (windowID != 0)
            {
                GUI.Label(new Rect(10, 20, 200, 30), "Not 0 Windows Label");
            }
            else
            {
                GUI.Label(new Rect(10, 30, 200, 30), "This is 0 Windows Label");
            }

            if (GUI.Button(new Rect(10, 60, 100, 30), "Button"))
            {
                windowChanged = true;
                print("Windows Button is clicked");
            }
        }
    }
}