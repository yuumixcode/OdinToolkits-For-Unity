# Odin Toolkits

<p align="center">
  <a href="https://yuumixcode.github.io/odintoolkitsdocs/">
    <img src="https://cdn.jsdelivr.net/gh/yuumixcode/odintoolkitsdocs@main/docs/assets/logo-odintoolkits-color-noshadow.png" width="320" alt="Odin Toolkits">
  </a>
</p>

<p align="center">
  <strong>
    Odin Toolkits 是 Odin Inspector and Serializer 插件的第三方扩展工具包。<br/ >
    挖掘插件用法、提供更多优雅的解决方案，优化开发流程。
  </strong>
</p>

---

## 项目愿景

- 成为使用 `Odin Inspector` 的开发者的必备扩展工具包。
- 帮助开发者快速学习使用 `Odin Inspector`，发挥其更多的价值。
- 帮助开发者快速开发，提供更多优雅的解决方案。

## 主要模块

1. 多语言特性扩展，在构造函数层面直接添加多语言参数。

2. `Odin Inspector` 提供的所有 `Attribute` 的中文解析窗口。

3. 文档生成工具，选择特定的类，一键生成 `API` 文档。

4. 模版代码生成工具，选择特定的类，一键生成模版代码。

5. 社区模块，收集、整理、分享 `Odin Inspector` 的使用案例。

## 项目结构

``` markdown
Plugins/
│  ├─ Yuumix/
│  │  ├─ OdinToolkits/
│  │  │  ├─ Community/
│  │  │  │  ├─ Editor/
│  │  │  │  │  ├─ Resources/
│  │  │  │  │  ├─ Scripts/
│  │  │  │  ├─ Modules/
│  │  │  │  │  ├─ ResolvedParametersOverview/
│  │  │  ├─ Core/
│  │  │  │  ├─ Editor/
│  │  │  │  │  ├─ Bilingualism/
│  │  │  │  │  ├─ Misc/
│  │  │  │  │  ├─ Windows/
│  │  │  │  ├─ Runtime/
│  │  │  │  │  ├─ Bilingualism/
│  │  │  │  │  ├─ Logger/
│  │  │  │  │  ├─ Misc/
│  │  │  │  │  ├─ YuumixEditor/
│  │  │  │  │  ├─ Yuumix.cs # OdinToolkits 会自动运行的类
│  │  │  ├─ Modules/
│  │  │  │  ├─ AttributeOverviewPro/
│  │  │  │  ├─ CustomAttributes/
│  │  │  │  ├─ Editor/
│  │  │  │  │  ├─ DirectoryTreeGen/
│  │  │  │  │  ├─ QuickGenerateSO/
│  │  │  │  │  ├─ TemplateCodeGen/
│  │  │  │  ├─ ScriptDocGen/
│  │  │  ├─ Resources/
│  │  │  │  ├─ LogTags/
│  │  │  │  ├─ OdinToolkitsPreferences.asset
│  │  │  ├─ Universal/
│  │  │  │  ├─ BilingualComment/
│  │  │  │  ├─ Extensions/
│  │  │  │  ├─ Singleton/
│  │  │  │  ├─ Utilities/
│  │  ├─ CHANGELOG.md
```

## 兼容性

- 构建版本：Unity 2021.3 LTS
- 理论上支持 Odin Inspector 的最低兼容版本，为 Unity 2019.4 LTS

## 开始使用

### 1.导入 Odin Inspector 插件（必须）

> Odin Toolkits 依赖 Odin Inspector 插件，请先自行导入 Odin Inspector 插件到项目。

你可以从 [Unity AssetStore](https://assetstore.unity.com/packages/tools/utilities/odin-inspector-and-serializer-89041) 和 [Sirenix 官网](https://odininspector.com/) 上购买插件或者其他方式获取插件。

### 2.下载 Release 版本并导入项目

1. 下载 [Release](https://github.com/Yuumi-Zeus/OdinToolkits-For-Unity/releases) 中的 `.unitypackage` 文件。

2. 导入 `.unitypackage` 文件到项目中。

## 贡献指南

- 暂无

## 联系作者

zeriying@gmail.com

## 项目推荐

[![Built with Material for MkDocs](https://img.shields.io/badge/Material_for_MkDocs-526CFE?style=for-the-badge&logo=MaterialForMkDocs&logoColor=white)](https://squidfunk.github.io/mkdocs-material/)

[Wcowin 的 MkDocs 博客](https://wcowin.work/Mkdocs-Wcowin/)

[QFramework - Unity 开发框架](https://github.com/liangxiegame/QFramework)

> 感谢你看到这里，如果 Odin Toolkits 在你的 Unity 开发中切实提供了帮助，恳请为项目点亮一颗 ★ Star！
> 如果 Odin Toolkits 打包出现错误，或者代码规范不符合要求，请提 issue，或者联系我，我会尽快处理。
