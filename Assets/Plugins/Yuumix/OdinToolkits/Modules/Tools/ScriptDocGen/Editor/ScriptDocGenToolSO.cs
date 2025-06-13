using UnityEngine;
using Yuumix.OdinToolkits.Common.Editor.ScriptableSingleton;
using Yuumix.OdinToolkits.Common.Runtime.ResetTool;
using Yuumix.OdinToolkits.Modules.CustomExtensions.Classes.InspectorGUIWidgets;

namespace Yuumix.OdinToolkits.Modules.Tools.ScriptDocGen.Editor
{
    public class ScriptDocGenToolSO : OdinEditorScriptableSingleton<ScriptDocGenToolSO>, IPluginReset
    {
        public void PluginReset() { }

        public InspectorHeaderWidget header = new InspectorHeaderWidget(
            "Script文档生成工具",
            "Script Document Generation Tool",
            "给定一个 `Type` 类型的值，生成 `Markdown` 格式的文档，可选 Scripting API 或者所有成员文档，默认支持 MkDocs-material",
            "Given a value of type `Type`, generate a document in the format of `Markdown`, optional Scripting API and all member documents, and MkDocs-material is supported by default."
        );
    }
}
