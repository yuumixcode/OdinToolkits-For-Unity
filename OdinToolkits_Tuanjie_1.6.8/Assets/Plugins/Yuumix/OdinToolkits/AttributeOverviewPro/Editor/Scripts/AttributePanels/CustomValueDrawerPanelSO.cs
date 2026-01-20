using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Yuumix.OdinToolkits.Core.Editor;

namespace Yuumix.OdinToolkits.AttributeOverviewPro.Editor
{
    [AttributeCategory(OdinAttributeCategory.Essentials)]
    public class CustomValueDrawerPanelSO : AbstractAttributePanelSO
    {
        public override void Initialize()
        {
            SetData(new CustomValueDrawerAttributeData());
        }
    }
}
