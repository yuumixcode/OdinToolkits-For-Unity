using Yuumix.OdinToolkits.Core;
using Sirenix.OdinInspector;
using System;
using System.Collections.Generic;

namespace Yuumix.OdinToolkits.ScriptDocGen.Editor
{
    /// <summary>
    /// 存放 Type 的配置资源，提供给脚本文档生成工具复用，无需每次重新选择 Type
    /// </summary>
    [Summary("存放 Type 的配置资源，提供给脚本文档生成工具复用，无需每次重新选择 Type")]
    public class TypesConfigSO : SerializedScriptableObject
    {
        #region Serialized Fields

        public List<Type> Types;

        #endregion
    }
}
