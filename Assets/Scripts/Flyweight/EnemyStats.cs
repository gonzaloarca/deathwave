using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Flyweight
{
    [CreateAssetMenu(fileName = "EnemyStats", menuName = "Stats/EnemyStats", order = 0)]
    public class EnemyStats : ScriptableObject
    {
        [SerializeField] private EnemyStatsValues _statsValues;
        public float DropFreq => _statsValues.DropFreq;
        public float Vision => _statsValues.Vision;

        public int Score => _statsValues.Score;
    }

    [System.Serializable]
    public struct EnemyStatsValues
    {
        public float DropFreq;
        public float Vision;
        public int Score;
    }
}