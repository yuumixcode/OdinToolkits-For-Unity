---
icon: simple/contributorcovenant
---
# CONTRIBUTING

[Contributor Covenant - 贡献者公约](https://www.contributor-covenant.org/zh-cn/version/3/0/code_of_conduct/)

!!! warning

    在 v1.0.0 版本前，API 可能随时改变。不推荐依赖 `Odin Toolkits` API，建议仅作为编辑器工具使用。

## 贡献准备

fork 项目到自己的仓库，clone 到本地。

!!! warning

    首次打开时报错为正常现象，因为项目本身并不包含 `Odin Inspector` 插件。请无视错误进入项目后，自行导入 `Odin Inspector` 插件。

    如有其他问题，请邮箱联系。

## `Odin Toolkits` 集成模块贡献指南

### 代码样式规范

1. `Odin Toolkits` 代码样式基于 Rider 默认的代码样式。
    1. 修改常量命名样式为 `ALL_UPPER`，完全大写样式。
    2. `换行/特性的排列` 中，除了记录字段以外，其他情况特性均独占一行，设置为 `从不`。
    3. `文件布局` 中使用 `Unity/带区域的默认值`。

### 其他要求

静态变量要求兼容 `Play Mode`，尤其是运行时的静态变量，需要手动处理初始化，避免用户在 `Play Mode` 时出错。

### 注意事项

推荐贡献者在脚本最上方添加贡献注释，包括但不限于名称，邮箱，贡献时间，采用后将持续保留在脚本中。

此模块将由 `Odin Toolkits` 核心开发者维护，将进行代码审查，保障不出现恶意代码。

后续修改已贡献内容将不会告知原贡献者。

## `Community` 社区模块贡献指南

### 贡献规范

1. 尽量以单个文件夹的形式贡献
2. 在用户不主动使用的情况下，不能影响运行逻辑，不影响其他模块的正常使用。可以有后台自动运行的实例，比如场景单例。
3. 删除相关文件夹不报错，不会导致项目运行时错误。
4. 编辑器扩展方面尽量使用 `Odin Inspector`，可以给其他开发者参考，学习 `Odin Inspector` 的使用。
5. Community 模块的代码样式无要求，选择你喜欢的方式，但是保持一致性，推荐采用 Rider 设置 `编辑器/代码样式/C#/文件布局/Unity/带区域的默认值`。
6. 只包含 `Odin Toolkits` 集成模块和 `Community` 模块的新项目打包无警告或错误。
7. 静态变量要求兼容 `Play Mode`，尤其是运行时的静态变量，需要手动处理初始化，避免用户在 `Play Mode` 时出错。

!!! warning "免责声明"

    Community 模块主要基于社区贡献，`Odin Toolkits` 核心开发者不负责维护 Community 模块的代码样式，也不对 Community 模块的代码负责。使用时请自行承担风险。
    
    `Odin Toolkits` 将默认提供剔除 `Community` 模块的发行版本。

## 静态变量兼容 `Play Mode` 示例

[Tuanjie 关于 Play Mode 的文档](https://docs.unity.cn/cn/tuanjiemanual/1.6/Manual/ConfigurableEnterPlayMode.html)

[Unity 关于 Play Mode 的文档](https://docs.unity3d.com/2022.3/Documentation/Manual/ConfigurableEnterPlayMode.html)

``` csharp title="运行时静态变量初始化"
// 使用 [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.SubsystemRegistration)] 初始化静态变量
public class StaticCounterExampleFixed : MonoBehaviour
{
    static int counter = 0;

    [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.SubsystemRegistration)]
    static void Init()
    {
        Debug.Log("Counter reset.");
        counter = 0;   
    }

    // 每帧调用一次 Update
    void Update()
    {
        if (Input.GetButtonDown("Jump"))
        {
        counter++;
        Debug.Log("Counter: " + counter);
        }
    }
}
```

``` csharp title="运行时静态事件订阅处理"
using UnityEngine;

// 使用 [RuntimeInitializeOnLoadMethod] 初始化静态事件订阅处理
public class StaticEventExampleFixed : MonoBehaviour
{
    [RuntimeInitializeOnLoadMethod()]
    static void RunOnStart()
    {
        Debug.Log("Unregistering quit function");
        Application.quitting -= Quit;
    }

    void Start()
    {
        Debug.Log("Registering quit function");
        Application.quitting += Quit;
    }

    static void Quit()
    {
        Debug.Log("Quitting the Player");
    }
}
```

对于 Editor 脚本（比如自定义的 Editor 窗口或使用静态对象的 Inspector），使用 `[InitializeOnEnterPlayMode]` 属性来重置静态字段和事件处理程序。
