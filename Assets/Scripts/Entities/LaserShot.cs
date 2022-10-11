using System;
using Flyweight;
using Strategy;
using UnityEngine;

namespace Entities
{
    public class LaserShot : Projectile, IBullet
    {
        public float Speed => BulletStats.Speed;
        public float Range => _range;
        [SerializeField] private float _range;


        public void Travel()
        {
            transform.Translate(Vector3.forward * (Speed * Time.deltaTime));
        }

        public void OnTriggerEnter(Collider other)
        {
            // TODO: spawn particle effect
            // TODO: spawn sound effect
            // TODO: force feedback
            // TODO: destroy self
            Debug.Log("TRIGGER ENTER");
            Destroy(gameObject);
            
        }

        public void SetRange(float range)
        {
            _range = range;
        }

        public void Start()
        {
            var lifeTime = Range / Speed;
            Destroy(gameObject, lifeTime);
        }

        private void Update()
        {
            Travel();
        }
    }
}