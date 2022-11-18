using Managers;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace UI
{
    [RequireComponent(typeof(Animator))]
    public class UIRoundChangeListener : MonoBehaviour
    {
        private Animator _animator;

        public void OnRoundChange(int number){
            _animator.SetTrigger("RoundChange");
          
        }

        private void Start(){
            _animator = GetComponent<Animator>();
            EventsManager.Instance.OnRoundChange += OnRoundChange;
        }
    }
}