# CHANGELOG

这个项目的所有显著变化都将记录在这个文件中。

格式基于 [Keep a Changelog](https://keepachangelog.com/en/1.1.0/)，以及这个项目尽量遵循 [Semantic Versioning](https://semver.org/spec/v2.0.0.html)。

---

All notable changes to this project will be documented in this file.

The format is based on [Keep a Changelog](https://keepachangelog.com/en/1.1.0/),and this project adheres to [Semantic Versioning](https://semver.org/spec/v2.0.0.html).

## [0.5.5 - alpha] - 2025-11-27

### Changed

- 移除 `BilingualTitleAttribute` 同时支持多个特性的设置。
- 优化 `ScriptDocGenerator` 窗口大小，使其更加合适。

## [0.5.4 - alpha] - 2025-11-23

### Added

- `[Important]` 新增 `Attribute Overview Pro` Beta 版本，已完成基础框架设计，只需填充数据内容。Beta 版由旧版，官方 Attribute Overview，Resolved Paramater Window 三部分整合而成。原来的旧版 `Attribute Overview Pro` 模块内容已经归纳到 Deprecated 文件夹，为 Deprecated 版，后续将不再维护，将在 Beta 版完成数据填充后移除。
    - 新增 `AssetsOnly Attribute`。
    - 新增 `AssetList Attribute`。
- 新增 `Tuanjie 1.6.7 LTS` 版本项目，将作为 Odin Toolkits 的后续开发版本。同时保留原有 `Unity 2021.3.45f2c1` 版本项目，新版本将导入 `Unity 2021.3.45f2c1` 版本验证。
- 新增 `Script Doc Generator` 可视化面板拖拽脚本操作，相比之前的选择 Type 操作，更加方便快捷。优化使用体验。
- 新增 `Script Doc Generator` 右键菜单，在选中脚本文件后，可以一键添加到可视化面板中，以及直接打开可视化面板。优化使用体验。
- 新增 `EditorScriptableSingleton<T>`，非 Odin 序列化的编辑器阶段的 ScriptableObject 单例类，用于在编辑器阶段管理单例资源。

### Changed

- 优化 `BilingualHeaderWidget` 布局，使用 4.0.1.0 版本的 `Visual Designer` 进行布局设计，再编写代码实现。用于兼容 Odin Inspector 低版本。
- 整合 `Process Summary` 菜单项，将 `Summary` 有关的菜单项调整为 `Process Summary/Sync`,`Process Summary/Replace`,`Process Summary/Remove` 三个子菜单项。

### Fixed

- 优化 Odin Toolkits 菜单项设计，修复 v0.5.3 - alpha 版本中的不足。

## [0.5.3 - alpha] - 2025-11-15

### Added

- 新增工具 MenuItemViewer，一键查看项目内所有的菜单项，收录到 Tool Packages 模块。
- 新增 Script Doc Generator 工具生成文档时，支持保留 Front Matter 部分，目前支持 "---" 和 "+++" 格式的 Front Matter。

### Changed

- 修改菜单项，使 Odin Toolkits 和 Odin 菜单项更加接近，减少其他插件的菜单项插入到两者之间的可能性，避免用户体验产生割裂感。
- 优化 Odin Toolkits 导出设置，将 `OdinToolkitsEditorInfo` 版本信息同步到导出设置中，防止再次出现滞后的问题。
- 优化 Script Doc Generator 模块，将可视化面板与生成文档逻辑代码分离，`ScriptDocGeneratorVisualPanelSO` 负责接收用户配置输入和行为输入，`ScriptDocGeneratorController` 负责处理业务逻辑，生成文档。

### Fixed

- 修复 `OdinToolkitsEditorInfo` 中版本滞后的问题。
- 修复 Script Doc Generator 生成文档时，首次生成的文档包含增量标识符，之后修改文档生成设置，关闭生成增量标识符后，覆盖原有文档造成用户自定义内容丢失的问题。如果原有文档包含增量标识符和自定义内容，即使此时文档生成设置中关闭了生成增量标识符，新生成的相同文档覆盖时仍然会保留自定义内容.
- 修复 Community 的 SwitchButtonAttribute 模块的路径问题。

## [0.5.2 - alpha] - 2025-11-12

### Fixed

- 修复中文 API 文档生成设置中，接口的类型种类的字符串中包含 `abstract` 关键字的问题。

## [0.5.1 - alpha] - 2025-11-11

### Added

- 新增 TypeData 完整解析 `ReferenceLinkURLAttribute` 处理。

### Fixed

- 修复 Script Doc Generator 生成文档目标文件夹不支持绝对路径的问题。
- 修复 Script Doc Generator 生成文档时，文档编码格式默认使用 UTF-8 with BOM 的问题，此问题曾导致文档的首个标题在特定情况下渲染异常。
- 修复因移除实例成员的自定义默认值解析而出现的单元测试问题。

## [0.5.0 - alpha] - 2025-11-10

- Alpha 阶段不保证向前兼容性。

### 核心架构

#### Changed

- 优化插件结构，设置一级模块和二级模块概念，突出重要模块，一级模块位于 OdinToolkits 文件夹下，二级模块位于 OdinToolkits/Modules 文件夹下。
- 移动 Community 模块，文件路径和 OdinToolkits 同级，将发布两个版本的 UnityPackage，一个包含 Community 模块，一个不包含 Community 模块。修正依赖关系，Community 依赖 OdinToolkits 集成模块，没有循环依赖。因此 OdinToolkits 集成模块可以独立使用。
- 更新项目 Unity 版本为 2021.3.45f2c1
- 修改 `AttributeOverviewPro` 为一级模块，位于 OdinToolkits 文件夹下。
- 修改 `ChineseSummaryAttribute` 为 `SummaryAttribute`，不考虑同时存在中文和英文的注释特性情况，只有一个 Summary 注释。

### Script Doc Generator 脚本文档生成器

#### Added

- 新增 `MemberData`、`TypeData`、`ConstructorData`、`EventData`、`MethodData`、`ParameterData`、`PropertyData`、`FieldData` 类，用于存储成员的解析数据，与 C# 反射相关的类一一对应。其中 `MethodData` 类不包含构造方法。
- 新增 TypeAnalyzer 类型解析器的类的单元测试，共 143 个测试用例，其中 `TypeData` 14 个，`ConstructorData` 2 个，`EventData` 6 个，`MethodData` 23 个，`PropertyData`  13 个，`FieldData` 85 个，基本覆盖大部分类型解析情况。
- 新增 `IsFromInheritance(this MemberInfo memberInfo)` 方法的单元测试，一共 4 个测试用例，确保方法可以正确判断成员是否来自继承关系。
- 新增 `DocGeneratorSettingSO` 的设置字段，包括是否生成命名空间文件夹，是否自定义文档扩展名，是否自动生成增量标识符。增加了文档生成的灵活性，可以根据需要自定义文档的组织方式和文件名。

#### Changed

- 遵循 SOLID 原则，使用工厂模式，优化代码结构，增强代码结构扩展性，但是增加了代码冗余，可以作为工厂模式参考的示例。
- 修改部分脚本命名。
- 补充部分脚本注释。

#### Removed

- 移除解析实例成员的自定义默认值，仅支持静态、常量默认值。实例成员的默认值可以从构造函数中初始化，也可以硬编码在类中。实际源代码中，可能存在多个构造函数，仅能部分特定实例成员的自定义默认值，避免文档造成误导，因此移除所有实例成员的自定义默认值解析。

## [0.4.0 - alpha] - 2025-09-21

- Alpha 阶段不保证兼容性，内容随时可能修改。

### 已知问题

- `Attribute Overview Pro` 处于重制阶段（其原模块为 `Chinese Attribute Overview`），当前仍存在错误；但该错误仅属于展示层面问题，大部分特性正常显示，不影响其他未报错内容，且不影响打包流程

### Changed

- 优化代码架构，更新版本需要重新导入
- 移动 `Wigdet` 部分到 `YuumixEditor` 中，重命名为 `InspectorWigdet`，设计为仅限编辑器阶段使用，打包后剔除，需要使用 `UNITY_EDITOR` 宏定义包裹

### Added

- 新增 `InspectorBiligualismConfigSO`，设置面板语言，提供 `OnLanguageChange` 事件接口
- 新增 `OdinToolkits` 文件夹定位接口，`OdinToolkitsPaths.GetRootPath()` 可以获取到 `OdinToolkits` 文件夹的相对路径，默认为 `"Assets/Plugins/Yuumix/OdinToolkits"`
- 新增 `Add ChineseSummary` 和 `Remove ChineseSummary` 右键菜单项，可以一键同步脚本中的 `Summary` 注释到 `ChineseSummary` 特性，同时提供一键移除操作，在不需要时可以快速脱离，不影响项目

### 重要更新 - Script Doc Generator 脚本文档生成器

#### Changed

- 升级为独立模块，重构代码逻辑和用户 UI 界面

#### Added

- 新增 `ChineseSummaryAttribute` 特性，用于替代 XML 的 Summary 注释，将内容作为元数据标记到成员中，使得反射可以获取
- 新增支持分析公共实例构造函数、非构造方法、运算符重载方法、属性、事件及字段
- 新增支持分析目标程序集中的类型并生成文档
- 新增 `Test` 文件夹，经手动测试，可以支持相当多的脚本内容分析。

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
