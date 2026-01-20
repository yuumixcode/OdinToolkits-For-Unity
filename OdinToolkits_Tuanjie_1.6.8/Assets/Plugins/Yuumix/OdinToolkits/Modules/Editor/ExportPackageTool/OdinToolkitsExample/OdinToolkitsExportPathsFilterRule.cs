namespace Yuumix.OdinToolkits.Modules.Editor
{
    public class OdinToolkitsExportPathsFilterRule : ExportPathsFilterRule
    {
        public override bool IsExcludedFile(string filePath) => !filePath.StartsWith("Assets/Plugins/");

        public override bool IsExcludedFolder(string folderPath) => !folderPath.StartsWith("Assets/");
    }
}
