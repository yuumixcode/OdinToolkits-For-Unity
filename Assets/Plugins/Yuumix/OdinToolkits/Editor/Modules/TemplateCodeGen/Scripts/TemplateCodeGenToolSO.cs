using System;
using System.Collections.Generic;
using System.IO;
using Sirenix.OdinInspector;
using Sirenix.OdinInspector.Editor;
using UnityEditor;
using UnityEngine;
using Yuumix.OdinToolkits.Core;
using Yuumix.OdinToolkits.Editor.Shared;
using YuumixEditor;

namespace Yuumix.OdinToolkits.Editor
{
    public class TemplateCodeGenToolSO : OdinEditorScriptableSingleton<TemplateCodeGenToolSO>
    {
        public static MultiLanguageData GenerateTemplateToolMenuPathData =
            new MultiLanguageData("模板代码生成工具", "Generate Template Tool");

        public const string NAME_SPACE_SYMBOL = "#NAMESPACE#";
        public const string CLASS_NAME_SYMBOL = "#CLASSNAME#";
        public static event Action<ToastPosition, SdfIconType, string, Color, float> ToastEvent;

        [PropertyOrder(-99)]
        public MultiLanguageHeaderWidget headerWidget = new MultiLanguageHeaderWidget(
            GenerateTemplateToolMenuPathData.GetChinese(),
            GenerateTemplateToolMenuPathData.GetEnglish(),
            "快速配置模板代码，一键生成脚本。", "Quickly configure template code and generate scripts with one click.");

        [PropertyOrder(-1)]
        [MultiLanguageTitle("脚本所在命名空间", "Namespace Config")]
        [HideLabel]
        public string codeNamespace;

        [PropertyOrder(-1)]
        [MultiLanguageTitle("脚本类名", "Script Class Name")]
        [HideLabel]
        public string codeClassName;

        [PropertyOrder(-1)]
        [MultiLanguageTitle("脚本模板选择器", "Script Template Selector")]
        [ValueDropdown(nameof(templateList), ExcludeExistingValuesInList = true)]
        [HideLabel]
        public string templateSelector;

        [PropertyOrder(-1)]
        [MultiLanguageTitle("生成脚本文件的文件夹路径", "Target Folder Path")]
        [PropertySpace(SpaceBefore = 0, SpaceAfter = 10)]
        [ValueDropdown(nameof(TryGetPath), IsUniqueList = true, ExcludeExistingValuesInList = true)]
        [HideLabel]
        public string codeTargetPath;

        [PropertyOrder(50)]
        [MultiLanguageTitleGroup("Id", "工具配置", "Tool Config")]
        [MultiLanguageInfoBox("脚本模板文件必须为 .txt 文本文件，修改模板路径后需要点击应用工具配置"
            , "The script template file must be a .txt text file. After modifying the template path, you need to click the \"Apply Config\" button to configure the tool.")]
        [Sirenix.OdinInspector.FilePath(IncludeFileExtension = true, RequireExistingPath = true)]
        [MultiLanguageText("脚本模板路径配置", "Script Template Path Config")]
        public List<string> templatePathConfig;

        [PropertyOrder(50)]
        [MultiLanguageTitleGroup("Id", "工具配置", "Tool Config")]
        [FolderPath]
        [MultiLanguageText("目标文件夹路径配置", "Target Folder Path Config")]
        public List<string> preSavePaths;

        [PropertyOrder(50)]
        [MultiLanguageTitleGroup("Id2", "过程数据", "Process Data")]
        [ReadOnly]
        [ListDrawerSettings(IsReadOnly = true)]
        public List<string> templateList;

        [PropertyOrder(50)]
        [MultiLanguageTitleGroup("Id2", "过程数据", "Process Data")]
        [ReadOnly]
        [DictionaryDrawerSettings(DisplayMode = DictionaryDisplayOptions.Foldout)]
        public Dictionary<string, string> TemplatePathMaps = new Dictionary<string, string>();

        [PropertyOrder(999)]
        public MultiLanguageFooterWidget footer = new MultiLanguageFooterWidget(
            "2025/06/27", new[]
            {
                new MultiLanguageData(VersionSpecialString.Roadmap + "多脚本生成",
                    VersionSpecialString.Roadmap + "Multi-script Generation")
            });

        [ButtonGroup("Btn")]
        [ShowIfChinese]
        [Button("生成代码文件", ButtonSizes.Large, Icon = SdfIconType.ArrowUpSquareFill)]
        public void Generate1()
        {
            GenerateCode(codeNamespace, codeClassName, codeTargetPath, templateSelector);
        }

        [ButtonGroup("Btn")]
        [ShowIfEnglish]
        [Button("Generate Code", ButtonSizes.Large, Icon = SdfIconType.ArrowUpSquareFill)]
        public void Generate2()
        {
            GenerateCode(codeNamespace, codeClassName, codeTargetPath, templateSelector);
        }

        [ButtonGroup("Btn")]
        [ShowIfChinese]
        [Button("应用工具配置", ButtonSizes.Large, Icon = SdfIconType.ArrowDownSquareFill)]
        public void Apply1()
        {
            ApplyConfig();
        }

        [ButtonGroup("Btn")]
        [ShowIfEnglish]
        [Button("Apply Config", ButtonSizes.Large, Icon = SdfIconType.ArrowDownSquareFill)]
        public void Apply2()
        {
            ApplyConfig();
        }

        void ApplyConfig()
        {
            const string msg1 = "配置已应用，重新生成模板映射";
            OnToastEvent(ToastPosition.BottomRight, SdfIconType.InfoSquareFill, msg1, Color.white, 3);
            templateList.Clear();
            TemplatePathMaps.Clear();
            var noTxtNumber = 0;
            foreach (string template in templatePathConfig)
            {
                string content = template.Split('/')[^1];
                // OdinEditorLog.Log(content);
                if (content.EndsWith(".txt"))
                {
                    string templateName = content.Replace(".txt", "");
                    if (!TemplatePathMaps.TryAdd(templateName, template))
                    {
                        const string msg2 = "发现重复添加模板，请修改";
                        OnToastEvent(ToastPosition.BottomRight, SdfIconType.ExclamationLg, msg2, Color.yellow, 5);
                    }
                    else
                    {
                        templateList.Add(templateName);
                    }
                }
                else
                {
                    if (content == string.Empty)
                    {
                        return;
                    }

                    noTxtNumber += 1;
                }
            }

            if (noTxtNumber > 0)
            {
                string msg3 = "存在" + noTxtNumber + "个非 .txt 类型的文件，请重新选择路径";
                OnToastEvent(ToastPosition.BottomRight, SdfIconType.ExclamationLg, msg3, Color.yellow, 5);
            }
        }

        List<string> TryGetPath() => preSavePaths;

        void GenerateCode(string targetNamespace, string targetClassName, string targetPath,
            string targetTemplateKey)
        {
            if (string.IsNullOrEmpty(targetNamespace))
            {
                targetNamespace = "Default";
            }

            if (string.IsNullOrEmpty(targetClassName) ||
                string.IsNullOrEmpty(targetPath))
            {
                const string msg = "请填写完整工具信息！";
                OnToastEvent(ToastPosition.BottomRight, SdfIconType.ExclamationLg, msg, Color.red, 5);
                return;
            }

            if (!File.Exists(targetPath))
            {
                const string msg = "目标文件夹路径不存在，请修改！";
                OnToastEvent(ToastPosition.BottomRight, SdfIconType.ExclamationLg, msg, Color.red, 5);
            }

            TemplatePathMaps.TryGetValue(targetTemplateKey, out string templatePath);
            if (templatePath == null)
            {
                const string msg = "不存在这个模板，请修改模板路径配置后，点击应用配置按钮";
                OnToastEvent(ToastPosition.BottomRight, SdfIconType.ExclamationLg, msg, Color.red, 5);
                return;
            }

            string absolutePath = Path.GetFullPath(templatePath);
            // Debug.Log("模板绝对路径为: " + absolutePath);
            string templateContent = File.ReadAllText(absolutePath);
            if (!templateContent.Contains(NAME_SPACE_SYMBOL) || !templateContent.Contains(CLASS_NAME_SYMBOL))
            {
                const string msg = "模板中不存在 " + NAME_SPACE_SYMBOL + " 或 " + CLASS_NAME_SYMBOL + " 占位符";
                OnToastEvent(ToastPosition.BottomRight, SdfIconType.ExclamationLg, msg, Color.red, 5);
                return;
            }

            templateContent = templateContent.Replace(NAME_SPACE_SYMBOL, targetNamespace);
            templateContent = templateContent.Replace(CLASS_NAME_SYMBOL, targetClassName);
            // Debug.Log("读取到的模板代码替换后为: " + templateContent);
            string codeRelativePath = targetPath + "/" + targetClassName + ".cs";
            string codeAbsolutePath = Path.GetFullPath(codeRelativePath);
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
            ProjectEditorUtil.PingAndSelectAsset(codeRelativePath);
        }

        static void OnToastEvent(ToastPosition position, SdfIconType icon, string msg,
            Color color, float duration)
        {
            ToastEvent?.Invoke(position, icon, msg, color, duration);
        }
    }
}
