using System;
using System.Runtime.CompilerServices;

namespace YOGA.Modules.OdinToolkits.Schwapo.Editor
{
    [AttributeUsage(AttributeTargets.Class)]
    public class ResolvedParameterExampleAttribute : Attribute
    {
        public ResolvedParameterExampleAttribute(
            [CallerFilePath] string filePath = "unknown",
            [CallerLineNumber] int lineNumber = -1)
        {
            FilePath = filePath;
            LineNumber = lineNumber;
        }

        public string FilePath { get; private set; }
        public int LineNumber { get; private set; }
    }
}