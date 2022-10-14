using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Strategy;

[RequireComponent(typeof(MeshFilter))]
public class PlaneSpawn : MonoBehaviour , ISpawn
{
    public GameObject SpawnObject => _spawnObject;
    [SerializeField] private GameObject _spawnObject;
   
    private MeshFilter _filter;
    private float _minX;
    private float _minZ;
    private float _maxX;
    private float _maxZ;
    // Start is called before the first frame update
    void Start()
    {
        
        List<Vector3> vertex = new List<Vector3>(gameObject.GetComponent<MeshFilter>().sharedMesh.vertices);
    
        
        if(transform.TransformPoint(vertex[0]).x > transform.TransformPoint(vertex[120]).x){
            _minX = transform.TransformPoint(vertex[120]).x;
            _minZ = transform.TransformPoint(vertex[120]).z;
            _maxX = transform.TransformPoint(vertex[0]).x;
            _maxZ = transform.TransformPoint(vertex[0]).z;
        }else{
            _minX = transform.TransformPoint(vertex[0]).x;
            _minZ = transform.TransformPoint(vertex[0]).z;
            _maxX = transform.TransformPoint(vertex[120]).x;
            _maxZ = transform.TransformPoint(vertex[120]).z;
        }
        Debug.Log("Plane spawn at:: p1:(" + _minX + " : " + _minZ + ") p2:(" + _maxX + " : " +_maxZ +")");




    }

    private float _time = 0f;
    private float _nextSpawnTime = 0f;
    // Update is called once per frame
 

    public void Spawn(){
        
        var position = new Vector3(Random.Range(_minX, _maxX), transform.position.y + 2, Random.Range(_minZ, _maxZ));
        Instantiate(SpawnObject, position, Quaternion.identity);
    }


}
