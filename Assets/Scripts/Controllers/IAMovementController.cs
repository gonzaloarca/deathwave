using System;
using Entities;
using Strategy;
using UnityEngine;


namespace Controllers{
    
    [RequireComponent(typeof(IA),typeof(Animator))]
    public class IAMovementController : MonoBehaviour, IFollower
    {
        public float MouseXSensitivity => 0;
        public float MouseYSensitivity => 0;
    
        public float MovementSpeed => _movementSpeed;
        private float _movementSpeed;
        public float RotationSpeed => GetComponent<IA>().IAStats.RotationSpeed;
        public float JumpStrength => GetComponent<IA>().IAStats.JumpStrength;
        // public Transform _torso;
        private Animator _animator;
        private float _verticalRotation = 0f;
        public int GroundLayer => LayerMask.NameToLayer($"Ground");

    
        private void Start()
        {
  
            // _rigidbody = GetComponent<Rigidbody>();
            _animator = GetComponent<Animator>();
            _animator.SetBool("running" , true);
            _movementSpeed = GetComponent<IA>().IAStats.SprintSpeed;
        }

        public void Travel(Vector3 direction)
        {
            _animator.SetFloat("Vertical", 1f);
            Debug.Log("speed " + _movementSpeed);
            transform.Translate(direction * (Time.deltaTime * MovementSpeed));
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
            // Only jump if the player is on the ground
            if (Physics.Raycast(transform.position, -Vector3.up, 100.0f, 1 << GroundLayer))
            {
                Debug.Log("JUMP - VAN HALEN");
               // _rigidbody.AddForce(Vector3.up * JumpStrength, ForceMode.Impulse);
            }
        }

        public void Sprint(bool isSprinting)
        {
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