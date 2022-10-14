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
        
        void Start()
        {
            EventsManager.Instance.OnGameOver += OnGameOver;
        }

        private void OnGameOver(bool isVictory)
        {
            _isGameOver = true;
            _isVictory = isVictory;

            _gameoverMessage.text = isVictory ? "VICTORIA!" : "DERROTA!";
            _gameoverMessage.color = isVictory ? Color.cyan : Color.red;
        }
    }
}