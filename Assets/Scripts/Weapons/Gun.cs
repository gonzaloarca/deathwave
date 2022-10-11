using System;
using Commands;
using Controllers;
using Entities;
using EventQueue;
using Flyweight;
using Strategy;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Weapons
{
    public class Gun : MonoBehaviour, IGun
    {
        [SerializeField] private GunStats _stats;
        
        public GameObject MuzzleFlash => _stats.MuzzleFlash;
        public int MagSize => _stats.MagSize;
        public float ReloadTime => _stats.ReloadTime;
        public float Cooldown => _stats.Cooldown;
        public float PlayerSpeedModifier => _stats.PlayerSpeedModifier;
        public int Damage => _stats.Damage;
        public int MaxMags => _stats.MaxMags;
        public GunRecoil GunRecoil => _stats.Recoil;
        public int TotalBulletsLeft => _totalBulletsLeft;
        public float Range => _stats.Range;
        public float Spread => _stats.Spread;
        [SerializeField] private int _totalBulletsLeft;

        public int BulletsLeftInMag => _bulletsLeftInMag;
        [SerializeField] private int _bulletsLeftInMag;

        public float CooldownTimer => _nextTimeToFire;
        [SerializeField] private float _nextTimeToFire = 0f;

        private RecoilController _recoilController;
        private CmdRecoilFire _cmdRecoilFire;

        private ParticleSystem _muzzleFlashParticles;
        
        private int _hitBoxLayer;

        private void Start()
        {
            Refill();
            _bulletsLeftInMag = MagSize;
            _recoilController = transform.root.GetComponentInChildren<RecoilController>();
            _cmdRecoilFire = new CmdRecoilFire(_recoilController, GunRecoil);
            _hitBoxLayer = LayerMask.NameToLayer("Hitbox");
            
            var bulletSpawn = transform.GetComponentInChildren<BulletSpawnController>()?.transform;
            var muzzleFlash = Instantiate(MuzzleFlash, bulletSpawn.position, bulletSpawn.rotation);
            muzzleFlash.transform.parent = bulletSpawn;
            _muzzleFlashParticles = muzzleFlash.GetComponent<ParticleSystem>();
        }

        // Bullet instantiation method for modularity across extended classes
        protected virtual void ShootBullet(Transform theTransform)
        {
            Debug.Log("SHOOT");
            _muzzleFlashParticles.Play();
            // apply spread to the bullet
            var bulletTarget = theTransform.forward + new Vector3(Random.Range(-Spread, Spread), Random.Range(-Spread, Spread), 0);
            // Raycast to see if we hit anything
            var mask = 1 << _hitBoxLayer;
            Debug.DrawRay(theTransform.position , theTransform.TransformDirection(Vector3.forward) * 10 , Color.red , 1f);
            if ( Physics.Raycast(theTransform.position, bulletTarget, out var hit, Range, mask) ) 
            {
                Debug.Log("HIT: " + hit.collider.transform.gameObject.name);
                // If we hit something, instantiate a bullet at the hit point
               hit.collider.transform.gameObject.GetComponent<IHittable>()?.Hit(Damage);
              
            }
        }

        public virtual void Shoot()
        {
            if (_totalBulletsLeft == 0) return; // TODO: Play empty sound
            if (_bulletsLeftInMag == 0)
            {
                Reload();
                return;
            }

            if (Time.time < _nextTimeToFire) return;

            _bulletsLeftInMag--;
            _nextTimeToFire = Time.time + Cooldown;
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
            if (_nextTimeToFire > 0)
            {
                _nextTimeToFire -= Time.deltaTime;
            }
        }
    }
}