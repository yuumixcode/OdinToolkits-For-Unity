using System;
using UnityEngine;

namespace YOGA.Modules.Object_Binder.Demos.架构模式
{
    public class Health
    {
        const int minHealth = 0;
        const int maxHealth = 100;
        public int CurrentHealth { get; set; }

        public int MinHealth
        {
            get => minHealth;
        }

        public int MaxHealth
        {
            get => maxHealth;
        }

        public event Action HealthChanged;

        public void Increment(int amount)
        {
            CurrentHealth += amount;
            CurrentHealth = Mathf.Clamp(CurrentHealth, minHealth, maxHealth);
            UpdateHealth();
        }

        public void Decrement(int amount)
        {
            CurrentHealth -= amount;
            CurrentHealth = Mathf.Clamp(CurrentHealth, minHealth, maxHealth);
            UpdateHealth();
        }

        public void Restore()
        {
            CurrentHealth = maxHealth;
            UpdateHealth();
        }

        public void UpdateHealth()
        {
            HealthChanged?.Invoke();
        }
    }
}
