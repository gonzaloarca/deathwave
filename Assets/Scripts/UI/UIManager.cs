using Managers;
using UnityEngine;
using UnityEngine.UI;

namespace UI
{
    public class UIManager : MonoBehaviour
    {
        [SerializeField] private Text _ammo;
        [SerializeField] private Text _score;
        [SerializeField] private Text _round;
    
        [SerializeField] private Image _health;
        [SerializeField] private Image _guns;
    
        private void OnAmmoChange(int ammo, int maxAmmo)
        {
            _ammo.text = $"{ammo}/{maxAmmo}";
        }
        
        private void OnScoreChange(int score)
        {
            _score.text = $"${score}";
        }
    
        void Start()
        {
            EventsManager.Instance.OnAmmoChange += OnAmmoChange;
            EventsManager.Instance.OnScoreChange += OnScoreChange;
        }

    }
}
