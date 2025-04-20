using Sirenix.OdinInspector;
using UnityEngine;

namespace YOGA.OdinToolkits.AnalysisManual.OdinAttributes.Editor
{
    [IsChineseAttributeExample]
    public class PreviewFieldExample : ExampleScriptableObject
    {
        [FoldoutGroup("无参数使用")]
        [PreviewField]
        public Texture regularPreviewField;

        [FoldoutGroup("ObjectFieldAlignment")]
        [PreviewField(ObjectFieldAlignment.Center)]
        public Texture previewField2;

        [FoldoutGroup("Height")]
        [PreviewField(Height = 70)]
        public Texture2D texture2D;

        [FoldoutGroup("FilterMode")]
        [PreviewField(FilterMode = FilterMode.Point)]
        public Texture2D texture2D2;

        [FoldoutGroup("previewGetter")]
        [InfoBox("实际预览的是 texture2D4 字段，原本的 texture2D3 被隐藏，" +
                 "但是预览框的 Select 赋值的是 texture2D3，texture2D3 的真实图像会被覆盖")]
        [PreviewField(previewGetter: "$texture2D4", ObjectFieldAlignment.Left)]
        [InlineButton("Log3", "输出 Tx3")]
        [InlineButton("Log4", "输出 Tx4")]
        public Texture2D texture2D3;

        [FoldoutGroup("previewGetter")]
        [SerializeField]
        [InfoBox("私有序列化")]
        Texture2D texture2D4;

        void Log3()
        {
            Debug.Log("texture2D3 的值为: " + texture2D3);
        }

        void Log4()
        {
            Debug.Log("texture2D4 的值为: " + texture2D4);
        }
    }
}