using UnityEngine;

namespace Strategy
{
    public interface IMovable
    {
        float MovementSpeed { get; }
        float RotationSpeed { get; }
    
        void Travel(Vector3 direction);
        void Rotate(Vector3 direction);
    }
}
