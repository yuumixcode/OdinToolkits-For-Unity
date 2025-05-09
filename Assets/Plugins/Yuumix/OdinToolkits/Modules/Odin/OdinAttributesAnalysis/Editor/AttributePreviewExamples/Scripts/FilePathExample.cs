using Plugins.YOGA.OdinToolkits.Modules.OdinAttributesAnalysis.Common.Editor;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Plugins.YOGA.OdinToolkits.Modules.OdinAttributesAnalysis.Editor.AttributePreviewExamples.Scripts
{
    [IsChineseAttributeExample]
    public class FilePathExample : ExampleScriptableObject
    {
        [FoldoutGroup("无参数使用")] [FilePath] [HideLabel] [InlineButton("Log1", "输出值")]
        public string path1;

        [FoldoutGroup("AbsolutePath 绝对路径")] [FilePath(AbsolutePath = true)] [HideLabel] [InlineButton("Log2", "输出值")]
        public string path2;

        [FoldoutGroup("Extensions")] [FilePath(Extensions = ".asset")] [HideLabel] [InlineButton("Log3", "输出值")]
        public string path3;

        [FoldoutGroup("ParentFolder")]
        [HideLabel]
        [FilePath(ParentFolder = "Assets/Plugins/OdinToolkits")]
        [InlineButton("Log4", "输出值")]
        public string path4;

        [FoldoutGroup("RequireExistingPath")]
        [HideLabel]
        [FilePath(RequireExistingPath = true)]
        [InlineButton("Log5", "输出值")]
        public string path5;

        [FoldoutGroup("UseBackslashes")] [HideLabel] [FilePath(UseBackslashes = true)] [InlineButton("Log6", "输出值")]
        public string path6;

        [FoldoutGroup("IncludeFileExtension")]
        [HideLabel]
        [FilePath(IncludeFileExtension = false)]
        [InlineButton("Log7", "输出值")]
        public string path7;

        private void Log1()
        {
            Debug.Log(path1);
        }

        private void Log2()
        {
            Debug.Log(path2);
        }

        private void Log3()
        {
            Debug.Log(path3);
        }

        private void Log4()
        {
            Debug.Log(path4);
        }

        private void Log5()
        {
            Debug.Log(path5);
        }

        private void Log6()
        {
            Debug.Log(path6);
        }

        private void Log7()
        {
            Debug.Log(path7);
        }
    }
}