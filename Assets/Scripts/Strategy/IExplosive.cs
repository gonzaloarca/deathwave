using UnityEngine;

namespace Strategy
{
    public interface IExplosive 
    {
       
    
        public string Name { get; }
        GameObject ExplosionEffect { get; }
        public float Damage { get; }
        public float ExplosionRange { get; }
    }
}
