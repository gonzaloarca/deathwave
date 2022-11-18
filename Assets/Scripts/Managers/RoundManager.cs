using Controllers;
using UnityEngine;
using UnityEngine.UI;

namespace Managers
{
    [RequireComponent(typeof(SpawnManager))]
    public class RoundManager : MonoBehaviour
    {
        [SerializeField] private int _maxRounds = 2;
        [SerializeField] private bool _isVictory = false;
        [SerializeField] private Sprite _image;

        public int _currentRound;
        private int _maxEnemies = 5;
        private int _roundKills = 0;
        private SpawnManager spawnManager;
     
        void Start()
        {
            spawnManager = gameObject.GetComponent<SpawnManager>();
            _currentRound = 1;
            EventsManager.Instance.EventRoundChange(_currentRound);
            EventsManager.Instance.OnEnemyDeath += OnEnemyDeath;
            spawnManager.Reset(_maxEnemies);
        }


        void OnEnemyDeath()
        {
             if (_currentRound > _maxRounds)
            {
                return;
            }
            _roundKills += 1;

            if (_roundKills >= _maxEnemies)
            {
                _maxEnemies = (int)(_maxEnemies * 1.25);
                _roundKills = 0;
                _currentRound++;

                if (_currentRound > _maxRounds)
                {
                    EventsManager.Instance.EventGameOver(true);
                    return;
                }


                EventsManager.Instance.EventRoundChange(_currentRound);
                spawnManager.Reset(_maxEnemies);
            }
        }
    }
}