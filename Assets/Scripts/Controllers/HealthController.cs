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
        public float MaxHealth => GetComponent<Actor>().ActorStats.MaxHealth;
        
        [SerializeField] private float _currentHealth;

        private void Start()
        {
            _currentHealth = MaxHealth;
        }

        public void TakeDamage(float damage)
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