using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Flyweight
{
    [CreateAssetMenu(fileName = "ExplosiveStats", menuName = "Stats/Explosives", order = 0)]
    public class ExplosiveStats : ScriptableObject
    {
        [SerializeField] private ExplosiveStatsValues _statsValues;

        public float Damage => _statsValues.Damage;
        public float Radius => _statsValues.Radius;
        public float Force => _statsValues.Force;
        public float Delay => _statsValues.Delay;
        public GameObject ExplosionEffect => _statsValues.ExplosionEffect;
    }
    
    
    [System.Serializable]
    public struct ExplosiveStatsValues
    {
        public float Damage;
        public float Radius;
        public float Force;
        public float Delay;
        public GameObject ExplosionEffect;
    }
}