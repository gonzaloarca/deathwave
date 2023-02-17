using Entities;
using Flyweight;
using Strategy;
using UnityEngine;
using Controllers;
namespace Weapons
{
    public class Mine : Explosive
    {
       
       
        protected void Update()
        {
            
            //
        }
        private void OnTriggerEnter(Collider other){
           
            if(other.gameObject.layer != LayerMask.NameToLayer("Enemy"))
                return;
                
            Debug.Log("!BOOM");
            base.Explode();
        }

    }
}