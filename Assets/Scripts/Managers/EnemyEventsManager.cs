using System;
using UnityEngine;

namespace Managers
{
    public class EnemyEventsManager : MonoBehaviour
    {
        public static EnemyEventsManager Instance;

        private void Awake()
        {
            if (Instance != null) Destroy(this);
            Instance = this;
        }

        public event Action<bool> OnGameOver;

        public void EventGameOver(bool isVictory)
        {
            OnGameOver?.Invoke(isVictory);
        }
    }
}