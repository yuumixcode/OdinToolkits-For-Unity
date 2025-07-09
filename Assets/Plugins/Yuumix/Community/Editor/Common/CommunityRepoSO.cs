using Sirenix.OdinInspector;
using Sirenix.Utilities.Editor;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEditor;
using Yuumix.OdinToolkits.Core;

namespace Yuumix.OdinToolkits.Community.Editor
{
    [Searchable()]
    public class CommunityRepoSO : ScriptableSingletonForEditorStage<CommunityRepoSO>, IOdinToolkitsReset
    {
        #region Header

        [HideLabel]
        [PropertyOrder(-100)]
        [ShowInInspector]
        [HideIf("$SelectedTagsIsZero")]
        public string SelectedTags
        {
            get
            {
                var sb = new StringBuilder();
                if (InspectorMultiLanguageManagerSO.IsChinese)
                {
                    sb.Append("选择显示的标签有: ");
                }

                if (InspectorMultiLanguageManagerSO.IsEnglish)
                {
                    sb.Append("Selected Tags: ");
                }

                for (var i = 0; i < showCardsWithTag.Count; i++)
                {
                    sb.Append(showCardsWithTag[i].name);
                    if (i != showCardsWithTag.Count - 1)
                    {
                        sb.Append(", ");
                    }
                }

                return sb.ToString();
            }
        }

        bool SelectedTagsIsZero => showCardsWithTag.Count == 0;

        [PropertyOrder(-99)]
        [AssetList]
        [CustomContextMenu("Reset Tags", nameof(ResetTags))]
        [MultiLanguageText("筛选显示卡片的标签", "Tags used for filtering and displaying the cards")]
        public List<CommunityTagSO> showCardsWithTag = new List<CommunityTagSO>();

        [PropertyOrder(0)]
        [OnInspectorGUI]
        public void Separate()
        {
            SirenixEditorGUI.DrawThickHorizontalSeperator(4, 10, 7);
        }

        #endregion

        #region Odin Toolkits Reset

        public void OdinToolkitsReset()
        {
            ResetTags();
        }

        void ResetTags()
        {
            showCardsWithTag.Clear();
            var list = AssetDatabase.FindAssets("t:" + nameof(CommunityTagSO));
            foreach (var t in list)
            {
                var path = AssetDatabase.GUIDToAssetPath(t);
                var tag = AssetDatabase.LoadAssetAtPath<CommunityTagSO>(path);
                if (tag.name == "Sirenix")
                {
                    showCardsWithTag.Add(tag);
                }
            }
        }

        #endregion

        #region Cards Example - Yuumix

        [PropertyOrder(5)]
        [ShowInInspector]
        [ShowIf("$CanShowCardForResolvedParametersOverview")]
        public CardForResolvedParametersOverview CardForResolvedParametersOverview =>
            CardForResolvedParametersOverview.Instance;

        bool CanShowCardForResolvedParametersOverview()
        {
            return CardForResolvedParametersOverview.Instance.CanShowInCommunityRepo();
        }

        #endregion

        #region Community Extensions

        // 此处可以按顺序添加社区扩展

        #endregion
    }
}
