using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using System.Linq;

namespace SourceGenerator;

[Generator]
public class UnityPerformanceAnalyzer : IIncrementalGenerator
{
    public void Initialize(IncrementalGeneratorInitializationContext context)
    {
        var syntaxProvider = context.SyntaxProvider.CreateSyntaxProvider(
                predicate: static (node, _) => IsPotentialMonoBehaviour(node),
                transform: static (context, _) => (ClassDeclarationSyntax)context.Node)
            .Where(static node => node != null);
        context.RegisterSourceOutput(syntaxProvider, ((productionContext, classNode) =>
        {
            if (HasEmptyUpdateMethod(classNode))
            {
                var diagnostic = Diagnostic.Create(EmptyUpdateRule,
                    classNode.Identifier.GetLocation(),
                    classNode.Identifier.Text);
                productionContext.ReportDiagnostic(diagnostic);
            }

            #region 检测存在多个 Debug.Log() 方法 Demo

            // if (HasExcessiveDebugLogs(classNode))
            // {
            //     var diagnostic = Diagnostic.Create(ExcessiveDebugLogs,
            //         classNode.Identifier.GetLocation(),
            //         classNode.Identifier.Text);
            //     productionContext.ReportDiagnostic(diagnostic);
            // }

            #endregion
        }));
    }

    static readonly DiagnosticDescriptor EmptyUpdateRule = new DiagnosticDescriptor(
        id: "UPA001",
        title: "Empty Update() Detected",
        messageFormat: "Class '{0}' contains an empty Update() method, which may harm performance",
        category: "Performance",
        DiagnosticSeverity.Warning,
        isEnabledByDefault: true);

    static readonly DiagnosticDescriptor ExcessiveDebugLogs = new DiagnosticDescriptor(
        id: "UPA002",
        title: "Excessive Debug.Log() Detected",
        messageFormat: "Class '{0}' has excessive Debug.Log() calls, which may impact performance",
        category: "Performance",
        DiagnosticSeverity.Warning,
        isEnabledByDefault: true);

    static bool HasExcessiveDebugLogs(ClassDeclarationSyntax classDeclaration) =>
        classDeclaration.Members.OfType<MethodDeclarationSyntax>()
            .SelectMany(m => m.Body?.Statements.OfType<ExpressionStatementSyntax>() ?? [])
            .Count(syntax => syntax.Expression is InvocationExpressionSyntax invocation &&
                             invocation.Expression.ToString().Contains("Debug.Log")) >= 3;

    static bool HasEmptyUpdateMethod(ClassDeclarationSyntax classDeclaration)
        => classDeclaration.Members.OfType<MethodDeclarationSyntax>()
            .Any(m => m.Identifier.Text == "Update" && m.Body?.Statements.Count == 0);

    static bool IsPotentialMonoBehaviour(SyntaxNode node) =>
        node is ClassDeclarationSyntax classDeclaration &&
        classDeclaration.BaseList?.Types.Any(x => x.Type.ToString() == "MonoBehaviour") == true;
}
