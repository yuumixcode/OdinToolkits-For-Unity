using Sirenix.OdinInspector;
using Sirenix.Utilities.Editor;
using System.Collections.Generic;
using System.Text;
using UnityEditor;
using Yuumix.Community.Schwapo.Editor;
using Yuumix.Community.SwitchAttribute.Editor;
using Yuumix.OdinToolkits.Core;
using Yuumix.OdinToolkits.Core.Editor;
using YuumixEditor;

namespace Yuumix.Community.Editor
{
    /// <summary>
    /// Community 资源卡片仓库，扩展的资源卡片编写在此类中
    /// </summary>
    [Searchable(FilterOptions = SearchFilterOptions.ISearchFilterableInterface)]
    public partial class CommunityRepositoryVisualPanelSO :
        OdinEditorScriptableSingleton<CommunityRepositoryVisualPanelSO>,
        IOdinToolkitsEditorReset
    {
        void OnEnable()
        {
            header = new BilingualHeaderWidget(
                "社区",
                "Community Overview",
                "社区资源或者优质资源推荐的总览窗口",
                "The overview window for recommending community resources or high-quality resources");
        }

        #region Header

        [PropertyOrder(-100)]
        [PropertySpace(0, 10)]
        public BilingualHeaderWidget header;

        [PropertySpace(5)]
        [HideLabel]
        [PropertyOrder(-99)]
        [ShowInInspector]
        [HideIf("$SelectedTagsIsZero")]
        [DisplayAsString(EnableRichText = true, FontSize = 13)]
        [EnableGUI]
        [InlineButton("ShowSelectTags", "$GetOpenButtonLabel", ShowIf = "@!" + nameof(CanSelectTags))]
        [InlineButton("HideSelectTags", "$GetCloseButtonLabel", ShowIf = nameof(CanSelectTags))]
        public string SelectedTags
        {
            get
            {
                var sb = new StringBuilder();
                if (InspectorBilingualismConfigSO.IsChinese)
                {
                    sb.Append("选择的标签:  ");
                }

                if (InspectorBilingualismConfigSO.IsEnglish)
                {
                    sb.Append("Selected Tags:  ");
                }

                for (var i = 0; i < showCardsWithTag.Count; i++)
                {
                    sb.Append(showCardsWithTag[i].name.ToGreen());
                    if (i != showCardsWithTag.Count - 1)
                    {
                        sb.Append(", ");
                    }
                }

                return sb.ToString();
            }
        }

        BilingualData _openButton = new BilingualData("选择显示的标签", "Select Tags");
        BilingualData _closeButton = new BilingualData("隐藏标签面板", "Hide Tags List");
        string GetOpenButtonLabel => _openButton.GetCurrentOrFallback();
        string GetCloseButtonLabel => _closeButton.GetCurrentOrFallback();
        public bool CanSelectTags { get; set; }

        void ShowSelectTags()
        {
            CanSelectTags = true;
        }

        void HideSelectTags()
        {
            CanSelectTags = false;
        }

        bool SelectedTagsIsZero => showCardsWithTag.Count == 0;

        [PropertyOrder(-99)]
        [AssetList]
        [CustomContextMenu("Reset Tags", nameof(ResetTags))]
        [BilingualText("筛选显示卡片的标签", "Tags used for filtering and displaying the cards")]
        [ShowIf("CanSelectTags")]
        public List<CommunityTagSO> showCardsWithTag = new List<CommunityTagSO>();

        [PropertyOrder(0)]
        [OnInspectorGUI]
        public void Separate()
        {
            SirenixEditorGUI.DrawThickHorizontalSeperator(4, 10, 7);
        }

        #endregion

        #region Odin Toolkits Reset

        public void EditorReset()
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
    }

    #region Order = 5 - Cards Example - Yuumix

    public partial class CommunityRepositoryVisualPanelSO
    {
        [PropertyOrder(5)]
        [ShowInInspector]
        [ShowIf("$CanShowCardForResolvedParametersOverview")]
        public ResolvedParametersOverviewCardSO ResolvedParametersOverviewCardSO =>
            ResolvedParametersOverviewCardSO.Instance;

        static bool CanShowCardForResolvedParametersOverview() =>
            ResolvedParametersOverviewCardSO.Instance.CanShowInCommunityRepo();
    }

    #endregion

    #region Order = 10 - Yuumix

    public partial class CommunityRepositoryVisualPanelSO
    {
        [PropertyOrder(10)]
        [ShowInInspector]
        [ShowIf("CanShowSwitchButtonAttributeCard")]
        public SwitchButtonAttributeCardSO SwitchButtonAttributeCardSO =>
            SwitchButtonAttributeCardSO.Instance;

        static bool CanShowSwitchButtonAttributeCard() =>
            SwitchButtonAttributeCardSO.Instance.CanShowInCommunityRepo();
    }

    #endregion
}
