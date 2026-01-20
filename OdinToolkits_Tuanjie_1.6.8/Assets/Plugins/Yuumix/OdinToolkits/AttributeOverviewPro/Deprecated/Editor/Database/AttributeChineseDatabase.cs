using System.Collections.Generic;
using Sirenix.OdinInspector;
using Yuumix.OdinToolkits.Core.SafeEditor;

namespace Yuumix.OdinToolkits.AttributeOverviewPro.Deprecated.Editor
{
    public class AttributeChineseDatabase : AbsOdinDatabase<AttributeChineseDatabase>
    {
        #region Serialized Fields

        [TitleGroup("容器文件映射表", "用于构建正确的 OdinMenuEditorWindow", TitleAlignments.Centered)]
        [LabelText("容器文件映射")]
        [DictionaryDrawerSettings(KeyLabel = "类型分组", ValueLabel = "特性容器",
            DisplayMode = DictionaryDisplayOptions.OneLine)]
        public Dictionary<AttributeType, List<OdinAttributeContainerSO>> ContainerMaps =
            new Dictionary<AttributeType, List<OdinAttributeContainerSO>>();

        #endregion

        public static AttributeChineseDatabase Instance =>
            ScriptableObjectSafeEditorUtility.GetSingletonAssetAndDeleteOther<AttributeChineseDatabase>();
    }
}
