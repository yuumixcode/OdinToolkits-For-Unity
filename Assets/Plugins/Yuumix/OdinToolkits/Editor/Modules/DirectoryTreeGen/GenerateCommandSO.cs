using UnityEngine;

namespace Yuumix.OdinToolkits.Editor
{
    public abstract class GenerateCommandSO : ScriptableObject
    {
        public abstract string Generate(DirectoryAnalysisData data, int maxDepth);

        public abstract string GetIndentation(int depth);
    }
}
