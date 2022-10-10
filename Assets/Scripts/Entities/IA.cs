using Flyweight;
using UnityEngine;

namespace Entities
{
    public class IA : MonoBehaviour
    {
        public IAStats IAStats => _IAStats;
        [SerializeField] private IAStats _IAStats;
    }
}