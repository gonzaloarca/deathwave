using UnityEngine;
using UnityEngine.Serialization;

namespace Flyweight
{
    [CreateAssetMenu(fileName = "GunStats", menuName = "Stats/Gun", order = 0)]
    public class GunStats : ScriptableObject
    {
        [SerializeField] private GunStatsValues _gunStatsValues;

        public GameObject BulletPrefab => _gunStatsValues.BulletPrefab;
        public int Damage => _gunStatsValues.Damage;
        public int MagSize => _gunStatsValues.MagSize;
    }
}

[System.Serializable]
public struct GunStatsValues
{
    public GameObject BulletPrefab;
    public int Damage;
    public int MagSize;
}