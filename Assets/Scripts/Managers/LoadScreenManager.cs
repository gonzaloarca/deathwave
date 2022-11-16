using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

using UnityEngine.UI;

namespace Managers{
    public class LoadScreenManager : MonoBehaviour
    {
        [SerializeField] private Image _progressBar;
        [SerializeField] private Text _progressValue;
        [SerializeField] private string _targetScene = "Level";

        void Start()
        {
            StartCoroutine(LoadAsync());
        }

        IEnumerator LoadAsync()
        {
            AsyncOperation operation = SceneManager.LoadSceneAsync(_targetScene);
            operation.allowSceneActivation = false;
            float progress = 0;

            while (!operation.isDone)
            {
                progress = operation.progress;
                _progressBar.fillAmount = progress;
                _progressValue.text = $"Cargando ... {progress * 100} %";

                if(operation.progress >= .9f)
                {
                    _progressValue.text = "Presionar espacio para continuar";
                    
                    if(Input.GetKeyDown(KeyCode.Space)) operation.allowSceneActivation = true;
                }

                yield return null;
            }
        }
    }
}
