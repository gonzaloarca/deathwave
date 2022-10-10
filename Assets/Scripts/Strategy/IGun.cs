using UnityEngine;

namespace Strategy
{
    public interface IGun
    {
        GameObject BulletPrefab { get; }
        public int MagSize { get; }
        public float ReloadTime { get; }
        public int Damage { get; }
        public int MaxMags { get; }
        public float Cooldown { get; }
        public float PlayerSpeedModifier { get; }
        public GunRecoil GunRecoil { get; }
        public int TotalBulletsLeft { get; }

        void Shoot();
        void Reload();
   
    }
}