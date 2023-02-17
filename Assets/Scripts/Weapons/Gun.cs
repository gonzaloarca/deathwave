using System;
using Buyable;
using Commands;
using Controllers;
using Entities;
using EventQueue;
using Flyweight;
using Managers;
using Strategy;
using UnityEngine;
using Random = UnityEngine.Random;


namespace Weapons
{
    public class Gun : MonoBehaviour, IGun
    {
        [SerializeField] private GunStats _stats;

        public ArmsRotation ArmsRotation => _stats.ArmsRotation;
        public ArmsShift ArmsShift => _stats.ArmsShift;
        public GunType Type => _stats.Type;
        public string Name => _stats.Name;
        public GameObject MuzzleFlash => _stats.MuzzleFlash;
        public GameObject Bullet => _stats.Bullet;
        public int MagSize => _stats.MagSize;
        public float ReloadTime => _stats.ReloadTime;
        public float Cooldown => _stats.Cooldown;
        public float PlayerSpeedModifier => _stats.PlayerSpeedModifier;
        public float Damage => _stats.Damage;
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

        protected ParticleSystem MuzzleFlashParticles;

        private int _hitBoxLayer;

        public Transform BulletSpawnPoint => _bulletSpawnPoint;
        [SerializeField] private Transform _bulletSpawnPoint;

        public Transform MuzzleSpawnPoint => _muzzleSpawnPoint;
        [SerializeField] private Transform _muzzleSpawnPoint;

        private bool _reloading = false;
        private float _reloadTimer = 0f;

        private Animator _animations;


        // Bullet instantiation method for modularity across extended classes
        protected virtual void InstantiateBullet(Vector3 position, Quaternion rotation)
        {
            var bullet = Instantiate(Bullet, position, rotation);
            var bulletScript = bullet.GetComponent<IBullet>();
            bullet.name = "Bullet";
            bulletScript.SetRange(Range);
            bulletScript.SetDamage(Damage);
        }

        protected virtual void ShootBullet(Transform theTransform)
        {
            Debug.Log("SHOOT");
            MuzzleFlashParticles.Play();
            // apply spread to the bullet
            var crosshairRay = Camera.main.ViewportPointToRay(new Vector3(0.5f, 0.5f, 0));
            var spread = Random.insideUnitCircle * Spread;
            var bulletDirection = crosshairRay.direction + new Vector3(spread.x, spread.y, 0);

            InstantiateBullet(_bulletSpawnPoint.position, Quaternion.LookRotation(bulletDirection));
        }

        public void ChangeGun()
        {
           
            _reloading = false;
            _reloadTimer = 0;
            _animations?.SetBool("change_gun", true);
            _animations?.SetBool("draw_gun", false);
            _animations?.SetBool("reload_start", false);
        }

        public void DrawGun()
        {
             UI_AmmoUpdater();
            _animations?.SetBool("change_gun", false);
            _animations?.SetBool("draw_gun", true);
            _animations?.SetTrigger("draw_gun 0");
        }

        public virtual void Shoot()
        {
            if (_reloading) return; // TODO: Play empty sound

            if (_bulletsLeftInMag == 0)
            {
                EventsManager.Instance.EventEmptyMag();
                Reload();
                return;
            }
            
            if (Time.time < _nextTimeToFire) return;

            _bulletsLeftInMag--;
            _nextTimeToFire = Time.time + Cooldown;
            var transform1 = transform;

            EventQueueManager.Instance.AddCommand(_cmdRecoilFire);
            
            ShootBullet(transform1);
            
            EventsManager.Instance.EventGunShot();
            UI_AmmoUpdater();
        }


        public void Reload()
        {
            if (_totalBulletsLeft <= 0|| _bulletsLeftInMag == MagSize) return;
            EventsManager.Instance.EventGunReloadStart();
            // animation with lerping\
            _reloading = true;
            _reloadTimer = _stats.ReloadTime;
            _animations?.SetBool("reload_finish" , false);
            _animations?.SetBool("reload_start" , true);
        }


        public void ReloadFinish(){
            EventsManager.Instance.EventGunReloadEnd();
            _animations?.SetBool("reload_start" , false);
            _animations?.SetBool("reload_finish" , true);
            _reloading = false;
            var emptyRounds = MagSize - _bulletsLeftInMag;
            if (emptyRounds > _totalBulletsLeft)
                emptyRounds = _totalBulletsLeft;

            _totalBulletsLeft -= emptyRounds;
            _bulletsLeftInMag += emptyRounds;
            
            UI_AmmoUpdater();
        }


        public void RefillAmmo()
        {
            _totalBulletsLeft = _stats.MaxMags * _stats.MagSize;
            _bulletsLeftInMag = MagSize;

            UI_AmmoUpdater();
        }

        public void AddMags(int mags)
        {
            _totalBulletsLeft = Math.Min(_totalBulletsLeft + mags * MagSize, MaxMags * MagSize);
            
            UI_AmmoUpdater();
        }
        
        public void UI_AmmoUpdater() {
            if(this.gameObject.active) EventsManager.Instance.EventAmmoChange(_bulletsLeftInMag, _totalBulletsLeft);
        }

        private void Start()
        {
            RefillAmmo();
            
            _recoilController = transform.root.GetComponentInChildren<RecoilController>();
            _cmdRecoilFire = new CmdRecoilFire(_recoilController, GunRecoil);
            _hitBoxLayer = LayerMask.NameToLayer("Hitbox");
            EventsManager.Instance.OnAmmoPickup += OnAmmoPickup;
            var muzzleFlash = Instantiate(MuzzleFlash, _muzzleSpawnPoint.position, _muzzleSpawnPoint.rotation);
            muzzleFlash.transform.parent = _muzzleSpawnPoint;
            MuzzleFlashParticles = muzzleFlash.GetComponent<ParticleSystem>();
            _animations = GetComponent<Animator>();
        
        }


        private void Update()
        {
            if (_reloading)
            {
                _reloadTimer -= Time.deltaTime;
                if (_reloadTimer <= 0)
                    ReloadFinish();
            }
        }

        private void OnAmmoPickup(){
            RefillAmmo();
        }
    }
}