using System;
using UnityEngine;

namespace Managers
{
    public class EnemyEventsManager : MonoBehaviour
    {
        public static EnemyEventsManager Instance;

        public float time => _time;
        [SerializeField] float _time = 60;

        private void Awake()
        {
            if (Instance != null) Destroy(this);
            Instance = this;
        }

        private void Update(){
            _time -= Time.deltaTime;
            if(_time == 0)
                this.EventGameOver(true);
        }

        public event Action<bool> OnGameOver;

        public void EventGameOver(bool isVictory)
        {
            OnGameOver?.Invoke(isVictory);
        }
    }
}