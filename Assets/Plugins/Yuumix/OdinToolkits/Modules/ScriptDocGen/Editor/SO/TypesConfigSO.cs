using System;
using System.Collections.Generic;
using Sirenix.OdinInspector;

namespace Yuumix.OdinToolkits.Modules.ScriptDocGen.Editor
{
    /// <summary>
    /// 存放 Type 的配置资源，提供给脚本文档生成工具复用，无需每次重新选择 Type
    /// </summary>
    public class TypesConfigSO : SerializedScriptableObject
    {
        public List<Type> Types;
    }
}
