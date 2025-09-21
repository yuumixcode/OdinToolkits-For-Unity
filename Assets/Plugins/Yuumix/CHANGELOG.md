---
tags: 
创建日期: 星期五, 2025-07-04, 15:44
修改日期: 星期日, 2025-09-21, 17:47
---

# CHANGELOG

这个项目的所有显著变化都将记录在这个文件中。

格式基于 [Keep a Changelog](https://keepachangelog.com/en/1.1.0/)，以及这个项目 `尽量` 遵循 [Semantic Versioning](https://semver.org/spec/v2.0.0.html)。

---

All notable changes to this project will be documented in this file.

The format is based on [Keep a Changelog](https://keepachangelog.com/en/1.1.0/),and this project adheres to [Semantic Versioning](https://semver.org/spec/v2.0.0.html).

## [0.4.0 - alpha] - 2025-09-21

- Alpha 阶段不保证兼容性，内容随时可能修改。

### 已知问题

- `Attribute Overview Pro` 正处于重制阶段（其原模块为 `Chinese Attribute Overview`），当前仍存在错误；但该错误仅属于展示层面问题，不影响其他未报错内容，且不影响打包流程

### Change

- 优化代码架构，更新版本需要重新导入
- 移动 `Wigdet` 部分到 `YuumixEditor` 中，重命名为 `InspectorWigdet`，设计为仅限编辑器阶段使用，打包后剔除，需要使用 `UNITY_EDITOR` 宏定义包裹

### Added

- 新增 `InspectorBiligualismConfigSO`，设置面板语言，提供 `OnLanguageChange` 事件接口
- 新增 `OdinToolkits` 文件夹定位接口，`OdinToolkitsPaths.GetRootPath()` 可以获取到 `OdinToolkits` 文件夹的相对路径，默认为 `"Assets/Plugins/Yuumix/OdinToolkits"`
- 新增 `Add ChineseSummary` 和 `Remove ChineseSummary` 右键菜单项，可以一键同步脚本中的 `Summary` 注释到 `ChineseSummary` 特性，同时提供一键移除操作，在不需要时可以快速脱离，不影响项目
- 新增 `OdinToolkits` 设计原则文档

### 重要更新 - Script Doc Generator 脚本文档生成器

#### Change

- 升级为独立模块，重构代码逻辑和用户 UI 界面

#### Added

- 新增 `ChineseSummaryAttribute` 特性，用于替代 XML 的 Summary 注释，将内容作为元数据标记到成员中，使得反射可以获取
- 新增支持分析公共实例构造函数、非构造方法、运算符重载方法、属性、事件及字段
- 新增支持分析目标程序集中的类型并生成文档
- 新增 `Test` 文件夹，经测试，可以支持相当多的脚本内容分析，包含一份支持文档。

#### Fixed

- 修复在中文 API 文档生成器中，方法在高层级基类（不限层级，如祖父类及以上）声明、在下层基类（不限层级）重写时未被正确归纳至继承方法列表的问题
- 修复在方法分析数据中，接口声明的方法在当前类实现时，代码逻辑中并不存在 override 关键字，而方法声明字符串中错误显示 override 的问题

### Community 社区模块

#### Added

- 新增 `ResolvedParametersOverviewCardSO` 卡片案例，为纯 `Editor` 模块
- 新增 `SwitchButtonAttributeCardSO` 卡片案例，包含 `Runtime` 和 `Editor` 两部分

## [0.3.3 - alpha] - 2025-08-17

- Alpha 阶段不保证兼容性，内容随时可能修改。
- 目前 `v0.3.3 - alpha` 已经可以支持基础功能运行，具体查看 [Odin Toolkits For Unity](https://github.com/yuumixcode/OdinToolkits-For-Unity)

## [0.1.3.beta] - 2025-05-21

- Beta 阶段不保证向前兼容，删除旧版本再导入

### Added

- 新增 `SwitchButtonAttribute`，将 `bool` 显示为开关，兼容 `ToggleLeftAttribute` 特性 - 修改自 `Schwapo` 开源项目
- 新增 `Singleton` 单例模块

### Changed

- 完善生成模板代码工具 `GenerateTemplateCode`，并完善 `OdinToolkits` 文档网站信息
- 调整菜单路径
- 调整 `OdinToolkits` 文件夹定位，支持 `OdinToolkits` 文件夹整体移动
- 调整 `Common` 文件夹
- 完善 `QuickGenerateSO` 文档信息

### Removed

- `ComponentBinder`，组件绑定工具，在项目中并未完善，无法使用
- 移除 `OdinToolkits` 编辑器配置，未修改完善

## [0.1.2.beta] - 2025-05-09

### Changed

- Beta 阶段不向前兼容
- 重新设计项目结构，根目录为 Plugins/Yuumix/OdinToolkits，将 SO 框架移至 OdinToolkits 文件夹内。

## [0.1.1] - 2025-04-28

### Added

- 新增两个第三方引用，位于 ThirdParty 文件夹，包括一个 [Odin 相关开源库](https://github.com/Schwapo/Odin-Resolved-Parameters-Overview) 和一个 [Log相关免费资源](https://rubickanov.itch.io/)
- 新增 Odin 自带特性解析手册，中文解析 Odin 默认提供的 Attributes，比官方的手册更适合中文开发者。
- 新增模版代码生成工具
- 新增 ComponentBinder ，组件绑定工具
- 新增 OdinToolkits 编辑器配置
- 新增自定义扩展 Odin Attribute
- 新增提取 Odin Syntax Highlight 资源文件，让 Odin 的语法高亮为开发者所用
- 新增通用模块 Common，一些跨项目通用类，方法

## [0.1.0] - 2025-04-21

### Added

- 新增一个简单工具，QuickGenerateSO - 快速右键生成 SO 资源，自动遍历选择的资源，根据继承了 `ScriptableObject` 的脚本，生成 SO 资源。
