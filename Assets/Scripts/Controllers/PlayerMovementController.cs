using System;
using Entities;
using Strategy;
using UnityEngine;

namespace Controllers
{
    [RequireComponent(typeof(Actor))]
    public class PlayerMovementController : MonoBehaviour, IMovable
    {
        public float MovementSpeed => _movementSpeed;
        private float _movementSpeed;

        public float SpeedModifier => _speedModifier;
        [SerializeField] private float _speedModifier = 1f;

        [SerializeField] private float _maxX=0;
        [SerializeField] private float _maxZ=0;
        [SerializeField] private float _minX=0;
        [SerializeField] private float _minZ=0;

        public float JumpStrength => GetComponent<Actor>().ActorStats.JumpStrength;

        public int GroundLayer => LayerMask.NameToLayer($"Ground");

        private Rigidbody _rigidbody;

        private void Update(){
            //World boundaries
            Vector3 newPos = transform.position;
            if(transform.position.x > _maxX){
                newPos.x = _maxX;
            }
            if(transform.position.x < _minX){
                newPos.x = _minX;
            }
            if(transform.position.z > _maxZ){
                newPos.z = _maxZ;
            }
            if(transform.position.z < _minZ){
                newPos.z = _minZ;
            }
        
            transform.position = newPos;
        }

        private void Start()
        {
            _rigidbody = GetComponent<Rigidbody>();
            _movementSpeed = GetComponent<Actor>().ActorStats.WalkSpeed;
        }

        public void Travel(Vector3 direction)
        {
            transform.Translate(direction * (Time.deltaTime * MovementSpeed * SpeedModifier));
        }

        public void Rotate(Vector3 direction)
        {
            return;
        }

        public void Jump()
        {
            // Only jump if the player is on the ground
            if (Physics.Raycast(transform.position, -Vector3.up, 0.01f, 1 << GroundLayer))
            {
             
                _rigidbody.AddForce(Vector3.up * JumpStrength, ForceMode.Impulse);
            }
        }

        public void Sprint(bool isSprinting)
        {
            if (isSprinting)
            {
                _movementSpeed = GetComponent<Actor>().ActorStats.SprintSpeed;
            }
            else
            {
                _movementSpeed = GetComponent<Actor>().ActorStats.WalkSpeed;
            }
        }

        public void SetSpeedModifier(float amount)
        {
            _speedModifier = amount;
        }
    }
}