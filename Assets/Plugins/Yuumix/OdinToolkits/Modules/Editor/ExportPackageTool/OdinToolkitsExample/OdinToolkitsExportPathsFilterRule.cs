namespace Yuumix.OdinToolkits.Modules.Editor
{
    public class OdinToolkitsExportPathsFilterRule : ExportPathsFilterRule
    {
        public override bool IsExcludedFile(string filePath)
        {
            return !filePath.StartsWith("Assets/Plugins/");
        }

        public override bool IsExcludedFolder(string folderPath)
        {
            return !folderPath.StartsWith("Assets/");
        }
    }
}
