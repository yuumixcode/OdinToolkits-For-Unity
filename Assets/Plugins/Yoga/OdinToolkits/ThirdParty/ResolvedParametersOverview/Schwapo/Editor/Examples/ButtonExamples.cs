using Sirenix.OdinInspector;

namespace YOGA.Modules.OdinToolkits.Schwapo.Editor.Examples
{
    [ResolvedParameterExample]
    public class ButtonExamples_Name
    {
        public string AwesomeName = "Peace, Love & Ducks";
        public string BoringName = "Button";
        public bool UseAwesomeName;
        public string AwesomeNameProperty => UseAwesomeName ? AwesomeName : BoringName;

        [FoldoutGroup("Field Name Example")]
        [Button(Name = "$AwesomeName")]
        public void FieldNameExample() { }

        [FoldoutGroup("Property Name Example")]
        [Button(Name = "$AwesomeNameProperty")]
        public void PropertyNameExample() { }

        [FoldoutGroup("Attribute Expression Example")]
        [Button(Name = "@UseAwesomeName ? AwesomeName : BoringName")]
        public void AttributeExpressionExample() { }

        [FoldoutGroup("Method Name Example")]
        [Button(Name = "$GetButtonName")]
        public void MethodNameExample() { }

        string GetButtonName() => UseAwesomeName ? AwesomeName : BoringName;
    }
    // End
}