using System.Collections.Generic;
using System.Collections;
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
        private Rigidbody _rigidbody;
        //  private CmdIdle _idle;
        private float _vision;
        private float _range;
        public bool _gunsDrawn = true;

        private void Start()
        {
            base.Start();
        
            _rigidbody = transform.parent.gameObject.GetComponent<Rigidbody>();
            _movementController = GetComponent<FlyingEnemyMovementController>();
            _vision = this.EnemyStats.Vision;
            _range = this.ActorStats.Range;
            EventsManager.Instance.OnPooling += OnPooling;
        }

        private bool lineOfSight ( Vector3 targetPos){
            
        // Bit shift the index of the layer (8) to get a bit mask
        int layerMask = 1 << LayerMask.NameToLayer($"Enemy");

        // This would cast rays only against colliders in layer 8.
        // But instead we want to collide against everything except layer 8. The ~ operator does this, it inverts a bitmask.
        layerMask = ~layerMask;
        Vector3 direction = (targetPos - transform.position).normalized;
        RaycastHit hit;
        // Does the ray intersect any objects excluding the player layer
        if (Physics.Raycast(transform.position, direction, out hit, Mathf.Infinity, layerMask))
        {
            if(hit.transform.gameObject.layer == LayerMask.NameToLayer($"Player")){
                
                return true;
            }
        }
       
            return false;
        }
        
        
        void Update()
        {
            if(!_movementController.IsEnabled()){
                
               return;
            }
            // Debug.Log("Bad to the bone");
            float distance = Vector3.Distance(_target.transform.position, transform.position);
            Vector3 targetPos = _target.transform.position;
            bool sight =  lineOfSight(targetPos);
            if( !sight){
                Debug.Log("outta sight");
                _movementController.Travel(targetPos);
                return;
            }
            //si estoy a mucha distancia y saque las armas, las guardo
            if (distance >= _range && _gunsDrawn)
            {
                  Debug.Log("getting guns inside");
                _gunsDrawn = false;
                //targetPos.y = transform.position.y;
                //_movementController.LookAt(targetPos);
                _movementController.Travel(targetPos);
                return;
            }


            


            //Si estoy cerca y no tengo las armas afuera
       
            if(distance <= (_range-15f) && !_gunsDrawn){
                Debug.Log("im close but i dont have guns out");
                targetPos.y = transform.position.y; 
       //         _movementController.LookAt(targetPos);
                //_movementController.Travel(Vector3.forward);
                _movementController.DrawGun();
                StartCoroutine(StartDraw());
                return;
            }


            if (distance <= _range)
            {
                  Debug.Log("gotta shoot");
                targetPos.y += 1f;
                _movementController.LookAt(targetPos);
                _movementController.DrawGun();
                 _movementController.Stop();
                _rigidbody.velocity = Vector3.zero;
                _rigidbody.angularVelocity = Vector3.zero; 

                if(_gunsDrawn) _movementController.Attack(targetPos);
                return;
            }

            Debug.Log("nothing");
             _movementController.Travel(targetPos);


        }
        
        public void GunDrawnTrue(){
            _gunsDrawn = true;
        }
        
        public void GunDrawnFalse(){
            _gunsDrawn = false;
        }
        IEnumerator StartDraw(){
            yield return new WaitForSeconds(1);
            GunDrawnTrue();
        }



        protected virtual void OnDisable(){;
            if(!EventsManager.Instance.IsGameOver() && !_pooling ){
                Instantiate(_deathSound, this.transform.position , Quaternion.identity);
            }
            base.OnDisable();
        }
   

     public void OnEnable(){
            Start();
        }
    }
}