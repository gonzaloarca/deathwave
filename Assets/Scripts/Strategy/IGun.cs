using UnityEngine;

namespace Strategy
{
    public interface IGun
    {
        GameObject MuzzleFlash { get; }
        public int MagSize { get; }
        public float ReloadTime { get; }
        public int Damage { get; }
        public int MaxMags { get; }
        public float Cooldown { get; }
        public float PlayerSpeedModifier { get; }
        public GunRecoil GunRecoil { get; }
        public int TotalBulletsLeft { get; }
        public float Range { get; }
        public float Spread { get; }
        

        void Shoot();
        void Reload();
   
    }
}