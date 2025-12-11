using Sirenix.OdinInspector;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using UnityEditor;
using Yuumix.OdinToolkits.Core;
using Yuumix.OdinToolkits.Core.Editor;

namespace Yuumix.OdinToolkits.AttributeOverviewPro.Editor
{
    public class AttributeOverviewProDatabaseSO :
        OdinEditorScriptableSingleton<AttributeOverviewProDatabaseSO>, IOdinToolkitsEditorReset
    {
        #region Serialized Fields

        public Dictionary<string, AbstractAttributeVisualPanelSO[]> VisualPanelArrayMap;

        public Dictionary<string, AbstractAttributeVisualPanelSO> VisualPanelMap =
            new Dictionary<string, AbstractAttributeVisualPanelSO>();

        AbstractAttributeVisualPanelSO[] _essentialVisualPanels;
        AbstractAttributeVisualPanelSO[] _buttonVisualPanels;
        AbstractAttributeVisualPanelSO[] _collectionVisualPanels;
        AbstractAttributeVisualPanelSO[] _groupVisualPanels;
        AbstractAttributeVisualPanelSO[] _conditionalVisualPanels;
        AbstractAttributeVisualPanelSO[] _numberVisualPanels;
        AbstractAttributeVisualPanelSO[] _typeSpecificVisualPanels;
        AbstractAttributeVisualPanelSO[] _validationVisualPanels;
        AbstractAttributeVisualPanelSO[] _miscVisualPanels;
        AbstractAttributeVisualPanelSO[] _metaVisualPanels;
        AbstractAttributeVisualPanelSO[] _unityVisualPanels;
        AbstractAttributeVisualPanelSO[] _debugVisualPanels;

        #endregion

        static AbstractAttributeVisualPanelSO[] AllVisualPanels => GetAllVisualPanels();

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
            VisualPanelArrayMap = new Dictionary<string, AbstractAttributeVisualPanelSO[]>
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
            VisualPanelMap = new Dictionary<string, AbstractAttributeVisualPanelSO>();
            foreach (var (category, visualPanelSoArray) in VisualPanelArrayMap)
            {
                foreach (var visualPanelSO in visualPanelSoArray)
                {
                    visualPanelSO.Initialize();
                    var menuName = visualPanelSO.HeaderWidget.HeaderName.ChineseDisplay;
                    VisualPanelMap.Add(category + "/" + menuName, visualPanelSO);
                }
            }

            return this;
        }

        static AbstractAttributeVisualPanelSO[] GetAllVisualPanels()
        {
            return AssetDatabase.FindAssets("t:" + typeof(AbstractAttributeVisualPanelSO))
                .Select(x =>
                    AssetDatabase.LoadAssetAtPath<AbstractAttributeVisualPanelSO>(
                        AssetDatabase.GUIDToAssetPath(x)))
                .ToArray();
        }
    }
}
