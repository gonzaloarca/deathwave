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
            else if( OnSlope()){
                _rigidbody.drag = _groundDrag;
            }else
            {
                _rigidbody.drag = 0;
            }
        }

        private void Start()
        {
           // Debug.Log("READY TO JUMP SET TO TRUE");
            _readyToJump = true;
            _rigidbody = GetComponent<Rigidbody>();
            _movementSpeed = GetComponent<Actor>().ActorStats.WalkSpeed;
        }
        private Vector3 moveDirection;

        public void Travel(Vector3 direction)
        {
            var transform1 = transform;
            moveDirection = transform1.forward * direction.z + transform1.right * direction.x;
            var movementForce = moveDirection * (MovementSpeed * SpeedModifier * Time.deltaTime * 1000);

            if (!_grounded && !OnSlope())
            {
              //  Debug.Log("AIR MODIFIER");
                movementForce *= _airModifier;
            }

              

            _rigidbody.AddForce(movementForce, ForceMode.Force);
           // Debug.Log(_rigidbody.velocity.magnitude);

            var velocity = _rigidbody.velocity;
            velocity.y = 0;

            if (OnSlope() && !exitingSlope)
            {
                _rigidbody.AddForce(GetSlopeMoveDirection() * _movementSpeed *10f, ForceMode.Force);

                if (_rigidbody.velocity.y > 0)
                    _rigidbody.AddForce(Vector3.down * 50f, ForceMode.Force);
            }

            SpeedControl();
            // velocity check
            if (velocity.magnitude > MovementSpeed)
            {
                //Debug.Log("SPEED THROTTLING");
                var clampedVelocity = velocity.normalized * MovementSpeed;
                clampedVelocity.y = _rigidbody.velocity.y;
                _rigidbody.velocity = clampedVelocity;
            }

            _rigidbody.useGravity = !OnSlope();
               
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
             exitingSlope = true;
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
            exitingSlope = false;
        }



        [Header("Slope Handling")]
        public float maxSlopeAngle;
        private RaycastHit slopeHit;
        private bool exitingSlope;
        private bool wasSlope = false;
         private bool OnSlope()
        {
            if(Physics.Raycast(transform.position, Vector3.down, out slopeHit, 2f * 0.5f + 0.3f))
            {
                float angle = Vector3.Angle(Vector3.up, slopeHit.normal);
                return angle < maxSlopeAngle && angle != 0;
            }

            return false;
        }

        private Vector3 GetSlopeMoveDirection()
        {
            return Vector3.ProjectOnPlane(moveDirection, slopeHit.normal).normalized;
        }

            private void SpeedControl()
        {
            // limiting speed on slope
            if (OnSlope() && !exitingSlope)
            {
                if (_rigidbody.velocity.magnitude >  _movementSpeed)
                    _rigidbody.velocity = _rigidbody.velocity.normalized *  _movementSpeed;
            }

            // limiting speed on ground or in air
            else
            {
                Vector3 flatVel = new Vector3(_rigidbody.velocity.x, 0f, _rigidbody.velocity.z);

                // limit velocity if needed
                if (flatVel.magnitude >  _movementSpeed)
                {
                    Vector3 limitedVel = flatVel.normalized *  _movementSpeed;
                    _rigidbody.velocity = new Vector3(limitedVel.x, _rigidbody.velocity.y, limitedVel.z);
                }
            }
    }
    }
}