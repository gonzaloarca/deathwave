using UnityEngine;

namespace Strategy
{
    public interface IDamageable
    {
        float MaxHealth { get; }

        void TakeDamage(float damage);

        void Die();
    }
}