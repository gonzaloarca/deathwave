using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class StepSFX : MonoBehaviour
{
    [SerializeField]
    private AudioClip[] _clips;
    private AudioSource _audioSource;

    void Start()
    {
        _audioSource = GetComponent<AudioSource>();
    }

    
    void Step(){
        AudioClip clip = _clips[UnityEngine.Random.Range(0 , _clips.Length)];
        _audioSource.PlayOneShot(clip);
    }


}
