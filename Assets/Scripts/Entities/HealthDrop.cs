using Flyweight;
using UnityEngine;
using Managers;
namespace Entities
{
    [RequireComponent(typeof(Collider))]
    public class HealthDrop : MonoBehaviour
    {

        private int _playerLayer;
        [SerializeField] private float _healthPoints = 100f;
        void Start(){
            _playerLayer = LayerMask.NameToLayer("Player");
        }

        public void setHealthPoints(float points){
            _healthPoints = points;
        }
        private void OnTriggerEnter(Collider other){
         
            if(other.gameObject.layer != _playerLayer ) return;
            Debug.Log("!healing");
            EventsManager.Instance.EventHealthPickup(_healthPoints);
            Destroy(this.gameObject);
        }

    }
}