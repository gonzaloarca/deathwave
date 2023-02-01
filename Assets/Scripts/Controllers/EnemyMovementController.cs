using System;
using Entities;
using Strategy;
using UnityEngine;
using UnityEngine.AI;


namespace Controllers{
    
    [RequireComponent(typeof(Enemy),typeof(Animator))]
    public class EnemyMovementController : MonoBehaviour, IFollower
    {

          // INSTANCIAS
        private NavMeshAgent _agent;
        public float MovementSpeed => _movementSpeed;
        private float _movementSpeed;
         private float _meleeRange;
        // public Transform _torso;
        private Animator _animator;
        public int GroundLayer => LayerMask.NameToLayer($"Ground");
        private bool _sprint;
        private bool _lpunch = false;
        private float _stop;
        private void Start()
        {
            _agent = GetComponent<NavMeshAgent>();
            _stop = 0f;
            _sprint = true;
            // _rigidbody = GetComponent<Rigidbody>();
            _animator = GetComponent<Animator>();
            _animator.SetBool("running" , true);
            _movementSpeed = GetComponent<Enemy>().ActorStats.SprintSpeed;
            _meleeRange = GetComponent<Enemy>().ActorStats.Range;
             _agent.stoppingDistance = _meleeRange;
            _agent.speed = _movementSpeed;
        }

        private void Update(){
            
              if(_stop > 0f)
                    _stop -= Time.deltaTime;
        }

        public void SetSpeedModifier(float num ){}
        public void Travel(Vector3 direction)
        {
            
             _agent.ResetPath();
            if(_stop >0f)
                return;
            _animator.SetFloat("Vertical", 1f);
           // Debug.Log("speed " + _movementSpeed);
            _agent.SetDestination(direction);
           
        }

        public void Attack(Vector3 objective){
            _agent.ResetPath();
            if(_stop >0f)  
                return;
                 
            _animator.SetFloat("Vertical", 0f);
            
            if(_lpunch) _animator.SetBool("punch_L", true);
            else _animator.SetBool("punch_R",true);
            
            _stop = 0.5f;
            _lpunch = !_lpunch;
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
                _animator.SetBool("running" , true);
                _movementSpeed = GetComponent<Actor>().ActorStats.SprintSpeed;
            }
            else
            {
    
                _animator.SetBool("running" , false);
                _movementSpeed = GetComponent<Actor>().ActorStats.WalkSpeed;
            }
        }

    }
}