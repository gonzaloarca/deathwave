using System;
using Entities;
using Strategy;
using UnityEngine;
using Flyweight;
using Sounds;
namespace Controllers{
    
    [RequireComponent(typeof(Enemy),typeof(Animator),typeof(DroneSoundController))]
    public class FlyingEnemyMovementController : MonoBehaviour, IFollower
    {

        [SerializeField] private Transform[] _guns;
        [SerializeField] private GunStats _droneGunStats;
        
        public GunStats DroneGunStats => _droneGunStats;
        //[SerializeField] private MuzzleFlash[] _muzzleFlashes;
        private int _gunCount;
        private int _shoots = 0;
        public float MovementSpeed => _movementSpeed;
        private float _movementSpeed;
        // public Transform _torso;
        //private Animator _animator;
        public int GroundLayer => LayerMask.NameToLayer($"Ground");
        private bool _sprint;
        private float _stop;
        private float _damage;
        private float _range;
        private Animator _animator;
        private GameObject _bullet;
        private DroneSoundController _sounds;
        private void Start()
        {
            _stop = 0f;
            _sprint = true;
            _gunCount = _guns.Length;
            // _rigidbody = GetComponent<Rigidbody>();
          //  _animator = GetComponent<Animator>();
            //_animator.SetBool("running" , true);
            _movementSpeed = GetComponent<Enemy>().ActorStats.SprintSpeed;
            _animator = GetComponent<Animator>();
            _damage = DroneGunStats.Damage;
            _range = DroneGunStats.Range;
            _bullet = DroneGunStats.Bullet;
            _sounds = GetComponent<DroneSoundController>();
        }

        private void Update(){
              if(_stop > 0f)
                    _stop -= Time.deltaTime;
        }

        public void SetSpeedModifier(float num ){}
        
        public void Travel(Vector3 direction)
        {
      
            if(_stop >0f)
                return;
            _animator.SetBool("Moving" , true);
            _animator.SetBool("DrawGun" , false);
            _animator.SetBool("Shooting" , false);
        //    _animator.SetFloat("Vertical", 1f);
           // Debug.Log("speed " + _movementSpeed);
            transform.Translate(direction * (Time.deltaTime * MovementSpeed));
        }

        public void Attack(Vector3 objective){

     
            if(_stop >0f)  
                return;
            
            _animator.SetBool("Moving" , false);
            _animator.SetBool("DrawGun" , true);
            _animator.SetBool("Shooting" , true);
            _shoots++;
            _stop = DroneGunStats.Cooldown;
            ShootBullet(_shoots % (_gunCount) , objective);
            // _animator.SetFloat("Vertical", 0f);     
            // if(_lpunch) _animator.SetBool("punch_L", true);
            // else _animator.SetBool("punch_R",true);
            
            // _stop = 0.5f;
            // _lpunch = !_lpunch;
        }

        public void DrawGun(){
             if(_stop >0f)  
                return;
            _animator.SetBool("DrawGun" , true);
            _animator.SetBool("Moving" , false);
        }
        protected virtual void ShootBullet(int gunIndex , Vector3 objective)
        {
            Transform gun = _guns[gunIndex];
            _sounds.Shoot();
                //var MuzzleFlashParticles = _muzzleFlashes[gunIndex];
          //  MuzzleFlashParticles.Play();
            // apply spread to the bullet
            InstantiateBullet(gun.position, gun.rotation , objective);
        }

        public void LookAt(Vector3 direction)
        {
            transform.LookAt(direction);
        }
        public void Rotate(Vector3 direction)
        {
            transform.Rotate(direction);
        }
        
        public void Jump(){
        
            if(_stop >0f)
                return;
            
            // Only jump if the player is on the ground
            if (Physics.Raycast(transform.position, -Vector3.up, 100.0f, 1 << GroundLayer))
            {
             
               // _rigidbody.AddForce(Vector3.up * JumpStrength, ForceMode.Impulse);
            }
        }

        public void Sprint(bool isSprinting)
        {
            _sprint = isSprinting;
            if (isSprinting)
            {
          //      _animator.SetBool("running" , true);
                _movementSpeed = GetComponent<Actor>().ActorStats.SprintSpeed;
            }
            else
            {
    
            //    _animator.SetBool("running" , false);
                _movementSpeed = GetComponent<Actor>().ActorStats.WalkSpeed;
            }
        }

        protected virtual void InstantiateBullet(Vector3 position, Quaternion rotation, Vector3 objective)
        {
            var bullet = Instantiate(_bullet, position, rotation);
            bullet.transform.LookAt(objective);
            bullet.transform.localScale = new Vector3(0.1f,0.1f,0.1f);
            var bulletScript = bullet.GetComponent<IBullet>();
            bullet.name = "Enemy Bullet";
            bulletScript.SetTargetLayer(11);
            bulletScript.SetRange(_range);
            bulletScript.SetDamage(_damage);
        }

    }
}