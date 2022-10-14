using UnityEngine;

namespace Strategy
{
    public interface IDamageable
    {
        float MaxHealth { get; }

        void TakeDamage(float damage);

        void Heal(float amount);

        void Die();
    }
}