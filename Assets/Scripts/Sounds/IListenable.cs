using UnityEngine;

namespace Sounds
{
    public interface IListenable
    {
        AudioSource AudioSource { get; }

        void InitAudioSource();
        void PlayOneShot(AudioClip clip);
        
        void Stop();
    }
}