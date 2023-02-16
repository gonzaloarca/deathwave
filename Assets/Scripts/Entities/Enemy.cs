using Flyweight;
using UnityEngine;
using Managers;
using Strategy;
namespace Entities
{
    public class Enemy : Actor 
    {
        public ObjectPool Parent;
        public EnemyStats EnemyStats => enemyStats;
        [SerializeField] private EnemyStats enemyStats;
        [SerializeField] protected GameObject _ammoDrop;
        [SerializeField] protected GameObject _healthDrop;
        [SerializeField] protected bool _drop = true;
        protected GameObject _target;
        protected bool _pooling = true;

        protected virtual void Awake(){

            // ChangeWeapon(0);
            GameObject[] players;
            players = GameObject.FindGameObjectsWithTag("Player");
            if (players.Length == 0)
            {
                Debug.Log("No game objects are tagged with Player");
            }
            else
            {
                Debug.Log("found players: " + players.Length);
                _target = players[0];
            }
        }
        protected virtual void Start(){

            // // ChangeWeapon(0);
            // GameObject[] players;
            // players = GameObject.FindGameObjectsWithTag("Player");
            // if (players.Length == 0)
            // {
            //     Debug.Log("No game objects are tagged with Player");
            // }
            // else
            // {
            //     Debug.Log("found players: " + players.Length);
            //     _target = players[0];
            // }
            EventsManager.Instance.OnGameOver+= OnGameOver;
            EventsManager.Instance.OnPooling += OnPooling;
        }

        protected virtual void DeadDrop(){
            var position = this.transform.position;
            position.y = _target.transform.position.y + 1;
            //position.y += 1;
            if(Random.Range(0f,1f) < EnemyStats.DropFreq ){
                if(Random.Range(0f,1f) < 0.5f){
                    Instantiate(_healthDrop , position , Quaternion.identity );
                }else{
                    Instantiate(_ammoDrop , position , Quaternion.identity );
                }
            }
        }

        protected virtual void OnGameOver(bool isVictory){
            _drop = false;
        }

        protected virtual void OnPooling(bool start){
            //starting dropping when pooling stops
            _pooling = start;
        }

        protected virtual void OnDisable(){
            if(!EventsManager.Instance.IsGameOver() ){
                EventsManager.Instance.EventEnemyDeath();
                if(_drop & !_pooling) DeadDrop();
            }
            Parent.ReturnObjectToPool(this.transform.parent.gameObject);

           // this.transform.parent.gameObject.enabled = false;
        }
        protected virtual void OnEnable(){
            EventsManager.Instance.OnGameOver+= OnGameOver;
            EventsManager.Instance.OnPooling += OnPooling;
        }
 
    
    }
}