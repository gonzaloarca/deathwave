using UnityEngine;

namespace Strategy
{
    public interface IDamageable
    {
        int MaxHealth { get; }

        void TakeDamage(int damage);

        void Die();
    }
}