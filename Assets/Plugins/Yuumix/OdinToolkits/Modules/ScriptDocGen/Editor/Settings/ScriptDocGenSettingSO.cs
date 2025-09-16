using System.Text;
using Sirenix.OdinInspector;
using Yuumix.OdinToolkits.Core;
using Yuumix.OdinToolkits.Core.Editor;
using YuumixEditor;

namespace Yuumix.OdinToolkits.Modules.ScriptDocGen.Editor.Settings
{
    public class ScriptDocGenSettingSO : OdinEditorScriptableSingleton<ScriptDocGenSettingSO>, IOdinToolkitsReset
    {
        public const string MENU_PATH = "脚本文档生成工具设置";

        public const string DEFAULT_DOC_FOLDER_PATH =
            OdinToolkitsPaths.ODIN_TOOLKITS_ANY_DATA_ROOT_FOLDER + "/Editor/Documents/";

        public const string IDENTIFIER = "## Additional Description";

        static StringBuilder _userIdentifierParagraph = new StringBuilder()
            .AppendLine(IDENTIFIER)
            .AppendLine()
            .AppendLine("> 首个 `" + IDENTIFIER + "` 是标识符，请勿修改标题级别和内容，重新生成文档时将保留标识符之后的内容，可以对某一个成员添加详细说明或者对此类添加额外说明。")
            .AppendLine("> ")
            .AppendLine("> 本文档由 [`Odin Toolkits For Unity`](" + OdinToolkitsWebLinks.GITHUB_REPOSITORY +
                        ") 的脚本文档生成工具辅助完成");

        [PropertyOrder(-5)]
        public BilingualHeaderWidget header = new BilingualHeaderWidget(
            "脚本文档生成工具", "Script Doc Generator",
            "根据配置的 `Type` 对象，生成 Markdown 格式的文档，可选 Scripting API 或者 Complete References，默认支持 MkDocs-Material。Scripting API 表示对外的，用户可以调用的程序接口文档，Complete References 表示包含所有成员的参考文档",
            "Based on the value of the configured `Type`, generate a Markdown format document., generate a document in the format of Markdown, optional Scripting API and Complete References, and MkDocs-Material is supported by default. Scripting API refers to the external, user-accessible interface documentation, and Complete References refers to the documentation containing all members"
        );

        [PropertyOrder(2)]
        [BilingualTitle("生成文档的文件夹路径", "Folder Path For Doc")]
        [HideLabel]
        [FolderPath(AbsolutePath = true)]
        [InlineButton(nameof(ResetDocFolderPath), SdfIconType.ArrowClockwise, "")]
        [CustomContextMenu("Reset Default", nameof(ResetDocFolderPath))]
        public string folderPath;

        public void OdinToolkitsReset()
        {
            ResetDocFolderPath();
        }

        void ResetDocFolderPath()
        {
            folderPath = DEFAULT_DOC_FOLDER_PATH;
        }
    }
}
