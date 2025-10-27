using Sirenix.OdinInspector;
using System.Collections;
using System.Collections.Generic;

namespace Yuumix.Community.Schwapo.Editor
{
    [ResolvedParameterExample]
    public class ValueDropdownExamples_ValuesGetter
    {
        public List<string> AlternativeValues = new List<string> { "1", "2", "3" };

        [FoldoutGroup("Attribute Expression Example")]
        [ValueDropdown("@UseAlternativeValues ? AlternativeValues : ValuesProperty")]
        public string AttributeExpressionExample;

        [FoldoutGroup("Field Name Example")]
        [ValueDropdown("@Values")]
        public string FieldNameExample;

        [FoldoutGroup("Method Name Example")]
        [ValueDropdown("GetValues")]
        public string MethodNameExample;

        [FoldoutGroup("Property Name Example")]
        [ValueDropdown("@ValuesProperty")]
        public string PropertyNameExample;

        public bool UseAlternativeValues;
        public List<string> Values = new List<string> { "Value 1", "Value 2", "Value 3" };
        public List<string> ValuesProperty => UseAlternativeValues ? AlternativeValues : Values;

        IEnumerable GetValues() => UseAlternativeValues ? AlternativeValues : Values;
    }
    // End
}
