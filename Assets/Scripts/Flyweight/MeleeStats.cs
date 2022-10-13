using UnityEngine;
using UnityEngine.Serialization;

namespace Flyweight
{
    [CreateAssetMenu(fileName = "MeleeStats", menuName = "Stats/Melee", order = 0)]
    public class MeleeStats : ScriptableObject
    {
        [SerializeField] private MeleeStatsValues _MeleeStatsValues;
        public float BaseDamage => _MeleeStatsValues.BaseDamage;
        public float DeltaDamage => _MeleeStatsValues.DeltaDamage;
    }
}

[System.Serializable]
public struct MeleeStatsValues
{
    public float BaseDamage;
    public float DeltaDamage;
}