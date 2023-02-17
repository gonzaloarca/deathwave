using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrenadeThrowingController : MonoBehaviour
{
    [SerializeField] private GameObject _grenadePrefab;
    [SerializeField] private float _throwForce = 40f;
    [SerializeField] private KeyCode _throwKey = KeyCode.G;

    void Update()
    {
        if (Input.GetKeyDown(_throwKey)) ThrowGrenade();
    }

    private void ThrowGrenade()
    {
      
        var camTransf = (Camera.main.transform.eulerAngles.x -180 );
        Debug.Log("CAMF: " + camTransf);
        float throwModifier = 0.5f;
        if(camTransf > 0){ 
            throwModifier += ( (180 - camTransf) /90 ) /4;
        }else{
            throwModifier -= (180 + camTransf)/90 / 2;
        }
        Debug.Log("MODIFIER: " + throwModifier);
        var pos = transform.position;

        GameObject grenade = Instantiate(_grenadePrefab, pos + transform.forward * 2 + transform.up *1.5f   , transform.rotation);

        Rigidbody rb = grenade.GetComponent<Rigidbody>();
        rb.AddForce(Camera.main.transform.forward * _throwForce + transform.up * _throwForce *throwModifier * 0.5f, ForceMode.VelocityChange);
    }
}
