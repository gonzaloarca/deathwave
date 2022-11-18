using System.Collections.Generic;
using Managers;
using UnityEngine;

namespace Sounds
{
    [RequireComponent(typeof(AudioSource))]
    public class DroneSoundController : MonoBehaviour, IListenable
    {

        [SerializeField] private AudioClip _gunsDraw;
        [SerializeField] private AudioClip[] _gunShotSounds; 
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

        public void Shoot()
        {
            _audioSource.PlayOneShot(_gunShotSounds[Random.Range(0, _gunShotSounds.Length)]);
        }

        
        
        public void GunsDraw()
        {
           PlayOneShot(_gunsDraw);
        }

        public void Start()
        {
            InitAudioSource();
        }

        
    }
}