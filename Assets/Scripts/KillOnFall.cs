using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Strategy;

[RequireComponent(typeof(Collider))]
public class KillOnFall : MonoBehaviour
{
    // Start is called before the first frame update
   private void OnTriggerEnter(Collider other)
    {
        IDamageable damageable = other.gameObject.GetComponent<IDamageable>();

        damageable.Die();
    }
}
