using System;
using Commands;
using Controllers;
using Entities;
using EventQueue;
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

        public float CooldownTimer => _cooldownTimer;
        [SerializeField] private float _cooldownTimer = 0f;

        private RecoilController _recoilController;
        private CmdRecoilFire _cmdRecoilFire;

        private void Start()
        {
            Refill();
            _bulletsLeftInMag = MagSize;
            _recoilController = transform.root.GetComponentInChildren<RecoilController>();
            _cmdRecoilFire = new CmdRecoilFire(_recoilController, GunRecoil);
        }

        protected void InstantiateBullet(Vector3 position, Quaternion rotation, string bulletName)
        {
            var bullet = Instantiate(BulletPrefab, position, rotation);
            bullet.name = bulletName;
            bullet.GetComponent<Bullet>().SetOwner(this);
        }

        // Bullet instantiation method for modularity across extended classes
        protected virtual void ShootBullet(Transform theTransform) =>
            InstantiateBullet(theTransform.position, theTransform.rotation, "Bullet");

        public virtual void Shoot()
        {
            if (_totalBulletsLeft == 0) return; // TODO: Play empty sound
            if (_bulletsLeftInMag == 0)
            {
                Reload();
                return;
            }

            if (_cooldownTimer > 0) return;

            _bulletsLeftInMag--;
            _cooldownTimer = Cooldown;
            var transform1 = transform;

            EventQueueManager.Instance.AddCommand(_cmdRecoilFire);

            ShootBullet(transform1);
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

        protected virtual void Update()
        {
            if (_cooldownTimer > 0)
            {
                _cooldownTimer -= Time.deltaTime;
            }
        }
    }
}