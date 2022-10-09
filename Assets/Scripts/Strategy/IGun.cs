using UnityEngine;

namespace Strategy
{
    public interface IGun
    {
        GameObject BulletPrefab { get; }
        int MagSize { get; }
   
        int Damage { get; }
        int BulletCount { get; }

        void Attack();
        void Reload();
   
    }
}