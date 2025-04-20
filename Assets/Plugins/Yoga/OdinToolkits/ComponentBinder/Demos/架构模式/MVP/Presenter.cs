using UnityEngine;
using YOGA.Modules.Object_Binder.Demos.架构模式.Common;

namespace YOGA.Modules.Object_Binder.Demos.架构模式.MVP
{
    /// <summary>
    /// Presenter 持有模型 Model 和视图 View，负责处理业务逻辑，并更新视图 View
    /// </summary>
    public class Presenter : MonoBehaviour
    {
        public MVPView view;
        public Model model;

        void Start()
        {
            // 获取 Model 
            model = new Model();
            // 订阅 Model 事件
            model.OnValueChanged += UpdateView;
            // 首次通常需要更新一下 View
            UpdateView(model.ObservableNumber);
            // 绑定 View 操作
            view.button.onClick.AddListener(Command);
        }

        void OnDestroy()
        {
            if (model != null)
            {
                model.OnValueChanged -= UpdateView;
            }
        }

        // View 执行的操作命令
        void Command()
        {
            // 最好可以封装一个方法，避免直接修改 Model
            model.ObservableNumber++;
        }

        void UpdateView(int v)
        {
            view.text.text = v.ToString();
        }
    }
}
