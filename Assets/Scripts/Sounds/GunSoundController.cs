using System;
using Managers;
using UnityEngine;
using Random = UnityEngine.Random;
using Strategy;
namespace Sounds
{
    [RequireComponent(typeof(AudioSource))]
    public class GunSoundController : MonoBehaviour, IListenable
    {
        public AudioSource AudioSource => _audioSource;
        private AudioSource _audioSource;
        [SerializeField] private AudioClip[] _gunShotSounds;  
        [SerializeField] private AudioClip _reloadSoundStart;
        [SerializeField] private AudioClip _reloadSoundEnd;
        [SerializeField] private AudioClip _emptyMagSound;
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
            EventsManager.Instance.OnGunReloadStart += PlayReloadStart;
            EventsManager.Instance.OnGunReloadEnd += PlayReloadEnd;
            EventsManager.Instance.OnEmptyMag += onEmptyMag;
        }
        
        private void PlayGunShot()
        {
            _audioSource.PlayOneShot(_gunShotSounds[Random.Range(0, _gunShotSounds.Length)]);
        }

        private void PlayReloadStart(){
            _audioSource.PlayOneShot(_reloadSoundStart);
        }
        private void PlayReloadEnd(){
            _audioSource.PlayOneShot(_reloadSoundEnd);
        }

        private void onEmptyMag(){
            _audioSource.PlayOneShot(_emptyMagSound);
        }
    }
}