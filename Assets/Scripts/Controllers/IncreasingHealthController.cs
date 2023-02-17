using System;
using Strategy;
using UnityEngine;
using Entities;
using Managers;

namespace Controllers
{
    [RequireComponent(typeof(Actor))]
    public class IncreasingHealthController : HealthController, IDamageable
    {
   
        [SerializeField] private float _currentMaxHealth = 0;
      

        protected virtual void Start()
        {
            _currentMaxHealth = MaxHealth + MaxHealth *0.1f * EventsManager.Instance.Round;    
            _currentHealth = _currentMaxHealth;
        }

        public virtual void Heal(float amount)
        {   
          
            _currentHealth += amount;
            if (_currentHealth >  _currentMaxHealth) _currentHealth =  _currentMaxHealth;
        }

        
    }
}