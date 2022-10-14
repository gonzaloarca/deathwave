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
    
        void Start()
        {
            EventsManager.Instance.OnAmmoChange += OnAmmoChange;
        
        }

        // Update is called once per frame
        void Update()
        {
        
        }
    }
}
