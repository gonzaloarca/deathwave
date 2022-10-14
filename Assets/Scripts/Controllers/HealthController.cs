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

        [SerializeField] protected float _currentHealth;


        protected virtual void Start()
        {
            _currentHealth = MaxHealth;
        }

        public virtual void TakeDamage(float damage)
        {
            if(_currentHealth <= 0 ) return;
            _currentHealth -= damage;

            if (_currentHealth <= 0) Die();
        }

        public virtual void Heal(float amount)
        {   
          
            _currentHealth += amount;
            if (_currentHealth > MaxHealth) _currentHealth = MaxHealth;
        }

        public virtual void Die() { 
            Destroy(this.gameObject);
        }

        private void OnDestroy()
        {
            //EventsManager.Instance.EventGameOver(false);
        }
    }
}