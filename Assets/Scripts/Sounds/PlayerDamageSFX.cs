using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class PlayerDamageSFX : MonoBehaviour
{
     [SerializeField]
    private AudioClip[] _clips;
    private AudioSource _audioSource;

    // Start is called before the first frame update
    void Start()
    {
        _audioSource = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    public void Sound()
    {
        AudioClip clip = _clips[UnityEngine.Random.Range(0 , _clips.Length)];
        _audioSource.PlayOneShot(clip);
    }
}
