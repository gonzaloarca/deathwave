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
    public class Drone : Enemy
    {
               // INSTANCIAS
        private FlyingEnemyMovementController _movementController;
        [SerializeField] private GameObject _deathSound;
        //  private CmdIdle _idle;
        private float _vision;
        private float _range;
        private bool _gunsDrawn = true;
        private void Start()
        {
            base.Start();
            _movementController = GetComponent<FlyingEnemyMovementController>();
            _vision = this.EnemyStats.Vision;
            _range = this.ActorStats.Range;
        }

        void Update()
        {
            // Debug.Log("Bad to the bone");
            float distance = Vector3.Distance(_target.transform.position, transform.position);
            Vector3 targetPos = _target.transform.position;
            if (distance >= _range && _gunsDrawn)
            {
                _gunsDrawn = false;
                targetPos.y = transform.position.y;
                _movementController.LookAt(targetPos);
                _movementController.Travel(Vector3.forward);
                return;
            }


       
            if(distance >= (_range-15f) && !_gunsDrawn){
                targetPos.y = transform.position.y; 
                _movementController.LookAt(targetPos);
                _movementController.Travel(Vector3.forward);
                _movementController.DrawGun();
                return;
            }


            if (distance <= _range)
            {
                targetPos.y += 1f;
                _movementController.LookAt(targetPos);
                _movementController.DrawGun();
                if(_gunsDrawn) _movementController.Attack(targetPos);
                return;
            }


        }
        
        public void GunDrawnTrue(){
            _gunsDrawn = true;
        }
        
        public void GunDrawnFalse(){
            _gunsDrawn = false;
        }

        protected virtual void OnDestroy(){;
            if(!EventsManager.Instance.IsGameOver()){
                Instantiate(_deathSound, this.transform.position , Quaternion.identity);
            }
            base.OnDestroy();
        }

    }
}