using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GlobalData : MonoBehaviour
{
    public int SelectedRounds = 1;
    private void Awake(){
        GameObject[] musicObj = GameObject.FindGameObjectsWithTag("GlobalData");
        if(musicObj.Length > 1)
        {
            Destroy(this.gameObject);
        }
        DontDestroyOnLoad(this.gameObject);
    }
}
