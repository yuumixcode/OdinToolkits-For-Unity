using Sirenix.OdinInspector;
using UnityEngine;

namespace Yuumix.OdinToolkits.ThirdParty.ResolvedParametersOverview.Schwapo.Editor.Examples
{
    [ResolvedParameterExample]
    public class FilePathExamples_ParentFolder
    {
        [FoldoutGroup("Attribute Expression Example")]
        [InfoBox("Click the folder icon to see the attribute in effect")]
        [FilePath(ParentFolder = "@UseDataPath ? UnityDataPath : ParentFolder")]
        public string AttributeExpressionExample;

        [FoldoutGroup("Field Name Example")]
        [InfoBox("Click the folder icon to see the attribute in effect")]
        [FilePath(ParentFolder = "$ParentFolder")]
        public string FieldNameExample;

        [FoldoutGroup("Method Name Example")]
        [InfoBox("Click the folder icon to see the attribute in effect")]
        [FilePath(ParentFolder = "$GetParentFolder")]
        public string MethodNameExample;

        public string ParentFolder = "C:\\";

        [FoldoutGroup("Property Name Example")]
        [InfoBox("Click the folder icon to see the attribute in effect")]
        [FilePath(ParentFolder = "$UnityDataPath")]
        public string PropertyNameExample;

        public bool UseDataPath = true;
        public string UnityDataPath => UseDataPath ? Application.dataPath : ParentFolder;

        string GetParentFolder() => UseDataPath ? UnityDataPath : ParentFolder;
    }
    // End

    [ResolvedParameterExample]
    public class FilePathExamples_Extensions
    {
        public string AllowedExtensions = "cs, dll, txt, png, jpg";

        [FoldoutGroup("Attribute Expression Example")]
        [FilePath(Extensions = "@OnlyAllowImages ? \"png, jpg\" : AllowedExtensions")]
        public string AttributeExpressionExample;

        [FoldoutGroup("Field Name Example")]
        [FilePath(Extensions = "$AllowedExtensions")]
        public string FieldNameExample;

        [FoldoutGroup("Method Name Example")]
        [FilePath(Extensions = "$GetAllowedExtensions")]
        public string MethodNameExample;

        public bool OnlyAllowImages = true;

        [FoldoutGroup("Property Name Example")]
        [FilePath(Extensions = "$AllowedExtensionsProperty")]
        public string PropertyNameExample;

        public string AllowedExtensionsProperty => OnlyAllowImages ? "png, jpg" : AllowedExtensions;

        string GetAllowedExtensions() => OnlyAllowImages ? "png, jpg" : AllowedExtensions;
    }
    // End
}
