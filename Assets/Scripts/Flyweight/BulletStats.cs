using UnityEngine;
using UnityEngine.Serialization;

namespace Flyweight
{
    [CreateAssetMenu(fileName = "LaserShotStats", menuName = "Stats/LaserShot", order = 0)]
    public class BulletStats : ScriptableObject
    {
        [SerializeField] private BulletStatValues bulletStatsValues;
        public float Speed => bulletStatsValues.Speed;
    }
}

[System.Serializable]
public struct BulletStatValues
{
    public float Speed;
}