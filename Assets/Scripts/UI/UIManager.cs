using Managers;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace UI
{
    public class UIManager : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI _ammo;
        [SerializeField] private TextMeshProUGUI _score;
        [SerializeField] private TextMeshProUGUI _timer;
    
        [SerializeField] private Image _health;
        [SerializeField] private Image _guns;
    
        private void OnAmmoChange(int ammo, int maxAmmo)
        {
            _ammo.text = $"{ammo}/{maxAmmo}";
        }
        
        private void OnScoreChange(int score)
        {
            _score.text = $"{score}";
        }
        
        private void OnPlayerHealthChange(float health, float maxHealth)
        {
            _health.fillAmount = health / maxHealth;
        }
        
        private void OnSecondPassed(float time)
        {
            float minutes = Mathf.Floor(time / 60);
            float seconds = Mathf.Floor(time % 60);
            
            _timer.text = $"{minutes:00}:{seconds:00}";
        }
    
        void Start()
        {
            EventsManager.Instance.OnAmmoChange += OnAmmoChange;
            EventsManager.Instance.OnScoreChange += OnScoreChange;
            EventsManager.Instance.OnPlayerHealthChange += OnPlayerHealthChange;
            EventsManager.Instance.OnSecondPassed += OnSecondPassed;
        }

    }
}
