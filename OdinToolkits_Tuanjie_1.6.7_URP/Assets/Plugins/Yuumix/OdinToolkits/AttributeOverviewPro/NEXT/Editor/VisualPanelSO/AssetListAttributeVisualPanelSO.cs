using Sirenix.OdinInspector;
using Sirenix.Utilities.Editor;
using System;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using Yuumix.OdinToolkits.AttributeOverviewPro.NEXT;

namespace Yuumix.OdinToolkits.AttributeOverviewPro.Editor
{
    public class AssetListAttributeVisualPanelSO : AbstractAttributeVisualPanelSO
    {
        [PropertyOrder(-1000)]
        [OnInspectorInit]
        void Initialize()
        {
            SetModel(new AssetListAttributeModel());
        }
    }
}
