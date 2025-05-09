using Sirenix.OdinInspector.Editor;
using UnityEditor;
using UnityEngine;

namespace Yuumix.OdinToolkits.Modules.Odin.AttributeOverviewPro.Editor
{
    [CustomEditor(typeof(AnalysisSO), true)]
    public class AnalysisSOEditor : OdinEditor
    {
        private AnalysisSO _analysisSO;

        protected override void OnEnable()
        {
            base.OnEnable();
            _analysisSO = target as AnalysisSO;
        }

        public override void OnInspectorGUI()
        {
            serializedObject.Update();

            EditorGUILayout.BeginVertical();
            {
                EditorGUILayout.BeginHorizontal();
                {
                    var attributeName = serializedObject.FindProperty("_attributeName");
                    if (attributeName != null)
                    {
                        GUILayout.Label(attributeName.stringValue);
                    }
                    else
                    {
                        GUILayout.Label("Attribute not found");
                    }

                    var urlName = _analysisSO.urlName;
                    if (urlName == "")
                    {
                        urlName = "未找到";
                    }

                    if (GUILayout.Button(urlName))
                    {
                        Debug.Log("打开文档网站");
                    }
                }
                EditorGUILayout.EndHorizontal();
            }
            EditorGUILayout.EndVertical();

            // 调用基类的 OnInspectorGUI 方法，它会绘制 Odin 风格的默认 Inspector 界面
            base.OnInspectorGUI();

            // 你可以在这里添加自定义的绘制逻辑
            if (GUILayout.Button("Custom Button"))
            {
                Debug.Log("Custom button clicked!");
            }

            serializedObject.ApplyModifiedProperties();
        }
    }
}
