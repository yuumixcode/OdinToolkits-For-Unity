using Plugins.YOGA.Common.Utility.YuumiEditor;
using Sirenix.OdinInspector;
using System;
using UnityEngine;

namespace Plugins.YOGA.OdinToolkits.Modules.ComponentBinder.Runtime
{
    [Serializable]
    public class BinderUnit
    {
        [HorizontalGroup(0.4F)]
        [HideLabel]
        public GameObject labelObj;

        [HorizontalGroup(0.6F)]
        [ValueDropdown("GetTypesString")]
        [HideLabel]
        public string componentFullName = "UnityEngine.Transform";

        [LabelText("组件变量名: ")]
        [LabelWidth(80)]
        [InlineButton("DefaultFieldName", "设置默认值")]
        public string fieldName;

        [DisplayAsString(13, Overflow = false)]
        [LabelText("Find ( ) 路径: ")]
        [LabelWidth(80)]
        public string hierarchyPath;

        public BinderUnit(BinderAssistant assistant, BinderLabel labelObj)
        {
            this.labelObj = labelObj.SelfObj;
            UpdatePath(assistant);
        }

        private void DefaultFieldName()
        {
            fieldName = labelObj.name + componentFullName.Split('.')[^1];
        }

        public void UpdatePath(BinderAssistant assistant)
        {
            hierarchyPath = HierarchyUtility.GetRelativePath(assistant.HierarchyPath,
                labelObj.GetComponent<BinderLabel>().HierarchyPath);
        }

        private ValueDropdownList<string> GetTypesString()
        {
            var list = new ValueDropdownList<string>();
            if (!labelObj)
            {
                list.Add("Transform", "UnityEngine.Transform");
                return list;
            }

            foreach (var type in labelObj.GetComponent<BinderLabel>().Types)
            {
                list.Add(type.Name, type.FullName);
            }

            return list;
        }
    }
}
