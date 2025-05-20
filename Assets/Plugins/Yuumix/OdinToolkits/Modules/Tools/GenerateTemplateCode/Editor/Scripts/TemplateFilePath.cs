using System.Collections.Generic;
using Yuumix.OdinToolkits.Common.Editor.Locator;

namespace Yuumix.OdinToolkits.Modules.Tools.GenerateTemplateCode.Editor
{
    public static class TemplateFilePath
    {
        public static readonly Dictionary<TemplateType, string> TemplateFilePathDict =
            new Dictionary<TemplateType, string>
        {
            { TemplateType.AttributeExample, TemplateFolderPath + "/AttributeExample.txt" },
            { TemplateType.AttributeContainer, TemplateFolderPath + "/AttributeContainer.txt" }
        };

        private static string TemplateFolderPath => OdinToolkitsPaths.GetRootPath() +
                                                    "/Modules/Tools/GenerateTemplateCode/Editor/Templates";
    }

    public enum TemplateType
    {
        // * Odin Toolkits Start
        AttributeExample,
        AttributeContainer
        // * Odin Toolkits End
    }
}
