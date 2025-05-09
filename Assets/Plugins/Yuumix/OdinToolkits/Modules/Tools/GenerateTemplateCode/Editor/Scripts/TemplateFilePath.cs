using System.Collections.Generic;
using Yuumix.OdinToolkits.Common.Editor;

namespace Yuumix.OdinToolkits.Modules.Tools.GenerateTemplateCode.Editor.Scripts
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