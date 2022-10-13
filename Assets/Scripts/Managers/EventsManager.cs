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

        public event Action<bool> OnGameOver;
        public event Action OnPlayerDamage;
        public event Action OnGunShot;
        public void EventGameOver(bool isVictory)
        {
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
    }
}