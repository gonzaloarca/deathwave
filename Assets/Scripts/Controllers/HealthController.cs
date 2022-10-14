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

        private int _layer;

        private void Start()
        {
            _currentHealth = MaxHealth;
            _layer = gameObject.layer;

            UI_UpdateHealth();
        }

        private void UI_UpdateHealth()
        {
            if (_layer == LayerMask.NameToLayer("Player"))
            {
                EventsManager.Instance.EventPlayerHealthChange(Mathf.Clamp(_currentHealth, 0, MaxHealth), MaxHealth);
            }
        }

        public void TakeDamage(float damage)
        {
            _currentHealth -= damage;

            UI_UpdateHealth();

            if (_currentHealth <= 0) Die();
        }

        public void Die() => Destroy(this.gameObject);

        private void OnDestroy()
        {
            //EventsManager.Instance.EventGameOver(false);
        }
    }
}