using System;
using UnityEngine;

namespace Managers
{
    public class MainMenuManager : MonoBehaviour
    {
        [SerializeField] private GameLevelManager _gameLevelManager;

        private void Start()
        {
            Cursor.lockState = CursorLockMode.None;
        }

        public void PlaySynthwaveLevel()
        {
            _gameLevelManager.FadeToLevel(LevelIndex.SynthwaveLevel);    
        }

        public void PlayIslandLevel()
        {
            _gameLevelManager.FadeToLevel(LevelIndex.IslandLevel);
        }

        public void QuitGame()
        {
            Debug.Log("Quitting...");
            Application.Quit();
        }
        
    }
}