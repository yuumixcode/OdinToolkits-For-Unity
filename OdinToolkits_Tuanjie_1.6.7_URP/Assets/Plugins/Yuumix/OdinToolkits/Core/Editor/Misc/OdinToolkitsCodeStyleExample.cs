using System;
using System.Collections.Generic;
using UnityEngine;

// 示例使用，避免警告提示
// ReSharper disable ConvertToAutoPropertyWithPrivateSetter  
// ReSharper disable ConvertToAutoPropertyWhenPossible  

// 示例使用，避免警告提示
#pragma warning disable CS0067

namespace Yuumix.OdinToolkits.Core.Editor
{
    // 枚举命名：遵循Rider默认（帕斯卡命名法）
    public enum Direction
    {
        North,
        South,
        East,
        West
    }

    // 带Flags特性的枚举：显式标注值
    [Flags]
    public enum AttackModes
    {
        None = 0,
        Melee = 1,
        Ranged = 2,
        Special = 4,
        MeleeAndSpecial = Melee | Special
    }

    // 接口命名：I前缀+帕斯卡命名法
    public interface IDamageable
    {
        string DamageTypeName { get; }
        float DamageValue { get; }
        bool ApplyDamage(string description, float damage, int numberOfHits);
    }

    // 泛型接口：参数类型标注清晰
    public interface IDamageable<in T>
    {
        void Damage(T damageTaken);
    }

    // 类命名：帕斯卡命名法，继承 MonoBehaviour 时显式标注
    public class OdinToolkitsCodeStyleExample : MonoBehaviour
    {
        // 常量命名：全大写+下划线分隔；其他变量遵循 Rider 默认
        const int MAX_COUNT = 100;
        static int _sharedCount;

        // 私有字段：下划线前缀+驼峰命名法
        int _elapsedTimeInDays;
        int _maxHealth;

        // 只读属性：对外暴露不允许修改的成员
        public int MaxHealthReadOnly => _maxHealth;

        // 读写属性：完整get/set实现
        public int MaxHealth
        {
            get => _maxHealth;
            set => _maxHealth = value;
        }

        // 私有set属性：控制外部修改权限
        public int Health { private get; set; }

        // 自动属性：带默认值初始化
        public string DescriptionName { get; set; } = "Fireball";

        // 单语句方法：简化为表达式体
        public void SetMaxHealth(int newMaxValue) => _maxHealth = newMaxValue;

        // 事件声明：优先使用Action/Func泛型委托
        public event Action OpeningDoor;
        public event Action DoorOpened;
        public event Action<int> PointsScored;
        public event Action<CustomEventArgs> ThingHappened;

        // 事件触发方法：Raise+事件名前缀，空值检查后触发
        public void RaiseDoorOpened()
        {
            DoorOpened?.Invoke();
        }

        public void RaisePointsScored(int points)
        {
            PointsScored?.Invoke(points);
        }

        // 事件订阅方法：订阅者类名_事件触发方法名
        public void OdinToolkitsCodeStyleExample_OnDoorOpened()
        {
            Debug.Log("订阅了 OnDoorOpened 事件的方法被调用");
        }

        // 多参数方法：参数含义清晰，避免模糊命名
        public void SetInitialPosition(float x, float y, float z)
        {
            transform.position = new Vector3(x, y, z);
        }

        // 布尔返回值方法：表达式体简化，命名体现判断语义
        public bool IsNewPosition(Vector3 newPosition) => transform.position == newPosition;

        // 代码格式示例：结构清晰，嵌套层级不超过3层
        void FormatExamples(int someExpression)
        {
            // 集合初始化：var关键字简化类型声明
            var powerUps = new List<PlayerStats>();
            var dict = new Dictionary<string, List<GameObject>>();

            // switch语句：每个case独立分行，格式统一
            switch (someExpression)
            {
                case 0:
                    // 业务逻辑注释
                    break;
                case 1:
                    // 业务逻辑注释
                    break;
                case 2:
                    // 业务逻辑注释
                    break;
            }

            // for循环：变量声明简化，大括号换行
            for (var i = 0; i < 100; i++)
            {
                DoSomething(i);
            }

            // 嵌套循环：缩进一致，避免过度嵌套
            for (var i = 0; i < 10; i++)
            {
                for (var j = 0; j < 10; j++)
                {
                    DoSomething(j);
                }
            }
        }

        // 空实现方法：保持格式统一，后续扩展用
        void DoSomething(int x) { }

        #region Nested type

        // 事件参数结构体：参数较多时用结构体整合
        public struct CustomEventArgs
        {
            public int ObjectID { get; }
            public Color Color { get; }

            // 构造函数：初始化所有属性
            public CustomEventArgs(int objectId, Color color)
            {
                ObjectID = objectId;
                Color = color;
            }
        }

        #endregion

        #region Serialized Fields

        [SerializeField]
        bool isPlayerDead;

        [SerializeField]
        PlayerStats stats;

        [Tooltip("单个特性独占一行，提升可读性")]
        [SerializeField]
        float anotherStat;

        #endregion
    }

    // 可序列化结构体：用于存储配置数据，字段命名简洁
    [Serializable]
    public struct PlayerStats
    {
        public int movementSpeed;
        public int hitPoints;
        public bool hasHealthPotion;
    }
}
