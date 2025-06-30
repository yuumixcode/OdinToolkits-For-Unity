using UnityEngine;

namespace Yuumix.OdinToolkits.Modules.LearnArchive.EditorExtensions.IMGUI.Scripts
{
    public class GUILayoutSimpleDemo : MonoBehaviour
    {
        // background image that is 256 x 32
        public Texture2D bgImage;

        // foreground image that is 256 x 32
        public Texture2D fgImage;

        // a float between 0.0 and 1.0
        public float playerEnergy = 1.0f;

        void OnGUI()
        {
            // Automatic Layout
            GUILayout.Button("I am an Automatic Layout Button");
            // Fixed Layout
            GUI.Button(new Rect(0, 0, 100, 30), "I am a Fixed Layout Button");
            // 布局组
            // Create one Group to contain both images
            // Adjust the first 2 coordinates to place it somewhere else on-screen
            GUI.BeginGroup(new Rect(10, 300, 256, 32));

            // Draw the background image
            GUI.Box(new Rect(0, 0, 256, 32), bgImage);

            // Create a second Group which will be clipped
            // We want to clip the image and not scale it, which is why we need the second Group
            GUI.BeginGroup(new Rect(0, 0, playerEnergy * 256, 32));

            // Draw the foreground image
            GUI.Box(new Rect(0, 0, 256, 32), fgImage);

            // End both Groups
            GUI.EndGroup();
            GUI.EndGroup();
        }
    }
}