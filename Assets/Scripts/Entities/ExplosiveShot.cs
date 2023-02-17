using System;
using Flyweight;
using Strategy;
using UnityEngine;
using Weapons;
using Entities;
using Strategy;

namespace Entities
{
    [RequireComponent(typeof(Explosive))]
    public class ExplosiveShot : LaserShot, IBullet
    {
        private Explosive _explosive;
    
        public void Start(){
            base.Start();
            _explosive = gameObject.GetComponent<Explosive>();
        }
            private void OnTriggerEnter(Collider other)
        {
            // TODO: spawn sound effect
            // TODO: force feedback
            // TODO: destroy self
         

            if (other.gameObject.layer != 8) return;
               Debug.Log("TRIGGER ENTER");
            // take damage
            // var hittable = other.gameObject.GetComponent<IHittable>();
            // hittable?.Hit(Damage);

            // spawn particle effect, normal to surface
            var impact = Instantiate(ImpactParticles, transform.position, Quaternion.identity);
            // impact.transform.parent = other.transform;
            impact.Play();

            Destroy(impact.gameObject, 1f);
            _explosive.SetLevel(Level);
            _explosive.Explode();
        }
    }
}