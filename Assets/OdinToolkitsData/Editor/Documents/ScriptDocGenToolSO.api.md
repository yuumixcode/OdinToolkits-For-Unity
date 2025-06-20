# `ScriptDocGenToolSO class`
## Introduction
- Assembly: `Assembly-CSharp-Editor-firstpass`

``` csharp
public class ScriptDocGenToolSO : Yuumix.OdinToolkits.Common.Editor.ScriptableSingleton.OdinEditorScriptableSingleton<ScriptDocGenToolSO>, Yuumix.OdinToolkits.Common.Editor.Windows.ICanSetBelongToWindow, UnityEngine.ISerializationCallbackReceiver, Yuumix.OdinToolkits.Common.Runtime.ResetTool.IPluginReset
```

## Constructors
| 构造函数 | 注释 | Comment |
| :--- | :--- | :--- |
| `public ScriptDocGenToolSO()` | 无 | No Comment |
## Methods
| 方法 | 注释 | Comment |
| :--- | :--- | :--- |
| `public void AnalyzeTitle()` | 无 | No Comment |
| `public virtual (override)void ClearWindow()` | 无 | No Comment |
| `public void CompleteConfig()` | 无 | No Comment |
| `public void FirstTitle()` | 无 | No Comment |
| `public void GenerateTitle()` | 无 | No Comment |
| `public virtual (override)void PluginReset()` | 无 | No Comment |
| `public void ResetDocFolderPath()` | 无 | No Comment |
| `public void ResetSOSaveFolderPath()` | 无 | No Comment |
| `public virtual (override)void SetWindow(OdinMenuEditorWindow window)` | 无 | No Comment |
| `public void Title()` | 无 | No Comment |
| `public void TypeTitle()` | 无 | No Comment |


## Fields
| 字段 | 注释 | Comment |
| :--- | :--- | :--- |
| `public const string DefaultDocFolderPath` | 无 | No Comment |
| `public const string DefaultSaveFolderPath` | 无 | No Comment |
| `public Type TargetType` | 无 | No Comment |
| `public List<Type> TempTypeList` | 无 | No Comment |
| `public LocalizedButtonWidget analyzeMultiTypes` | 无 | No Comment |
| `public LocalizedButtonWidget analyzeSingleType` | 无 | No Comment |
| `public DocCategory docCategory` | 无 | No Comment |
| `public string folderPath` | 无 | No Comment |
| `public LocalizedButtonWidget generateButtonMulti` | 无 | No Comment |
| `public LocalizedButtonWidget generateButtonSingle` | 无 | No Comment |
| `public LocalizedHeaderWidget header` | 无 | No Comment |
| `public MarkdownCategory markdownCategory` | 无 | No Comment |
| `public LocalizedDisplayAsStringWidget multiMode` | 无 | No Comment |
| `public string saveFolderPath` | 无 | No Comment |
| `public LocalizedDisplayAsStringWidget singleMode` | 无 | No Comment |
| `public LocalizedButtonWidget switchMode` | 无 | No Comment |
| `public TypeData typeData` | 无 | No Comment |
| `public List<TypeData> typeDataList` | 无 | No Comment |
| `public TypeStorageSO typeStorage` | 无 | No Comment |

## Inherited Members
| 成员 | 注释 | 声明此方法的类 |
| :--- | :--- | :--- |
| `public virtual (override)bool Equals(Object other)` | 无 | `UnityEngine.Object` |
| `public virtual (override)int GetHashCode()` | 无 | `UnityEngine.Object` |
| `public int GetInstanceID()` | 无 | `UnityEngine.Object` |
| `public Type GetType()` | 无 | `System.Object` |
| `public virtual (override)string ToString()` | 无 | `UnityEngine.Object` |
| `protected virtual (override)void Finalize()` | 无 | `System.Object` |
| `protected Object MemberwiseClone()` | 无 | `System.Object` |
| `protected virtual (override)void OnAfterDeserialize()` | 无 | `Sirenix.OdinInspector.SerializedScriptableObject` |
| `protected virtual (override)void OnBeforeSerialize()` | 无 | `Sirenix.OdinInspector.SerializedScriptableObject` |
| `public HideFlags hideFlags { public get; public set; }` | 无 | `UnityEngine.Object` |
| `public string name { public get; public set; }` | 无 | `UnityEngine.Object` |
| `[Obsolete] public void SetDirty()` | 无 | `UnityEngine.ScriptableObject` |

## Remarks
- Remarks 之后的内容不会被覆盖，可以对特定的方法进行特殊说明
- 不要修改标题级别和内容，`## Remarks` 是识别符
