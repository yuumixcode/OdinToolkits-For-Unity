# SingletonModule

## [1.0.9] - 2024-10-10
### Changed
- 移植到 `Unity 6`，以及 `Odin` 插件支持，更新到`YuumiFramework`

## [1.0.8] - 2024-06-14

### Added 

- 新增完整 `Samples` 案例，补充相关注释

## [1.0.7] - 2024-06-14

### Added

- 新增 `SingletonCreator` 和 `SingletonAssistant` 类，优化单例生成过程，`SingletonAssistant` 帮助无法直接继承单例抽象基类的类构建单例

### Changes

- 缩短单例抽象基类类名，便于记忆和使用，例如 `PersistSingletonRemoveOld`
- 优化`Samples` 文件夹结构，细分`Sample`

## [1.0.6] - 2024-06-11

### Removed
- 移除 `Singleton` 单元测试，维持框架功能模块的结构统一，均采用场景案例测试

## [1.0.5] - 2024-06-05

### Change

- 修改文件夹结构，使用 Samples 存储场景使用示例，UnitTest 存储单元测试（纯 C#）

## [1.0.4] - 2024-06-02

### Added

- 新增 Odin 序列化的 Mono 类型的单例抽象基类的案例测试
- 新增 Odin 序列化说明文档

## [1.0.3] - 2024-06-02

### Added

- 新增 Unity 原生的 Mono 类型的单例抽象基类的案例测试

## [1.0.2] - 2024-06-02

### Added

- 新增程序集定义，ZeusFramework.Modules.Singleton
- 新增`Singleton`的单元测试，确保无法外界直接 `new`新对象，只能使用`T.Instance`的方式获取单例

### Change

- 修改 `Singleton`的实例对象构造机制，使用反射获取私有无参构造函数

## [1.0.1] - 2024-06-01

### Added

- 新增两个单例抽象基类
- `OdinSingletonDestroyFormer`：可以选择销毁之前存在的单例对象的特殊 `OdinSingleton` 单例
- `PersistentOdinSingletonDestroyFormer` ：可以选择销毁之前存在的单例对象的特殊 `PersistentOdinSingleton` 单例

## [1.0.0] - 2024-06-01

### Added

- 新增七个单例抽象基类
- `Singleton` ：纯 C# 的单例
- `MonoSingleton` ：继承了 `MonoBehaviour` 的单例
- `OdinSingleton` ： 继承了 Odin 插件的 `SerializedMonoBehaviour` 的单例
- `PersistentSingleton` ： 持久化的 `MonoSingleton` 单例
- `PersistentOdinSingleton` ：持久化的 `OdinSingleton` 单例
- `MonoSingletonDestroyFormer` ：可以选择销毁之前存在的单例对象的特殊 `MonoSingleton` 单例
- `PersistentMonoSingletonDestroyFormer` ：可以选择销毁之前存在的单例对象的特殊 `PersistentSingleton` 单例