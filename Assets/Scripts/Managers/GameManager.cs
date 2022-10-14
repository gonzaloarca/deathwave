using Controllers;
using UnityEngine;
using UnityEngine.UI;

namespace Managers
{
    public class GameManager : MonoBehaviour
    {
        [SerializeField] private bool _isGameOver = false;
        [SerializeField] private bool _isVictory = false;
        [SerializeField] private Text _gameoverMessage;
        [SerializeField] private float _gameTime;

        void Start()
        {
            EventsManager.Instance.OnGameOver += OnGameOver;

            _gameTime = 0;
        }

        private void OnGameOver(bool isVictory)
        {
            _isGameOver = true;
            _isVictory = isVictory;

            _gameoverMessage.text = isVictory ? "VICTORIA!" : "DERROTA!";
            _gameoverMessage.color = isVictory ? Color.cyan : Color.red;
        }

        void Update()
        {
            var prevTime = _gameTime;
            if (!_isGameOver) _gameTime += Time.deltaTime;
            
            // if a second has passed, update the UI
            if (Mathf.FloorToInt(prevTime) != Mathf.FloorToInt(_gameTime))
            {
                EventsManager.Instance.EventSecondPassed(_gameTime);
            }
        }
    }
}