using Sirenix.OdinInspector;
using UnityEngine;
using Yuumix.OdinToolkits.Core;
using Yuumix.OdinToolkits.ScriptDocGenerator;

namespace Yuumix.OdinToolkits.ScriptDocGen.Editor
{
    /// <summary>
    /// 文档生成器设置，抽象类，ScriptableObject 资源类
    /// </summary>
    [Summary("文档生成器设置，抽象类，ScriptableObject 资源类")]
    public abstract class DocGeneratorSettingSO : ScriptableObject
    {
        #region Serialized Fields

        [BilingualText("生成命名空间文件夹", "Generate Namespace Folder")]
        public bool generateNamespaceFolder = true;

        [BilingualText("自定义文档文件扩展名", "Customize Doc File Extension Name")]
        public bool customizeDocFileExtensionName;

        [EnableIf("customizeDocFileExtensionName")]
        [BilingualText("文档文件扩展名", "Doc File Extension Name")]
        public string docFileExtensionName = ".md";

        [BilingualText("自动生成增量标识符", "Auto Generate Incremental Identifier")]
        public bool generateIdentifier = true;

        #endregion

        /// <summary>
        /// 根据 TypeData 数据，自定义生成文档内容，注意：不要在此方法中添加增量生成标识符
        /// </summary>
        [Summary("根据 TypeData 数据，自定义生成文档内容，注意：不要在此方法中添加增量生成标识符")]
        public abstract string GetGeneratedDoc(ITypeData data);
    }
}
