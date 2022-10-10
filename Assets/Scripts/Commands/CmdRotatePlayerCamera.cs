using Controllers;
using UnityEngine;

namespace Commands
{
    public class CmdRotatePlayerCamera : ICommand
    {
        private PlayerCameraController _playerCameraController;
        private Vector3 _direction;

        public CmdRotatePlayerCamera(PlayerCameraController playerCameraController, Vector3 direction)
        {
            _playerCameraController = playerCameraController;
            _direction = direction;
        }

        public void Execute()
        {
            _playerCameraController.Rotate(_direction);
        }
    }
}