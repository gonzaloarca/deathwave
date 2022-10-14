using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using Managers;

namespace Managers{
    public class GameLevelManager : MonoBehaviour
    {
        [SerializeField] private AudioClip _startingLoop;
        [SerializeField] private AudioClip _firstTrack;
        [SerializeField] private AudioSource _source;
        [SerializeField] private Animator _fadeAnimator;
        [SerializeField] private bool _menu;
        [SerializeField] private bool _ending;
        private int _levelToload; 
        // Start is called before the first frame update
        void Start()
        {
            _source = GameObject.FindWithTag("GameMusic")?.GetComponent<AudioSource>();
            if(_menu){
                _source.clip = _startingLoop;
                _source.loop = true;
                _source.Play();
            }
            EventsManager.Instance.OnGameOver += OnGameOver;
        }

        // Update is called once per frame
        void Update()
        {
            if(_menu){
                if(_source.loop && Input.GetKey(KeyCode.Space)){
                _source.loop = false;
                }
                if(_source.isPlaying == false){
                _source.clip = _firstTrack;
                _source.Play();
                FadeToLevel(1);
                }
                return;
            }
            if(_ending){
                if(Input.GetKey(KeyCode.Space)){
                    FadeToLevel(0);
                }
                return;
            }
        }

        public void FadeToLevel(int levelIndex){
            _fadeAnimator.SetTrigger("FadeOut");
            _levelToload = levelIndex;
        }

        public void OnFadeComplete(){
            SceneManager.LoadScene(_levelToload);
        }
        private void OnGameOver(bool isVictory){
            if(!_menu) GameObject.FindWithTag("GameMusic")?.GetComponent<AudioSource>()?.Stop();
            if(isVictory) FadeToLevel(2);
            else FadeToLevel(3);
        }
    }
}