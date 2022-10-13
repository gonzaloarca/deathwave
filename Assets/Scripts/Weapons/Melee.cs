using Entities;
using Flyweight;
using Strategy;
using UnityEngine;
using Controllers;
namespace Weapons
{
    public class Melee : MonoBehaviour, IMelee
    {
        [SerializeField] private MeleeStats _stats;
    
        public float Damage(){
            return _stats.BaseDamage + Random.Range(- _stats.DeltaDamage , _stats.DeltaDamage);
        } 

        private void OnTriggerEnter(Collider other){
           
            if(other.gameObject.layer != LayerMask.NameToLayer("Player"))
                return;

            other.gameObject.GetComponent<HealthController>()?.TakeDamage(this.Damage());
        }


    }
}