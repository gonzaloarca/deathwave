using System;
using Entities;
using Strategy;
using UnityEngine;

namespace Controllers
{
    [RequireComponent(typeof(Actor))]
    public class MovementController : MonoBehaviour, IMovable
    {
        public float MovementSpeed => _movementSpeed;
        private float _movementSpeed;
        
        public float SpeedModifier => _speedModifier;
        [SerializeField] private float _speedModifier = 1f;
        public float MouseXSensitivity => GetComponent<Actor>().ActorStats.MouseXSensitivity;
        public float MouseYSensitivity => GetComponent<Actor>().ActorStats.MouseYSensitivity;

        public float JumpStrength => GetComponent<Actor>().ActorStats.JumpStrength;
        public float MaxVerticalRotation => GetComponent<Actor>().ActorStats.MaxVerticalRotation;
        public float MinVerticalRotation => GetComponent<Actor>().ActorStats.MinVerticalRotation;

        public Transform _torso;

        private float _verticalRotation = 0f;
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
            // Chracter Prefab Hips Axis are flipped, therefore we use the z-axis for torso vertical rotation,
            // and the y-axis for the player horizontal rotation
            var mouseX = direction.x * MouseXSensitivity * Time.deltaTime;
            var mouseY = direction.y * MouseYSensitivity * Time.deltaTime;

            var horizontalRotation = new Vector3(0, mouseX, 0);

            transform.Rotate(horizontalRotation);

            _verticalRotation -= mouseY;
            _verticalRotation = Mathf.Clamp(_verticalRotation, MinVerticalRotation, MaxVerticalRotation);
            
            _torso.localEulerAngles = new Vector3(-180, 0, _verticalRotation);
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