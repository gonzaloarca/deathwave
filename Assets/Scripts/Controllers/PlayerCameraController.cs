using Managers;
using UnityEngine;

namespace Controllers
{
    public class PlayerCameraController : MonoBehaviour
    {
        // CAMERA ROTATION
        public float XMouseSensitivity => _xMouseSensitivity;
        [SerializeField] private float _xMouseSensitivity = 100f;
        public float YMouseSensitivity => _yMouseSensitivity;
        [SerializeField] private float _yMouseSensitivity = 100f;
        public float VerticalClampAngle => _verticalClampAngle;
        [SerializeField] private float _verticalClampAngle = 80f;

        private float _verticalRotation = 0f;

        public Transform VerticalTransform => _verticalTransform;
        [SerializeField] private Transform _verticalTransform;

        public void Start()
        {
            Cursor.lockState = CursorLockMode.Locked;
        }

        public void Update()
        {
            if (PauseManager.GameIsPaused)
            {
                Cursor.lockState = CursorLockMode.None;
            }
            else
            {
                Cursor.lockState = CursorLockMode.Locked;
            }
        }

        public void Rotate(Vector3 direction)
        {
            // Chracter Prefab Hips Axis are flipped, therefore we use the z-axis for torso vertical rotation,
            // and the y-axis for the player horizontal rotation
            var mouseX = direction.x * XMouseSensitivity * Time.deltaTime;
            var mouseY = direction.y * YMouseSensitivity * Time.deltaTime;

            var horizontalRotation = new Vector3(0, mouseX, 0);

            transform.Rotate(horizontalRotation);

            _verticalRotation -= mouseY;
            _verticalRotation = Mathf.Clamp(_verticalRotation, -VerticalClampAngle, VerticalClampAngle);

            VerticalTransform.localEulerAngles = new Vector3(-180, 0, _verticalRotation);
        }
    }
}