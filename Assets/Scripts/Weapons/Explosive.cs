using Entities;
using Flyweight;
using Strategy;
using UnityEngine;
using Controllers;
namespace Weapons
{
    public class Explosive : MonoBehaviour
    {

        [SerializeField] private ExplosiveStats _stats;
        public string Name  => _name;
        private string _name = "Explosive";
        public GameObject ExplosionEffect => _stats.ExplosionEffect;
        
        public float Damage  => _stats.Damage;
    
        public float Force => _stats.Force;
        public float Delay => _stats.Delay;

        public float Radius => _stats.Radius;
        
        private float _countdown;
        
        void Start(){
            _countdown = Delay;
        }

        private bool _hasExploded = false;
        protected void Update()
        {
            
            _countdown -= Time.deltaTime;
            if (_countdown <= 0 && !_hasExploded)
            {
                Debug.Log("!BOOM");
                _hasExploded = true;
                Explode();
            }
        }
        
    
        public void Explode()
        {
            Debug.Log("Grenade exploded!!!");

            // 1. Efecto visual de la explosi�n
            if(ExplosionEffect != null)
                Instantiate(ExplosionEffect, transform.position, Quaternion.identity);

            // 2. Detecci�n de los objetos cercanos
            Collider[] colliders = Physics.OverlapSphere(transform.position, _stats.Radius );
            
            foreach (Collider collider in colliders)
            {   
                if(collider.gameObject.layer == LayerMask.NameToLayer("Player")){
                Rigidbody rb = collider.gameObject.GetComponent<Rigidbody>();
                    if (rb != null)
                    {
                        Debug.Log("Moving player");
                        rb.AddExplosionForce(Force * 1000 , transform.position, Radius);
                    }else{
                        Debug.Log("No collider found for player");
                    }
                }else
                 
               // Debug.Log("checking collider: " + collider.gameObject.layer + "vs " + LayerMask.NameToLayer("Enemy"));
                if((collider.gameObject.layer == LayerMask.NameToLayer("Enemy"))){ 
                        

                   Debug.Log("GameObject: " + collider.gameObject.name);    
                

                    Rigidbody rb = collider.gameObject.transform.parent?.gameObject.GetComponent<Rigidbody>();
                    if (rb != null)
                    {
                        Debug.Log("Moving enemy");
                        rb.AddExplosionForce(Force * 1000 , transform.position, Radius);
                    }else{
                        Debug.Log("No collider found");
                    }

                    IDamageable life = collider.GetComponent<IDamageable>();
                    if (life != null )
                    {
                        Debug.Log("Damaging enemy");
                        life.TakeDamage(Damage);
                    }
                }
                
            }

            // 3. Destruir la granada
            Destroy(gameObject);
        }


    }
}