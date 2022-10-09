using System;
using Entities;
using Strategy;
using UnityEngine;

namespace Controllers
{
    [RequireComponent(typeof(Actor))]
    public class MovementController : MonoBehaviour, IMovable
    {
        public float MovementSpeed => GetComponent<Actor>().ActorStats.MovementSpeed;
        public float MouseXSensitivity => GetComponent<Actor>().ActorStats.MouseXSensitivity;
        public float MouseYSensitivity => GetComponent<Actor>().ActorStats.MouseYSensitivity;


        public float MaxVerticalRotation => GetComponent<Actor>().ActorStats.MaxVerticalRotation;
        public float MinVerticalRotation => GetComponent<Actor>().ActorStats.MinVerticalRotation;

        public Transform _torso;

        private float _verticalRotation = 0f;

        private void Start()
        {
            // _torso = transform.Find("Spine_01");
        }

        public void Travel(Vector3 direction)
        {
            transform.Translate(direction * (Time.deltaTime * MovementSpeed));
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
    }
}