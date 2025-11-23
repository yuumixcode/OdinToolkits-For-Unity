# README

<p align="center">
  <a href="https://www.odintoolkits.cn/"><img src="https://cdn.jsdelivr.net/gh/yuumixcode/Zensical-For-OdinToolkits@main/odintoolkits-logo.webp" width="240" alt="Odin Toolkits Logo"></a>
</p>

<p align="center"><strong>探索 Odin Inspector 进阶功能、整合优质资源、优化游戏开发流程</strong></p>

<p align="center">
<a href="https://docs.unity.cn/cn/tuanjiemanual/1.6/Manual/"><img alt="Tuanjie Version" src="https://img.shields.io/badge/Tuanjie-1.6.7_LTS-blue"></a>
<a href="https://docs.unity3d.com/2021.3/Documentation/Manual/index.html"><img alt="Unity Version" src="https://img.shields.io/badge/Unity-2021.3.45f2c1-blue?style=flat&logo=unity"></a>
<a href="https://odininspector.com/"><img alt="Odin Inspector" src="https://img.shields.io/badge/Odin_Inspector-4.0.1.0-orange?style=flat"></a>
</p>

<p align="center">
<a href="https://github.com/yuumixcode/OdinToolkits-For-Unity?tab=MIT-1-ov-file"><img alt="License" src="https://img.shields.io/badge/License-MIT-green?style=flat"></a>
<a href="https://www.odintoolkits.cn/"><img alt="Main Site GitHub Pages" src="https://img.shields.io/badge/%E4%B8%BB%E7%BD%91%E7%AB%99-www.odintoolkits.cn-green?style=flat"></a>
<a href="https://zensical-for-odintoolkits.readthedocs.io"><img alt="Read The Docs" src="https://img.shields.io/badge/%E5%A4%87%E7%94%A8%E7%BD%91%E7%AB%99-readthedocs.io-orange?style=flat"></a>
</p>

## ⚠️ 重要声明

- `Odin Toolkits` 是开源的、第三方扩展工具集，主要面向 `Odin Inspector and Serializer` 的中文用户。
- `Odin Toolkits` 提供了英文界面解决方案，欢迎贡献完善英文界面。
- 本项目与 Sirenix 公司及官方产品 `Odin Inspector and Serializer` 无任何隶属、合作关系，并非官方衍生产品。
- 本项目不包含 `Odin Inspector and Serializer` 的发行版本。
- 本文中的 `Odin Inspector` 为 `Odin Inspector and Serializer` 的简称。

## 项目愿景

- 成为使用 `Odin Inspector and Serializer` 的开发者的必备扩展工具集。

## 主要模块

1. `Script Doc Generator` ：脚本文档生成工具，一键生成 `API` 文档，也可以自定义文档格式。
2. `Attribute Overview Pro` ：Odin 特性总览窗口 Pro 版，在官方的 `Attribute Overview` 内容上进行扩展。
3. `Tool Packages` ：工具箱模块，提供一些简单的编辑器工具。
4. `Community` ：社区模块，收集、整理、分享使用 `Odin Inspector` 制作的工具以及其他优质资源。
5. 原生多语言特性，在构造函数层面直接添加多语言参数。

## 安装

### 前提条件

- `Tuanjie 1.6.7 LTS` 或 `Unity 2021.3.45f2c1 LTS` 或对应引擎的更高版本
- `Odin Inspector And Serializer` 最新版本。本项目最初基于 `3.3.1.14` 开发。

### 具体步骤

> `Odin Toolkits` 依赖 `Odin Inspector` 插件，请先自行导入 `Odin Inspector` 插件到项目。
>
> 从 [Unity Global AssetStore](https://assetstore.unity.com/packages/tools/utilities/odin-inspector-and-serializer-89041) 和 [Sirenix 官网](https://odininspector.com/) 上购买 `Odin Inspector` 或其他方式获取。

1. 确保项目中已安装 `Odin Inspector`
2. 下载最新发布包
3. 将包导入到 `Unity` 项目中
4. 通过 `Tools/Odin Toolkits` 菜单访问工具

## 开始使用

导入后，您可以通过 Unity 编辑器菜单访问 `Odin Toolkits`：

- `Tools/Odin Toolkits/Getting Started` - 开始菜单
- `Tools/Odin Toolkits/Editor Settings` - 配置编辑器阶段设置
- `Tools/Odin Toolkits/Runtime Config` - 配置运行时设置
- `Tools/Odin Toolkits/Attribute Overview Pro` - Odin 特性总览窗口 Pro 版
- `Tools/Odin Toolkits/Script Doc Generator` - 脚本文档生成工具
- `Tools/Odin Toolkits/Tool Packages` - 工具箱
- `Tools/Odin Toolkits/Community` - 社区模块

## 项目结构

``` markdown
Plugins/
├─ Yuumix/
│  ├─ Community/
│  │  ├─ Editor/
│  │  ├─ Modules/
│  ├─ OdinToolkits/
│  │  ├─ AttributeOverviewPro/
│  │  ├─ Core/
│  │  │  ├─ Editor/
│  │  │  ├─ Resources/
│  │  │  ├─ Runtime/
│  │  ├─ Modules/
│  │  ├─ ScriptDocGenerator/
│  │  ├─ Tests/
```

## 相关链接

[推荐 - Odin Toolkits 主网站](https://www.odintoolkits.cn/)

[Odin Toolkits 备用网站](https://zensical-for-odintoolkits.readthedocs.io)

[Odin Inspector 官方网站](https://odininspector.com/)

[Odin Inspector 许可信息](https://odininspector.com/pricing)

## 许可证

本项目基于 MIT 许可证授权。

### Logo 许可证

查看 `LOGO-LICENSE.md` 文档。

## 更新日志

详细版本历史和变更请查看网站 `Information/CHANGELOG` 章节或项目中的 `CHANGELOG.md`。

## 贡献指南

!!! note

    暂不推荐，因为目前 API 可能随时改变。查看 `CONTRIBUTING.md` 文档。

## 支持与反馈

[zeriying@gmail.com](mailto:zeriying@gmail.com)，发送邮箱时，请添加 `[Odin Toolkits - xxx]` 前缀，例如 `[Odin Toolkits - BUG]`，这样更便于接收反馈，避免遗漏。

感谢你看到这里，如果 `Odin Toolkits` 在你的 `Unity` 开发过程中切实提供了帮助，恳请为项目点亮一颗 ★ Star！

如果 `Odin Toolkits` 打包出现错误，请提 issue，或者联系我，我会尽快处理，业余时间开发，无法即时回复，请优先邮件联系。

## 项目及友链推荐

[QFramework - Unity 开发框架](https://github.com/liangxiegame/QFramework)

[ES Framework - Unity 开发框架](https://github.com/Ey-Sive-I-Save/ESFrameWorkPublish.git)

[Wcowin 的 MkDocs 博客](https://wcowin.work/Mkdocs-Wcowin/)

[Aaron 的博客](https://jaywhj.netlify.app/)

[![Built with Material for MkDocs](https://img.shields.io/badge/Material_for_MkDocs-526CFE?style=for-the-badge&logo=MaterialForMkDocs&logoColor=white)](https://squidfunk.github.io/mkdocs-material/)

[![Zensical](https://img.shields.io/badge/Zensical-orange?style=for-the-badge)](https://zensical.org)
