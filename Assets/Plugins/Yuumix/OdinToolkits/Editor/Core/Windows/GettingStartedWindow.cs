using Sirenix.OdinInspector;
using Sirenix.OdinInspector.Editor;
using Sirenix.Utilities;
using Sirenix.Utilities.Editor;
using UnityEditor;
using UnityEngine;
using Yuumix.OdinToolkits.Core;
using Yuumix.OdinToolkits.Editor.Shared;
using YuumixEditor;

namespace Yuumix.OdinToolkits.Editor.Core
{
    public class GettingStartedWindow : OdinEditorWindow
    {
        [GUIColor("green")]
        [PropertyOrder(-100)]
        [PropertySpace]
        [MultiLanguageDisplayAsStringWidgetConfig(fontSize: 24, enableRichText: true, alignment: TextAlignment.Center)]
        public MultiLanguageDisplayAsStringWidget titleHeader =
            new MultiLanguageDisplayAsStringWidget("简介", "Introduction");

        [PropertyOrder(-100)]
        [OnInspectorGUI]
        public void Separate()
        {
            SirenixEditorGUI.DrawThickHorizontalSeperator(4, 5, 10);
        }

        [PropertyOrder(-90)]
        [MultiLanguageDisplayAsStringWidgetConfig(fontSize: 14, enableRichText: true, alignment: TextAlignment.Center)]
        public MultiLanguageDisplayAsStringWidget introduction = new MultiLanguageDisplayAsStringWidget(
            "Odin Toolkits 是依赖 Unity 插件 Odin Inspector And Serializer 的第三方扩展工具集，模块化设计，积累对整个项目低侵入性的解决方案，按需使用。",
            "Odin Toolkits is a third-party extension toolkit that relies on the Unity plugin Odin Inspector And Serializer. It is designed in a modular way, accumulating low-intrusive solutions for the entire project and can be used as needed.");

        [GUIColor("green")]
        [PropertyOrder(-50)]
        [PropertySpace(16)]
        [MultiLanguageDisplayAsStringWidgetConfig(fontSize: 24, enableRichText: true, alignment: TextAlignment.Center)]
        public MultiLanguageDisplayAsStringWidget core =
            new MultiLanguageDisplayAsStringWidget("核心功能模块", "Core Module");

        [PropertyOrder(-50)]
        [OnInspectorGUI]
        public void Separate1()
        {
            SirenixEditorGUI.DrawThickHorizontalSeperator(4, 5, 10);
        }

        [PropertyOrder(-40)]
        [PropertySpace(5)]
        [MultiLanguageButton("Odin 特性中文总览", "AttributeOverview Pro", ButtonSizes.Large)]
        public void OpenAttributeOverviewPro()
        {
            OdinAttributeOverviewProWindow.ShowWindow();
        }

        [PropertyOrder(-40)]
        [PropertySpace(5)]
        [Button("$ScriptDocGenButtonName", ButtonSizes.Large)]
        public void OpenScriptDocGen()
        {
            ToolsPackageWindow.ShowWindow();
            GetWindow<ToolsPackageWindow>().TrySelectMenuItemWithObject(ScriptDocGenToolSO.Instance);
        }

        string ScriptDocGenButtonName => ScriptDocGenToolSO.ScriptDocGenToolMenuPathData.GetCurrentOrFallback();

        [PropertyOrder(-40)]
        [Button("$TemplateCodeGenButtonName", ButtonSizes.Large)]
        [PropertySpace(5)]
        public void OpenTemplateCodeGen()
        {
            ToolsPackageWindow.ShowWindow();
            GetWindow<ToolsPackageWindow>().TrySelectMenuItemWithObject(TemplateCodeGenToolSO.Instance);
        }

        string TemplateCodeGenButtonName =>
            TemplateCodeGenToolSO.GenerateTemplateToolMenuPathData.GetCurrentOrFallback();

        [PropertyOrder(-40)]
        [MultiLanguageButton("双语特性模块", "MultiLanguage Attributes", ButtonSizes.Large)]
        [PropertySpace(5)]
        public void PingMultiLanguage()
        {
            ProjectEditorUtil.PingAndSelectAsset(OdinToolkitsPaths.GetRootPath() +
                                                 "/Runtime/Core/InspectorMultiLanguage/Attributes");
            ShowToast(ToastPosition.BottomRight, SdfIconType.Info, "已跳转至 MultiLanguage Attributes 文件夹", Color.white,
                3f);
        }

        [GUIColor("green")]
        [PropertyOrder(-30)]
        [PropertySpace(16)]
        [MultiLanguageDisplayAsStringWidgetConfig(fontSize: 24, enableRichText: true, alignment: TextAlignment.Center)]
        public MultiLanguageDisplayAsStringWidget help =
            new MultiLanguageDisplayAsStringWidget("链接", "Help Links");

        [PropertyOrder(-29)]
        [OnInspectorGUI]
        public void Separate2()
        {
            SirenixEditorGUI.DrawThickHorizontalSeperator(4, 5, 10);
        }

        [PropertyOrder(-25)]
        [MultiLanguageButton("GitHub 仓库: https://github.com/Yuumi-Zeus/OdinToolkits-For-Unity",
            "GitHub Repo: https://github.com/Yuumi-Zeus/OdinToolkits-For-Unity", ButtonSizes.Large,
            icon: SdfIconType.Github)]
        [PropertySpace(5)]
        public void OpenUrlGitHub()
        {
            Application.OpenURL("https://github.com/Yuumi-Zeus/OdinToolkits-For-Unity");
        }

        [PropertyOrder(-20)]
        [MultiLanguageButton("Odin Toolkits 文档网站: https://www.odintoolkits.cn/",
            "Odin Toolkits Web: https://odintoolkits.cn/", ButtonSizes.Large)]
        [PropertySpace(5)]
        public void OpenOdinToolkitsWeb()
        {
            Application.OpenURL("https://www.odintoolkits.cn/");
        }

        [PropertyOrder(-20)]
        [MultiLanguageButton("Odin Inspector 官网: https://odininspector.com/",
            "Odin Inspector Web: https://odininspector.com/", ButtonSizes.Large)]
        [PropertySpace(5)]
        public void OpenOdinInspector()
        {
            Application.OpenURL("https://odininspector.com/");
        }

        [PropertyOrder(-20)]
        [MultiLanguageButton("关于 Odin Inspector 许可协议: https://odininspector.com/pricing",
            "Odin Inspector License Prices: https://odininspector.com/pricing", ButtonSizes.Large)]
        [PropertySpace(5)]
        public void OpenOdinInspectorPrice()
        {
            Application.OpenURL("https://odininspector.com/pricing");
        }

        [PropertyOrder(100)]
        [OnInspectorGUI]
        [PropertySpace(10)]
        public void Separate3()
        {
            SirenixEditorGUI.DrawThickHorizontalSeperator(4, 5, 10);
        }

        [PropertyOrder(110)]
        [DisplayAsString(TextAlignment.Center)]
        [ShowInInspector]
        [HideLabel]
        public string Version => OdinToolkitsVersions.VERSION;

        [MenuItem(OdinToolkitsWindowMenuItems.GETTING_STARTED, false,
            OdinToolkitsWindowMenuItems.GETTING_STARTED_PRIORITY)]
        public static void OpenWindow()
        {
            var window = GetWindow<GettingStartedWindow>();
            window.position = GUIHelper.GetEditorWindowRect().AlignCenter(800, 700);
            window.titleContent = new GUIContent(OdinToolkitsWindowMenuItems.GETTING_STARTED_WINDOW_NAME);
            window.ShowUtility();
        }
    }
}
