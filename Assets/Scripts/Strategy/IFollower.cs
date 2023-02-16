using UnityEngine;

namespace Strategy
{
    public interface IFollower : IMovable
    {
       
        void LookAt(Vector3 direction);
        void Attack(Vector3 objective);

        void Warp(Vector3 position);

        bool IsEnabled();
    }
}
