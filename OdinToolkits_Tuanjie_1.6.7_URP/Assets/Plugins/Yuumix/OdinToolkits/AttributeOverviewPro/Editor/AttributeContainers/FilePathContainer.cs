using System.Collections.Generic;
using Yuumix.OdinToolkits.AttributeOverviewPro.Editor;
using Yuumix.OdinToolkits.AttributeOverviewPro.Shared;

namespace Yuumix.OdinToolkits.Modules.Editor
{
    public class FilePathContainer : OdinAttributeContainerSO
    {
        protected override string GetHeader() => "FilePath";

        protected override string GetIntroduction() => "将 string 类型 Property 绘制成一个文件路径读取器";

        protected override List<string> GetTips() =>
            new List<string>
            {
                "Mac 路径是 / 连接，Windows 是 \\ 连接",
                "设定了父文件夹时，获得实际路径必须手动把填写的父文件夹加上，字段值并不会包含父文件夹"
            };

        protected override List<ParameterValue> GetParamValues() =>
            new List<ParameterValue>
            {
                new ParameterValue
                {
                    ReturnType = "bool",
                    ParameterName = "AbsolutePath",
                    ParameterDescription = "是否使用绝对路径，默认为 false"
                },
                new ParameterValue
                {
                    ReturnType = "string[]",
                    ParameterName = "Extensions",
                    ParameterDescription = "文件后缀，默认为空"
                },
                new ParameterValue
                {
                    ReturnType = "string",
                    ParameterName = "ParentFolder",
                    ParameterDescription = "父文件夹路径，默认为空"
                },
                new ParameterValue
                {
                    ReturnType = "bool",
                    ParameterName = "RequireExistingPath",
                    ParameterDescription = "是否要求文件存在，默认为 false"
                },
                new ParameterValue
                {
                    ReturnType = "bool",
                    ParameterName = "UseBackslashes",
                    ParameterDescription = "是否使用反斜杠，默认为 false"
                },
                new ParameterValue
                {
                    ReturnType = "bool",
                    ParameterName = "IncludeFileExtension",
                    ParameterDescription = "是否包含文件后缀，默认为 true"
                }
            };

        protected override string GetOriginalCode() => ReadCodeWithoutNamespace(typeof(FilePathExample));
    }
}
