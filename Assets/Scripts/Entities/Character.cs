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
    public class Character : Actor
    {
        // INSTANCIAS
        private MovementController _movementController;
        [SerializeField] private List<Gun> _guns;
        private Gun _currentGun;

        // BINDING MOVEMENT
        [SerializeField] private KeyCode _moveForward = KeyCode.W;
        [SerializeField] private KeyCode _moveBack = KeyCode.S;
        [SerializeField] private KeyCode _moveLeft = KeyCode.A;
        [SerializeField] private KeyCode _moveRight = KeyCode.D;

        // BIINDING COMBAT
        [SerializeField] private KeyCode _attack = KeyCode.Mouse0;
        [SerializeField] private KeyCode _reload = KeyCode.R;

        [SerializeField] private KeyCode _weaponSlot1 = KeyCode.Alpha1;
        [SerializeField] private KeyCode _weaponSlot2 = KeyCode.Alpha2;
        [SerializeField] private KeyCode _weaponSlot3 = KeyCode.Alpha3;

        [SerializeField] private KeyCode _setVictory = KeyCode.Return;
        [SerializeField] private KeyCode _setDefeat = KeyCode.Backspace;

        /* Commands */
        private CmdMovement _cmdMoveForward;
        private CmdMovement _cmdMoveBack;
        private CmdMovement _cmdMoveLeft;
        private CmdMovement _cmdMoveRight;
        private CmdAttack _cmdAttack;

        private void Start()
        {

            _movementController = GetComponent<MovementController>();
            // ChangeWeapon(0);
            Debug.Log("💀");

            _cmdMoveForward = new CmdMovement(_movementController, Vector3.forward);
            _cmdMoveBack = new CmdMovement(_movementController, -Vector3.forward);
            _cmdMoveLeft = new CmdMovement(_movementController, -Vector3.right);
            _cmdMoveRight = new CmdMovement(_movementController, Vector3.right);

            _cmdAttack = new CmdAttack(_currentGun);
        }

        void Update()
        {
            // W-A-ÎS-D
            Debug.Log("MBEH2");
            if (Input.GetKey(_moveForward)) EventQueueManager.Instance.AddCommand(_cmdMoveForward);
            if (Input.GetKey(_moveBack)) EventQueueManager.Instance.AddCommand(_cmdMoveBack);
            if (Input.GetKey(_moveLeft)) EventQueueManager.Instance.AddCommand(_cmdMoveLeft);
            if (Input.GetKey(_moveRight)) EventQueueManager.Instance.AddCommand(_cmdMoveRight);

            // Rotation
            var verticalRotation = Input.GetAxis("Mouse Y"); 
            var horizontalRotation = Input.GetAxis("Mouse X");
            var rotationDirection = new Vector3(horizontalRotation, verticalRotation, 0);

            EventQueueManager.Instance.AddCommand(new CmdRotation(_movementController, rotationDirection));

            //
            // if (Input.GetKeyDown(_attack)) EventQueueManager.Instance.AddCommand(_cmdAttack);
            // if (Input.GetKeyDown(_reload)) _currentGun?.Reload();
            //
            // if (Input.GetKeyDown(_weaponSlot1)) ChangeWeapon(0);
            // if (Input.GetKeyDown(_weaponSlot2)) ChangeWeapon(1);
            // if (Input.GetKeyDown(_weaponSlot3)) ChangeWeapon(2);
            //
            // if (Input.GetKeyDown(_setVictory)) EventsManager.Instance.EventGameOver(true);
            // if (Input.GetKeyDown(_setDefeat)) GetComponent<IDamageable>().TakeDamage(20);


            // if (_timer <= 0) Debug.Log("timer fin");
        }

        private void ChangeWeapon(int index)
        {
            foreach (var gun in _guns) gun.gameObject.SetActive(false);
            _currentGun = _guns[index];
            _currentGun.gameObject.SetActive(true);
            _cmdAttack = new CmdAttack(_currentGun);
        }
    }
}