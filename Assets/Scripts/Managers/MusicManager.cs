using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using Managers;

namespace Managers
{
    public class MusicManager : MonoBehaviour
    {

        private int _currentSong = 0;
        private AudioClip[] _playlist;
        [SerializeField] private AudioClip[] _songs;
        [SerializeField] private AudioClip _firstTrack;
        private AudioSource _source;
        // Start is called before the first frame update
        void Start()
        {
            _source = GameObject.FindWithTag("GameMusic")?.GetComponent<AudioSource>();
            _playlist = _songs;
            EventsManager.Instance.OnGameOver += OnGameOver;
        }
        
        private bool _active = false;
        public void Activate(){
            _source.loop = false;
            _active = true;
        }
        public void Deactivate(){
            _source.loop = true;
            _active = false;
            _source.Stop();
        }
        // Update is called once per frame
        void Update()
        {
            if(_active == false) return;

            if (Input.GetKeyDown(KeyCode.M))
            {
                NextSong();
                return;
            }
            if(_source.isPlaying == false){
                NextSong();
            }
        }

        private void NextSong(){
            if(!_active) return;
            _currentSong++;
            _source.clip = _playlist[(_currentSong ) % _playlist.Length];
            _source.Play();
        }

        public void OnGameOver(bool isVictory)
        {
          Deactivate();
        }

    }
}