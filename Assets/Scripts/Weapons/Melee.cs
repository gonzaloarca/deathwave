using Entities;
using Flyweight;
using Strategy;
using UnityEngine;

namespace Weapons
{
    public class Melee : MonoBehaviour, IMelee
    {
        [SerializeField] private MeleeStats _stats;
    
        public int Damage(){
            
            return _stats.BaseDamage + UnityEngine.Random.Range(- _stats.DeltaDamage , _stats.DeltaDamage);
        } 


    }
}