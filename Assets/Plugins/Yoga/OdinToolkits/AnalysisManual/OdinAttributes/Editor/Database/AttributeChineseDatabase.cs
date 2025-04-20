using Sirenix.OdinInspector;
using System.Collections.Generic;
using YOGA.Modules.OdinToolkits.Editor;
using YOGA.Modules.Utilities;
using YOGA.OdinToolkits.Editor;

namespace YOGA.OdinToolkits.AnalysisManual.OdinAttributes.Editor
{
    public class AttributeChineseDatabase : AbsOdinDatabase<AttributeChineseDatabase>
    {
        [TitleGroup("容器文件映射表", "用于构建正确的 OdinMenuEditorWindow", TitleAlignments.Centered)]
        [LabelText("容器文件映射")]
        [DictionaryDrawerSettings(KeyLabel = "类型分组", ValueLabel = "特性容器",
            DisplayMode = DictionaryDisplayOptions.OneLine)]
        public Dictionary<AttributeType, List<AbsContainer>> ContainerMaps =
            new Dictionary<AttributeType, List<AbsContainer>>();

        public static AttributeChineseDatabase Instance =>
            ProjectEditorUtility.SO.GetScriptableObjectDeleteExtra<AttributeChineseDatabase>();
    }
}