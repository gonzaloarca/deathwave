using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Flyweight
{
    [CreateAssetMenu(fileName = "ActorStats", menuName = "Stats/Actor", order = 0)]
    public class ActorStats : ScriptableObject
    {
        [SerializeField] private ActorStatValues _statsValues;

        public int MaxLife => _statsValues.MaxLife;
        public float MovementSpeed => _statsValues.MovementSpeed;
        public float RotationSpeed => _statsValues.RotationSpeed;
        public float MaxVerticalRotation => _statsValues.MaxVerticalRotation;
        public float MinVerticalRotation => _statsValues.MinVerticalRotation;
    }

    [System.Serializable]
    public struct ActorStatValues
    {
        public int MaxLife;
        public float MovementSpeed;
        public float RotationSpeed;
        public float MaxVerticalRotation;
        public float MinVerticalRotation;
    }
}