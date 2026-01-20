using Sirenix.OdinInspector;
using UnityEngine;

namespace Dev
{
    [CreateAssetMenu(fileName = "GameObjectVariable", menuName = "Variables/GameObject Variable", order = 7)]
    public class GameObjectVariableSO : BaseVariableSO<GameObject>
    {
        [ButtonGroup("操作")]
        [Button("清除引用")]
        public void ClearReference()
        {
            SetValue(null);
        }

        [ShowInInspector]
        [ReadOnly]
        [PropertyOrder(50)]
        [LabelText("是否有效")]
        public bool IsValid => value != null;

        [ShowInInspector]
        [ReadOnly]
        [PropertyOrder(51)]
        [LabelText("对象名称")]
        public string ObjectName => value != null ? value.name : "无";

        [ShowInInspector]
        [ReadOnly]
        [PropertyOrder(52)]
        [LabelText("层级中是否激活")]
        [EnableIf(nameof(IsValid))]
        public bool IsActiveInHierarchy => value != null && value.activeInHierarchy;

        [ShowInInspector]
        [ReadOnly]
        [PropertyOrder(53)]
        [LabelText("实例 ID")]
        [EnableIf(nameof(IsValid))]
        public int InstanceID => value != null ? value.GetInstanceID() : 0;

        [ButtonGroup("GameObject 操作")]
        [Button("激活")]
        [EnableIf(nameof(IsValid))]
        public void Activate()
        {
            if (value != null)
            {
                value.SetActive(true);
            }
        }

        [ButtonGroup("GameObject 操作")]
        [Button("停用")]
        [EnableIf(nameof(IsValid))]
        public void Deactivate()
        {
            if (value != null)
            {
                value.SetActive(false);
            }
        }

        [ButtonGroup("GameObject 操作")]
        [Button("切换激活状态")]
        [EnableIf(nameof(IsValid))]
        public void ToggleActive()
        {
            if (value != null)
            {
                value.SetActive(!value.activeSelf);
            }
        }
    }
}
