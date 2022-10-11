using Flyweight;
using UnityEngine;

namespace Entities
{
    public class Enemy : Actor
    {
        public EnemyStats EnemyStats => enemyStats;
        [SerializeField] private EnemyStats enemyStats;
    }
}