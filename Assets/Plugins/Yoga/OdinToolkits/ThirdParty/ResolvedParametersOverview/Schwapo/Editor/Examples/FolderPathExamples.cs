using Sirenix.OdinInspector;

namespace YOGA.Modules.OdinToolkits.Schwapo.Editor.Examples
{
    [ResolvedParameterExample]
    public class FolderPathExamples_ParentFolder
    {
        [FoldoutGroup("Attribute Expression Example")]
        [InfoBox("Click the folder icon to see the attribute in effect")]
        [FolderPath(ParentFolder = "@UseDataPath ? UnityDataPath : ParentFolder")]
        public string AttributeExpressionExample;

        [FoldoutGroup("Field Name Example")]
        [InfoBox("Click the folder icon to see the attribute in effect")]
        [FolderPath(ParentFolder = "$ParentFolder")]
        public string FieldNameExample;

        [FoldoutGroup("Method Name Example")]
        [InfoBox("Click the folder icon to see the attribute in effect")]
        [FolderPath(ParentFolder = "$GetParentFolder")]
        public string MethodNameExample;

        public string ParentFolder = "C:\\";

        [FoldoutGroup("Property Name Example")]
        [InfoBox("Click the folder icon to see the attribute in effect")]
        [FolderPath(ParentFolder = "$UnityDataPath")]
        public string PropertyNameExample;

        public bool UseDataPath = true;
        public string UnityDataPath => UseDataPath ? UnityEngine.Application.dataPath : ParentFolder;

        string GetParentFolder() => UseDataPath ? UnityDataPath : ParentFolder;
    }
    // End
}