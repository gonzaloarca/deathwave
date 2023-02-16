using UnityEngine;

namespace Factory
{
    public interface IFactory<in T1, out T2>
    {
        T2 Create(T1 v);
    }
}