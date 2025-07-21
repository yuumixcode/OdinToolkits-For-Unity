using System.Text;

namespace Yuumix.OdinToolkits.Editor
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

            for (var i = 0; i < data.SubDirectoryData.Count; i++)
            {
                DirectoryAnalysisData subData = data.SubDirectoryData[i];
                int depth = subData.Depth;
                string indentString = GetIndentation(depth);
                string finalLine = indentString + "├─ " + data.SubDirectoryData[i].Name;
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

        public override string GetIndentation(int level)
        {
            var indent = "";
            for (var i = 1; i < level; i++)
            {
                indent += "│  ";
            }

            return indent;
        }
    }
}
