using Flyweight;
using UnityEngine;

namespace Entities
{
    public class Projectile : MonoBehaviour
    {
        public BulletStats BulletStats => bulletStats;
        [SerializeField] private BulletStats bulletStats;

        public int Level => _level;
        [SerializeField] private int _level = 1;
        public void SetLevel(int level){
            _level = level;
        }
    }
}