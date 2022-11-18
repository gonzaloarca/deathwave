using Controllers;
using UnityEngine;
using UnityEngine.UI;

namespace Managers
{
    public class GameManager : MonoBehaviour
    {
        [SerializeField] private bool _isGameOver = false;
        [SerializeField] private bool _isVictory = false;
        [SerializeField] private Sprite _image;
        private int score = 0;

        void Start()
        {
            EventsManager.Instance.OnEnemyDeath += OnEnemyDeath;
            EventsManager.Instance.OnGameOver += OnGameOver;
        }

        private void OnGameOver(bool isVictory)
        {
            _isGameOver = true;
            _isVictory = isVictory;
            Image image = GameObject.FindWithTag("GameOverText")?.GetComponent<Image>();
            if (image)
                image.sprite = _image;
        }

        void OnEnemyDeath()
        {
            score += 100;
            EventsManager.Instance.EventScoreChange(score);
        }
    }
}