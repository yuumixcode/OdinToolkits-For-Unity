# CONTRIBUTING

## `Community` 社区模块贡献

优先贡献 `Community` 模块，按模块贡献，要求无侵入性，或者说用户无感的模块，要求使用 `Odin Inspector` ，可以用于参考 `Odin Inspector` 使用的模块，代码样式无要求，选择你喜欢的方式，只需要可以打包，无报错即可。

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

## `Modules` 核心模块贡献

待补充...

## `Core` 核心部分贡献

待补充...

如贡献 `Core` 或 `Module` 模块，请参考 `Core/Editor/Misc/OdinToolkitsCodeStyleExample.cs` 代码样式文件。
