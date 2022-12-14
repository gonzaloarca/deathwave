using Strategy;
using UnityEngine;
using Entities;
using Managers;
namespace Controllers
{
    public class DamageableHitboxController : MonoBehaviour, IHittable
    {
        private HealthController _healthController;
        [SerializeField] private bool _head = false;
        [SerializeField] private float _damageMultiplier = 1;
        private void FindOwnerHealthController()
        {
            GameObject healthOwner = gameObject;
            _healthController = healthOwner?.GetComponent<HealthController>();
            while (_healthController == null && healthOwner != null)
            {
                //Debug.Log("finding:" + healthOwner.name);
                healthOwner = healthOwner.transform.parent.gameObject;
                _healthController = healthOwner.GetComponent<HealthController>();
            }

          Debug.Log("owner: " + healthOwner?.name + "health controller:" + _healthController);
        }

        public void Start()
        {
            FindOwnerHealthController();

        }

        public void Hit(float damage)
        {
            if(_head) EventsManager.Instance.EventHeadshot();
           // Debug.Log("GUNHIT");
            _healthController?.TakeDamage(damage * _damageMultiplier);
        }
    }
}