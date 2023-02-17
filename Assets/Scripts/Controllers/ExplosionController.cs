using System;
using Strategy;
using Sounds;
using UnityEngine;
using UnityEngine.AI;

namespace Controllers{
    [RequireComponent(typeof(AudioSource))]
    public class ExplosionController : MonoBehaviour
    {
        private AudioSource _boom;
        [SerializeField] private float _duration = 2f;
        void Start(){
            _boom = this.gameObject.GetComponent<AudioSource>();
        }

        void Update(){
            _duration -= Time.deltaTime;
           // Debug.Log("duration: " + _duration);
            if(_duration<=0){
                Destroy(gameObject);
            }
        }
    }
}