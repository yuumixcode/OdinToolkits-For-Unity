using System.Collections.Generic;
using System.Text;
using Sirenix.OdinInspector;
using Sirenix.Utilities.Editor;
using UnityEditor;
using Yuumix.OdinToolkits.Community.Editor;
using Yuumix.OdinToolkits.Core;
using Yuumix.OdinToolkits.Core.Editor;

namespace Yuumix.OdinToolkits.Community.Editor
{
    /// <summary>
    /// Community 资源卡片仓库，扩展的资源卡片编写在此类中
    /// </summary>
    [Searchable]
    public class CommunityRepositorySO : OdinEditorScriptableSingleton<CommunityRepositorySO>, IOdinToolkitsReset
    {
        #region Header

        [PropertySpace(5)]
        [HideLabel]
        [PropertyOrder(-100)]
        [ShowInInspector]
        [HideIf("$SelectedTagsIsZero")]
        [DisplayAsString(EnableRichText = true, FontSize = 14)]
        [EnableGUI]
        [InlineButton("ShowSelectTags", "$GetOpenButtonLabel", ShowIf = "@!" + nameof(CanSelectTags))]
        [InlineButton("HideSelectTags", "$GetCloseButtonLabel", ShowIf = nameof(CanSelectTags))]
        public string SelectedTags
        {
            get
            {
                var sb = new StringBuilder();
                if (BilingualSetting.IsChinese)
                {
                    sb.Append("选择的标签:  ");
                }

                if (BilingualSetting.IsEnglish)
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

        public void OdinToolkitsReset()
        {
            ResetTags();
        }

        void ResetTags()
        {
            showCardsWithTag.Clear();
            string[] list = AssetDatabase.FindAssets("t:" + nameof(CommunityTagSO));
            foreach (string t in list)
            {
                string path = AssetDatabase.GUIDToAssetPath(t);
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
        public CardWithResolvedParametersOverview CardWithResolvedParametersOverview =>
            CardWithResolvedParametersOverview.Instance;

        bool CanShowCardForResolvedParametersOverview() =>
            CardWithResolvedParametersOverview.Instance.CanShowInCommunityRepo();

        #endregion

        #region Community Extensions

        // 此处添加社区扩展

        #endregion
    }
}
