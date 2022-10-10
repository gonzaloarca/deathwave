using Entities;
using UnityEngine;

namespace Weapons
{
    public class Shotgun : Gun
    {
        [SerializeField] private int _bulletsInShell = 5;

        protected override void ShootBullet(Transform theTransform)
        {
            base.ShootBullet(theTransform);
        }
    }
}