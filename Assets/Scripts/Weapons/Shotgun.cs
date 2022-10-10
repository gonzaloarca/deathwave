using Entities;
using UnityEngine;

namespace Weapons
{
    public class Shotgun : Gun
    {
        [SerializeField] private int _bulletsInShell = 5;
        
        protected override void ShootBullet(Transform theTransform)
        {
            for (int i = 0; i < _bulletsInShell; i++)
            {
                InstantiateBullet(
                    theTransform.position + Random.insideUnitSphere * 1,
                    theTransform.rotation, "Shotgun Bullet");
            }
        }
    }
}