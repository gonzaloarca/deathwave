using UnityEngine;

namespace Strategy
{
    public interface IMovable
    {
        float MovementSpeed { get; }
        void Travel(Vector3 direction);
        void Rotate(Vector3 direction);
        void Jump();
        void Sprint(bool isSprinting);
        void SetSpeedModifier(float amount);
    }
}
