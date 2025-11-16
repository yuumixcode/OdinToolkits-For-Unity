using Sirenix.OdinInspector;
using UnityEngine;

namespace Yuumix.OdinToolkits.Modules.Editor
{
    [OdinToolkitsAttributeExample]
    public class FolderPathExample : ExampleSO
    {
        #region Serialized Fields

        [FoldoutGroup("无参数使用")]
        [FolderPath]
        [HideLabel]
        [InlineButton("Log1", "输出值")]
        public string path1;

        [FoldoutGroup("AbsolutePath 绝对路径")]
        [FolderPath(AbsolutePath = true)]
        [HideLabel]
        [InlineButton("Log2", "输出值")]
        public string path2;

        [FoldoutGroup("ParentFolder")]
        [HideLabel]
        [FolderPath(ParentFolder = "Assets/Plugins/OdinToolkits")]
        [InlineButton("Log4", "输出值")]
        public string path4;

        [FoldoutGroup("RequireExistingPath")]
        [HideLabel]
        [FolderPath(RequireExistingPath = true)]
        [InlineButton("Log5", "输出值")]
        public string path5;

        [FoldoutGroup("UseBackslashes")]
        [HideLabel]
        [FolderPath(UseBackslashes = true)]
        [InlineButton("Log6", "输出值")]
        public string path6;

        #endregion

        void Log1()
        {
            Debug.Log(path1);
        }

        void Log2()
        {
            Debug.Log(path2);
        }

        void Log4()
        {
            Debug.Log(path4);
        }

        void Log5()
        {
            Debug.Log(path5);
        }

        void Log6()
        {
            Debug.Log(path6);
        }
    }
}
