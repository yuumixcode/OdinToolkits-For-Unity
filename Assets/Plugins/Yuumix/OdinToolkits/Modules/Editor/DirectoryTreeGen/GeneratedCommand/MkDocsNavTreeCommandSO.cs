using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Yuumix.OdinToolkits.Modules.Editor
{
    public class MkDocsNavTreeCommandSO : GenerateCommandSO
    {
        readonly string[] _folderFilterArray =
        {
            "blog",
            "assets"
        };

        readonly string[] _fileFilterArray =
        {
            "CNAME"
        };

        public override string Generate(DirectoryAnalysisData data, int maxDepth)
        {
            var sb = new StringBuilder();
            sb.AppendLine("nav:");
            GenerateFirstLevelFileLine(data, sb);
            foreach (DirectoryAnalysisData subData in data.SubDirectoryData.Where(s =>
                         s.Depth == 1 && s.AnalysisType == DirectoryAnalysisData.DirectoryAnalysisType.Folder))
            {
                RecursiveGenerate(subData, sb, maxDepth);
            }

            var resultText = sb.ToString();
            return resultText;
        }

        void GenerateFirstLevelFileLine(DirectoryAnalysisData data, StringBuilder sb)
        {
            List<DirectoryAnalysisData> subDataList = data.SubDirectoryData;
            if (subDataList == null || subDataList.Count <= 0)
            {
                return;
            }

            foreach (DirectoryAnalysisData directoryAnalysisData in subDataList.Where(s =>
                         s.AnalysisType == DirectoryAnalysisData.DirectoryAnalysisType.File &&
                         !_fileFilterArray.Contains(s.Name)))
            {
                sb.AppendLine(GetIndentation(directoryAnalysisData.Depth) + directoryAnalysisData.Name.Split('.')[0] +
                              ":");
                sb.AppendLine(GetIndentation(directoryAnalysisData.Depth + 1) + directoryAnalysisData.Name);
            }
        }

        void RecursiveGenerate(DirectoryAnalysisData data, StringBuilder sb, int maxDepth)
        {
            if (data.Depth == 1 && _folderFilterArray.Contains(data.Name))
            {
                return;
            }

            if (data.Depth == 1 && data.AnalysisType == DirectoryAnalysisData.DirectoryAnalysisType.Folder)
            {
                string indentString = GetIndentation(data.Depth);
                string finalLine = indentString + data.Name + ":";
                sb.AppendLine(finalLine);
            }

            List<DirectoryAnalysisData> subDataList = data.SubDirectoryData;
            if (subDataList == null || subDataList.Count <= 0)
            {
                return;
            }

            foreach (DirectoryAnalysisData subData in data.SubDirectoryData)
            {
                int depth = subData.Depth;
                string indentString = GetIndentation(depth);
                string finalLine = indentString + subData.Name + ":";
                if (subData.AnalysisType == DirectoryAnalysisData.DirectoryAnalysisType.File)
                {
                    finalLine = indentString + subData.Name.Split('.')[0] + ": " + subData.RelativePath;
                }

                sb.AppendLine(finalLine);
                if (depth >= maxDepth)
                {
                    continue;
                }

                RecursiveGenerate(subData, sb, maxDepth);
            }
        }

        protected override string GetIndentation(int depth)
        {
            var indent = "";
            for (var i = 0; i < depth; i++)
            {
                indent += "  ";
            }

            indent += "- ";
            return indent;
        }
    }
}
