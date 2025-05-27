using UnityEngine;

namespace Yuumix.OdinToolkits.Modules.LearnArchive.EditorExtensions.IMGUI._7_使用IMGUI扩展编辑器._3_自定义编辑器Editor
{
    [ExecuteInEditMode]
    public class LookAtPoint : MonoBehaviour
    {
        public Vector3 point = Vector3.zero;

        void Update()
        {
            transform.LookAt(point);
        }
    }
}