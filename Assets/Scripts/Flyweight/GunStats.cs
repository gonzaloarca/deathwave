using UnityEngine;
using UnityEngine.Serialization;
using Weapons;

namespace Flyweight
{
    [CreateAssetMenu(fileName = "GunStats", menuName = "Stats/Gun", order = 0)]
    public class GunStats : ScriptableObject
    {
        [SerializeField] private GunStatsValues _gunStatsValues;

        public GunType Type => _gunStatsValues.Type;
        public string Name => _gunStatsValues.Name;
        public GameObject MuzzleFlash => _gunStatsValues.MuzzleFlash;
        public GameObject Bullet => _gunStatsValues.Bullet;
        public float Damage => _gunStatsValues.Damage;
        public int MagSize => _gunStatsValues.MagSize;
        public int MaxMags => _gunStatsValues.MaxMags;
        public float ReloadTime => _gunStatsValues.ReloadTime;
        public float Cooldown => _gunStatsValues.Cooldown;
        public float PlayerSpeedModifier => _gunStatsValues.PlayerSpeedModifier;
        public GunRecoil Recoil => _gunStatsValues.Recoil;
        public float Range => _gunStatsValues.Range;
        
        public float Spread => _gunStatsValues.Spread;
        public int WeaponPrice => _gunStatsValues.WeaponPrice;
        public int AmmoPrice => _gunStatsValues.AmmoPrice;
    }
}

[System.Serializable]
public struct GunStatsValues
{
    public GunType Type;
    public string Name;
    public GameObject MuzzleFlash;
    public GameObject Bullet;
    public float Damage;
    public int MagSize;
    public int MaxMags;
    public float Cooldown; // in seconds
    public float ReloadTime; // in seconds
    public float PlayerSpeedModifier;
    public GunRecoil Recoil;
    public float Range; // in meters
    public float Spread;
    public int WeaponPrice;
    public int AmmoPrice;
}

[System.Serializable]
public struct GunRecoil
{
    public float x;
    public float y;
    public float z;
}