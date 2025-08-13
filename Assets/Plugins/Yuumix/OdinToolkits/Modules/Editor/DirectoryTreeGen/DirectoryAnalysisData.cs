using System.Collections.Generic;
using System.IO;
using System.Linq;
using Sirenix.OdinInspector;
using Yuumix.Universal;

namespace Yuumix.OdinToolkits.Modules.Editor
{
    public class DirectoryAnalysisData
    {
        public static string CurrentRootPath;

        internal enum DirectoryAnalysisType
        {
            Folder,
            File
        }

        internal DirectoryAnalysisType AnalysisType;

        public string Name;
        public string RelativePath = string.Empty;

        [ShowInInspector] public int Depth => !RelativePath.IsNullOrWhiteSpace() ? RelativePath.Split('/').Length : 0;

        public List<DirectoryAnalysisData> SubDirectoryData;

        public static DirectoryAnalysisData FromDirectoryInfo(DirectoryInfo directoryInfo)
        {
            var data = new DirectoryAnalysisData
            {
                AnalysisType = DirectoryAnalysisType.Folder,
                Name = directoryInfo.Name,
                RelativePath = directoryInfo.FullName.Equals(CurrentRootPath)
                    ? "Root"
                    : directoryInfo.FullName.Replace(CurrentRootPath + "/", "")
            };
            DirectoryInfo[] subDirectoryInfos = directoryInfo.GetDirectories();
            FileInfo[] subFileInfos = directoryInfo.GetFiles();
            if (subDirectoryInfos.Length + subFileInfos.Length <= 0)
            {
                data.SubDirectoryData = null;
                return data;
            }

            data.SubDirectoryData = new List<DirectoryAnalysisData>();
            foreach (DirectoryInfo subDirectoryInfo in subDirectoryInfos)
            {
                data.SubDirectoryData.Add(FromDirectoryInfo(subDirectoryInfo));
            }

            foreach (FileInfo subFileInfo in subFileInfos)
            {
                if (PredicateFilterFileInfo(subFileInfo))
                {
                    continue;
                }

                data.SubDirectoryData.Add(FromFileInfo(subFileInfo));
            }

            // Debug.Log(CurrentRootPath);
            return data;
        }

        public static DirectoryAnalysisData FromFileInfo(FileInfo fileInfo)
        {
            var data = new DirectoryAnalysisData
            {
                AnalysisType = DirectoryAnalysisType.File,
                Name = fileInfo.Name,
                SubDirectoryData = null,
                RelativePath = fileInfo.FullName.Replace(CurrentRootPath + "/", string.Empty)
            };

            return data;
        }

        public static bool PredicateFilterFileInfo(FileInfo fileInfo)
        {
            if (DirectoryTreeGenToolSO.FileExtensionsFilterArray.Any(x => fileInfo.Extension.Contains(x)))
            {
                return true;
            }

            if (fileInfo.Name.StartsWith("."))
            {
                return true;
            }

            return false;
        }
    }
}
