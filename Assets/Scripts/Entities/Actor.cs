using Flyweight;
using UnityEngine;

namespace Entities
{
    public class Actor : MonoBehaviour
    {
        public ActorStats ActorStats => _actorStats;
        [SerializeField] private ActorStats _actorStats;
    }
}