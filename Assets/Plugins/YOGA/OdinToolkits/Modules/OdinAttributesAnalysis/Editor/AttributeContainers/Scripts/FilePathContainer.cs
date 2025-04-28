using System.Collections.Generic;

namespace YOGA.OdinToolkits.AnalysisManual.OdinAttributes.Editor
{
    public class FilePathContainer : AbsContainer
    {
        protected override string SetHeader()
        {
            return "FilePath";
        }

        protected override string SetBrief()
        {
            return "将 string 类型 Property 绘制成一个文件路径读取器";
        }

        protected override List<string> SetTip()
        {
            return new List<string>
            {
                "Mac 路径是 / 连接，Windows 是 \\ 连接",
                "设定了父文件夹时，获得实际路径必须手动把填写的父文件夹加上，字段值并不会包含父文件夹"
            };
        }

        protected override List<ParamValue> SetParamValues()
        {
            return new List<ParamValue>
            {
                new()
                {
                    returnType = "bool",
                    paramName = "AbsolutePath",
                    paramDescription = "是否使用绝对路径，默认为 false"
                },
                new()
                {
                    returnType = "string[]",
                    paramName = "Extensions",
                    paramDescription = "文件后缀，默认为空"
                },
                new()
                {
                    returnType = "string",
                    paramName = "ParentFolder",
                    paramDescription = "父文件夹路径，默认为空"
                },
                new()
                {
                    returnType = "bool",
                    paramName = "RequireExistingPath",
                    paramDescription = "是否要求文件存在，默认为 false"
                },
                new()
                {
                    returnType = "bool",
                    paramName = "UseBackslashes",
                    paramDescription = "是否使用反斜杠，默认为 false"
                },
                new()
                {
                    returnType = "bool",
                    paramName = "IncludeFileExtension",
                    paramDescription = "是否包含文件后缀，默认为 true"
                }
            };
        }

        protected override string SetOriginalCode()
        {
            return ReadCodeWithoutNamespace(typeof(FilePathExample));
        }
    }
}