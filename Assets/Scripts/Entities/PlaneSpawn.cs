using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Strategy;

[RequireComponent(typeof(MeshFilter))]
public class PlaneSpawn : MonoBehaviour , ISpawn
{

   
    private MeshFilter _filter;
    private float _minX;
    private float _minZ;
    private float _maxX;
    private float _maxZ;
    // Start is called before the first frame update
    void Start()
    {
        
        List<Vector3> vertex = new List<Vector3>(gameObject.GetComponent<MeshFilter>().sharedMesh.vertices);
     
        var minX = transform.TransformPoint(vertex[120]).x;
        var minZ = transform.TransformPoint(vertex[120]).z;
        var maxX = transform.TransformPoint(vertex[0]).x;
        var maxZ = transform.TransformPoint(vertex[0]).z;

        if(maxX> minX){
            _minX = minX;
            _maxX = maxX;
        }else{
            _minX = maxX;
            _maxX = minX;
        }

        if(maxZ> minZ){
            _minZ = minZ;
            _maxZ = maxZ;
        }else{
            _minZ = maxZ;
            _maxZ = minZ;
        }


    
        Debug.Log("Plane spawn at:: p1:(" + _minX + " : " + _minZ + ") p2:(" + _maxX + " : " +_maxZ +")");




    }

    private float _time = 0f;
    private float _nextSpawnTime = 0f;
    // Update is called once per frame

    public void Spawn(GameObject o){
        var position = new Vector3(Random.Range(_minX, _maxX), transform.position.y + 2, Random.Range(_minZ, _maxZ));
        var skeletonRoot = Instantiate(o, position, Quaternion.identity);
      
    }


}
