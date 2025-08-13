using Sirenix.OdinInspector;
using UnityEngine;

namespace Yuumix.OdinToolkits.Community.Schwapo.Editor
{
    [ResolvedParameterExample]
    public class InlineButtonExamples_Label
    {
        [FoldoutGroup("Attribute Expression Example")]
        [InlineButton("Action", Label = "@Label")]
        public string AttributeExpressionExample;

        public string Label = "Peace, Love & Ducks";

        [FoldoutGroup("Method Name Example")]
        [InlineButton("Action", Label = "$GetLabel")]
        public string MethodNameExample;

        void Action()
        {
            Debug.Log("We don't care about the action in this example");
        }

        string GetLabel() => Label;
    }
    // End

    [ResolvedParameterExample]
    public class InlineButtonExamples_ShowIf
    {
        [FoldoutGroup("Attribute Expression Example")]
        [InlineButton("Action", Label = "Click", ShowIf = "@Show")]
        public string AttributeExpressionExample;

        [FoldoutGroup("Method Name Example")]
        [InlineButton("Action", Label = "Click", ShowIf = "GetShowState")]
        public string MethodNameExample;

        public bool Show;

        void Action()
        {
            Debug.Log("We don't care about the action in this example");
        }

        bool GetShowState() => Show;
    }
    // End

    [ResolvedParameterExample]
    public class InlineButtonExamples_Action
    {
        [FoldoutGroup("Attribute Expression Example")]
        [InlineButton("@Debug.Log(\"Attribute Expression Example\")", Label = "Log Attribute Expression Example")]
        public string AttributeExpressionExample;

        [FoldoutGroup("Method Name Example")]
        [InlineButton("LogMethodNameExample")]
        public string MethodNameExample;

        void LogMethodNameExample()
        {
            Debug.Log("Method Name Example");
        }
    }
    // End
}
