using System;
using Entities;
using Flyweight;
using Strategy;
using UnityEngine;

namespace Weapons
{
    public class Gun : MonoBehaviour, IGun
    {
        [SerializeField] private GunStats _stats;

        public GameObject BulletPrefab => _stats.BulletPrefab;
        public int MagSize => _stats.MagSize;
        public float ReloadTime => _stats.ReloadTime;
        public float Cooldown => _stats.Cooldown;
        public float PlayerSpeedModifier => _stats.PlayerSpeedModifier;
        public int Damage => _stats.Damage;
        public int MaxMags => _stats.MaxMags;
        public GunRecoil GunRecoil => _stats.Recoil;
        public int TotalBulletsLeft => _totalBulletsLeft;
        [SerializeField] private int _totalBulletsLeft;

        public int BulletsLeftInMag => _bulletsLeftInMag;
        [SerializeField] private int _bulletsLeftInMag;

        private void Start()
        {
            Refill();
            _bulletsLeftInMag = MagSize;
        }

        public virtual void Shoot()
        {
            if (_totalBulletsLeft == 0) return; // TODO: Play empty sound
            if (_bulletsLeftInMag == 0)
            {
                Reload();
                return;
            }

            _bulletsLeftInMag--;
            var transform1 = transform;
            var bullet = Instantiate(BulletPrefab, transform1.position, transform1.rotation);
            bullet.name = "Bullet";
            bullet.GetComponent<Bullet>().SetOwner(this);
        }

        public void Reload()
        {
            if (_totalBulletsLeft < MagSize) return;
            _totalBulletsLeft -= MagSize - _bulletsLeftInMag;
            _bulletsLeftInMag = MagSize;
        }

        public void Refill()
        {
            _totalBulletsLeft = _stats.MaxMags * _stats.MagSize;
        }

        public void AddMags(int mags)
        {
            _totalBulletsLeft = Math.Min(_totalBulletsLeft + mags * MagSize, MaxMags * MagSize);
        }
    }
}