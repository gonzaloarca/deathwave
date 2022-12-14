using UnityEngine;

namespace Strategy
{
    public interface IBullet
    {
        float Speed { get; }
        
        float Range { get; }

        void Travel();
        
        void SetRange(float range);
        void SetDamage(float damage); 
        void SetTargetLayer(int layer);
    }
}