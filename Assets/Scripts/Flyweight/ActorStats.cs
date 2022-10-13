using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Flyweight
{
    [CreateAssetMenu(fileName = "ActorStats", menuName = "Stats/Actor", order = 0)]
    public class ActorStats : ScriptableObject
    {
        [SerializeField] private ActorStatValues _statsValues;

        public float MaxHealth => _statsValues.MaxHealth;
        public float WalkSpeed => _statsValues.WalkSpeed;
        public float SprintSpeed => _statsValues.SprintSpeed;
        public float JumpStrength => _statsValues.JumpStrength;
        public float MeleeRange => _statsValues.MeleeRange;
        public float RotationSpeed => _statsValues.RotationSpeed;
    }

    [System.Serializable]
    public struct ActorStatValues
    {
        public float MaxHealth;
        public float WalkSpeed;
        public float SprintSpeed;
        public float JumpStrength;
        public float MeleeRange;
        public float RotationSpeed;
    }
}