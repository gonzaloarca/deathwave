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
    public class Skeleton : Enemy
    {
        // INSTANCIAS
        private EnemyMovementController _movementController;

       
        //  private CmdIdle _idle;
        private float _vision;
        private float _meleeRange;
        private void Start()
        {
              base.Start();
           
            _movementController = GetComponent<EnemyMovementController>();
            _vision = this.EnemyStats.Vision;
            _meleeRange = this.ActorStats.Range;
        }

        void Update()
        {
            // Debug.Log("Bad to the bone");
            float distance = Vector3.Distance(_target.transform.position, transform.position);
            // _movementController.LookAt(_target.transform.position);
            // if (distance >= _vision)
            // {
            //     _movementController.Travel(Vector3.forward);
            //     //Debug.Log("Not Following");
            //     // _animator.SetFloat("Vertical", 0f);
            //     // _animator.SetFloat("Horizontal", 0f);
            //     // _animator.SetBool("running", false);
            //     return;
            // }

            if (distance <= _meleeRange)
            {
                _movementController.LookAt(_target.transform.position);

                _movementController.Attack(_target.transform.position);
                return;
            }
            _movementController.Travel(_target.transform.position);

        }
        


    }
}