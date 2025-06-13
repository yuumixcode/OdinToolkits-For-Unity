using UnityEditor;
using UnityEngine;

namespace Yuumix.OdinToolkits.Modules.LearnArchive.EditorExtensions.IMGUI._7_使用IMGUI扩展编辑器._3_自定义编辑器Editor.Editor
{
    [CustomEditor(typeof(LookAtPoint)), CanEditMultipleObjects]
    public class LookAtPointEditor : UnityEditor.Editor
    {
        LookAtPoint _target;

        void OnEnable()
        {
            // target 指的是这个编辑器正在展示的对象，也就是当前选中的对象，
            // 然后它挂载了 LookAtPoint 脚本，也就是可以转换为当前选中的 LookAtPoint 对象
            _target = (LookAtPoint)target;
        }

        public override void OnInspectorGUI()
        {
            serializedObject.Update();
            Event m_Event = Event.current;

            if (m_Event.type == EventType.MouseDown)
            {
                Debug.Log("Mouse Down.");
            }

            if (m_Event.type == EventType.MouseDrag)
            {
                Debug.Log("Mouse Dragged.");
            }

            if (m_Event.type == EventType.MouseUp)
            {
                Debug.Log("Mouse Up.");
            }

            if (m_Event.type == EventType.Repaint)
            {
                Debug.Log("Repaint.");
            }

            _target.point = EditorGUILayout.Vector3Field("Point", _target.point);
            if (_target.point.y > _target.transform.position.y)
            {
                EditorGUILayout.LabelField("(Above this object)");
            }

            if (_target.point.y < _target.transform.position.y)
            {
                EditorGUILayout.LabelField("(Below this object)");
            }

            serializedObject.ApplyModifiedProperties();
        }

        public void OnSceneGUI()
        {
            EditorGUI.BeginChangeCheck();
            Vector3 pos = Handles.PositionHandle(_target.point, Quaternion.identity);
            if (EditorGUI.EndChangeCheck())
            {
                Undo.RecordObject(target, "Move point");
                _target.point = pos;
            }
        }
    }
}