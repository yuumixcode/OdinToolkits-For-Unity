using Sirenix.OdinInspector;
using Sirenix.Utilities.Editor;
using UnityEditor;
using UnityEngine;

namespace Yuumix.OdinToolkits.AttributeOverviewPro.Editor
{
    public class AssetListAttributeVisualPanelSO : AbstractAttributeVisualPanelSO
    {
        [PropertyOrder(-1000)]
        [OnInspectorInit]
        public void Initialize()
        {
            SetModel(new AssetListAttributeModel());
        }
    }
}
