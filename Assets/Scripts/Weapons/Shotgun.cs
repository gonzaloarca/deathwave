using Entities;
using UnityEngine;

namespace Weapons
{
    public class Shotgun : Gun
    {
        [SerializeField] private int _bulletsInShell = 5;

        public override void Shoot()
        {
            for (int i = 0; i < _bulletsInShell; i++)
            {
                var bullet = Instantiate(
                    BulletPrefab,
                    transform.position + Random.insideUnitSphere * 1,
                    transform.rotation);

                bullet.name = "Shotgun Bullet";
                bullet.GetComponent<Bullet>().SetOwner(this);
            }
        }
    }
}