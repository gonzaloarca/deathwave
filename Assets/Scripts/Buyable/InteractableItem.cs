using TMPro;
using UnityEngine;
using UnityEngine.Events;

namespace Buyable
{
    public class InteractableItem : MonoBehaviour , IBuyable
    {
        [SerializeField] protected Canvas promptCanvas;
        [SerializeField] protected TextMeshProUGUI promptText;
        [SerializeField] protected KeyCode interactionKey = KeyCode.F;
        [SerializeField] private GameObject _sfx;
        
        public UnityEvent onInteract;
        public int price;
        public string prompt;
        public bool  _playerInside = false;

        protected void Update(){
          
            if(!_playerInside)
                 return;
            
        

            if (Input.GetKeyDown(interactionKey))
            {
                onInteract?.Invoke();
            }
        }
        
        private void OnTriggerEnter(Collider other)
        {
            if (!other.CompareTag("Player")) return;

            _playerInside = true;
            promptCanvas.enabled = true;
            promptText.text = $"Press {interactionKey} to {prompt}";

            if (price >= 0)
            {
                promptText.text += $" [{price}]";
            }
        }

        private void OnTriggerExit(Collider other)
        {
            if (!other.CompareTag("Player")) return;
            _playerInside = false;
            promptCanvas.enabled = false;
            promptText.text = "";
        }

        public void Buy(){
            if(_sfx)
                Instantiate(_sfx , transform.position , transform.rotation);
        }

        
    }
}