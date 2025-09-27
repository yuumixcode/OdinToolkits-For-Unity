﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using Sirenix.OdinInspector.Editor;
using Sirenix.Utilities;
using UnityEditor;

namespace Yuumix.OdinToolkits.Community.Schwapo.Editor
{
    public class ResolvedParameterExample
    {
        static readonly MethodInfo parseMethod = Type.GetType(
                "Sirenix.OdinInspector.Editor.Examples.SyntaxHighlighter," +
                "Sirenix.OdinInspector.Editor," +
                "Version=1.0.0.0," +
                "Culture=neutral," +
                "PublicKeyToken=null")
            .GetMethod("Parse", BindingFlags.Public | BindingFlags.Static);

        static readonly Func<string, string> ApplyCodeHighlighting =
            (Func<string, string>)Delegate.CreateDelegate(typeof(Func<string, string>), parseMethod);

        readonly PropertyTree tree;

        public string Code;
        public string HighlightedCode;

        public ResolvedParameterExample(Type exampleType)
        {
            ResolvedParameterExampleAttribute exampleAttribute = TypeCache
                .GetTypesWithAttribute<ResolvedParameterExampleAttribute>()
                .Where(type => type == exampleType)
                .Select(type => type.GetAttribute<ResolvedParameterExampleAttribute>())
                .Single();

            IEnumerable<string> splitCode = File.ReadLines(exampleAttribute.FilePath)
                .Skip(exampleAttribute.LineNumber)
                .TakeWhile(line => !line.TrimStart().StartsWith("// End"));

            Code = string.Join("\n", splitCode);
            HighlightedCode = ApplyCodeHighlighting(Code);

            tree = PropertyTree.Create(Activator.CreateInstance(exampleType));
        }

        public void DrawPreview()
        {
            tree.Draw(false);
        }

        public void CollapsePreviews()
        {
            tree.RootProperty.Children
                .Recurse().ForEach(child => child.State.Expanded = false);
        }

        public void Dispose()
        {
            tree.Dispose();
        }
    }
}
