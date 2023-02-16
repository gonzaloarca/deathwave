using System;
using Managers;
using Strategy;
using UnityEngine;

namespace Controllers
{
    public class PlayerScoreController : MonoBehaviour, IScore
    {
        private const int MaxScore = 999999;

        public int Score => _score;
        [SerializeField] private int _score;

        private void UI_UpdateScore()
        {
            EventsManager.Instance.EventScoreChange(_score);
        }

        public void AddScore(int score)
        {
            _score = Mathf.Clamp(_score + score, 0, MaxScore);
            UI_UpdateScore();
        }

        public void ResetScore()
        {
            _score = 0;
            UI_UpdateScore();
        }

        public void SubtractScore(int cost)
        {
            _score = Mathf.Clamp(_score - cost, 0, MaxScore);
            UI_UpdateScore();
        }

        public bool CanSubtract(int cost) => _score - cost >= 0;

        private void Start()
        {
            ResetScore();
        }
    }
}