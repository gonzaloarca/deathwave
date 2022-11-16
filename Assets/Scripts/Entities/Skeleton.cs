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
        private bool _drop = true;
        /* Commands */
        private CmdJump _cmdJump;
        private CmdSprint _cmdStartSprint;
        private CmdSprint _cmdStopSprint;
        [SerializeField] private GameObject _ammoDrop;
        //  private CmdIdle _idle;
        private float _vision;
        private float _meleeRange;
        private GameObject _target;

        private void Start()
        {
            _movementController = GetComponent<EnemyMovementController>();
            // ChangeWeapon(0);
            GameObject[] players;
            players = GameObject.FindGameObjectsWithTag("Player");

            if (players.Length == 0)
            {
                Debug.Log("No game objects are tagged with Player");
            }
            else
            {
                _target = players[0];
            }

            _vision = this.EnemyStats.Vision;
            _meleeRange = this.ActorStats.MeleeRange;
            _cmdStartSprint = new CmdSprint(_movementController, true);
            _cmdStopSprint = new CmdSprint(_movementController, false);
            _cmdJump = new CmdJump(_movementController);
            //_cmdPunch= new CmdPunch(_punch);
            EventsManager.Instance.OnGameOver+= OnGameOver;
        }

        void Update()
        {
            // Debug.Log("Bad to the bone");
            float distance = Vector3.Distance(_target.transform.position, transform.position);
            _movementController.LookAt(_target.transform.position);
            if (distance >= _vision)
            {
                _movementController.Travel(Vector3.forward);
                //Debug.Log("Not Following");
                // _animator.SetFloat("Vertical", 0f);
                // _animator.SetFloat("Horizontal", 0f);
                // _animator.SetBool("running", false);
                return;
            }

            if (distance <= _meleeRange)
            {
                _movementController.Attack();
            return;
            }

            
            _movementController.Travel(Vector3.forward);

        }

        private void OnDestroy(){
            EventsManager.Instance.EventEnemyDeath();
            if(_drop) DeadDrop();
            Destroy(this.transform.parent.gameObject);
        }

        public void OnGameOver(bool isVictory){
            _drop = false;
        }

        void DeadDrop(){
            var position = this.transform.position;
            position.y += 1;
            if(Random.Range(0f,1f) < EnemyStats.DropFreq )
                Instantiate(_ammoDrop , position , Quaternion.identity );
        }

        
    }
}