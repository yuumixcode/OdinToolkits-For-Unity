# Odin Toolkits

<p align="center">
  <a href="https://yuumixcode.github.io/odintoolkitsdocs/">
    <img src="https://cdn.jsdelivr.net/gh/yuumixcode/odintoolkitsdocs@main/docs/assets/logo-odintoolkits-color-noshadow.png" width="320" alt="Odin Toolkits">
  </a>
</p>

<p align="center">
<strong>Odin Toolkits 是 Odin Inspector and Serializer 的第三方扩展工具包。<br/ >
探索进阶功能、整合社区优质项目、优化游戏开发流程。
</strong>
</p>
<p align="center">
  <img src="https://img.shields.io/badge/Unity-2021.3.45f1-blue.svg" />
  <img src="https://img.shields.io/badge/Odin%20Inspector-3.3%2B-orange.svg" />
  <img src="https://img.shields.io/badge/license-MIT-green.svg" /><br/ >
  <a href="https://www.odintoolkits.cn/"><img src="https://img.shields.io/badge/Docs Website-GitHub%20Pages-purple.svg" /></a>
  <a href="https://odintoolkitsdocumentation.readthedocs.io/latest/"><img src="https://img.shields.io/badge/Docs Website-Read%20the%20Docs-orange.svg" /></a>
</p>
<div style="background-color: #fff8e1; border-left: 4px solid #ffb300; padding: 16px; margin: 16px 0; border-radius: 3px;">
  <h2 style="color: #e65100; margin-top: 0; margin-bottom: 12px; font-size: 16px;">⚠️ 重要声明</h2>
  <p style="color: #4e342e; margin: 0 0 10px 0; line-height: 1.5;">
    <strong>Odin Toolkits</strong> 是由个人开发者自发开发的开源第三方扩展工具包，主要服务于中文开发者群体，也为英文界面支持预留了解决方案。
  </p>
  <p style="color: #4e342e; margin: 0 0 10px 0; line-height: 1.5;">
    本项目与 <strong>Sirenix 公司</strong> 及官方产品 <strong>Odin Inspector and Serializer</strong> 无任何隶属、合作或授权关系，并非官方衍生产品。
  </p>
  <p style="color: #4e342e; margin: 0; line-height: 1.5;">
    本项目不包含 <strong>Odin Inspector and Serializer</strong> 的发行版本包。
  </p>
</div>

## 项目愿景

- 成为使用 `Odin Inspector` 的开发者的必备扩展工具包。
- 帮助开发者快速学习使用 `Odin Inspector`，发挥其更多的价值。
- 帮助开发者快速开发，提供更多低侵入性的解决方案。

## 主要模块

1. 原生多语言特性，在构造函数层面直接添加多语言参数。
2. `Odin Inspector` 提供的所有 `Attribute` 的中文解析窗口。
3. 脚本文档生成工具，选择特定的类，一键生成 `API` 文档。
4. 模版代码生成工具，选择特定的模板文件，一键生成模版代码。
5. 社区模块，收集、整理、分享 `Odin Inspector` 的使用案例以及推荐其他优质资源。

## 安装

### 前提条件

- Unity 2021.3 或更高版本
- Odin Inspector And Serializer 3.3 或更高版本

### 步骤

> Odin Toolkits 依赖 Odin Inspector 插件，请先自行导入 Odin Inspector 插件到项目。从 [Unity AssetStore](https://assetstore.unity.com/packages/tools/utilities/odin-inspector-and-serializer-89041) 和 [Sirenix 官网](https://odininspector.com/) 上购买插件或者其他方式获取插件。

1. 确保项目中已安装 Odin Inspector
2. 下载最新发布包
3. 将包导入到 Unity 项目中
4. 通过 `Tools/Odin Toolkits` 菜单访问工具

## 使用

导入后，您可以通过 Unity 编辑器菜单访问 Odin Toolkits：

- `Tools/Odin Toolkits/Getting Started` - 主入口点
- `Tools/Odin Toolkits/Editor Settings` - 配置编辑器偏好设置
- `Tools/Odin Toolkits/Runtime Config` - 配置运行时设置
- `Tools/Odin Toolkits/Attributes Overview Pro` - 查看特性文档
- `Tools/Odin Toolkits/Script Doc Generator` - 生成脚本文档
- `Tools/Odin Toolkits/Community` - 访问社区资源

## 项目结构

``` markdown
Plugins/
├─ Yuumix/
│  ├─ OdinToolkits/
│  │  ├─ Community/
│  │  │  ├─ Editor/
│  │  │  ├─ Modules/
│  │  ├─ Core/
│  │  │  ├─ Editor/
│  │  │  ├─ Resources/
│  │  │  ├─ Runtime/
│  │  ├─ Modules/
│  │  │  ├─ AttributeOverviewPro/
│  │  │  ├─ CustomAttributes/
│  │  │  ├─ Editor/
│  │  │  ├─ ScriptDocGen/
│  ├─ CHANGELOG.md
```

## 开发原则

### 程序集设计

- 最小化程序集数量，避免导入后项目中出现大量 `csproj` 文件
- 使用 `Yuumix` 命名空间前缀以最大程度减少冲突可能性
- 清晰分离运行时和编辑器代码

### 命名约定

- 命名空间尽量简短，控制在 5 层以内
- 第一层保留 `Yuumix` 前缀以减少冲突
- 不保留 `Runtime` 结尾的命名空间，无特殊标记则默认属于运行时

### `Config` 和 `Setting` 命名规则

- `Config` 表示运行时脚本可以读取的配置
- `Setting` 表示仅编辑器脚本可以读取的设置

### 特殊设计

#### `Widget` 系统

- 等同于 Unity 中 IMGUI 的 `Control`，专为 Unity 编辑器设计的自定义类
- 内置样式模块，封装独立的编辑器样式逻辑
- 高灵活性，无需与特定变量绑定，可灵活适配不同编辑场景
- 编辑器专属，本质是 " 即插即用 " 的编辑器字段
- 模块化设计，可以像定义普通字段一样轻松搭建自定义 Inspector 界面

#### `YuumixEditor` 命名空间

- 存储运行时脚本可以调用但仅限编辑器阶段的类
- 等同于 Unity 中的 `UnityEditor` 命名空间
- 使用时需要 `UNITY_EDITOR` 宏定义包裹

## 相关链接

[推荐 -Odin Toolkits 文档网站 - GitHub 部署](https://www.odintoolkits.cn/)

[Odin Toolkits 文档网站 - ReadtheDocs 部署](https://odintoolkitsdocumentation.readthedocs.io/latest/)

[Odin Inspector 官方网站](https://odininspector.com/)

[Odin Inspector 许可信息](https://odininspector.com/pricing)

## 项目及友链推荐

[![Built with Material for MkDocs](https://img.shields.io/badge/Material_for_MkDocs-526CFE?style=for-the-badge&logo=MaterialForMkDocs&logoColor=white)](https://squidfunk.github.io/mkdocs-material/)

[Wcowin 的 MkDocs 博客](https://wcowin.work/Mkdocs-Wcowin/)

[QFramework - Unity 开发框架](https://github.com/liangxiegame/QFramework)

[ES Framework - Unity 开发框架](https://github.com/Ey-Sive-I-Save/ESFrameWorkPublish.git)

[CLZ Framework - Unity 开发框架 - 开发中](https://github.com/Canglangzi/CocKleBurs-DevelopmentKit)

## 许可证

本项目基于 MIT 许可证授权。

## 更新日志

详细版本历史和变更请查看项目中的 CHANGELOG.md 或者 [Odin Toolkits 文档网站](https://www.odintoolkits.cn/) 的 CHANGELOG 章节。

## 贡献指南

欢迎贡献！请优先贡献 `Community` 模块，按模块贡献，要求无侵入性，或者说用户无感的模块，要求使用 `Odin Inspector` ，可以用于参考 `Odin Inspector` 使用的模块，代码样式无要求，选择你喜欢的方式，只需要可以打包，无报错即可。

另外静态变量要求兼容 `Play Mode`，在禁止域重新加载时不出错，示例如下：

``` csharp
#if UNITY_EDITOR  
  
        #region 兼容 [禁用域重新加载]  
  
        [InitializeOnLoadMethod]  
        static void Initialize()  
        {            
            EditorApplication.playModeStateChanged -= OnPlayModeStateChanged;  
            EditorApplication.playModeStateChanged += OnPlayModeStateChanged;  
        }
          
        static void OnPlayModeStateChanged(PlayModeStateChange state)  
        {            
            if (state == PlayModeStateChange.EnteredPlayMode)  
            {                
                OnLanguageChange = null;  
            }        
        }  
        
        #endregion  
  
#endif
```

如贡献 `Core` 或 `Module` 模块，请参考 `Core/Editor/Misc/OdinToolkitsCodeStyleExample.cs` 代码样式文件。

## 支持

[zeriying@gmail.com](mailto:zeriying@gmail.com)

感谢你看到这里，如果 Odin Toolkits 在你的 Unity 开发中切实提供了帮助，恳请为项目点亮一颗 ★ Star！如果 Odin Toolkits 打包出现错误，请提 issue，或者联系我，我会尽快处理。业余时间开发，无法即时回复，请优先邮件联系。
