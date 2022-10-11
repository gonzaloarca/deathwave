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

        public float JumpStrength => GetComponent<Actor>().ActorStats.JumpStrength;

        public int GroundLayer => LayerMask.NameToLayer($"Ground");

        private Rigidbody _rigidbody;

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
                Debug.Log("JUMP - VAN HALEN");
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