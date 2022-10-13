using Entities;
using Strategy;
using UnityEngine;

namespace Weapons
{
    public class Shotgun : Gun
    {
        [SerializeField] private int _bulletsInShell = 5;

        // protected override void ShootBullet(Transform theTransform)
        // {
        //     MuzzleFlashParticles.Play();
        //     base.ShootBullet(theTransform);
        // }

        protected override void InstantiateBullet(Vector3 position, Quaternion rotation)
        {
            for (int i = 0; i < _bulletsInShell; i++)
            {
                // add spread to bullet rotation
                var bullet = Instantiate(Bullet, position + (Random.insideUnitSphere * Spread), rotation);
                bullet.name = "Shotgun Bullet";

                var bulletScript = bullet.GetComponent<IBullet>();

                bulletScript.SetDamage(Damage / _bulletsInShell);
                bulletScript.SetRange(Range);
            }
        }
    }
}