using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.Text;
using System.CodeDom.Compiler;
using System.IO;
using System.Text;

namespace SourceGenerator;

[Generator]
public class ExampleSourceGenerator : IIncrementalGenerator
{
    #region ISourceGenerator

    // public void Initialize(GeneratorInitializationContext context)
    // {
    //     // noop
    // }
    //
    // public void Execute(GeneratorExecutionContext context)
    // {
    //     var sourceStream = new MemoryStream();
    //     var sourceStreamWriter = new StreamWriter(sourceStream, Encoding.UTF8);
    //     var codeWriter = new IndentedTextWriter(sourceStreamWriter, "    ");
    //
    //     codeWriter.WriteLine("using System;");
    //     codeWriter.WriteLine("namespace ExampleSourceGenerated {");
    //     codeWriter.Indent++;
    //     
    //     codeWriter.WriteLine("public static class ExampleSourceGenerated {");
    //     codeWriter.Indent++;
    //     
    //     codeWriter.WriteLine("public static string GetTestText()");
    //     codeWriter.WriteLine("{");
    //     codeWriter.Indent++;
    //     
    //     codeWriter.WriteLine("return \"This is from source generator - Generated at build time\";");
    //     
    //     codeWriter.Indent--;
    //     codeWriter.WriteLine("}");
    //     
    //     codeWriter.Indent--;
    //     codeWriter.WriteLine("}");
    //     
    //     codeWriter.Indent--;
    //     codeWriter.WriteLine("}");
    //
    //     sourceStreamWriter.Flush();
    //     context.AddSource("ExampleSourceGenerator.generate.cs",
    //         SourceText.From(sourceStream, Encoding.UTF8, canBeEmbedded: true));
    // }

    #endregion

    public void Initialize(IncrementalGeneratorInitializationContext context)
    {
        var compilationProvider = context.CompilationProvider;
        context.RegisterSourceOutput(compilationProvider, (ctx, compilation) =>
        {
            if (compilation.AssemblyName is "Assembly-CSharp" or "Assembly-CSharp-firstpass")
            {
                ctx.AddSource("ExampleSourceGenerator.generated.cs", GenerateSource());
            }
        });
    }

    SourceText GenerateSource()
    {
        using var sourceStream = new StringWriter();
        using var codeWriter = new IndentedTextWriter(sourceStream, "    ");
        codeWriter.WriteLine("using System;");
        codeWriter.WriteLine("namespace ExampleSourceGenerated {");
        codeWriter.Indent++;

        codeWriter.WriteLine("public static class ExampleSourceGenerated {");
        codeWriter.Indent++;

        codeWriter.WriteLine("public static string GetTestText()");
        codeWriter.WriteLine("{");
        codeWriter.Indent++;

        codeWriter.WriteLine("return \"This is from source generator - Generated at build time\";");

        codeWriter.Indent--;
        codeWriter.WriteLine("}");

        codeWriter.Indent--;
        codeWriter.WriteLine("}");

        codeWriter.Indent--;
        codeWriter.WriteLine("}");

        return SourceText.From(sourceStream.ToString(), Encoding.UTF8);
    }
}
