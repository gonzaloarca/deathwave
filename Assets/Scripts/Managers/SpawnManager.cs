using System;
using UnityEngine;
using Strategy;
using System.Collections;
using System.Collections.Generic;
namespace Managers
{
public class SpawnManager : MonoBehaviour
{

        private List<GameObject> _enemySpawners;
        private GameObject _player;
       
        private float _time = 0;

        private float _nextSpawnTime = 0;
 
        [SerializeField] private float _minSpawnCooldown;

        [SerializeField] private float _maxSpawnCooldown;

        [SerializeField] private float _alphaCooldown;

        [SerializeField] private float _timeRateCooldown;

        [SerializeField] private int _closestSpawnsCount;

        [SerializeField] private int _maxEnemies;

        public GameObject[] SpawnObjects => _spawnObjects;
        private ObjectPool[] _objectPools;
        
        [SerializeField] private GameObject[] _spawnObjects;
        [SerializeField] private int[] _maxObjects;
        
        private int _round = 0;

        private int _enemies;
        private int _deadEnemies = 0;
        private int _maxAliveEnemies = 5;

        public void OnEnemyDeath(){
            _deadEnemies++;
        }

        private GameObject GetEnemyToSpawn(){
            // cada 3 rondas hay una que spawnea drones
         
            // aca podria spawnear el boss
            if(_round % 10 == 0){
                return _spawnObjects[1];
            }
            
            if( _round > 10){
                return _spawnObjects[1];
            }

            if (_round % 5 == 0 && _enemies % 2 == 0 ||  _round % 3 == 0 && _enemies % 3 == 0 ){
                    return _objectPools[0].GetObject();
            }


            return _objectPools[1].GetObject();
        }


        private void Start()
        {
            _enemySpawners = new List<GameObject>(GameObject.FindGameObjectsWithTag("EnemySpawn")); 
            EventsManager.Instance.OnEnemyDeath += OnEnemyDeath;
            _player = GameObject.FindWithTag("Player");
            _objectPools = new ObjectPool[_spawnObjects.Length];
            EventsManager.Instance.EventPooling(true);
            for(int i = 0 ; i < _spawnObjects.Length ; i++){
                _objectPools[i] = ObjectPool.CreateInstance(_spawnObjects[i] , _maxObjects[i] );
            }
            EventsManager.Instance.EventPooling(false);

        }

        void Update()
        {   
            _time += Time.deltaTime;

            if(_nextSpawnTime <= _time){
                
                int maxEnemiesRoundModifier = (int) _round/3;
                int alive = _enemies - _deadEnemies;
                if(  (alive) >= (_maxAliveEnemies + maxEnemiesRoundModifier) ) return;
                
                if(_maxEnemies <= _enemies) return;
                GetNextSpawnTime();
                //SortByDistance();
                var index = UnityEngine.Random.Range(0, _closestSpawnsCount+1);
                _enemySpawners[0]?.GetComponent<ISpawn>()?.Spawn(GetEnemyToSpawn());
                _enemies++;
            }
        }

        public void Reset(int roundEnemies , int round){
            _time = 0;
            _nextSpawnTime = 5;
            _enemies = 0;
            _maxEnemies = roundEnemies;
            _round = round;
        }
        
        
        
        private void GetNextSpawnTime(){
            var nextTime = _maxSpawnCooldown - _alphaCooldown * Mathf.Exp( _time * _timeRateCooldown);

            if(nextTime < _minSpawnCooldown)
                nextTime = _minSpawnCooldown;

            if(nextTime > _maxSpawnCooldown)
                nextTime = _maxSpawnCooldown;

            _nextSpawnTime = _time + nextTime;
            
        }   

        private void SortByDistance(){
            _enemySpawners.Sort(delegate(GameObject a, GameObject b){ 
                return Vector3.Distance(_player.transform.position,a.transform.position)
                .CompareTo(Vector3.Distance(_player.transform.position, b.transform.position) 
                );
            });
        }
    }
}