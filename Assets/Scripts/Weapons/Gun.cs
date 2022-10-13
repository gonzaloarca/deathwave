using System;
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

        public GameObject MuzzleFlash => _stats.MuzzleFlash;
        public GameObject Bullet => _stats.Bullet;
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
        
        public Transform BulletSpawnPoint => _bulletSpawnPoint;
        [SerializeField] private Transform _bulletSpawnPoint;


        private bool _reloading = false;
        private float _reloadTimer = 0f;

        private Animator _animations;

    

        // Bullet instantiation method for modularity across extended classes
        protected void InstantiateBullet(Vector3 position, Quaternion rotation)
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
            _muzzleFlashParticles.Play();
            // apply spread to the bullet
            var crosshairRay = Camera.main.ViewportPointToRay(new Vector3(0.5f, 0.5f, 0));
            var spread = Random.insideUnitCircle * Spread;
            var bulletDirection = crosshairRay.direction + new Vector3(spread.x, spread.y, 0);

            InstantiateBullet(_bulletSpawnPoint.position, Quaternion.LookRotation(bulletDirection));
        }

        public void ChangeGun(){
            _reloading = false;
            _reloadTimer = 0;
            _animations?.SetBool("change_gun" , true);
            _animations?.SetBool("draw_gun" , false);
            _animations?.SetBool("reloading_start" , false);
        }

        public void DrawGun(){
            _animations?.SetBool("change_gun" , false);
            _animations?.SetBool("draw_gun" , true);
            _animations?.SetTrigger("draw_gun 0");
        }

        public virtual void Shoot()
        {
            if (_reloading || _totalBulletsLeft == 0) return; // TODO: Play empty sound
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
            EventsManager.Instance.EventGunShot();
            
            ShootBullet(transform1);
        }



        public void Reload()
        {
            if (_totalBulletsLeft <= 0|| _bulletsLeftInMag == MagSize) return;
            
          
            
            // animation with lerping\
            _reloading = true;
            _reloadTimer = _stats.ReloadTime;
             _animations?.SetBool("reload_finish" , false);
            _animations?.SetBool("reload_start" , true);
        }

        public void ReloadFinish(){
            _animations?.SetBool("reload_start" , false);
            _animations?.SetBool("reload_finish" , true);
            _reloading = false;
            var emptyRounds = MagSize - _bulletsLeftInMag;
            if(emptyRounds > _totalBulletsLeft )
                emptyRounds = _totalBulletsLeft;

            _totalBulletsLeft -= emptyRounds;
            _bulletsLeftInMag += emptyRounds;
          
        }


        public void Refill()
        {
            _totalBulletsLeft = _stats.MaxMags * _stats.MagSize;
        }

        public void AddMags(int mags)
        {
            _totalBulletsLeft = Math.Min(_totalBulletsLeft + mags * MagSize, MaxMags * MagSize);
        }



        private void Start()
        {
            Refill();
            _bulletsLeftInMag = MagSize;
            _recoilController = transform.root.GetComponentInChildren<RecoilController>();
            _cmdRecoilFire = new CmdRecoilFire(_recoilController, GunRecoil);
            _hitBoxLayer = LayerMask.NameToLayer("Hitbox");

            var muzzleFlash = Instantiate(MuzzleFlash, _bulletSpawnPoint.position, _bulletSpawnPoint.rotation);
            muzzleFlash.transform.parent = _bulletSpawnPoint;
            _muzzleFlashParticles = muzzleFlash.GetComponent<ParticleSystem>();
            _animations = GetComponent<Animator>();
        }


        private void Update(){
            if(_reloading){
                _reloadTimer -= Time.deltaTime;
                if(_reloadTimer <=0)
                    ReloadFinish();
            }
        }

      
    }
}