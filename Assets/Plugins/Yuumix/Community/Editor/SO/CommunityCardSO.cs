using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Sirenix.OdinInspector;
using Sirenix.OdinInspector.Editor;
using UnityEngine;
using Yuumix.OdinToolkits.Core;
using YuumixEditor;

namespace Yuumix.OdinToolkits.Community.Editor
{
    /// <summary>
    /// Community 资源卡片的基类，继承此类型实现资源卡片
    /// </summary>
    /// <typeparam name="T">目标资源卡片</typeparam>
    public abstract class CommunityCardSO<T> : SerializedScriptableObject where T : CommunityCardSO<T>
    {
        BilingualData _openEditLabel = new BilingualData("编辑标签", "Edit Tags");
        BilingualData _finishEditLabel = new BilingualData("完成编辑", "Finish Edit");

        [BoxGroup("B")]
        [HorizontalGroup("B/H1")]
        [VerticalGroup("B/H1/V1")]
        [PropertyOrder(10)]
        [AssetList]
        [BilingualText("卡片标签", "Card Tags")]
        [ShowIf("CanEditTags")]
        public List<CommunityTagSO> cardWithTags;

        [BoxGroup("B", false)]
        [PropertySpace(5)]
        [HorizontalGroup("B/H1", PaddingLeft = 5, PaddingRight = 5)]
        [VerticalGroup("B/H1/V1", PaddingTop = 5)]
        [HorizontalGroup("B/H1/V1/H2", 0.75f)]
        [PropertyOrder(-1)]
        [ShowInInspector]
        [BilingualDisplayAsStringWidgetConfig(false, TextAlignment.Left, 22)]
        [EnableGUI]
        public BilingualDisplayAsStringWidget HeaderName => GetCardHeader();

        [PropertySpace(5)]
        [BoxGroup("B")]
        [HorizontalGroup("B/H1")]
        [VerticalGroup("B/H1/V1")]
        [PropertyOrder(5)]
        [ShowInInspector]
        [BilingualDisplayAsStringWidgetConfig]
        public BilingualDisplayAsStringWidget Introduction => GetIntroduction();

        [BoxGroup("B")]
        [HorizontalGroup("B/H1")]
        [VerticalGroup("B/H1/V1")]
        [PropertyOrder(6)]
        [ShowInInspector]
        [EnableGUI]
        [HideLabel]
        public Author AuthorInfo => GetAuthor();

        [BoxGroup("B")]
        [HorizontalGroup("B/H1")]
        [VerticalGroup("B/H1/V1")]
        [HideLabel]
        [PropertyOrder(8)]
        [ShowInInspector]
        [DisplayAsString(EnableRichText = true, FontSize = 13)]
        [EnableGUI]
        [InlineButton("OpenEdit", "$GetOpenButtonLabel", ShowIf = "@!" + nameof(CanEditTags))]
        [InlineButton("FinishEdit", "$GetFinishButtonLabel", ShowIf = nameof(CanEditTags))]
        public string Tags
        {
            get
            {
                if (cardWithTags.Count <= 0)
                {
                    return "None Tag";
                }

                var sb = new StringBuilder();
                if (InspectorBilingualismConfigSO.IsChinese)
                {
                    sb.Append("标签: ");
                }

                if (InspectorBilingualismConfigSO.IsEnglish)
                {
                    sb.Append("Tags: ");
                }

                for (var i = 0; i < cardWithTags.Count; i++)
                {
                    sb.Append(cardWithTags[i].name.ToGreen());
                    if (i != cardWithTags.Count - 1)
                    {
                        sb.Append(", ");
                    }
                }

                return sb.ToString();
            }
        }

        public bool CanEditTags { get; set; }
        public string GetOpenButtonLabel => _openEditLabel.GetCurrentOrFallback();
        public string GetFinishButtonLabel => _finishEditLabel.GetCurrentOrFallback();

        [PropertyOrder(1)]
        [HorizontalGroup("B/H1")]
        [VerticalGroup("B/H1/V1")]
        [HorizontalGroup("B/H1/V1/H2")]
        [VerticalGroup("B/H1/V1/H2/V2")]
        [BilingualButton("打开窗口或者 Ping 文件夹", "Open Window Or Ping Folder", buttonHeight: 22)]
        public void OpenWindowOrPingFolderButton()
        {
            OpenWindowOrPingFolder();
        }

        [BoxGroup("B")]
        [PropertySpace(3)]
        [PropertyOrder(1)]
        [HorizontalGroup("B/H1")]
        [VerticalGroup("B/H1/V1")]
        [HorizontalGroup("B/H1/V1/H2")]
        [VerticalGroup("B/H1/V1/H2/V2")]
        [BilingualButton("模块链接", "Module Link", buttonHeight: 22, icon: SdfIconType.Link45deg)]
        public void OpenLink()
        {
            OpenModuleLink();
        }

        void OpenEdit()
        {
            CanEditTags = true;
        }

        void FinishEdit()
        {
            CanEditTags = false;
        }

        public bool CanShowInCommunityRepo()
        {
            if (cardWithTags.Count == 0 || CommunityRepositorySO.Instance.showCardsWithTag.Count == 0)
            {
                return false;
            }

            return CommunityRepositorySO.Instance.showCardsWithTag.All(showCardsWithTag =>
                cardWithTags.Contains(showCardsWithTag));
        }

        protected abstract BilingualDisplayAsStringWidget GetCardHeader();
        protected abstract BilingualDisplayAsStringWidget GetIntroduction();
        protected abstract Author GetAuthor();

        /// <summary>
        /// 打开窗口，或者跳转到具体的文件夹
        /// </summary>
        /// <example>
        ///     <c>xxxWindow.Show() or ProjectEditorUtil.PingAndSelectAsset(xxx)</c>
        /// </example>
        protected abstract void OpenWindowOrPingFolder();

        /// <summary>
        /// 打开模块相关链接，可以是任何文章，网站，文档，个人博客等
        /// </summary>
        protected abstract void OpenModuleLink();

        #region 单例

        static T _instance;

        public static T Instance
        {
            get
            {
                if (_instance)
                {
                    return _instance;
                }

                _instance = ScriptableObjectEditorUtility.GetAssetAndDeleteExtra<T>();
                return _instance;
            }
        }

        #endregion
    }

    internal class CommunityCardSOProcessor<T> : OdinAttributeProcessor<T>
        where T : CommunityCardSO<T>
    {
        public override void ProcessSelfAttributes(InspectorProperty property, List<Attribute> attributes)
        {
            attributes.Add(new HideLabelAttribute());
            attributes.Add(new EnableGUIAttribute());
            attributes.Add(new InlineEditorAttribute(InlineEditorObjectFieldModes.Hidden));
        }
    }
}
