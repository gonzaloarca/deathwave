using System;
using Entities;
using Strategy;
using UnityEngine;

namespace Controllers
{
    [RequireComponent(typeof(Actor))]
    public class PlayerMovementController : MonoBehaviour, IMovable
    {
        [SerializeField] private float _playerHeight;
        public float MovementSpeed => _movementSpeed;
        private float _movementSpeed;

        [SerializeField] private float _groundDrag;
        private bool _grounded;
        private bool _readyToJump;
        [SerializeField] private float _jumpCooldown;

        [SerializeField] private float _airModifier;

        public float SpeedModifier => _speedModifier;
        [SerializeField] private float _speedModifier = 1f;

        public float JumpStrength => GetComponent<Actor>().ActorStats.JumpStrength;

        public int GroundLayer => LayerMask.NameToLayer($"Ground");

        private Rigidbody _rigidbody;

        private void Update()
        {
            _grounded = Physics.Raycast(transform.position, -Vector3.up, _playerHeight * 0.1f, 1 << GroundLayer);

            if (_grounded)
            {
                _rigidbody.drag = _groundDrag;
            }
            else
            {
                _rigidbody.drag = 0;
            }
        }

        private void Start()
        {
            Debug.Log("READY TO JUMP SET TO TRUE");
            _readyToJump = true;
            _rigidbody = GetComponent<Rigidbody>();
            _movementSpeed = GetComponent<Actor>().ActorStats.WalkSpeed;
        }

        public void Travel(Vector3 direction)
        {
            var transform1 = transform;
            var moveDirection = transform1.forward * direction.z + transform1.right * direction.x;
            var movementForce = moveDirection * (MovementSpeed * SpeedModifier * Time.deltaTime * 1000);

            if (!_grounded)
            {
                Debug.Log("AIR MODIFIER");
                movementForce *= _airModifier;
            }


            _rigidbody.AddForce(movementForce, ForceMode.Force);
            Debug.Log(_rigidbody.velocity.magnitude);

            var velocity = _rigidbody.velocity;
            velocity.y = 0;

            // velocity check
            if (velocity.magnitude > MovementSpeed)
            {
                Debug.Log("SPEED THROTTLING");
                var clampedVelocity = velocity.normalized * MovementSpeed;
                clampedVelocity.y = _rigidbody.velocity.y;
                _rigidbody.velocity = clampedVelocity;
            }
        }

        public void Rotate(Vector3 direction)
        {
            return;
        }

        public void Jump()
        {
            if (!_readyToJump || !_grounded) return;

            var velocity = _rigidbody.velocity;
            velocity = new Vector3(velocity.x, 0f, velocity.z);
            _rigidbody.velocity = velocity;

            // Only jump if the player is on the ground
            _readyToJump = false;

            _rigidbody.AddForce(Vector3.up * JumpStrength, ForceMode.Impulse);

            Invoke(nameof(ResetJump), _jumpCooldown);
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

        public void ResetJump()
        {
            _readyToJump = true;
        }
    }
}