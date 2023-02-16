using TMPro;
using UnityEngine;
using UnityEngine.Events;

namespace Buyable
{
    public class InteractableItem : MonoBehaviour
    {
        [SerializeField] private Canvas promptCanvas;
        [SerializeField] private TextMeshProUGUI promptText;
        [SerializeField] private KeyCode interactionKey = KeyCode.F;
        
        public UnityEvent onInteract;
        public int price;
        public string prompt;

        private void OnTriggerEnter(Collider other)
        {
            if (!other.CompareTag("Player")) return;

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

            promptCanvas.enabled = false;
            promptText.text = "";
        }

        private void OnTriggerStay(Collider other)
        {
            if (!other.CompareTag("Player")) return;

            if (Input.GetKeyDown(interactionKey))
            {
                onInteract?.Invoke();
            }
        }
    }
}