using Plugins.YOGA.OdinToolkits.Editor;
using System.Collections.Generic;

namespace Plugins.YOGA.OdinToolkits.Modules.GenerateTemplateCode.Editor.Scripts
{
    public static class TemplateFilePath
    {
        public static readonly Dictionary<TemplateType, string> TemplateFilePathDict =
            new()
            {
                // * Odin Toolkits 内置模板 Start
                { TemplateType.AttributeExample, TemplateFolderPath + "/AttributeExample.txt" },
                { TemplateType.AttributeContainer, TemplateFolderPath + "/AttributeContainer.txt" }
                // * Odin Toolkits 内置模板 End
            };

        private static string TemplateFolderPath => OdinToolkitsPaths.GetRootPath() +
                                                    "/Tools/Editor/GenerateTemplateCode/Template";
    }

    public enum TemplateType
    {
        // * Odin Toolkits Start
        AttributeExample,

        AttributeContainer
        // * Odin Toolkits End
    }
}