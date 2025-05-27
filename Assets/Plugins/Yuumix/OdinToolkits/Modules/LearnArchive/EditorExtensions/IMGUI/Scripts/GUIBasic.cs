using UnityEngine;

namespace Yuumix.OdinToolkits.Modules.LearnArchive.EditorExtensions.IMGUI.Scripts
{
    public class GUIBasic : MonoBehaviour
    {
        public Texture2D icon;
        public string textFieldString = "text field";
        public string textAreaString = "text area";
        public bool toggleBool;

        // 每帧调用绘制，就像 Update 函数一样
        void OnGUI()
        {
            GUI.Box(new Rect(10, 60, 100, 100), "Hello World");
            if (GUI.Button(new Rect(20, 80, 80, 20), "Unity"))
            {
                Debug.Log(" Unity Button Clicked ");
            }

            if (GUI.Button(new Rect(20, 110, 80, 20), "C#"))
            {
                Debug.Log(" C# Button Clicked ");
            }

            // 创建一个闪烁的按钮，每隔 1 秒闪烁一次
            if (Time.time % 2 < 1)
            {
                if (GUI.Button(new Rect(20, 140, 80, 20), "Flashing"))
                {
                    Debug.Log(" Flashing Button Clicked ");
                }
            }

            // 控件类型 （位置大小，内容）
            GUI.Box(new Rect(0, 0, 100, 50), "Top - Left");
            GUI.Box(new Rect(Screen.width - 100, 0, 100, 50), "Top - Right");
            GUI.Box(new Rect(0, Screen.height - 50, 100, 50), "Bottom - Left");
            GUI.Box(new Rect(Screen.width - 100, Screen.height - 50, 100, 50), "Bottom - Right");

            GUI.Label(new Rect(10, 170, 100, 50), "This is the text string for a Label Control");

            if (GUI.Button(new Rect(10, 230, 100, 50), icon))
            {
                print("you clicked the icon");
            }

            if (GUI.Button(new Rect(10, 290, 100, 20), "This is text"))
            {
                print("you clicked the text button");
            }

            // GUIContent 图标在文字之前绘制
            GUI.Box(new Rect(10, 330, 100, 30), new GUIContent("This is text", icon));

            // This line feeds "This is the tooltip" into GUI.tooltip
            // 会把 "This is the tooltip" 存入 GUI.tooltip
            GUI.Button(new Rect(10, 370, 100, 20), new GUIContent("Click me", "This is the tooltip"));

            // This line reads and displays the contents of GUI.tooltip
            GUI.Label(new Rect(10, 400, 100, 20), GUI.tooltip);

            GUI.Button(new Rect(10, 430, 100, 20), new GUIContent("Click me", icon, "This is the tooltip"));
            GUI.Label(new Rect(10, 460, 100, 20), GUI.tooltip);

            // RepeatButton
            // 每帧执行，且只要是 Pressed 状态，就会一直执行
            // 只要按着就会一直执行
            if (GUI.RepeatButton(new Rect(10, 490, 100, 30), "RepeatButton"))
            {
                // This code is executed every frame that the RepeatButton remains clicked
                print("RepeatButton is clicked");
            }

            GUI.Label(new Rect(120, 10, 200, 20), "TextField 只可以单行");
            textFieldString = GUI.TextField(new Rect(120, 40, 100, 30), textFieldString);

            GUI.Label(new Rect(120, 80, 200, 30), "TextArea 可以有多行");
            textAreaString = GUI.TextArea(new Rect(120, 130, 100, 50), textAreaString);

            toggleBool = GUI.Toggle(new Rect(120, 190, 100, 30), toggleBool, "Toggle");
            
            // Toolbar 相当于多个 Button
            
        }
    }
}