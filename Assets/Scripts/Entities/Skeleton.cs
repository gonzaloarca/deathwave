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
    public class Skeleton : Enemy
    {
        // INSTANCIAS
        private EnemyMovementController _movementController;

        /* Commands */
        private CmdJump _cmdJump;
        private CmdSprint _cmdStartSprint;
        private CmdSprint _cmdStopSprint;

        //  private CmdIdle _idle;
        private float _vision;
        private float _meleeRange;
        private GameObject _target;

        private void Start()
        {
            _movementController = GetComponent<EnemyMovementController>();
            // ChangeWeapon(0);
            GameObject[] gameObjects;
            gameObjects = GameObject.FindGameObjectsWithTag("Player");

            if (gameObjects.Length == 0)
            {
                Debug.Log("No game objects are tagged with Player");
            }
            else
            {
                _target = gameObjects[0];
            }

            _vision = this.EnemyStats.Vision;
            _meleeRange = this.ActorStats.MeleeRange;
            _cmdStartSprint = new CmdSprint(_movementController, true);
            _cmdStopSprint = new CmdSprint(_movementController, false);
            _cmdJump = new CmdJump(_movementController);
            //_cmdPunch= new CmdPunch(_punch);
        }

        void Update()
        {
            // Debug.Log("Bad to the bone");
            float distance = Vector3.Distance(_target.transform.position, transform.position);
            if (distance >= _vision)
            {
                EventQueueManager.Instance.AddCommand(new CmdFollow(_movementController, _target.transform.position));
                //Debug.Log("Not Following");
                // _animator.SetFloat("Vertical", 0f);
                // _animator.SetFloat("Horizontal", 0f);
                // _animator.SetBool("running", false);
                return;
            }

            if (distance <= _meleeRange)
            {
                EventQueueManager.Instance.AddCommand(new CmdPunch(_movementController, _target.transform.position));
                return;
            }

            // Debug.Log("Wanna move");
            // //Desde el Jugador 
            // var direction = _target.transform.position - transform.position;
            // Debug.DrawRay(transform.position, direction,  Color.red, 0.1f);
            // Debug.DrawRay(transform.position, Vector3.forward,  Color.blue, 0.1f);
            EventQueueManager.Instance.AddCommand(new CmdFollow(_movementController, _target.transform.position));

            //    if(Vector3.Distance)
            //  if (Input.GetKey(_moveForward)) EventQueueManager.Instance.AddCommand(_cmdMoveForward);
            // if (Input.GetKey(_moveBack)) EventQueueManager.Instance.AddCommand(_cmdMoveBack);
            // if (Input.GetKey(_moveLeft)) EventQueueManager.Instance.AddCommand(_cmdMoveLeft);
            // if (Input.GetKey(_moveRight)) EventQueueManager.Instance.AddCommand(_cmdMoveRight);

            // // Jumping
            // if (Input.GetKeyDown(_jump)) EventQueueManager.Instance.AddCommand(_cmdJump);

            // // Sprinting
            // if (Input.GetKeyDown(_sprint)) EventQueueManager.Instance.AddCommand(_cmdStartSprint);
            // if (Input.GetKeyUp(_sprint)) EventQueueManager.Instance.AddCommand(_cmdStopSprint);

            // // Camera Rotation
            // var verticalRotation = Input.GetAxis("Mouse Y"); 
            // var horizontalRotation = Input.GetAxis("Mouse X");
            // var rotationDirection = new Vector3(horizontalRotation, verticalRotation, 0);

            // EventQueueManager.Instance.AddCommand(new CmdRotation(_movementController, rotationDirection));

            // //
            // // if (Input.GetKeyDown(_attack)) EventQueueManager.Instance.AddCommand(_cmdAttack);
            // // if (Input.GetKeyDown(_reload)) _currentGun?.Reload();
            // //
            // // if (Input.GetKeyDown(_weaponSlot1)) ChangeWeapon(0);
            // // if (Input.GetKeyDown(_weaponSlot2)) ChangeWeapon(1);
            // // if (Input.GetKeyDown(_weaponSlot3)) ChangeWeapon(2);
            // //
            // // if (Input.GetKeyDown(_setVictory)) EventsManager.Instance.EventGameOver(true);
            // // if (Input.GetKeyDown(_setDefeat)) GetComponent<IDamageable>().TakeDamage(20);


            // // if (_timer <= 0) Debug.Log("timer fin");     // W-A-S-D
        }

        // private void ChangeWeapon(int index)
        // {
        //     foreach (var gun in _guns) gun.gameObject.SetActive(false);
        //     _currentGun = _guns[index];
        //     _currentGun.gameObject.SetActive(true);
        //     _cmdAttack = new CmdAttack(_currentGun);
        // }
    }
}