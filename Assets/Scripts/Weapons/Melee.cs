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
    
        public int Damage(){
            return _stats.BaseDamage + UnityEngine.Random.Range(- _stats.DeltaDamage , _stats.DeltaDamage);
        } 

         private void OnTriggerEnter(Collider other){
            Debug.Log("hit");
            if(other.gameObject.layer != 9)
                return;
            other.gameObject.GetComponent<HealthController>().TakeDamage(this.Damage());
        }


    }
}