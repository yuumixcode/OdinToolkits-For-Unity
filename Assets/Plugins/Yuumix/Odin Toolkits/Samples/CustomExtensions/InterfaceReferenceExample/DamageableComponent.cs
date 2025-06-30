using UnityEngine;

namespace Yuumix.OdinToolkits.Examples.CustomExtensions.InterfaceReferenceExample
{
    public class DamageableComponent : MonoBehaviour, IDamageable
    {
        public void Damage(int damage)
        {
            Debug.Log($"DamageableComponent took damage: {damage}");
        }
    }
}