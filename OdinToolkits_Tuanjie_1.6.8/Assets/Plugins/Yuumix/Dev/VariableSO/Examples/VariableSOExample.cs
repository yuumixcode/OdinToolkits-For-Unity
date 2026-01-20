using Sirenix.OdinInspector;
using UnityEngine;

namespace Dev.Examples
{
    /// <summary>
    /// 演示如何使用 ScriptableObject 变量系统的示例。
    /// 展示了直接 SO 使用和引用模式两种方式。
    /// </summary>
    public class VariableSOExample : MonoBehaviour
    {
        [Title("直接引用 ScriptableObject 变量")]
        [InfoBox("这些直接引用 ScriptableObject 变量。更改将在所有脚本间共享。")]
        [AssetsOnly]
        public IntVariableSO playerHealth;

        [AssetsOnly]
        public FloatVariableSO playerSpeed;

        [AssetsOnly]
        public BoolVariableSO isGamePaused;

        [Title("变量引用")]
        [InfoBox("这些可以是常量值或 SO 引用。非常适合可配置的参数。")]
        public IntReference maxHealth = new IntReference();

        public FloatReference damageMultiplier = new FloatReference();

        public Vector3Reference spawnPosition = new Vector3Reference();

        public ColorReference playerColor = new ColorReference();

        [Title("运行时测试")]
        [Button("测试直接变量", ButtonSizes.Large)]
        [PropertyOrder(100)]
        public void TestDirectVariables()
        {
            if (playerHealth != null)
            {
                Debug.Log($"当前生命值: {playerHealth.Value}");
                playerHealth.SetValue(playerHealth.Value - 10);
                Debug.Log($"受伤后生命值: {playerHealth.Value}");
            }

            if (playerSpeed != null)
            {
                Debug.Log($"当前速度: {playerSpeed.Value}");
                playerSpeed.SetValue(playerSpeed.Value * 1.5f);
                Debug.Log($"加速后速度: {playerSpeed.Value}");
            }

            if (isGamePaused != null)
            {
                Debug.Log($"游戏暂停: {isGamePaused.Value}");
                isGamePaused.Toggle();
                Debug.Log($"切换后游戏暂停: {isGamePaused.Value}");
            }
        }

        [Button("测试变量引用", ButtonSizes.Large)]
        [PropertyOrder(101)]
        public void TestVariableReferences()
        {
            Debug.Log($"最大生命值: {maxHealth.Value}");
            Debug.Log($"伤害倍数: {damageMultiplier.Value}");
            Debug.Log($"出生位置: {spawnPosition.Value}");
            Debug.Log($"玩家颜色: {playerColor.Value}");

            // 可以通过引用修改值
            maxHealth.Value = 150;
            Debug.Log($"修改后的最大生命值: {maxHealth.Value}");
        }

        void OnEnable()
        {
            // 订阅事件
            if (playerHealth != null)
            {
                playerHealth.OnValueChanged += OnHealthChanged;
            }

            if (isGamePaused != null)
            {
                isGamePaused.OnValueChanged += OnPauseStateChanged;
            }

            // 通过引用订阅（仅在使用 SO 时有效，常量无效）
            maxHealth.SubscribeToChange(OnMaxHealthChanged);
        }

        void OnDisable()
        {
            // 取消订阅事件
            if (playerHealth != null)
            {
                playerHealth.OnValueChanged -= OnHealthChanged;
            }

            if (isGamePaused != null)
            {
                isGamePaused.OnValueChanged -= OnPauseStateChanged;
            }

            maxHealth.UnsubscribeFromChange(OnMaxHealthChanged);
        }

        void OnHealthChanged(int newHealth)
        {
            Debug.Log($"[事件] 生命值变更为: {newHealth}");

            if (newHealth <= 0)
            {
                Debug.Log("[事件] 玩家死亡！");
            }
        }

        void OnPauseStateChanged(bool isPaused)
        {
            Debug.Log($"[事件] 游戏暂停状态变更为: {isPaused}");
            Time.timeScale = isPaused ? 0f : 1f;
        }

        void OnMaxHealthChanged(int newMaxHealth)
        {
            Debug.Log($"[事件] 最大生命值变更为: {newMaxHealth}");
        }

        [Title("使用说明")]
        [InfoBox(@"
如何使用此系统：

1. 直接 SO 变量：
   - 创建新变量：右键项目窗口 > Create > Variables > [类型] Variable
   - 在检查器中将其分配给字段
   - 引用相同 SO 的所有脚本将看到相同的值
   - 订阅 OnValueChanged 以实现响应式行为

2. 变量引用：
   - 打开 'useConstant' 使用常量值（每个实例独立）
   - 关闭 'useConstant' 使用 ScriptableObject（共享）
   - 非常适合敌人属性、关卡配置等

3. 最佳实践：
   - 对玩家属性、全局游戏状态使用 SO 变量
   - 对可配置参数使用引用
   - 始终在 OnDisable 中取消订阅事件
   - 使用 SO 上的内置按钮在编辑器中测试值
        ", InfoMessageType.Info)]
        [PropertyOrder(200)]
        [ReadOnly]
        public string usageInfo;
    }
}
