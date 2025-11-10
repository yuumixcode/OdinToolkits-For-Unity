using Sirenix.OdinInspector;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Yuumix.OdinToolkits.Core;

namespace Yuumix.OdinToolkits.Modules.Editor
{
    public class DirectoryAnalysisData
    {
        public static string CurrentRootPath;

        internal DirectoryAnalysisType AnalysisType;

        public string Name;
        public string RelativePath = string.Empty;

        public List<DirectoryAnalysisData> SubDirectoryData;

        [ShowInInspector]
        public int Depth => !RelativePath.IsNullOrWhiteSpace() ? RelativePath.Split('/').Length : 0;

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
            var subDirectoryInfos = directoryInfo.GetDirectories();
            var subFileInfos = directoryInfo.GetFiles();
            if (subDirectoryInfos.Length + subFileInfos.Length <= 0)
            {
                data.SubDirectoryData = null;
                return data;
            }

            data.SubDirectoryData = new List<DirectoryAnalysisData>();
            foreach (var subDirectoryInfo in subDirectoryInfos)
            {
                data.SubDirectoryData.Add(FromDirectoryInfo(subDirectoryInfo));
            }

            foreach (var subFileInfo in subFileInfos)
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

        #region Nested type: DirectoryAnalysisType

        internal enum DirectoryAnalysisType
        {
            Folder,
            File
        }

        #endregion
    }
}
