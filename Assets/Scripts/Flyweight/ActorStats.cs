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
        public float WalkSpeed => _statsValues.WalkSpeed;
        public float SprintSpeed => _statsValues.SprintSpeed;
        public float MouseXSensitivity => _statsValues.MouseXSensitivity;
        public float MouseYSensitivity => _statsValues.MouseYSensitivity;
        public float MaxVerticalRotation => _statsValues.MaxVerticalRotation;
        public float MinVerticalRotation => _statsValues.MinVerticalRotation;

        public float JumpStrength => _statsValues.JumpStrength;
    }

    [System.Serializable]
    public struct ActorStatValues
    {
        public int MaxLife;
        public float WalkSpeed;
        public float SprintSpeed;
        public float MouseXSensitivity;
        public float MouseYSensitivity;
        public float MaxVerticalRotation;
        public float MinVerticalRotation;
        public float JumpStrength;
    }
}