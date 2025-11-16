using Sirenix.OdinInspector;
using System;
using System.Collections.Generic;
using Yuumix.OdinToolkits.Core;

namespace Yuumix.OdinToolkits.ScriptDocGenerator.Editor
{
    /// <summary>
    /// 存储 Type 的资源文件，提供给脚本文档生成工具复用，用户无需每次重新选择 Type
    /// </summary>
    [Summary("存储 Type 的资源文件，提供给脚本文档生成工具复用，用户无需每次重新选择 Type")]
    public class TypesCacheSO : SerializedScriptableObject
    {
        #region Serialized Fields

        /// <summary>
        /// 存储 Type 的列表
        /// </summary>
        [Summary("存储 Type 的列表")]
        public List<Type> Types;

        #endregion
    }
}
