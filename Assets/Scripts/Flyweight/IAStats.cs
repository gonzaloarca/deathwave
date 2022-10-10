using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Flyweight
{
    [CreateAssetMenu(fileName = "IAStats", menuName = "Stats/IAstats", order = 0)]
    public class IAStats : ScriptableObject
    {
        [SerializeField] private IAStatsValues _statsValues;

        public int MaxLife => _statsValues.MaxLife;
        public float WalkSpeed => _statsValues.WalkSpeed;
        public float RotationSpeed => RotationSpeed;
        public float SprintSpeed => _statsValues.SprintSpeed;
        public float JumpStrength => _statsValues.JumpStrength;
        public float MeleeRange => _statsValues.MeleeRange;
        public float Vision => _statsValues.Vision;
    }

    [System.Serializable]
    public struct IAStatsValues
    {
        public int MaxLife;
        public float WalkSpeed;
        public float SprintSpeed;
        public float RotationSpeed;
        public float JumpStrength;
        public float MeleeRange;
        public float Vision;
    }
}