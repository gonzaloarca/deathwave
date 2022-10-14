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
        [SerializeField] private TextMeshProUGUI _round;
    
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
    
        void Start()
        {
            EventsManager.Instance.OnAmmoChange += OnAmmoChange;
            EventsManager.Instance.OnScoreChange += OnScoreChange;
            EventsManager.Instance.OnPlayerHealthChange += OnPlayerHealthChange;
        }

    }
}
