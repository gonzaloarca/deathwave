using UnityEngine;
using UnityEngine.Serialization;

namespace Flyweight
{
    [CreateAssetMenu(fileName = "MeleeStats", menuName = "Stats/Melee", order = 0)]
    public class MeleeStats : ScriptableObject
    {
        [SerializeField] private MeleeStatsValues _MeleeStatsValues;
        public int BaseDamage => _MeleeStatsValues.BaseDamage;
        public int DeltaDamage => _MeleeStatsValues.DeltaDamage;
    }
}

[System.Serializable]
public struct MeleeStatsValues
{
    public int BaseDamage;
    public int DeltaDamage;
}