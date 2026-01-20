using Sirenix.OdinInspector;
using UnityEngine;

namespace Dev
{
    
    [CreateAssetMenu(fileName = "BoolVariable", menuName = "Variables/Bool Variable", order = 2)]
    public class BoolVariableSO : BaseVariableSO<bool>
    {
        [ButtonGroup("操作")]
        [Button("切换")]
        public void Toggle()
        {
            SetValue(!value);
        }

        [ButtonGroup("操作")]
        [Button("设为 True")]
        public void SetTrue()
        {
            SetValue(true);
        }

        [ButtonGroup("操作")]
        [Button("设为 False")]
        public void SetFalse()
        {
            SetValue(false);
        }
    }
}
