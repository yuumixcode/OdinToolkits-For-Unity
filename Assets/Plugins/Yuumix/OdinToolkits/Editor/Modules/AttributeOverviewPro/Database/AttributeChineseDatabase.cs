using System.Collections.Generic;
using Sirenix.OdinInspector;
using Yuumix.OdinToolkits.Editor.Core;
using YuumixEditor;

namespace Yuumix.OdinToolkits.Editor
{
    public class AttributeChineseDatabase : AbsOdinDatabase<AttributeChineseDatabase>
    {
        [TitleGroup("容器文件映射表", "用于构建正确的 OdinMenuEditorWindow", TitleAlignments.Centered)]
        [LabelText("容器文件映射")]
        [DictionaryDrawerSettings(KeyLabel = "类型分组", ValueLabel = "特性容器",
            DisplayMode = DictionaryDisplayOptions.OneLine)]
        public Dictionary<AttributeType, List<OdinAttributeContainerSO>> ContainerMaps =
            new Dictionary<AttributeType, List<OdinAttributeContainerSO>>();

        public static AttributeChineseDatabase Instance =>
            ScriptableObjectEditorUtil.GetAssetAndDeleteExtra<AttributeChineseDatabase>();
    }
}
