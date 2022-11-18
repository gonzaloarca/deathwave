using System;
using UnityEngine;

namespace Managers
{
    public class LevelSoundManager : MonoBehaviour
    {
        [SerializeField] private AudioClip _roundStartSound;
        [SerializeField] private AudioSource _source;

        private void Start()
        {
            EventsManager.Instance.OnRoundChange += OnRoundChange;
        }

        void OnRoundChange(int round)
        {
            _source.PlayOneShot(_roundStartSound);
        }
    }
}