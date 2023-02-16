using System.Collections.Generic;
using UnityEngine;
using Entities;
public class ObjectPool
{
    private GameObject _prefab;
    private int _size;
    private List<GameObject> _availableObjectsPool;

    private ObjectPool(GameObject Prefab, int Size)
    {
        this._prefab = Prefab;
        this._size = Size;
        _availableObjectsPool = new List<GameObject>(Size);
    }

    public static ObjectPool CreateInstance(GameObject Prefab, int Size)
    {
        ObjectPool pool = new ObjectPool(Prefab, Size);
        GameObject poolGameObject = new GameObject(Prefab + " Pool");
       
        pool.CreateObjects(poolGameObject);
      
        return pool;
    }

    private void CreateObjects(GameObject parent)
    {
    
        for (int i = 0; i < _size; i++)
        {
            GameObject poolableObject = GameObject.Instantiate(_prefab, Vector3.zero, Quaternion.identity, parent.transform);
     
            Enemy poolable = poolableObject.transform.GetChild(0).gameObject.GetComponent<Enemy>();
          
            poolable.Parent = this;
       
            poolableObject.gameObject.SetActive(false); // PoolableObject handles re-adding the object to the AvailableObjects
        }
     
    }

    public GameObject GetObject()
    {
        GameObject instance = _availableObjectsPool[0];

        _availableObjectsPool.RemoveAt(0);

        instance.gameObject.SetActive(true);

        return instance;
    }

    public void ReturnObjectToPool(GameObject Object)
    {
        _availableObjectsPool.Add(Object);
    }
}