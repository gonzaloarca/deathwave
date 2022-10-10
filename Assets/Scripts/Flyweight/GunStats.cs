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
        public int MaxMags => _gunStatsValues.MaxMags;
        public float ReloadTime => _gunStatsValues.ReloadTime;
        public float Cooldown => _gunStatsValues.Cooldown;
        public float PlayerSpeedModifier => _gunStatsValues.PlayerSpeedModifier;
        public GunRecoil Recoil => _gunStatsValues.Recoil;
        public float Range => _gunStatsValues.Range;
        
        public float Spread => _gunStatsValues.Spread;
    }
}

[System.Serializable]
public struct GunStatsValues
{
    public GameObject BulletPrefab;
    public int Damage;
    public int MagSize;
    public int MaxMags;
    public float Cooldown; // in seconds
    public float ReloadTime; // in seconds
    public float PlayerSpeedModifier;
    public GunRecoil Recoil;
    public float Range; // in meters
    public float Spread; 
}

[System.Serializable]
public struct GunRecoil
{
    public float x;
    public float y;
    public float z;
}