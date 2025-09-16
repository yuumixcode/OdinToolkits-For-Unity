using System;
using System.IO;
using UnityEditor;
using UnityEngine;
using Yuumix.OdinToolkits.Modules;

namespace SummaryAttributeToolExamples
{
    /// <summary>
    /// 使用示例类，展示 XmlToAttribute 工具的功能
    /// </summary>
    public class ExampleUsage
    {
        #region 同步功能示例

        /// <summary>
        /// 原始代码示例 - 带有 XML 注释但无 ChineseSummary 特性
        /// </summary>
        public class OriginalCodeExample
        {
            /// <summary>
            /// 玩家名称
            /// </summary>
            public string playerName;

            /// <summary>
            /// 玩家生命值
            /// </summary>
            /// <param name="initialHealth">初始生命值</param>
            /// <returns>当前生命值</returns>
            public int GetPlayerHealth(int initialHealth)
            {
                return initialHealth;
            }
        }

        /// <summary>
        /// 同步后的代码示例 - 自动添加了 ChineseSummary 特性
        /// </summary>
        public class SyncedCodeExample
        {
            /// <summary>
            /// 玩家名称
            /// </summary>
            [ChineseSummary("玩家名称")]
            public string playerName;

            /// <summary>
            /// 玩家生命值
            /// <param name="initialHealth">初始生命值</param>
            /// <returns>当前生命值</returns>
            [ChineseSummary("玩家生命值")]
            public int GetPlayerHealth(int initialHealth)
            {
                return initialHealth;
            }
        }

        /// <summary>
        /// 同步功能调用示例
        /// </summary>
        public void SyncExample()
        {
            string originalCode = @"
    /// <summary>
    /// 玩家名称
    /// </summary>
    public string playerName;

    /// <summary>
    /// 玩家生命值
    /// <param name=""initialHealth"">初始生命值</param>
    /// <returns>当前生命值</returns>
    public int GetPlayerHealth(int initialHealth)
    {
        return initialHealth;
    }";

            // 调用同步功能
            string syncedCode = UnityTools.SummaryAttributeTool.SyncSummaryToAttribute(originalCode);

            Debug.Log("同步完成！");
            Debug.Log(syncedCode);
        }

        #endregion

        #region 移除功能示例

        /// <summary>
        /// 包含 ChineseSummary 特性的原始代码
        /// </summary>
        public class CodeWithAttributes
        {
            [ChineseSummary("玩家名称")]
            [SerializeField]
            public string playerName;

            [ChineseSummary("玩家生命值")]
            public int playerHealth;

            [Obsolete("使用新方法")]
            [ChineseSummary("旧的计算方法")]
            public int OldCalculate()
            {
                return 0;
            }

            [SerializeField]
            [ChineseSummary("玩家分数")]
            public int playerScore;
        }

        /// <summary>
        /// 移除 ChineseSummary 特性后的代码
        /// </summary>
        public class CodeWithoutAttributes
        {
            [SerializeField]
            public string playerName;

            public int playerHealth;

            [Obsolete("使用新方法")]
            public int OldCalculate()
            {
                return 0;
            }

            [SerializeField]
            public int playerScore;
        }

        /// <summary>
        /// 移除功能调用示例
        /// </summary>
        public void RemoveExample()
        {
            string codeWithAttributes = @"
    [ChineseSummary(""玩家名称"")]
    [SerializeField]
    public string playerName;

    [ChineseSummary(""玩家生命值"")]
    public int playerHealth;

    [Obsolete(""使用新方法"")]
    [ChineseSummary(""旧的计算方法"")]
    public int OldCalculate()
    {
        return 0;
    }

    [SerializeField][ChineseSummary(""玩家分数"")] public int playerScore;";

            // 调用移除功能
            string cleanedCode = UnityTools.SummaryAttributeTool.RemoveAllChineseSummary(codeWithAttributes);

            Debug.Log("移除完成！");
            Debug.Log(cleanedCode);
        }

        #endregion

        #region Unity 编辑器集成示例

        /// <summary>
        /// Unity 编辑器窗口示例，展示如何在编辑器中集成工具
        /// </summary>
        public class SummaryAttributeEditorWindow : EditorWindow
        {
            [MenuItem("Tools/Summary Attribute Tool")]
            public static void ShowWindow()
            {
                GetWindow<SummaryAttributeEditorWindow>("Summary Attribute Tool");
            }

            private void OnGUI()
            {
                GUILayout.Label("XML to ChineseSummary Tool", EditorStyles.boldLabel);

                if (GUILayout.Button("Sync All Scripts"))
                {
                    SyncAllScriptsInProject();
                }

                if (GUILayout.Button("Remove All ChineseSummary"))
                {
                    RemoveAllChineseSummaryInProject();
                }
            }

            private void SyncAllScriptsInProject()
            {
                // 获取项目中所有 C# 脚本
                string[] scriptFiles = Directory.GetFiles(Application.dataPath, "*.cs", SearchOption.AllDirectories);

                foreach (string scriptFile in scriptFiles)
                {
                    try
                    {
                        string originalCode = File.ReadAllText(scriptFile);
                        string syncedCode = UnityTools.SummaryAttributeTool.SyncSummaryToAttribute(originalCode);

                        if (originalCode != syncedCode)
                        {
                            File.WriteAllText(scriptFile, syncedCode);
                            Debug.Log($"同步完成: {scriptFile}");
                        }
                    }
                    catch (Exception e)
                    {
                        Debug.LogError($"处理文件失败: {scriptFile}, 错误: {e.Message}");
                    }
                }
            }

            private void RemoveAllChineseSummaryInProject()
            {
                // 获取项目中所有 C# 脚本
                string[] scriptFiles = Directory.GetFiles(Application.dataPath, "*.cs", SearchOption.AllDirectories);

                foreach (string scriptFile in scriptFiles)
                {
                    try
                    {
                        string originalCode = File.ReadAllText(scriptFile);
                        string cleanedCode = UnityTools.SummaryAttributeTool.RemoveAllChineseSummary(originalCode);

                        if (originalCode != cleanedCode)
                        {
                            File.WriteAllText(scriptFile, cleanedCode);
                            Debug.Log($"清理完成: {scriptFile}");
                        }
                    }
                    catch (Exception e)
                    {
                        Debug.LogError($"处理文件失败: {scriptFile}, 错误: {e.Message}");
                    }
                }
            }
        }

        #endregion
    }
}
