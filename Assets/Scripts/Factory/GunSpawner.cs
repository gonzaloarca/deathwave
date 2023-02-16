using System.Collections.Generic;
using UnityEngine;
using Weapons;

namespace Factory
{
    public class GunSpawner : MonoBehaviour, IFactory<GunType, Gun>
    {
        [SerializeField] private List<Gun> _gunPrefabs;
        
        public Gun Create(GunType v)
        {
            Gun prefab = _gunPrefabs.Find(g => g.Type == v);
            return Instantiate(prefab);
        }
    }
}