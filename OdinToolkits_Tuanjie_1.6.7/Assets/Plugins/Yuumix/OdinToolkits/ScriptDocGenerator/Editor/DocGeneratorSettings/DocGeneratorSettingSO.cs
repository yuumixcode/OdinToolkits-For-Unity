using Sirenix.OdinInspector;
using UnityEngine;
using Yuumix.OdinToolkits.Core;

namespace Yuumix.OdinToolkits.ScriptDocGenerator.Editor
{
    /// <summary>
    /// 文档生成器设置抽象类
    /// </summary>
    [Summary("文档生成器设置抽象类")]
    public abstract class DocGeneratorSettingSO : ScriptableObject, IOdinToolkitsEditorReset
    {
        #region Serialized Fields

        /// <summary>
        /// 是否按命名空间生成文件夹
        /// </summary>
        [Summary("是否按命名空间生成文件夹")]
        [BilingualText("按命名空间生成文件夹", "Generate Namespace Folder")]
        public bool generateNamespaceFolder = true;

        /// <summary>
        /// 是否自定义文档扩展名
        /// </summary>
        [Summary("是否自定义文档扩展名")]
        [BilingualText("自定义文档扩展名", "Customize Doc Extension Name")]
        public bool customizeDocFileExtensionName;

        /// <summary>
        /// 设置的文档扩展名
        /// </summary>
        [Summary("设置的文档扩展名")]
        [EnableIf("customizeDocFileExtensionName")]
        [BilingualText("文档扩展名", "Doc Extension Name")]
        public string docFileExtensionName = ".md";

        /// <summary>
        /// 是否生成增量标识符
        /// </summary>
        [Summary("是否生成增量标识符")]
        [BilingualText("是否生成增量标识符", "Generate Identifier")]
        public bool generateIdentifier = true;

        #endregion

        #region IOdinToolkitsEditorReset Members

        /// <summary>
        /// 重置文档生成器设置
        /// </summary>
        [Summary("重置文档生成器设置")]
        public void EditorReset()
        {
            generateNamespaceFolder = true;
            customizeDocFileExtensionName = false;
            docFileExtensionName = ".md";
            generateIdentifier = true;
        }

        #endregion

        /// <summary>
        /// 通过 TypeData 实例对象，生成文档内容。注意：不要在此方法中添加增量生成标识符
        /// </summary>
        [Summary("通过 TypeData 实例对象，生成文档内容。注意：不要在此方法中添加增量生成标识符")]
        public abstract string GetGeneratedDoc(ITypeData data);
    }
}
