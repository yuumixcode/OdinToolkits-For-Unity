using Sirenix.OdinInspector;
using Sirenix.OdinInspector.Editor.Examples;
using UnityEngine;
using YOGA.OdinToolkits.AnalysisManual.OdinAttributes.Common.Scripts;

namespace YOGA.OdinToolkits.AnalysisManual.OdinAttributes.Editor
{
    [IsChineseAttributeExample]
    public class InlineEditorExample : ExampleScriptableObject
    {
        [FoldoutGroup("InlineEditor 无参数使用")] [InlineEditor]
        public CommonInlineObject inlineComponent;

        [FoldoutGroup("InlineEditor 显示模式")] [Title("InlineEditorModes.GUIOnly 默认模式")] [InlineEditor()]
        public CommonInlineObject inlineComponent2;

        [FoldoutGroup("InlineEditor 显示模式")]
        [Title("InlineEditorModes.FullEditor")]
        [InlineEditor(InlineEditorModes.FullEditor)]
        public Material fullInlineEditor;

        [FoldoutGroup("InlineEditor 显示模式")]
        [Title("InlineEditorModes.GUIAndHeader")]
        [InlineEditor(InlineEditorModes.GUIAndHeader)]
        public Material inlineMaterial;

        [FoldoutGroup("InlineEditor 显示模式")]
        [Title("InlineEditorModes.GUIAndPreview")]
        [InlineEditor(InlineEditorModes.GUIAndPreview)]
        public Material inlineMaterial2;

        [FoldoutGroup("InlineEditor 显示模式")]
        [Title("InlineEditorModes.SmallPreview")]
        [InlineEditor(InlineEditorModes.SmallPreview)]
        public Material[] inlineMaterialList;

        [FoldoutGroup("InlineEditor 显示模式")]
        [Title("InlineEditorModes.LargePreview")]
        [InlineEditor(InlineEditorModes.LargePreview)]
        public Mesh inlineMeshPreview;

        [FoldoutGroup("InlineEditor ObjectField 绘制模式")]
        [Title("InlineEditorObjectFieldModes.Boxed")]
        [InlineEditor(InlineEditorObjectFieldModes.Boxed)]
        public CommonInlineObject inlineComponent3;

        [FoldoutGroup("InlineEditor ObjectField 绘制模式")]
        [Title("InlineEditorObjectFieldModes.Foldout")]
        [InlineEditor(InlineEditorObjectFieldModes.Foldout)]
        public CommonInlineObject inlineComponent4;

        [FoldoutGroup("InlineEditor ObjectField 绘制模式")]
        [Title("InlineEditorObjectFieldModes.Hidden")]
        [InfoBox("不为空时隐藏")]
        [InlineEditor(InlineEditorObjectFieldModes.Hidden)]
        public CommonInlineObject inlineComponent5;

        [FoldoutGroup("InlineEditor ObjectField 绘制模式")]
        [Title("InlineEditorObjectFieldModes.CompletelyHidden")]
        [InfoBox("彻底隐藏，必须代码中赋值，否则一旦为 null，面板中无法赋值")]
        [InlineEditor(InlineEditorObjectFieldModes.CompletelyHidden)]
        public CommonInlineObject inlineComponent6;

        [OnInspectorInit]
        private void CreateData()
        {
            inlineComponent6 = ExampleHelper.GetScriptableObject<CommonInlineObject>("inlineComponent6");
        }

        // 可以直接标记类，不用标记单个字段
        // [InlineEditor]
        // public class ExampleTransform : ScriptableObject
        // { 
        //     public Vector3 Position;
        //     public Quaternion Rotation;
        //     public Vector3 Scale = Vector3.one;
        // }
    }
}