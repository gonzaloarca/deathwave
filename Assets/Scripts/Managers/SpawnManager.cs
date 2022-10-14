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

        private void Start()
        {
            _enemySpawners = new List<GameObject>(GameObject.FindGameObjectsWithTag("EnemySpawn")); 
           
            _player = GameObject.FindWithTag("Player");
        }

        void Update()
        {   
            _time += Time.deltaTime;

            if(_nextSpawnTime <= _time){
                GetNextSpawnTime();
                SortByDistance();
                var index = UnityEngine.Random.Range(0, _closestSpawnsCount+1);
                _enemySpawners[index]?.GetComponent<ISpawn>()?.Spawn();
            }
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