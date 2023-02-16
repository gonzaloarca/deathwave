using UnityEngine;
using Weapons;

namespace Strategy
{
    public interface IGun
    {
        public GunType Type { get; }
        public string Name { get; }
        GameObject MuzzleFlash { get; }
        GameObject Bullet { get; }
        public int MagSize { get; }
        public float ReloadTime { get; }
        public float Damage { get; }
        public int MaxMags { get; }
        public float Cooldown { get; }
        public float PlayerSpeedModifier { get; }
        public GunRecoil GunRecoil { get; }
        public int TotalBulletsLeft { get; }
        public float Range { get; }
        public float Spread { get; }


        void Shoot();
        void Reload();
        void AddMags(int mags);
        void RefillAmmo();

        void ChangeGun();

        void DrawGun();
        void ReloadFinish();
    }
}