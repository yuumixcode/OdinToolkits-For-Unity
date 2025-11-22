using Sirenix.OdinInspector;
using System.Collections.Generic;
using System.Reflection;
using Yuumix.OdinToolkits.Core.Editor;

namespace Yuumix.OdinToolkits.AttributeOverviewPro.Editor
{
    public class AttributeOverviewProDatabaseSO : OdinEditorScriptableSingleton<AttributeOverviewProDatabaseSO>
    {
        public Dictionary<string, SerializedScriptableObject[]> VisualPanelMap;

        public AttributeOverviewProDatabaseSO Initialize()
        {
            VisualPanelMap = new Dictionary<string, SerializedScriptableObject[]>()
            {
                {
                    nameof(OdinAttributeCategory.Essential), essentialVisualPanels
                },
                {
                    nameof(OdinAttributeCategory.Button), buttonVisualPanels
                },
                {
                    nameof(OdinAttributeCategory.Collection), collectionVisualPanels
                },
                {
                    nameof(OdinAttributeCategory.Group), groupVisualPanels
                },
                {
                    nameof(OdinAttributeCategory.Conditional), conditionalVisualPanels
                },
                {
                    nameof(OdinAttributeCategory.Number), numberVisualPanels
                },
                {
                    nameof(OdinAttributeCategory.TypeSpecific), typeSpecificVisualPanels
                },
                {
                    nameof(OdinAttributeCategory.Validation), validationVisualPanels
                },
                {
                    nameof(OdinAttributeCategory.Misc), miscVisualPanels
                },
                {
                    nameof(OdinAttributeCategory.Meta), metaVisualPanels
                },
                {
                    nameof(OdinAttributeCategory.Unity), unityVisualPanels
                },
                {
                    nameof(OdinAttributeCategory.Debug), debugVisualPanels
                },
            };
            return this;
        }

        [AssetList(AutoPopulate = true, CustomFilterMethod = nameof(IsEssential))]
        public SerializedScriptableObject[] essentialVisualPanels;

        [AssetList(AutoPopulate = true, CustomFilterMethod = nameof(IsButton))]
        public SerializedScriptableObject[] buttonVisualPanels;

        [AssetList(AutoPopulate = true, CustomFilterMethod = nameof(IsCollection))]
        public SerializedScriptableObject[] collectionVisualPanels;

        [AssetList(AutoPopulate = true, CustomFilterMethod = nameof(IsGroup))]
        public SerializedScriptableObject[] groupVisualPanels;

        [AssetList(AutoPopulate = true, CustomFilterMethod = nameof(IsConditional))]
        public SerializedScriptableObject[] conditionalVisualPanels;

        [AssetList(AutoPopulate = true, CustomFilterMethod = nameof(IsNumber))]
        public SerializedScriptableObject[] numberVisualPanels;

        [AssetList(AutoPopulate = true, CustomFilterMethod = nameof(IsTypeSpecific))]
        public SerializedScriptableObject[] typeSpecificVisualPanels;

        [AssetList(AutoPopulate = true, CustomFilterMethod = nameof(IsValidation))]
        public SerializedScriptableObject[] validationVisualPanels;

        [AssetList(AutoPopulate = true, CustomFilterMethod = nameof(IsMisc))]
        public SerializedScriptableObject[] miscVisualPanels;

        [AssetList(AutoPopulate = true, CustomFilterMethod = nameof(IsMeta))]
        public SerializedScriptableObject[] metaVisualPanels;

        [AssetList(AutoPopulate = true, CustomFilterMethod = nameof(IsUnity))]
        public SerializedScriptableObject[] unityVisualPanels;

        [AssetList(AutoPopulate = true, CustomFilterMethod = nameof(IsDebug))]
        public SerializedScriptableObject[] debugVisualPanels;

        bool IsEssential(SerializedScriptableObject panel)
        {
            var attribute = panel.GetType().GetCustomAttribute<AttributeCategoryAttribute>();
            return attribute != null && attribute.Category.HasFlag(OdinAttributeCategory.Essential);
        }

        bool IsButton(SerializedScriptableObject panel)
        {
            var attribute = panel.GetType().GetCustomAttribute<AttributeCategoryAttribute>();
            return attribute != null && attribute.Category.HasFlag(OdinAttributeCategory.Button);
        }

        bool IsCollection(SerializedScriptableObject panel)
        {
            var attribute = panel.GetType().GetCustomAttribute<AttributeCategoryAttribute>();
            return attribute != null && attribute.Category.HasFlag(OdinAttributeCategory.Collection);
        }

        bool IsGroup(SerializedScriptableObject panel)
        {
            var attribute = panel.GetType().GetCustomAttribute<AttributeCategoryAttribute>();
            return attribute != null && attribute.Category.HasFlag(OdinAttributeCategory.Group);
        }

        bool IsConditional(SerializedScriptableObject panel)
        {
            var attribute = panel.GetType().GetCustomAttribute<AttributeCategoryAttribute>();
            return attribute != null && attribute.Category.HasFlag(OdinAttributeCategory.Conditional);
        }

        bool IsNumber(SerializedScriptableObject panel)
        {
            var attribute = panel.GetType().GetCustomAttribute<AttributeCategoryAttribute>();
            return attribute != null && attribute.Category.HasFlag(OdinAttributeCategory.Number);
        }

        bool IsTypeSpecific(SerializedScriptableObject panel)
        {
            var attribute = panel.GetType().GetCustomAttribute<AttributeCategoryAttribute>();
            return attribute != null && attribute.Category.HasFlag(OdinAttributeCategory.TypeSpecific);
        }

        bool IsValidation(SerializedScriptableObject panel)
        {
            var attribute = panel.GetType().GetCustomAttribute<AttributeCategoryAttribute>();
            return attribute != null && attribute.Category.HasFlag(OdinAttributeCategory.Validation);
        }

        bool IsMisc(SerializedScriptableObject panel)
        {
            var attribute = panel.GetType().GetCustomAttribute<AttributeCategoryAttribute>();
            return attribute != null && attribute.Category.HasFlag(OdinAttributeCategory.Misc);
        }

        bool IsMeta(SerializedScriptableObject panel)
        {
            var attribute = panel.GetType().GetCustomAttribute<AttributeCategoryAttribute>();
            return attribute != null && attribute.Category.HasFlag(OdinAttributeCategory.Meta);
        }

        bool IsUnity(SerializedScriptableObject panel)
        {
            var attribute = panel.GetType().GetCustomAttribute<AttributeCategoryAttribute>();
            return attribute != null && attribute.Category.HasFlag(OdinAttributeCategory.Unity);
        }

        bool IsDebug(SerializedScriptableObject panel)
        {
            var attribute = panel.GetType().GetCustomAttribute<AttributeCategoryAttribute>();
            return attribute != null && attribute.Category.HasFlag(OdinAttributeCategory.Debug);
        }
    }
}
