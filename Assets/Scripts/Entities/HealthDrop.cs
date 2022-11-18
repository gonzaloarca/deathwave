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
        private ParticleSystem _pickupParticles;

        void Start()
        {
            _playerLayer = LayerMask.NameToLayer("Player");
            _pickupParticles = GetComponentInChildren<ParticleSystem>();
            _pickupParticles.Stop();
        }

        public void setHealthPoints(float points)
        {
            _healthPoints = points;
        }

        private void OnTriggerEnter(Collider other)
        {
            if (other.gameObject.layer != _playerLayer) return;
            Debug.Log("!healing");

            // before destroying, create new object with particle system and destroy it after duration
            var particles = Instantiate(_pickupParticles, transform.position, Quaternion.identity);

            particles.Play();
            Destroy(particles.gameObject, particles.main.duration);

            EventsManager.Instance.EventHealthPickup(_healthPoints);
            Destroy(this.gameObject);
        }
    }
}