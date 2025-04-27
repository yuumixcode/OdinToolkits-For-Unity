# QuickGenerateSO 模块文档

## 用法

1. 选择继承了 `ScriptableObject` 的脚本
2. 右键选择 `Create SO Asset From Selected`
3. 在当前目录生成一个 SO 资源

## 补充

1. 支持单选和多选，
   1. 单选 SO 脚本生成时可以改名。
   2. 多选 SO 脚本时将直接生成资源，使用默认命名。
2. 只有当选择的资源中包含有继承`ScriptableObject` 的脚本文件时才可以点击 `Create SO Asset From Selected`。