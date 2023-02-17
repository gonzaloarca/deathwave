using System.Collections.Generic;
using Commands;
using Controllers;
using EventQueue;
using Factory;
using Managers;
using Sounds;
using Strategy;
using UnityEngine;
using Weapons;

namespace Entities
{
    [RequireComponent(typeof(Rigidbody))]
    public class Player : Actor
    {
        // INSTANCIAS
        private PlayerMovementController _movementController;
        private PlayerCameraController _cameraController;

        [SerializeField] private GunSpawner _gunFactory;
        [SerializeField] private int _maxGuns = 2;
        [SerializeField] private List<GunType> _initialGunTypes;
        [SerializeField] private List<Gun> _guns;
        private Gun _currentGun;
        private int _currentGunIndex = 0;
        [SerializeField] private Transform _gunParent;

        private HealthController _healthController;

        public PlayerScoreController scoreController;

        // BINDING MOVEMENT
        [SerializeField] private KeyCode _moveForward = KeyCode.W;
        [SerializeField] private KeyCode _moveBack = KeyCode.S;
        [SerializeField] private KeyCode _moveLeft = KeyCode.A;
        [SerializeField] private KeyCode _moveRight = KeyCode.D;
        private readonly string _jump = "space";
        [SerializeField] private KeyCode _sprint = KeyCode.LeftShift;

        // BIINDING COMBAT
        [SerializeField] private KeyCode _shoot = KeyCode.Mouse0;
        [SerializeField] private KeyCode _reload = KeyCode.R;

        [SerializeField] private KeyCode _weaponSlot1 = KeyCode.Alpha1;
        [SerializeField] private KeyCode _weaponSlot2 = KeyCode.Alpha2;

        [SerializeField] private KeyCode _setVictory = KeyCode.Return;
        [SerializeField] private KeyCode _setDefeat = KeyCode.Backspace;


        /* Commands */
        private CmdMovement _cmdMoveForward;
        private CmdMovement _cmdMoveBack;
        private CmdMovement _cmdMoveLeft;
        private CmdMovement _cmdMoveRight;
        private CmdJump _cmdJump;
        private CmdSprint _cmdStartSprint;
        private CmdSprint _cmdStopSprint;
        private CmdShoot _cmdShoot;
        private CmdReload _cmdReload;

        private PlayerSoundController _soundController;
        private int _enemyLayer;
        private bool _alive = true;

        private void OnGameOver(bool isVictory)
        {
            _alive = isVictory;
        }

        private void Start()
        {
            _healthController = GetComponent<HealthController>();
            _movementController = GetComponent<PlayerMovementController>();
            _cameraController = GetComponent<PlayerCameraController>();
            _soundController = GetComponent<PlayerSoundController>();
            scoreController = GetComponent<PlayerScoreController>();

            _cmdMoveForward = new CmdMovement(_movementController, Vector3.forward);
            _cmdMoveBack = new CmdMovement(_movementController, -Vector3.forward);
            _cmdMoveLeft = new CmdMovement(_movementController, -Vector3.right);
            _cmdMoveRight = new CmdMovement(_movementController, Vector3.right);
            _cmdStartSprint = new CmdSprint(_movementController, true);
            _cmdStopSprint = new CmdSprint(_movementController, false);
            _cmdJump = new CmdJump(_movementController);

            // Set _currentGun

            foreach (var gunType in _initialGunTypes)
            {
               // Debug.log("GUN TYPE: " + gunType);
                _guns.Add(CreateGun(gunType));
            }

            ChangeWeapon(0);
            _cmdReload = new CmdReload(_currentGun);
            _cmdShoot = new CmdShoot(_currentGun);
            _enemyLayer = LayerMask.NameToLayer("Enemy");
        }

        void Update()
        {
            if (!_alive) return;
            // W-A-S-D
            if (Input.GetKey(_moveForward)) EventQueueManager.Instance.AddCommand(_cmdMoveForward);
            if (Input.GetKey(_moveBack)) EventQueueManager.Instance.AddCommand(_cmdMoveBack);
            if (Input.GetKey(_moveLeft)) EventQueueManager.Instance.AddCommand(_cmdMoveLeft);
            if (Input.GetKey(_moveRight)) EventQueueManager.Instance.AddCommand(_cmdMoveRight);

            // Jumping
            if (Input.GetKeyDown(_jump)) EventQueueManager.Instance.AddCommand(_cmdJump);

            // Sprinting
            if (Input.GetKeyDown(_sprint)) EventQueueManager.Instance.AddCommand(_cmdStartSprint);
            if (Input.GetKeyUp(_sprint)) EventQueueManager.Instance.AddCommand(_cmdStopSprint);

            // Camera Rotation
            var verticalRotation = Input.GetAxis("Mouse Y");
            var horizontalRotation = Input.GetAxis("Mouse X");
            var rotationDirection = new Vector3(horizontalRotation, verticalRotation, 0);

            EventQueueManager.Instance.AddCommand(new CmdRotatePlayerCamera(_cameraController, rotationDirection));


            // Gun Logic
            if (Input.GetKeyDown(_shoot)) EventQueueManager.Instance.AddCommand(_cmdShoot);
            if (Input.GetKeyDown(_reload)) EventQueueManager.Instance.AddCommand(_cmdReload);

            if (Input.GetKeyDown(_weaponSlot1)) ChangeWeapon(0);
            if (Input.GetKeyDown(_weaponSlot2)) ChangeWeapon(1);


            if (Input.GetKeyDown(KeyCode.Backspace)) _healthController.TakeDamage(30);

            //
            // if (Input.GetKeyDown(_setVictory)) EventsManager.Instance.EventGameOver(true);
            // if (Input.GetKeyDown(_setDefeat)) GetComponent<IDamageable>().TakeDamage(20);


            // if (_timer <= 0) Debug.Log("timer fin");
        }

        public bool OwnsGun(GunType gunType) => _guns.Exists(g => g.Type == gunType);
        public Gun GetGun(){
            return _currentGun;
        }
        private void ChangeWeapon(int index)
        {
            if (_guns.Count <= 0)
                return;

            if (_guns.Count <= index)
                return;

            foreach (var gun in _guns)
            {
                gun.gameObject.SetActive(false);
                gun.ChangeGun();
            }
            

            _currentGunIndex = index;
            _currentGun = _guns[index];
            _currentGun.gameObject.SetActive(true);
             EventsManager.Instance.EventGunChange();
            _cmdShoot = new CmdShoot(_currentGun);
            _cmdReload = new CmdReload(_currentGun);
            _currentGun.DrawGun();
            
           

            // Change speed of character based on weapon
            EventQueueManager.Instance.AddCommand(new CmdSetSpeedModifier(_movementController,
                _currentGun.PlayerSpeedModifier));
        }

        void OnCollisionEnter(Collision collision)
        {
            if (_enemyLayer != collision.gameObject.layer) return;


            IMelee melee = collision.gameObject.GetComponentInChildren<IMelee>();
            if (melee == null)
                return;

            _healthController.TakeDamage(melee.Damage());
        }

        private Gun CreateGun(GunType gunType)
        {
            Gun gun = _gunFactory.Create(gunType);
            gun.transform.parent = _gunParent;
            gun.transform.localPosition = new Vector3(gun.ArmsShift.x , gun.ArmsShift.y , gun.ArmsShift.z);
            gun.transform.localEulerAngles = new Vector3(gun.ArmsRotation.x , gun.ArmsRotation.y , gun.ArmsRotation.z);
            return gun;
        }

        public void AddGun(GunType gunType)
        {
            // replace current gun with new gun
            if (_guns.Count == _maxGuns)
            {
                _guns.RemoveAt(_currentGunIndex);
                    
                GunSoundController c = _currentGun.gameObject.GetComponent<GunSoundController>();
                c.Unsuscribe();
                Destroy(_currentGun.gameObject);
            }
            
            _guns.Add(CreateGun(gunType));
            int newGunIndex = _guns.Count;
            // foreach( Gun g in _guns){
                
            //     GunSoundController c = g.gameObject.GetComponent<GunSoundController>();
            //     if(c){
            //         Debug.Log("Found controller");
            //         c.Reset();
            //     }

            // }
            ChangeWeapon(newGunIndex-1);
        }

        public void RefillGunAmmo(GunType gunType)
        {
            Gun gun = _guns.Find(g => g.Type == gunType);

            if (gun != null)
            {
                gun.RefillAmmo();
            }
        }
    }
}