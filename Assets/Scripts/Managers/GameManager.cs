using Controllers;
using UnityEngine;
using UnityEngine.UI;

namespace Managers
{
    public class GameManager : MonoBehaviour
    {
        private const float GameTime = 60f;
        [SerializeField] private bool _isGameOver = false;
        [SerializeField] private bool _isVictory = false;
        [SerializeField] private Sprite _image;
        [SerializeField] private float _gameDuration = GameTime;

        void Start()
        {
            EventsManager.Instance.OnGameOver += OnGameOver;
        }

        private void OnGameOver(bool isVictory)
        {
            _isGameOver = true;
            _isVictory = isVictory;
            Image image = GameObject.FindWithTag("GameOverText")?.GetComponent<Image>();
            if(image)
                image.sprite = _image;
        }

        void Update()
        {
            var prevTime = _gameDuration;
            if (!_isGameOver) _gameDuration -= Time.deltaTime;
            
            if (_gameDuration <= 0)
            {
                EventsManager.Instance.EventGameOver(true);
                return;
            }
            
            // if a second has passed, update the UI
            if (Mathf.FloorToInt(prevTime) != Mathf.FloorToInt(_gameDuration))
            {
                EventsManager.Instance.EventSecondPassed(_gameDuration);
            }
            
           
        }
    }
}