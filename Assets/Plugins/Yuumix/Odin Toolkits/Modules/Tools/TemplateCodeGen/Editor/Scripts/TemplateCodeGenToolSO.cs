using Sirenix.OdinInspector;
using Sirenix.OdinInspector.Editor;
using System;
using System.Collections.Generic;
using System.IO;
using UnityEditor;
using UnityEngine;
using Yuumix.OdinToolkits.Common.Contributors;
using Yuumix.OdinToolkits.Common.Editor;
using Yuumix.OdinToolkits.Common.InspectorMultiLanguage;
using Yuumix.OdinToolkits.Common.Version;
using Yuumix.YuumixEditor;

namespace Yuumix.OdinToolkits.Modules.Tools.TemplateCodeGen.Editor
{
    public class TemplateCodeGenToolSO : EditorScriptableSingleton<TemplateCodeGenToolSO>
    {
        public static MultiLanguageData GenerateTemplateToolMenuPathData =
            new MultiLanguageData("模板代码生成工具", "Generate Template Tool");

        public const string NameSpaceSymbol = "#NAMESPACE#";
        public const string ClassNameSymbol = "#CLASSNAME#";
        public static event Action<ToastPosition, SdfIconType, string, Color, float> OnToastEvent;

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
        [ValueDropdown("TryGetPath", IsUniqueList = true, ExcludeExistingValuesInList = true)]
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
        public MultiLanguageFooterWidget footer = new MultiLanguageFooterWidget(new[]
                { YuumiZeus.ToContributor("2022/06/27") },
            lastUpdate: "2022/06/27", new[]
            {
                new MultiLanguageData(VersionSpecialString.Roadmap + "多脚本生成(求贡献)",
                    VersionSpecialString.Roadmap + "Multi-script Generation (request for contribution)")
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
            RaiseOnToastEvent(ToastPosition.BottomRight, SdfIconType.InfoSquareFill, msg1, Color.white, 3);
            templateList.Clear();
            TemplatePathMaps.Clear();
            var noTxtNumber = 0;
            foreach (var template in templatePathConfig)
            {
                var content = template.Split('/')[^1];
                // OdinEditorLog.Log(content);
                if (content.EndsWith(".txt"))
                {
                    var templateName = content.Replace(".txt", "");
                    if (!TemplatePathMaps.TryAdd(templateName, template))
                    {
                        const string msg2 = "发现重复添加模板，请修改";
                        RaiseOnToastEvent(ToastPosition.BottomRight, SdfIconType.ExclamationLg, msg2, Color.yellow, 5);
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
                var msg3 = "存在" + noTxtNumber + "个非 .txt 类型的文件，请重新选择路径";
                RaiseOnToastEvent(ToastPosition.BottomRight, SdfIconType.ExclamationLg, msg3, Color.yellow, 5);
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
                RaiseOnToastEvent(ToastPosition.BottomRight, SdfIconType.ExclamationLg, msg, Color.red, 5);
                return;
            }

            if (!File.Exists(targetPath))
            {
                const string msg = "目标文件夹路径不存在，请修改！";
                RaiseOnToastEvent(ToastPosition.BottomRight, SdfIconType.ExclamationLg, msg, Color.red, 5);
            }

            TemplatePathMaps.TryGetValue(targetTemplateKey, out var templatePath);
            if (templatePath == null)
            {
                const string msg = "不存在这个模板，请修改模板路径配置后，点击应用配置按钮";
                RaiseOnToastEvent(ToastPosition.BottomRight, SdfIconType.ExclamationLg, msg, Color.red, 5);
                return;
            }

            var absolutePath = Path.GetFullPath(templatePath);
            // Debug.Log("模板绝对路径为: " + absolutePath);
            var templateContent = File.ReadAllText(absolutePath);
            if (!templateContent.Contains(NameSpaceSymbol) || !templateContent.Contains(ClassNameSymbol))
            {
                const string msg = "模板中不存在 " + NameSpaceSymbol + " 或 " + ClassNameSymbol + " 占位符";
                RaiseOnToastEvent(ToastPosition.BottomRight, SdfIconType.ExclamationLg, msg, Color.red, 5);
                return;
            }

            templateContent = templateContent.Replace(NameSpaceSymbol, targetNamespace);
            templateContent = templateContent.Replace(ClassNameSymbol, targetClassName);
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
            ProjectEditorUtil.PingAndSelectAsset(codeRelativePath);
        }

        static void RaiseOnToastEvent(ToastPosition position, SdfIconType icon, string msg, Color color, float duration)
        {
            OnToastEvent?.Invoke(position, icon, msg, color, duration);
        }
    }
}
