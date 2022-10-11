using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class GameLevelChanger : MonoBehaviour
{
    [SerializeField] private AudioClip _startingClip;
    [SerializeField] private AudioSource _source;
    [SerializeField] private Animator _fadeAnimator;
    private int _levelToload; 
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
      if(_source.loop && Input.GetKey(KeyCode.Space)){
        _source.loop = false;
      }
      if(_source.isPlaying == false){
        _source.clip = _startingClip;
        _source.Play();
        FadeToLevel(1);
     }
    }

    public void FadeToLevel(int levelIndex){
        _fadeAnimator.SetTrigger("FadeOut");
        _levelToload = levelIndex;
    }

    public void OnFadeComplete(){
        SceneManager.LoadScene(_levelToload);
    }
}
