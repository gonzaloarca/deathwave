using Strategy;
using UnityEngine;

namespace Controllers
{
    public class DamageableHitboxController : MonoBehaviour, IHittable
    {
        public void Hit(float damage)
        {
            Debug.Log("HIT");
        }
    }
}