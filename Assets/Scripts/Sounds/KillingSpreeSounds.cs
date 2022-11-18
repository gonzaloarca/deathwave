using System;
using Managers;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Sounds
{
    [RequireComponent(typeof(AudioSource))]
    public class KillingSpreeSounds : MonoBehaviour, IListenable
    {
        public AudioSource AudioSource => _audioSource;
        private AudioSource _audioSource;  
        [SerializeField] private AudioClip _firstBlood;
        [SerializeField] private AudioClip _headshot;
        [SerializeField] private AudioClip _monsterkill;
        [SerializeField] private AudioClip _doublekill;
        
        private int _totalKills = 0;
        private int _killingSpree = 0;
        private float _lastKill = 0;

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

        public void OnEnemyDeath(){
            if(_totalKills == 0) PlayFirstBlood();
            _totalKills++;
            _killingSpree++;
            _lastKill = Time.time;
            if( _killingSpree == 2){
                PlayDoubleKill();
            }
            if(_killingSpree > 4){
                PlayMonsterkill();
            }
        }

        public void OnHeadShot(){
            PlayHeadshot();
        }
        private void Update(){
            if( Time.time - _lastKill > 2.5f ){
                _killingSpree = 0;
            }
        }

        private void Start()
        {
            InitAudioSource();
            EventsManager.Instance.OnEnemyDeath += OnEnemyDeath;
            EventsManager.Instance.OnHeadshot += PlayHeadshot;
        }
        
        private void PlayDoubleKill()
        {

            _audioSource.PlayOneShot(_doublekill, 1.2f);
        }

        private void PlayHeadshot(){
            _audioSource.PlayOneShot(_headshot , 0.6f);
        }
        private void PlayMonsterkill(){
            _audioSource.PlayOneShot(_monsterkill, 1.2f);
        }

        private void PlayFirstBlood(){
            _audioSource.PlayOneShot(_firstBlood , 1.2f);
        }

        

    }
}