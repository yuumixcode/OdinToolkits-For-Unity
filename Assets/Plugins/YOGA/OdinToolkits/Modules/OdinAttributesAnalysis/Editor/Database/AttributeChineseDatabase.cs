using System.Collections.Generic;
using Sirenix.OdinInspector;
using YOGA.OdinToolkits.Editor;
using Yoga.Shared.Utility;
using Yoga.Shared.Utility.YuumiEditor;

namespace YOGA.OdinToolkits.AnalysisManual.OdinAttributes.Editor
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