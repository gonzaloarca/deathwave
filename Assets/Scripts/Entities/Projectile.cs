using Flyweight;
using UnityEngine;

namespace Entities
{
    public class Projectile : MonoBehaviour
    {
        public BulletStats BulletStats => bulletStats;
        [SerializeField] private BulletStats bulletStats;
    }
}