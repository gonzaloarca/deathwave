using System.Collections.Generic;
using Strategy;
using UnityEngine;

namespace Entities
{
    [RequireComponent(typeof(Rigidbody), typeof(Collider))]
    public class Bullet : MonoBehaviour, IBullet
    {
        public IGun Gun => _owner;
        [SerializeField] private IGun _owner;

        public int Damage => _damage;
        [SerializeField] private int _damage = 10;

        public float LifeTime => _lifeTime;
        [SerializeField] private float _lifeTime = 5;

        public Rigidbody Rigidbody => _rigidbody;
        [SerializeField] private Rigidbody _rigidbody;

        public Collider Collider => _collider;
        [SerializeField] private Collider _collider;

        public float Speed => _speed;
        [SerializeField] private float _speed = 50;

        [SerializeField] private List<int> _layerTarget;

        public void Travel() => transform.Translate(Vector3.forward * (Time.deltaTime * _speed));

        public void OnTriggerEnter(Collider collider)
        {
            if (!_layerTarget.Contains(collider.gameObject.layer)) return;
            IDamageable damageable = collider.GetComponent<IDamageable>();
            damageable?.TakeDamage(_owner.Damage);

            Destroy(this.gameObject);
        }

        private void Start()
        {
            _rigidbody = GetComponent<Rigidbody>();
            _collider = GetComponent<Collider>();

            _collider.isTrigger = true;
            _rigidbody.useGravity = false;
            _rigidbody.isKinematic = true;
            _rigidbody.collisionDetectionMode = CollisionDetectionMode.ContinuousSpeculative;
        }

        public void SetOwner(IGun owner) => _owner = owner;

        private void Update()
        {
            _lifeTime -= Time.deltaTime;

            if (_lifeTime <= 0) Destroy(this.gameObject);

            Travel();
        }
    }
}