using System.Collections.Generic;
using Managers;
using UnityEngine;

namespace Sounds
{
    [RequireComponent(typeof(AudioSource))]
    public class PlayerSoundController : MonoBehaviour, IListenable
    {
        [SerializeField] private AudioClip[] _damageClips;
        
        public AudioSource AudioSource => _audioSource;
        private AudioSource _audioSource;
        
        public void InitAudioSource()
        {
            _audioSource = GetComponent<AudioSource>();
        }

        public void PlayOneShot(AudioClip clip)
        {
            AudioSource.PlayOneShot(clip);
        }

        public void Stop()
        {
            AudioSource.Stop();
        }

        public void OnPlayerDamage()
        {
            AudioClip clip = _damageClips[Random.Range(0 , _damageClips.Length)];
            AudioSource.PlayOneShot(clip);
        }

        public void Start()
        {
            InitAudioSource();
            EventsManager.Instance.OnPlayerDamage += OnPlayerDamage;
        }

        
    }
}