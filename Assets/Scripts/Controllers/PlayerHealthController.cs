using Managers;
using UnityEngine;

namespace Controllers
{
    public class PlayerHealthController : HealthController
    {
        [SerializeField] private float _healCooldown = 2f;
        [SerializeField] private float _healSpeed = 1f;

        private float _nextHealTime;
        private float _nextDamageTime;
        private void UI_UpdateHealth()
        {
            EventsManager.Instance.EventPlayerHealthChange(Mathf.Clamp(_currentHealth, 0, MaxHealth), MaxHealth);
        }

        public override void TakeDamage(float damage)
        {
            if(_nextDamageTime > Time.time) return;
            EventsManager.Instance.EventPlayerDamage();
            base.TakeDamage(damage);
            
            _nextHealTime = Time.time + _healCooldown;
            _nextDamageTime = Time.time + 0.1f;
            
            UI_UpdateHealth();
            // Do something else
        }

        protected override void Start()
        {
            _nextDamageTime = 0;
            base.Start();
            EventsManager.Instance.OnHealthPickup += Heal;
            UI_UpdateHealth();
        }

        public override void Die()
        {
            EventsManager.Instance.EventGameOver(false);
        }

        public override void Heal(float amount)
        {
            base.Heal(amount);
            UI_UpdateHealth();
        }

    

        private void Update()
        {
            if (Time.time > _nextHealTime && _currentHealth < MaxHealth)
            {
                Heal(_healSpeed * Time.deltaTime);
            }
        }
    }
}