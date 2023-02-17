using UnityEngine;
using Strategy;
namespace Sounds
{
    [RequireComponent(typeof(AudioSource))]
    public class RandomSfx : MonoBehaviour
    {
       [SerializeField] protected AudioClip[] _clips;
    
        protected AudioSource _audioSource;
        private float _nextTime;
        [SerializeField] private float _cooldown = 1f;
        void Start()
        {
            _nextTime = _cooldown;
            _audioSource = GetComponent<AudioSource>();
        }

        void Update(){
            _nextTime -= Time.deltaTime;
            if(_nextTime <=0){
                var value = Random.Range(0, 1.0f);
                if(value <= 0.3f){
                    Sfx();
                    _nextTime = _cooldown *2;
                }
                else{

                    _nextTime = _cooldown;
                }
            }
        }

          
        public void Sfx()
        {
            AudioClip clip = _clips[UnityEngine.Random.Range(0, _clips.Length)];
            _audioSource.PlayOneShot(clip , 1.5f);
        }
    }
}