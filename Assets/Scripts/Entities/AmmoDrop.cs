using Flyweight;
using UnityEngine;
using Managers;
namespace Entities
{
    [RequireComponent(typeof(Collider))]
    public class AmmoDrop : MonoBehaviour
    {

        private int _playerLayer;

        void Start(){
            _playerLayer = LayerMask.NameToLayer("Player");
        }

        private void OnTriggerEnter(Collider other){
         
            if(other.gameObject.layer != _playerLayer ) return;
            Debug.Log("!DROP AMMO");
            EventsManager.Instance.EventAmmoPickup();
            Destroy(this.gameObject);
        }

    }
}