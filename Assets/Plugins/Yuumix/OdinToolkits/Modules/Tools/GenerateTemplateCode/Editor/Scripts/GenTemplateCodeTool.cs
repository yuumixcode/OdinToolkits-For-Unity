using Sirenix.OdinInspector;
using System.Collections.Generic;
using System.IO;
using UnityEditor;
using UnityEngine;
using Yuumix.OdinToolkits.Common.Runtime;
using Yuumix.OdinToolkits.Modules.Utilities.YuumiEditor;

namespace Yuumix.OdinToolkits.Modules.Tools.GenerateTemplateCode.Editor.Scripts
{
    public class GenTemplateCodeTool : ScriptableObject
    {
        [LabelText("选择模板: ")] public TemplateType templateType;

        [LabelText("命名空间: ")] public string codeNamespace;

        [LabelText("类名: ")] public string codeClassName;

        [Title("目标文件夹路径", "最终生成脚本文件的位置")]
        [ValueDropdown("TryGetPath", IsUniqueList = true, ExcludeExistingValuesInList = true)]
        [HideLabel]
        public string codeTargetPath;

        [FolderPath] [LabelText("预存文件夹路径: ")] public List<string> preSavePaths;

        public List<string> TryGetPath()
        {
            return preSavePaths;
        }

        [Button("生成代码")]
        public void Generate()
        {
            GenerateCode(codeNamespace, codeClassName, codeTargetPath, templateType);
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
            // Debug.Log("读取到的模板代码替换后为: " + templateContent);
            var codeRelativePath = targetPath + "/" + targetClassName + ".cs";
            var codeAbsolutePath = Path.GetFullPath(codeRelativePath);
            // Debug.Log("目标文件的绝对路径为: " + codePath);
            if (File.Exists(codeAbsolutePath))
            {
                if (!EditorUtility.DisplayDialog("生成脚本冲突", "目标文件夹内已经存在相同名称的脚本，此操作无法撤回，是否确定覆盖原脚本?",
                        "确认覆盖", "取消"))
                {
                    return;
                }
            }

            File.WriteAllText(codeAbsolutePath, templateContent);
            AssetDatabase.ImportAsset(codeRelativePath);
            AssetDatabase.SaveAssets();
            AssetDatabase.Refresh();
            ProjectEditorUtility.PingAndSelectAsset(codeRelativePath);
        }
    }
}