using UnityEngine;

namespace Strategy
{
    public interface IDamageable
    {
        float MaxLife { get; }

        void TakeDamage(float damage);

        void Die();
    }
}