using UnityEngine;

namespace Strategy
{
    public interface IListenable
    {
        AudioSource AudioSource { get; }

        void InitAudioSource();
        void PlayOneShot(AudioClip clip);
        
        void Stop();
    }
}