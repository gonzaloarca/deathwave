using Entities;
using Flyweight;
using Strategy;
using UnityEngine;

namespace Weapons
{
    public class Gun : MonoBehaviour, IGun
    {
        [SerializeField] private GunStats _stats;
    
        public GameObject BulletPrefab => _stats.BulletPrefab;
        public int MagSize => _stats.MagSize;
        public int Damage => _stats.Damage;

        public int BulletCount => _bulletCount;
        [SerializeField] protected int _bulletCount;

        private void Start()
        {
            Reload();
        }

        public virtual void Attack()
        {
            var transform1 = transform;
            var bullet = Instantiate(BulletPrefab, transform1.position, transform1.rotation);
            bullet.name = "Bullet";
            bullet.GetComponent<Bullet>().SetOwner(this);
        }
    
        public void Reload() => _bulletCount = MagSize;
    }
}