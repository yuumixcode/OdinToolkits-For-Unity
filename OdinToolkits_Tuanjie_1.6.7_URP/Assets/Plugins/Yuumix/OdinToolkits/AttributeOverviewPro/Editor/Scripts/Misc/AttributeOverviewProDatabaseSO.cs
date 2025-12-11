using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Sirenix.OdinInspector;
using UnityEditor;
using Yuumix.OdinToolkits.Core;
using Yuumix.OdinToolkits.Core.Editor;

namespace Yuumix.OdinToolkits.AttributeOverviewPro.Editor
{
    public class AttributeOverviewProDatabaseSO :
        OdinEditorScriptableSingleton<AttributeOverviewProDatabaseSO>, IOdinToolkitsEditorReset
    {
        static AbstractAttributePanelSO[] AllVisualPanels => GetAllAttributePanels();

        #region IOdinToolkitsEditorReset Members

        public void EditorReset()
        {
            Initialize();
        }

        #endregion

        [Button("Initialize Database", ButtonSizes.Large)]
        public AttributeOverviewProDatabaseSO Initialize()
        {
            _essentialVisualPanels = AllVisualPanels.Where(x => x.GetType()
                    .GetCustomAttribute<AttributeCategoryAttribute>()
                    .Category
                    .HasFlagFast(OdinAttributeCategory.Essentials))
                .ToArray();
            _buttonVisualPanels = AllVisualPanels.Where(x => x.GetType()
                    .GetCustomAttribute<AttributeCategoryAttribute>()
                    .Category
                    .HasFlagFast(OdinAttributeCategory.Buttons))
                .ToArray();
            _collectionVisualPanels = AllVisualPanels.Where(x => x.GetType()
                    .GetCustomAttribute<AttributeCategoryAttribute>()
                    .Category
                    .HasFlagFast(OdinAttributeCategory.Collections))
                .ToArray();
            _groupVisualPanels = AllVisualPanels.Where(x => x.GetType()
                    .GetCustomAttribute<AttributeCategoryAttribute>()
                    .Category
                    .HasFlagFast(OdinAttributeCategory.Groups))
                .ToArray();
            _conditionalVisualPanels = AllVisualPanels.Where(x => x.GetType()
                    .GetCustomAttribute<AttributeCategoryAttribute>()
                    .Category
                    .HasFlagFast(OdinAttributeCategory.Conditionals))
                .ToArray();
            _numberVisualPanels = AllVisualPanels.Where(x => x.GetType()
                    .GetCustomAttribute<AttributeCategoryAttribute>()
                    .Category
                    .HasFlagFast(OdinAttributeCategory.Numbers))
                .ToArray();
            _typeSpecificVisualPanels = AllVisualPanels.Where(x => x.GetType()
                    .GetCustomAttribute<AttributeCategoryAttribute>()
                    .Category
                    .HasFlagFast(OdinAttributeCategory.TypeSpecifics))
                .ToArray();
            _validationVisualPanels = AllVisualPanels.Where(x => x.GetType()
                    .GetCustomAttribute<AttributeCategoryAttribute>()
                    .Category
                    .HasFlagFast(OdinAttributeCategory.Validation))
                .ToArray();
            _miscVisualPanels = AllVisualPanels.Where(x => x.GetType()
                    .GetCustomAttribute<AttributeCategoryAttribute>()
                    .Category
                    .HasFlagFast(OdinAttributeCategory.Misc))
                .ToArray();
            _metaVisualPanels = AllVisualPanels.Where(x => x.GetType()
                    .GetCustomAttribute<AttributeCategoryAttribute>()
                    .Category
                    .HasFlagFast(OdinAttributeCategory.Meta))
                .ToArray();
            _unityVisualPanels = AllVisualPanels.Where(x => x.GetType()
                    .GetCustomAttribute<AttributeCategoryAttribute>()
                    .Category
                    .HasFlagFast(OdinAttributeCategory.Unity))
                .ToArray();
            _debugVisualPanels = AllVisualPanels.Where(x => x.GetType()
                    .GetCustomAttribute<AttributeCategoryAttribute>()
                    .Category
                    .HasFlagFast(OdinAttributeCategory.Debug))
                .ToArray();
            AttributePanelArrayMap = new Dictionary<string, AbstractAttributePanelSO[]>
            {
                {
                    nameof(OdinAttributeCategory.Essentials), _essentialVisualPanels
                },
                {
                    nameof(OdinAttributeCategory.Buttons), _buttonVisualPanels
                },
                {
                    nameof(OdinAttributeCategory.Collections), _collectionVisualPanels
                },
                {
                    nameof(OdinAttributeCategory.Groups), _groupVisualPanels
                },
                {
                    nameof(OdinAttributeCategory.Conditionals), _conditionalVisualPanels
                },
                {
                    nameof(OdinAttributeCategory.Numbers), _numberVisualPanels
                },
                {
                    nameof(OdinAttributeCategory.TypeSpecifics), _typeSpecificVisualPanels
                },
                {
                    nameof(OdinAttributeCategory.Validation), _validationVisualPanels
                },
                {
                    nameof(OdinAttributeCategory.Misc), _miscVisualPanels
                },
                {
                    nameof(OdinAttributeCategory.Meta), _metaVisualPanels
                },
                {
                    nameof(OdinAttributeCategory.Unity), _unityVisualPanels
                },
                {
                    nameof(OdinAttributeCategory.Debug), _debugVisualPanels
                }
            };
            AttributePanelMap = new Dictionary<string, AbstractAttributePanelSO>();
            foreach (var (category, visualPanelSoArray) in AttributePanelArrayMap)
            {
                foreach (var visualPanelSO in visualPanelSoArray)
                {
                    visualPanelSO.Initialize();
                    var menuName = visualPanelSO.HeaderWidget.HeaderName.ChineseDisplay;
                    AttributePanelMap.Add(category + "/" + menuName, visualPanelSO);
                }
            }

            return this;
        }

        static AbstractAttributePanelSO[] GetAllAttributePanels()
        {
            return AssetDatabase.FindAssets("t:" + typeof(AbstractAttributePanelSO))
                .Select(x =>
                    AssetDatabase.LoadAssetAtPath<AbstractAttributePanelSO>(AssetDatabase.GUIDToAssetPath(x)))
                .ToArray();
        }

        public Dictionary<string, AbstractAttributePanelSO[]> AttributePanelArrayMap;

        public Dictionary<string, AbstractAttributePanelSO> AttributePanelMap =
            new Dictionary<string, AbstractAttributePanelSO>();

        AbstractAttributePanelSO[] _essentialVisualPanels;
        AbstractAttributePanelSO[] _buttonVisualPanels;
        AbstractAttributePanelSO[] _collectionVisualPanels;
        AbstractAttributePanelSO[] _groupVisualPanels;
        AbstractAttributePanelSO[] _conditionalVisualPanels;
        AbstractAttributePanelSO[] _numberVisualPanels;
        AbstractAttributePanelSO[] _typeSpecificVisualPanels;
        AbstractAttributePanelSO[] _validationVisualPanels;
        AbstractAttributePanelSO[] _miscVisualPanels;
        AbstractAttributePanelSO[] _metaVisualPanels;
        AbstractAttributePanelSO[] _unityVisualPanels;
        AbstractAttributePanelSO[] _debugVisualPanels;
    }
}
