using Flyweight;
using UnityEngine;
using Managers;

namespace Entities
{
    [RequireComponent(typeof(Collider))]
    public class AmmoDrop : MonoBehaviour
    {
        private int _playerLayer;
        private ParticleSystem _pickupParticles;

        void Start()
        {
            _playerLayer = LayerMask.NameToLayer("Player");
            _pickupParticles = GetComponentInChildren<ParticleSystem>();
            _pickupParticles.Stop();
        }

        private void OnTriggerEnter(Collider other)
        {
            if (other.gameObject.layer != _playerLayer) return;
            Debug.Log("!DROP AMMO");

            // before destroying, create new object with particle system and destroy it after duration
            var particles = Instantiate(_pickupParticles, transform.position, Quaternion.identity);

            particles.Play();
            Destroy(particles.gameObject, particles.main.duration);


            EventsManager.Instance.EventAmmoPickup();
            Destroy(this.gameObject);
        }
    }
}