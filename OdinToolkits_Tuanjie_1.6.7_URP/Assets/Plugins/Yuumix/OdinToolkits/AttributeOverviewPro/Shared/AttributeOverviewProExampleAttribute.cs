using System;
using System.Runtime.CompilerServices;
using UnityEngine;

namespace Yuumix.OdinToolkits.AttributeOverviewPro.Shared
{
    [HelpURL(
        "https://learn.microsoft.com/en-us/dotnet/api/system.runtime.compilerservices.callerfilepathattribute?view=net-9.0")]
    [AttributeUsage(AttributeTargets.Class)]
    public class AttributeOverviewProExampleAttribute : Attribute
    {
        public AttributeOverviewProExampleAttribute(
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
