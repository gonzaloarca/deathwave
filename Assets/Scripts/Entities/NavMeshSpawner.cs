using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Strategy;
using UnityEngine.AI;

public class NavMeshSpawner : MonoBehaviour , ISpawn
{
   
    private int _spawnAreaMask;
    private NavMeshTriangulation _triangulation;
    // Start is called before the first frame update
    void Start()
    {

        _triangulation = NavMesh.CalculateTriangulation();
        _spawnAreaMask  = 1 <<  NavMesh.GetAreaFromName("Spawnable");
       
        // NavMeshTriangulation _triangulation = new NavMeshTriangulation();

        //     // Set the area mask to exclude the NavMesh areas you want to skip
        //     int areaMask = ~((1 << NavMesh.GetAreaFromName("Walkable")));

        //     // if (NavMesh.HasNavMeshData)
        //     // {
        //         NavMesh.CreateNavMeshData();

        //         // Generate the triangulation, skipping the specified NavMesh areas
        //         bool success = NavMesh.GetTriangulation(triangulation, areaMask);

        //         if (success)
        //         {
                   
        //         }
        //         else
        //         {
        //             Debug.LogError("Failed to generate NavMesh triangulation");
        //         }
        // // }
    }

    public void Spawn(GameObject o){
      
        

        if (o != null)
        {
      
            
                     // ChangeWeapon(0);
            GameObject[] players;
            players = GameObject.FindGameObjectsWithTag("Player");
            Transform _target = players[0].transform;
            
          
            while(!trySpawn(o , _target)){}
        }
        else
        {
            Debug.LogError($"Unable to fetch enemy from object pool. Out of objects?");
        }

    }

    private bool trySpawn(GameObject o ,Transform _target){
            int VertexIndex = Random.Range(0, _triangulation.vertices.Length);
            NavMeshHit Hit;
            if (NavMesh.SamplePosition(_triangulation.vertices[VertexIndex], out Hit, 2f, -1))
            {   
                if(!IsPointInsideNavMeshArea(Hit.position))
                    return false;
              
                IFollower follower = o.transform.GetChild(0).GetComponent<IFollower>();
                if(follower == null){
                    Debug.LogError($"Unable to place NavMeshAgent on NavMesh. No follower detected");
                    return true;
                }
                Vector3 position = Hit.position;
                position.z +=1f;
                follower.Warp(position);
                return true;
            }
            return false;
    }


    public bool IsPointInsideNavMeshArea(Vector3 point)
    {
        NavMeshHit hit;
        return NavMesh.SamplePosition(point, out hit, 0.1f, _spawnAreaMask);
    }
}
