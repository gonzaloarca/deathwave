using UnityEngine;

namespace Strategy
{
    public interface IFollower : IMovable
    {
       
        void LookAt(Vector3 direction);
        void Attack(Vector3 objective);
    }
}
