using System.Collections.Generic;
using Commands;
using Controllers;
using EventQueue;
using Managers;
using Strategy;
using UnityEngine;
using Weapons;

namespace Entities
{
    public class Boss : Skeleton
    {
        [SerializeField]  private GameObject _deathSound;
        // INSTANCIAS
        protected virtual void OnDisable(){;
            if(!EventsManager.Instance.IsGameOver() && !_pooling ){
                Instantiate(_deathSound, this.transform.position , Quaternion.identity);
            }
            Debug.Log("boss disable");
            base.OnDisable();
        }
   
    }
}