using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using Managers;
using TMPro;
namespace Managers
{

    public class GameLevelManager : MonoBehaviour
    {

        [SerializeField] private AudioClip _startingLoop;
        [SerializeField] private AudioClip _firstTrack;
        [SerializeField] private AudioSource _source;
        [SerializeField] private Animator _fadeAnimator;
        [SerializeField] private Animator _textAnimator;
        [SerializeField] private TextMeshProUGUI _round;
        [SerializeField] private bool _menu;
        [SerializeField] private bool _ending;
        [SerializeField] private int[] _roundChoices;
        private GlobalData _data;
        private int _selectedRoundIndex;
        public int SelectedRoundCount;
        private MusicManager _musicManager;
        private int _levelToload;
        private bool _fading =false;
        private bool _changeLevel = false;

        // Start is called before the first frame update
        void Start()
        {
            _data = GameObject.FindWithTag("GlobalData")?.GetComponent<GlobalData>();
            _source = GameObject.FindWithTag("GameMusic")?.GetComponent<AudioSource>();
            _musicManager = GameObject.FindWithTag("GameMusic")?.GetComponent<MusicManager>();
            if (_menu)
            {
                
                SelectedRoundCount = _roundChoices[0];
                _data.SelectedRounds = SelectedRoundCount;
                _source.clip = _startingLoop;
                _source.loop = true;
                _source.Play();
                _source.volume = 1.0f;
            }

            EventsManager.Instance.OnGameOver += OnGameOver;
        }

        // Update is called once per frame
        void Update()
        {
            if (Input.GetKey(KeyCode.Escape))
            {
                Application.Quit();
            }

            if (_menu)
            {
                if (Input.GetKeyDown(KeyCode.R))
                {
                    _selectedRoundIndex++;
                    SelectedRoundCount = _roundChoices[_selectedRoundIndex % (_roundChoices.Length)];
                    if(SelectedRoundCount > 0){
                        _round.text = $"[R] Rounds: {SelectedRoundCount}";
                    }
                    else
                    {
                        _round.text = $"[R] Rounds: INFINITE";
                    }
                    _data.SelectedRounds = SelectedRoundCount;
                    _textAnimator.SetTrigger("RoundChange");
                }
                if (_source.loop && Input.GetKeyDown(KeyCode.Space))
                {
                    _source.loop = false;
                }

                if (_source.isPlaying == false)
                {
                    _source.clip = _firstTrack;
                    _source.Play();
                    FadeToLevel(1);
                }

                return;
            }

            if (_ending)
            {
                if (Input.GetKeyDown(KeyCode.Space))
                {
                    FadeToLevel(0);
                }

                return;
            }
        }

        public void FadeToLevel(int levelIndex)
        {
            _fadeAnimator.SetTrigger("FadeOut");
            _levelToload = levelIndex;
            if(!_fading)
                StartCoroutine(LoadAsync());
            _fading = true;
        }

        public void OnFadeComplete()
        {
            _changeLevel = true;
        }

        private void OnGameOver(bool isVictory)
        {
            if (!_menu) GameObject.FindWithTag("GameMusic")?.GetComponent<AudioSource>()?.Stop();
            
            if (isVictory){
                FadeToLevel(2);
            }
            else {
                FadeToLevel(3);
            }
        }

        IEnumerator LoadAsync()
        {
            AsyncOperation operation = SceneManager.LoadSceneAsync(_levelToload);
            operation.allowSceneActivation = false;
            float progress = 0;

            while (!operation.isDone)
            {
                // progress = operation.progress;
                // _progressBar.fillAmount = progress;
                // _progressValue.text = $"Cargando ... {progress * 100} %";

                if (operation.progress >= .9f)
                {
                    if (_changeLevel){
                        operation.allowSceneActivation = true;
                        if(_levelToload == 1){
                            _musicManager.Activate();
                        }else{
                            _musicManager.Deactivate();
                        }
                    }
                }

                yield return null;
            }
        }
    }
}