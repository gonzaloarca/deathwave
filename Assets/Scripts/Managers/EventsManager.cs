using System;
using UnityEngine;
using Strategy;
namespace Managers
{
    public class EventsManager : MonoBehaviour
    {
        public static EventsManager Instance;
        public int Round = 0;
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
        public event Action OnHeadshot;
        public event Action OnAmmoPickup;
        // public event Action<float> OnSecondPassed;
        public event Action<int> OnEnemyDeath;
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
         
        public void EventHeadshot()
        {
            OnHeadshot?.Invoke();
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
        public void EventEnemyDeath(int score)
        {
            OnEnemyDeath?.Invoke(score);
        }
        public void EventHealthPickup(float healthPoints){
            OnHealthPickup?.Invoke(healthPoints);
        }
        public void EventRoundChange(int newRound)
        {   
            Round = newRound;
            OnRoundChange?.Invoke(newRound);
        }


        
         public event Action<bool> OnPooling;
            public void EventPooling(bool start)
        {   
            OnPooling?.Invoke(start);
        }

        public event Action<string> OnGunNameChange;
        public void EventGunNameChange(string name){
            OnGunNameChange?.Invoke(name);
        }
        public event Action OnGunChange;
        public void EventGunChange(){
            OnGunChange?.Invoke();
        }
    }
}