using UnityEngine;
using Strategy;
namespace Sounds
{
    [RequireComponent(typeof(AudioSource))]
    public class StepSfx : MonoBehaviour
    {
        [SerializeField] protected AudioClip[] _clips;
    
        protected AudioSource _audioSource;
        void Start()
        {
           
            _audioSource = GetComponent<AudioSource>();
        }

      
        public void Step()
        {
            AudioClip clip = _clips[UnityEngine.Random.Range(0, _clips.Length)];
            _audioSource.PlayOneShot(clip);
        }
    }
}