using System;
using Managers;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Sounds
{
    [RequireComponent(typeof(AudioSource))]
    public class GunSoundController : MonoBehaviour, IListenable
    {
        public AudioSource AudioSource => _audioSource;
        [SerializeField] private AudioSource _audioSource;
        [SerializeField] private AudioClip[] _gunShotSounds;
        
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

        private void Start()
        {
            InitAudioSource();
            EventsManager.Instance.OnGunShot += PlayGunShot;
        }
        
        private void PlayGunShot()
        {
            _audioSource.PlayOneShot(_gunShotSounds[Random.Range(0, _gunShotSounds.Length)]);
        }
    }
}