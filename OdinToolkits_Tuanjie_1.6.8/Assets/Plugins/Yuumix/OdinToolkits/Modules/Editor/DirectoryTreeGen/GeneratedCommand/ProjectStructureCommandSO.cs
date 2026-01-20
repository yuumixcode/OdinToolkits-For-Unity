using System.Text;

namespace Yuumix.OdinToolkits.Modules.Editor
{
    public class ProjectStructureCommandSO : GenerateCommandSO
    {
        public override string Generate(DirectoryAnalysisData data, int maxDepth)
        {
            var sb = new StringBuilder();
            sb.AppendLine(data.Name + "/");
            RecursiveGenerate(data, sb, maxDepth);
            var resultText = sb.ToString();
            return resultText;
        }

        void RecursiveGenerate(DirectoryAnalysisData data, StringBuilder sb, int maxDepth)
        {
            if (data.SubDirectoryData == null || data.SubDirectoryData.Count <= 0)
            {
                return;
            }

            foreach (var t in data.SubDirectoryData)
            {
                var subData = t;
                var depth = subData.Depth;
                var indentString = GetIndentation(depth);
                var finalLine = indentString + "├─ " + t.Name;
                if (subData.AnalysisType == DirectoryAnalysisData.DirectoryAnalysisType.Folder)
                {
                    finalLine += "/";
                }

                sb.AppendLine(finalLine);
                if (depth >= maxDepth)
                {
                    continue;
                }

                RecursiveGenerate(subData, sb, maxDepth);
            }
        }

        protected override string GetIndentation(int level)
        {
            var indent = "";
            for (var i = 0; i < level; i++)
            {
                indent += "│  ";
            }

            return indent;
        }
    }
}
