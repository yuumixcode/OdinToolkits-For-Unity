using UnityEngine;

namespace Yuumix.OdinToolkits.Modules.ScriptDocGen.Editor
{
    /// <summary>
    /// 文档生成器 SO 抽象类
    /// </summary>
    public abstract class DocGeneratorSO : ScriptableObject
    {
        public abstract string GetGeneratedDoc(TypeAnalysisData data);
    }
}
