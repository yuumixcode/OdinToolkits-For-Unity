using System.Collections.Generic;
using System.IO;
using Sirenix.OdinInspector;
using UnityEditor;
using UnityEngine;
using YOGA.Modules.OdinToolkits.Editor.GenerateTemplateCode.Scripts;
using YOGA.OdinToolkits.Common.Runtime;
using Yoga.Shared.Utility;
using Yoga.Shared.Utility.YuumiEditor;

namespace YOGA.Modules.OdinToolkits.Editor.GenerateTemplateCode.Specific.AttributeAnalysisGenCode
{
    public class AttributeAnalysisGenCodeTool : ScriptableObject
    {
        [LabelText("Example 脚本命名空间: ")] public string exampleCodeNamespace;

        [LabelText("Container 脚本命名空间: ")] public string containerCodeNamespace;

        [LabelText("特性名称:")] public string attributeName;

        [TitleGroup("目标文件夹路径", "最终生成脚本文件的位置")]
        [BoxGroup("目标文件夹路径/Example 脚本所在文件夹")]
        [ValueDropdown("TryGetPath", IsUniqueList = true, ExcludeExistingValuesInList = true)]
        [HideLabel]
        public string exampleCodeTargetPath;

        [BoxGroup("目标文件夹路径/Container 脚本所在文件夹")]
        [ValueDropdown("TryGetPath", IsUniqueList = true, ExcludeExistingValuesInList = true)]
        [HideLabel]
        public string containerCodeTargetPath;

        [FolderPath] [LabelText("预存文件夹路径: ")] public List<string> preSavePaths;

        [PropertyOrder(10)]
        [TitleGroup("脚本信息预览")]
        [ShowInInspector]
        [DisplayAsString]
        [LabelText("最终 Example 脚本文件名: ")]
        public string PreviewExampleScriptName
        {
            get => attributeName + "Example";
            set => attributeName = value;
        }

        [PropertyOrder(10)]
        [TitleGroup("脚本信息预览")]
        [ShowInInspector]
        [DisplayAsString]
        [LabelText("最终 Container 脚本文件名: ")]
        public string PreviewContainerScriptName
        {
            get => attributeName + "Container";
            set => attributeName = value;
        }

        public List<string> TryGetPath()
        {
            return preSavePaths;
        }

        [Button("生成代码")]
        public void Generate()
        {
            GenerateCode(exampleCodeNamespace, PreviewExampleScriptName, exampleCodeTargetPath,
                TemplateType.AttributeExample);
            GenerateCode(containerCodeNamespace, PreviewContainerScriptName, containerCodeTargetPath
                , TemplateType.AttributeContainer);
        }

        private static void GenerateCode(string targetNamespace, string targetClassName, string targetPath,
            TemplateType targetTemplateType)
        {
            if (string.IsNullOrEmpty(targetNamespace) || string.IsNullOrEmpty(targetClassName) ||
                string.IsNullOrEmpty(targetPath))
            {
                OdinLog.Error("请填写完整信息");
                return;
            }

            var templatePath = TemplateFilePath.TemplateFilePathDict[targetTemplateType];
            var absolutePath = Path.GetFullPath(templatePath);
            // Debug.Log("模板绝对路径为: " + absolutePath);
            var templateContent = File.ReadAllText(absolutePath);
            templateContent = templateContent.Replace("#NAMESPACE#", targetNamespace);
            templateContent = templateContent.Replace("#CLASSNAME#", targetClassName);
            templateContent = templateContent.Replace("#ATTRIBUTE#", targetClassName.Replace("Container", ""));
            // Debug.Log("读取到的模板代码替换后为: " + templateContent);
            var codeRelativePath = targetPath + "/" + targetClassName + ".cs";
            // Debug.Log("目标文件的相对路径为: " + codeRelativePath);
            var codeAbsolutePath = Path.GetFullPath(codeRelativePath);
            // Debug.Log("目标文件的绝对路径为: " + codeAbsolutePath);
            if (File.Exists(codeAbsolutePath))
                if (!EditorUtility.DisplayDialog("生成脚本冲突", "目标文件夹内已经存在相同名称的脚本，此操作无法撤回，是否确定覆盖原脚本?",
                        "确认覆盖", "取消"))
                    return;

            File.WriteAllText(codeAbsolutePath, templateContent);
            AssetDatabase.ImportAsset(codeRelativePath);
            AssetDatabase.SaveAssets();
            AssetDatabase.Refresh();
            ProjectEditorUtility.PingAndSelectAsset(codeRelativePath);
        }
    }
}