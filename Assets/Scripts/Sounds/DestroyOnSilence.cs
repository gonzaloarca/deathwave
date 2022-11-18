using System.Collections.Generic;
using Managers;
using UnityEngine;

namespace Sounds
{
    [RequireComponent(typeof(AudioSource))]
    public class DestroyOnSilence : MonoBehaviour
    {
        
        public AudioSource AudioSource => _audioSource;
        private AudioSource _audioSource;
        
        private void Start()
        {
            _audioSource = GetComponent<AudioSource>();
        }


        private void Update()
        {
            if(!AudioSource.isPlaying) Destroy(this.gameObject);
        }
    }
}