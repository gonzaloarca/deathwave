using UnityEngine;

namespace Strategy
{
    public interface IBullet
    {
        float Speed { get; }
        
        float Range { get; }

        void Travel();
        
        void OnTriggerEnter(Collider other);
        
        void SetRange(float range);
    }
}