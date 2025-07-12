using Sirenix.OdinInspector;
using Sirenix.OdinInspector.Editor;
using System;
using System.Collections.Generic;
using System.Text;
using UnityEngine;
using Yuumix.OdinToolkits.Shared;
using Yuumix.OdinToolkits.Core;
using YuumixEditor;

namespace Yuumix.OdinToolkits.Community.Editor
{
    public abstract class CommunityCardSO<T> : SerializedScriptableObject where T : CommunityCardSO<T>
    {
        static T _instance;

        public static T Instance
        {
            get
            {
                if (_instance)
                {
                    return _instance;
                }

                _instance = ScriptableObjectEditorUtil.GetAssetAndDeleteExtra<T>(
                    OdinToolkitsPaths.GetRootPath() + "/Community/Editor/SO/Cards");
                return _instance;
            }
        }

        [PropertySpace(0, 4)]
        [BoxGroup("h", showLabel: false)]
        [HorizontalGroup("h/1", 0.7f)]
        [PropertyOrder(-1)]
        [ShowInInspector]
        [MultiLanguageDisplayAsStringWidgetConfig(false, TextAlignment.Left, 22)]
        [EnableGUI]
        public MultiLanguageDisplayAsStringWidget HeaderName => GetCardHeader();

        [PropertySpace(4, 4)]
        [PropertyOrder(1)]
        [ShowIfChinese]
        [HorizontalGroup("h/1", 0.29f)]
        [Button("打开窗口或者 Ping 文件夹", ButtonHeight = 22)]
        public void OpenWindowOrPingFolderInternal1()
        {
            OpenWindowOrPingFolder();
        }

        [PropertySpace(4, 4)]
        [PropertyOrder(1)]
        [ShowIfEnglish]
        [HorizontalGroup("h/1", 0.29f)]
        [Button("Open Window Or Ping Folder", ButtonHeight = 22)]
        public void OpenWindowOrPingFolderInternal2()
        {
            OpenWindowOrPingFolder();
        }

        [BoxGroup("h", showLabel: false)]
        [PropertyOrder(5)]
        [ShowInInspector]
        [MultiLanguageDisplayAsStringWidgetConfig()]
        public MultiLanguageDisplayAsStringWidget Introduction => GetIntroduction();

        [BoxGroup("h", showLabel: false)]
        [HideLabel]
        [PropertyOrder(8)]
        [ShowInInspector]
        [HideIf("@cardWithTags.Count == 0")]
        public string Tags
        {
            get
            {
                var sb = new StringBuilder();
                if (InspectorMultiLanguageManagerSO.IsChinese)
                {
                    sb.Append("标签有: ");
                }

                if (InspectorMultiLanguageManagerSO.IsEnglish)
                {
                    sb.Append("Tags: ");
                }

                for (var i = 0; i < cardWithTags.Count; i++)
                {
                    sb.Append(cardWithTags[i].name);
                    if (i != cardWithTags.Count - 1)
                    {
                        sb.Append(", ");
                    }
                }

                return sb.ToString();
            }
        }

        [BoxGroup("h", showLabel: false)]
        [PropertyOrder(10)]
        [AssetList]
        [MultiLanguageText("卡片的标签", "Card Tags")]
        public List<CommunityTagSO> cardWithTags;

        public bool CanShowInCommunityRepo()
        {
            if (cardWithTags.Count == 0 || CommunityRepoSO.Instance.showCardsWithTag.Count == 0)
            {
                return false;
            }

            foreach (var showCardsWithTag in CommunityRepoSO.Instance.showCardsWithTag)
            {
                if (cardWithTags.Contains(showCardsWithTag))
                {
                    continue;
                }

                return false;
            }

            return true;
        }

        protected abstract MultiLanguageDisplayAsStringWidget GetIntroduction();
        protected abstract MultiLanguageDisplayAsStringWidget GetCardHeader();

        /// <summary>
        /// 打开窗口，或者跳转到具体的文件夹
        /// </summary>
        /// <example><c>xxxWindow.Show() or ProjectEditorUtil.PingAndSelectAsset()</c></example>
        protected abstract void OpenWindowOrPingFolder();
    }

    internal class CommunityCardSOProcessor<T> : OdinAttributeProcessor<T>
        where T : CommunityCardSO<T>
    {
        public override void ProcessSelfAttributes(InspectorProperty property, List<Attribute> attributes)
        {
            // [HideLabel]
            // [EnableGUI]
            // [InlineEditor(InlineEditorObjectFieldModes.Hidden)]
            attributes.Add(new HideLabelAttribute());
            attributes.Add(new EnableGUIAttribute());
            attributes.Add(new InlineEditorAttribute(InlineEditorObjectFieldModes.Hidden));
        }
    }
}
