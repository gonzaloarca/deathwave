using System;
using UnityEngine;

namespace Managers
{
    public class EventsManager : MonoBehaviour
    {
        public static EventsManager Instance;

        private void Awake()
        {
            if (Instance != null) Destroy(this);
            Instance = this;
        }
        private bool _isGameOver;
        public bool IsGameOver(){
            return _isGameOver;
        }
        public event Action<bool> OnGameOver;
        public event Action OnPlayerDamage;
        public event Action OnGunShot;
        public event Action<int, int> OnAmmoChange;
        public event Action<int> OnScoreChange;
        public event Action<float, float> OnPlayerHealthChange;
        public event Action OnGunReloadStart;
        public event Action OnGunReloadEnd;
        public event Action OnEmptyMag;
        public event Action OnAmmoPickup;
        // public event Action<float> OnSecondPassed;
        public event Action OnEnemyDeath;
        public event Action<float> OnHealthPickup;
        public event Action<int> OnRoundChange;
        public void EventGameOver(bool isVictory)
        {
            _isGameOver = true;
            OnGameOver?.Invoke(isVictory);
        }
        
        public void EventPlayerDamage()
        {
            OnPlayerDamage?.Invoke();
        }
        
        public void EventGunShot()
        {
            OnGunShot?.Invoke();
        }
        
        public void EventGunReloadStart()
        {
            OnGunReloadStart?.Invoke();
        }
        
        public void EventGunReloadEnd()
        {
            OnGunReloadEnd?.Invoke();
        }

        public void EventEmptyMag(){
            OnEmptyMag?.Invoke();
        }
        
        public void EventAmmoChange(int ammo, int maxAmmo)
        {
            OnAmmoChange?.Invoke(ammo, maxAmmo);
        }
        
        public void EventScoreChange(int score)
        {
            OnScoreChange?.Invoke(score);
        }
        
        public void EventPlayerHealthChange(float health, float maxHealth)
        {
            OnPlayerHealthChange?.Invoke(health, maxHealth);
        }

        public void EventAmmoPickup(){
            OnAmmoPickup?.Invoke();
        }
        
        // public void EventSecondPassed(float time)
        // {
        //     OnSecondPassed?.Invoke(time);
        // }
        public void EventEnemyDeath()
        {
            OnEnemyDeath?.Invoke();
        }
        public void EventHealthPickup(float healthPoints){
            OnHealthPickup?.Invoke(healthPoints);
        }
        public void EventRoundChange(int newRound)
        {
            OnRoundChange?.Invoke(newRound);
        }
    }
}