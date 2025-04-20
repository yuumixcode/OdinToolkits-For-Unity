using Sirenix.OdinInspector;
using System;

namespace YOGA.Modules.Object_Binder.Demos.架构模式.Common
{
    [Serializable]
    public class Model
    {
        int _number = 10;
        public Action<int> OnValueChanged;

        [ShowInInspector]
        public int ObservableNumber
        {
            get => _number;
            set
            {
                _number = value;
                OnValueChanged?.Invoke(_number);
            }
        }

        #region MVC

        // Model 可以和 View 直接交互

        #endregion
    }
}
