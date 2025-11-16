using UnityEngine;

namespace Yuumix.OdinToolkits.Modules.Editor
{
    public abstract class GenerateCommandSO : ScriptableObject
    {
        public abstract string Generate(DirectoryAnalysisData data, int maxDepth);

        protected abstract string GetIndentation(int depth);
    }
}
