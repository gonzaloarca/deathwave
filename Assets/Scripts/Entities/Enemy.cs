using Flyweight;
using UnityEngine;
using Managers;

namespace Entities
{
    public class Enemy : Actor
    {
        public EnemyStats EnemyStats => enemyStats;
        [SerializeField] private EnemyStats enemyStats;
        [SerializeField] protected GameObject _ammoDrop;
        [SerializeField] protected GameObject _healthDrop;
        [SerializeField] protected bool _drop = true;
        protected GameObject _target;
      
      
        protected virtual void Start(){

            // ChangeWeapon(0);
            GameObject[] players;
            players = GameObject.FindGameObjectsWithTag("Player");

            if (players.Length == 0)
            {
                Debug.Log("No game objects are tagged with Player");
            }
            else
            {
                _target = players[0];
            }
            EventsManager.Instance.OnGameOver+= OnGameOver;
        }

        protected virtual void DeadDrop(){
            var position = this.transform.position;
            position.y = _target.transform.position.y + 1;
            //position.y += 1;
            if(Random.Range(0f,1f) < EnemyStats.DropFreq ){
                if(Random.Range(0f,1f) < 0.75f){
                    Instantiate(_healthDrop , position , Quaternion.identity );
                }else{
                    Instantiate(_ammoDrop , position , Quaternion.identity );
                }
            }
        }

        protected virtual void OnGameOver(bool isVictory){
            _drop = false;
        }

        protected virtual void OnDestroy(){;
            EventsManager.Instance.EventEnemyDeath();
            if(_drop) DeadDrop();
            Destroy(this.transform.parent.gameObject);
        }

    }
}