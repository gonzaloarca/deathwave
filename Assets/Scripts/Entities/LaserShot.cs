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

        public float Damage => _damage;
        [SerializeField] private float _damage;

        public ParticleSystem ImpactParticles => _impactParticles;
        [SerializeField] private ParticleSystem _impactParticles;

        public void Travel()
        {
            transform.Translate(Vector3.forward * (Speed * Time.deltaTime));
        }

        private void OnTriggerEnter(Collider other)
        {
            // TODO: spawn sound effect
            // TODO: force feedback
            // TODO: destroy self
            Debug.Log("TRIGGER ENTER");

            if (other.gameObject.layer == gameObject.layer)
                return;

            // take damage
            var hittable = other.GetComponent<IHittable>();
            hittable?.Hit(Damage);

            // spawn particle effect, normal to surface
            var impact = Instantiate(ImpactParticles, transform.position, Quaternion.identity);
            // impact.transform.parent = other.transform;
            impact.Play();

            Destroy(impact.gameObject, 1f);

            Destroy(gameObject);
        }

        public void SetRange(float range)
        {
            _range = range;
        }

        public void SetDamage(float damage)
        {
            _damage = damage;
        }

        public void Start()
        {
            // set lifetime of projectile
            var lifeTime = Range / Speed;
            Destroy(gameObject, lifeTime);
        }

        private void Update()
        {
            Travel();
        }
    }
}