using Plugins.YOGA.OdinToolkits.Modules.OdinAttributesAnalysis.Common.Editor;
using Sirenix.OdinInspector;
using System.Collections.Generic;
using Yuumix.OdinToolkits.Common.Editor;
using Yuumix.OdinToolkits.Modules.Utilities.YuumiEditor;

namespace Plugins.YOGA.OdinToolkits.Modules.OdinAttributesAnalysis.Editor.Database
{
    public class AttributeChineseDatabase : AbsOdinDatabase<AttributeChineseDatabase>
    {
        [TitleGroup("容器文件映射表", "用于构建正确的 OdinMenuEditorWindow", TitleAlignments.Centered)]
        [LabelText("容器文件映射")]
        [DictionaryDrawerSettings(KeyLabel = "类型分组", ValueLabel = "特性容器",
            DisplayMode = DictionaryDisplayOptions.OneLine)]
        public Dictionary<AttributeType, List<AbsContainer>> ContainerMaps = new();

        public static AttributeChineseDatabase Instance =>
            ProjectEditorUtility.SO.GetScriptableObjectDeleteExtra<AttributeChineseDatabase>();
    }
}