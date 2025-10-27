# CONTRIBUTING

[正在编写中...]

## `Community` 社区模块贡献指南

### 贡献规范

1. 尽量以单个文件夹的形式贡献
2. 在用户不使用的情况下，不能影响运行逻辑，不影响其他模块的正常使用。可以有后台自动运行的实例，比如场景单例。
3. 删除相关文件夹不报错，不会导致项目运行时错误。
4. 编辑器扩展方面尽量使用 `Odin Inspector`，可以给其他开发者参考，学习 `Odin Inspector` 的使用。
5. Community 模块的代码样式无要求，选择你喜欢的方式。
6. 只包含 `OdinToolkits` 的新项目打包无警告或错误。
7. 静态变量要求兼容 `Play Mode`，在禁止域重新加载时不出错，下文包含示例。

### 静态变量兼容 `Play Mode` 示例

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

## `Modules` 核心模块贡献

待补充...

## `Core` 核心部分贡献

待补充...

如贡献 `Core` 或 `Module` 模块，请参考 `Core/Editor/Misc/OdinToolkitsCodeStyleExample.cs` 代码样式文件。
