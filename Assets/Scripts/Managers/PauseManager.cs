using System;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Managers
{
    public class PauseManager : MonoBehaviour
    {
        [SerializeField] private int killCount = 0;
        [SerializeField] private int headshotCount = 0;
        [SerializeField] private TextMeshProUGUI killsText;
        [SerializeField] private TextMeshProUGUI headshotsText;
        public static bool GameIsPaused = false;

        public GameObject pauseMenuUI;

        private void Start()
        {
            EventsManager.Instance.OnEnemyDeath += OnEnemyDeath;
            EventsManager.Instance.OnHeadshot += OnHeadshot;
        }

        void Update()
        {
            if (Input.GetKeyDown(KeyCode.Escape))
            {
                if (GameIsPaused)
                {
                    Resume();
                }
                else
                {
                    Pause();
                }
            }
        }

        public void Resume()
        {
            pauseMenuUI.SetActive(false);
            Time.timeScale = 1f;
            GameIsPaused = false;
        }

        public void Pause()
        {
            pauseMenuUI.SetActive(true);
            Time.timeScale = 0f;
            GameIsPaused = true;
        }

        public void LoadMenu()
        {
            Resume();
            Debug.Log("Loading Menu...");
            SceneManager.LoadScene("Scenes/Menu");
        }

        public void QuitGame()
        {
            Debug.Log("Quitting...");
            Application.Quit();
        }

        public void OnEnemyDeath()
        {
            killCount += 1;
            killsText.text = $"KILLS: {killCount}";
        }

        public void OnHeadshot()
        {
            headshotCount += 1;
            headshotsText.text = $"HEADSHOTS: {headshotCount}";
        }
    }
}