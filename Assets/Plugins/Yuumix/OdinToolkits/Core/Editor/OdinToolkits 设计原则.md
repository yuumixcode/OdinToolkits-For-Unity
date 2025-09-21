# OdinToolkits 设计原则

## 程序集设计

>  尽量少的程序集，避免导入 Odin Toolkits 后，`Assets/` 立刻多了一堆 `csproj` 文件，第一层保留 `Yuumix`，可以最大程度的减少冲突可能性

`Yuumix.OdinToolkits.Runtime`
`Yuumix.OdinToolkits.Editor`
`Yuumix.OdinToolkits.Community.Runtime`
`Yuumix.OdinToolkits.Community.Editor`

## 命名空间

- 尽量简短，在 5 层以内
- 第一层保留 `Yuumix`，可以最大程度的减少冲突可能性
- 不保留 `Runtime` 结尾的命名空间，没有特殊标记则默认属于 `Runtime`，与 `private` 设计同源

## `Config` 和 `Setting` 命名规则

- `Config` 表示运行时脚本可以读取的配置。
  - `InspectorBilingualismConfigSO` 主要用于编辑器阶段；考虑到 `Inspector` 面板可能频繁调用该配置，为避免需经常用宏定义包裹，故将其设计为 `Config` 形式
- `Setting` 表示仅编辑器脚本可以读取的设置

## 特殊设计

### `Wigdet`

`Wigdet` 等同于 Unity 中 IMGUI 的 `Control`，是专为 Unity 编辑器设计的自定义类。

核心特性如下：

1. **内置样式模块**：每个 `Wigdet` 封装独立的编辑器样式逻辑，使用者仅需填写少量样式参数，即可在 Inspector 面板快速调用，无需手动编写绘制逻辑；
2. **高灵活性**：相较于通过 `Attribute` 特性标记实现的自定义绘制，`Wigdet` 无需与特定变量绑定，可灵活适配不同编辑场景；
3. **编辑器专属**：仅在 Unity 编辑器阶段生效，本质是 “即插即用” 的编辑器字段；
4. **模块化设计**：以模块化思路封装样式与交互，开发者可像定义普通字段一样，轻松用它搭建自定义 Inspector 界面。

#### `ScriptableObject` 中使用 `Widget` 的最佳实践

建议在 `OnEnable` 方法中为 `Widget` 相关的变量赋值，而非直接设置初始值。

原因如下：

1. **规避直接设初始值的局限**：`ScriptableObject` 属于 Unity 资源类型，若直接为 `Widget` 设置初始值，后续将无法调整该 `Widget` 配置；若需更改，只能重新创建 `ScriptableObject` 资源，操作成本高。
2. **确保样式修改即时生效**：通过 `OnEnable` 赋值时，每次打开 `ScriptableObject` 资源时，都会自动重新生成一个 `Widget` 对象，无需手动重建资源即可应用最新配置。

### `YuumixEditor` 文件夹以及命名空间

`YuumixEditor` 文件夹位于 `Runtime` 文件夹下，存储运行时脚本可以使用但仅限编辑器阶段的类，`YuumixEditor` 命名空间等同于 Unity 中的 `UnityEditor`，使用时需要 `UNITY_EDITOR` 宏定义包裹。

### `NEXT` 文件夹

等同于 `Beta` 版本的设计。

作用为存放正在开发中的内容，可能在未来实装。

### `OBSOLETE` 文件夹

代表过时的脚本。

实际作用有两项：
1. 兼容之前版本，更新 OdinToolkits 版本时不出现错误信息。
2. 存储部分完全弃用的脚本，其代码设计可以参考，主要是编辑器的代码。
