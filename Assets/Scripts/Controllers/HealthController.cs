using System;
using Strategy;
using UnityEngine;
using Entities;
using Managers;

namespace Controllers
{
    [RequireComponent(typeof(Actor))]
    public class HealthController : MonoBehaviour, IDamageable
    {
        public int MaxHealth => GetComponent<Actor>().ActorStats.MaxHealth;
        [SerializeField] private int _currentHealth;

        private void Start()
        {
            _currentHealth = MaxHealth;
        }

        public void TakeDamage(int damage)
        {
            _currentHealth -= damage;

            if (_currentHealth <= 0) Die();
        }

        public void Die() => Destroy(this.gameObject);

        private void OnDestroy()
        {
            //EventsManager.Instance.EventGameOver(false);
        }
    }
}