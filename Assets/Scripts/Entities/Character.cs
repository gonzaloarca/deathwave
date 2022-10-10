using System.Collections.Generic;
using Commands;
using Controllers;
using EventQueue;
using Managers;
using Strategy;
using UnityEngine;
using Weapons;

namespace Entities
{
    [RequireComponent(typeof(Rigidbody))]
    public class Character : Actor
    {
        // INSTANCIAS
        private MovementController _movementController;
        [SerializeField] private List<Gun> _guns;
        private Gun _currentGun;
        private LifeController _lifeController;
        // BINDING MOVEMENT
        [SerializeField] private KeyCode _moveForward = KeyCode.W;
        [SerializeField] private KeyCode _moveBack = KeyCode.S;
        [SerializeField] private KeyCode _moveLeft = KeyCode.A;
        [SerializeField] private KeyCode _moveRight = KeyCode.D;
        [SerializeField] private KeyCode _jump = KeyCode.Space;
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

        private PlayerDamageSFX _grunts;
        private int _enemyLayer;
        private void Start()
        {

            _lifeController = GetComponent<LifeController>();
            _movementController = GetComponent<MovementController>();

            _cmdMoveForward = new CmdMovement(_movementController, Vector3.forward);
            _cmdMoveBack = new CmdMovement(_movementController, -Vector3.forward);
            _cmdMoveLeft = new CmdMovement(_movementController, -Vector3.right);
            _cmdMoveRight = new CmdMovement(_movementController, Vector3.right);
            _cmdStartSprint = new CmdSprint(_movementController, true);
            _cmdStopSprint = new CmdSprint(_movementController, false);
            _cmdJump = new CmdJump(_movementController);
            
            // Set _currentGun
            ChangeWeapon(0);
            
            _cmdReload = new CmdReload(_currentGun);
            _cmdShoot = new CmdShoot(_currentGun);
            _grunts = GetComponent<PlayerDamageSFX>();
            _enemyLayer = LayerMask.NameToLayer("Enemy");
        }

        void Update()
        {
            // W-A-ÎS-D
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

            EventQueueManager.Instance.AddCommand(new CmdRotation(_movementController, rotationDirection));

            
            // Gun Logic
            if (Input.GetKeyDown(_shoot)) EventQueueManager.Instance.AddCommand(_cmdShoot);
            if (Input.GetKeyDown(_reload)) EventQueueManager.Instance.AddCommand(_cmdReload);

            if (Input.GetKeyDown(_weaponSlot1)) ChangeWeapon(0);
            if (Input.GetKeyDown(_weaponSlot2)) ChangeWeapon(1);

            //
            // if (Input.GetKeyDown(_setVictory)) EventsManager.Instance.EventGameOver(true);
            // if (Input.GetKeyDown(_setDefeat)) GetComponent<IDamageable>().TakeDamage(20);


            // if (_timer <= 0) Debug.Log("timer fin");
        }

        private void ChangeWeapon(int index)
        {
            if(_guns.Count <= 0)
                return;
            foreach (var gun in _guns) gun.gameObject.SetActive(false);
            _currentGun = _guns[index];
            _currentGun.gameObject.SetActive(true);
            _cmdShoot = new CmdShoot(_currentGun);
            
            // Change speed of character based on weapon
            EventQueueManager.Instance.AddCommand(new CmdSetSpeedModifier(_movementController, _currentGun.PlayerSpeedModifier));
        }

        void OnCollisionEnter(Collision collision)
        {
            if (_enemyLayer != collision.gameObject.layer ) return;

            
            IMelee melee = collision.gameObject.GetComponentInChildren<IMelee>();
            if(melee == null)
                return;
        
            _grunts.Sound();
            _lifeController.TakeDamage(melee.Damage());
        
        }
    }
}