<p align="center">
  <a href="https://www.odintoolkits.cn/"><img src="https://cdn.jsdelivr.net/gh/yuumixcode/OdinToolkitsDocumentation@main/cdn-assets/logo-color-noshadow.webp" width="240" alt="Odin Toolkits Logo"></a>
</p>

<p align="center"><strong>探索 Odin Inspector 进阶功能、整合社区优质项目、优化游戏开发流程。</strong></p>

<p align="center">
  <a href="https://docs.unity3d.com/2021.3/Documentation/Manual/index.html"><img src="https://img.shields.io/badge/Unity-2021.3.45f2c1-blue.svg" alt="Unity 2021 文档"></a>
  <a href="https://odininspector.com/"><img src="https://img.shields.io/badge/Odin%20Inspector-3.3%2B-orange.svg" alt="Odin Inspector 官网" ></a>
  <a href="https://github.com/yuumixcode/OdinToolkits-For-Unity?tab=MIT-1-ov-file"><img src="https://img.shields.io/badge/license-MIT-green.svg" alt="License" ></a><br/ >
  <a href="https://www.odintoolkits.cn/"><img src="https://img.shields.io/badge/Docs Website-GitHub%20Pages-purple.svg" alt="Odin Toolkits 部署 GitHub Pages" ></a>
  <a href="https://odintoolkitsdocumentation.readthedocs.io/latest/"><img src="https://img.shields.io/badge/Docs Website-Read%20the%20Docs-orange.svg" alt="Odin Toolkits 部署 Read the Docs" ></a>
</p>

## ⚠️ 重要声明

- `Odin Toolkits` 是开源的、第三方扩展工具集，主要面向 `Odin Inspector and Serializer` 的中文用户，同时为支持英文界面提供了解决方案，欢迎贡献完善英文界面。
- 本项目与 Sirenix 公司及官方产品 `Odin Inspector and Serializer` 无任何隶属、合作或授权关系，并非官方衍生产品。
- 本项目不包含 `Odin Inspector and Serializer` 的发行版本。
- 本文中的 `Odin Inspector` 为 `Odin Inspector and Serializer` 的简称。

## 项目愿景

- 成为使用 `Odin Inspector and Serializer` 的开发者的必备扩展工具集

## 主要模块

1. `Script Doc Generator`：脚本文档生成工具，一键生成 `API` 文档
2. `Attribute Overview Pro` ：中文特性总览窗口，在官方的 `Attribute Overview` 内容上进行扩展
3. `Community`：社区模块，收集、整理、分享使用`Odin Inspector` 制作的工具以及推荐其他优质资源
4. 原生多语言特性，在构造函数层面直接添加多语言参数
5. 模版代码生成工具，选择特定的模板文件，一键生成模版代码

## 安装

### 前提条件

- `Unity 2021.3.45f2c1 LTS` 或更高版本
- `Odin Inspector And Serializer 3.3` 或更高版本

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

- `Tools/Odin Toolkits/Getting Started` - 开始使用
- `Tools/Odin Toolkits/Editor Settings` - 配置编辑器偏好设置
- `Tools/Odin Toolkits/Runtime Config` - 配置运行时设置
- `Tools/Odin Toolkits/Attributes Overview Pro` - 中文特性总览
- `Tools/Odin Toolkits/Script Doc Generator` - 脚本文档生成工具
- `Tools/Odin Toolkits/Community` - 访问社区模块

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

## 开发原则

### 程序集设计

> 尽量少的程序集，第一层保留 `Yuumix`，最大程度地降低冲突可能性

`Yuumix.OdinToolkits.Runtime`

`Yuumix.OdinToolkits.Editor`

`Yuumix.Community.Runtime`

`Yuumix.Community.Editor`

### 命名空间设计

- 命名空间尽量简短，控制在 5 层以内
- 第一层保留 `Yuumix` 前缀以减少冲突
- 不保留 `Runtime` 结尾的命名空间，无特殊标记则默认属于运行时，与 `private` 设计同源

### `Config` 和 `Setting` 命名规则

- `Config` 表示运行时脚本可以读取的配置。
  - `InspectorBilingualismConfigSO` 主要用于编辑器阶段；考虑到 `Inspector` 面板可能频繁调用该配置，为避免需经常用宏定义包裹，故将其设计为 `Config` 形式
- `Setting` 表示仅编辑器脚本可以读取的设置

### 特殊概念

#### `Widget`

`Widget` 等同于 `Unity` 中 `IMGUI` 的 `Control`，是专为 `Unity` 编辑器设计的自定义类。核心特性如下：

1. **内置样式模块**：每个 `Widget` 封装独立的编辑器样式逻辑，使用者仅需填写少量样式参数，即可在 `Inspector` 面板快速调用，无需手动编写绘制逻辑；
2. **高灵活性**：相较于通过 `Attribute` 特性标记实现的自定义绘制，`Widget` 无需与特定变量绑定，可灵活适配不同编辑场景；
3. **编辑器专属**：仅在 `Unity` 编辑器阶段生效，本质是 “即插即用” 的编辑器字段；
4. **模块化设计**：以模块化思路封装样式与交互，开发者可像定义普通字段一样，轻松用它搭建自定义 `Inspector` 界面。

建议在 `ScriptableObject` 的 `OnEnable` 方法中为 `Widget` 相关的变量赋值，而非直接设置初始值。原因如下：

1. **规避直接设初始值的局限**：如果在 `ScriptableObject` 中使用，而 `ScriptableObject` 属于 `Unity` 资源类型，若直接为 `Widget` 设置初始值，后续将无法调整该 `Widget` 配置；若需更改，只能重新创建 `ScriptableObject` 资源，操作成本高。
2. **确保样式修改即时生效**：通过 `OnEnable` 赋值时，每次打开 `ScriptableObject` 资源时，都会自动重新生成一个 `Widget` 对象，无需手动重建资源即可应用最新配置。

#### `YuumixEditor` 文件夹以及命名空间

`YuumixEditor` 文件夹位于 `Runtime` 文件夹下，存储运行时脚本可以调用但仅限编辑器阶段的类，`YuumixEditor` 命名空间等同于 Unity 中的 `UnityEditor`，使用时需要 `UNITY_EDITOR` 宏定义包裹。

## 相关链接

[推荐 - Odin Toolkits 文档网站 - GitHub 部署](https://www.odintoolkits.cn/)

[Odin Toolkits 文档网站 - Read the Docs 部署](https://odintoolkitsdocumentation.readthedocs.io/latest/)

[Odin Inspector 官方网站](https://odininspector.com/)

[Odin Inspector 许可信息](https://odininspector.com/pricing)

## 许可证

本项目基于 MIT 许可证授权。

## Logo 许可证

查看 `LOGO-LICENSE.md` 文档。

## 更新日志

详细版本历史和变更请查看项目中的 `CHANGELOG.md` 或者 [Odin Toolkits 文档网站 - GitHub 部署](https://www.odintoolkits.cn/) 的 `CHANGELOG` 章节。

## 贡献指南

查看 `CONTRIBUTING.md` 文档。

## 支持与反馈

[zeriying@gmail.com](mailto:zeriying@gmail.com)

感谢你看到这里，如果 `Odin Toolkits` 在你的 `Unity` 开发过程中切实提供了帮助，恳请为项目点亮一颗 ★ Star！

如果 `Odin Toolkits` 打包出现错误，请提 issue，或者联系我，我会尽快处理，业余时间开发，无法即时回复，请优先邮件联系。

## 项目及友链推荐

[![Built with Material for MkDocs](https://img.shields.io/badge/Material_for_MkDocs-526CFE?style=for-the-badge&logo=MaterialForMkDocs&logoColor=white)](https://squidfunk.github.io/mkdocs-material/)

[Wcowin 的 MkDocs 博客](https://wcowin.work/Mkdocs-Wcowin/)

[QFramework - Unity 开发框架](https://github.com/liangxiegame/QFramework)

[ES Framework - Unity 开发框架](https://github.com/Ey-Sive-I-Save/ESFrameWorkPublish.git)
