using System;
using UnityEngine;

namespace Managers
{
    public class LevelSoundManager : MonoBehaviour
    {
        [SerializeField] private AudioClip _roundStartSound;
        [SerializeField] private AudioClip  _dropPickupSound;
        [SerializeField] private AudioSource _source;

        private void Start()
        {
            EventsManager.Instance.OnRoundChange += OnRoundChange;
            EventsManager.Instance.OnAmmoPickup += OnAmmoPickup;
            EventsManager.Instance.OnHealthPickup += OnHealthPickup;
        }

        void OnRoundChange(int round)
        {
            _source.PlayOneShot(_roundStartSound);
        }
        
        void OnHealthPickup(float health)
        {
            _source.PlayOneShot(_dropPickupSound);
        } 
        
        void OnAmmoPickup()
        {
            _source.PlayOneShot(_dropPickupSound);
        }
    }
}