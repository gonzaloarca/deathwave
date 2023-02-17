using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using Managers;
using UnityEngine.UI;
public class MineThrowingController : GrenadeThrowingController
{
 
    [SerializeField] private Image _icon;

    protected void Start(){
       this.Update_UI();
    }
    void Update()
    {
        _cooldown -= Time.deltaTime;
        if (Input.GetKeyDown(_throwKey)) ThrowGrenade();
    }

    private void Update_UI(){
        Debug.Log("Grenades");
        if(_totalGrenades <= 0){
            count.text = "";
            _icon.enabled = false;
        }else{            
            _icon.enabled = true;
            count.text = $"{_totalGrenades}";
        }
    }
    private void ThrowGrenade(){
    
        
        if(_totalGrenades <= 0 || _cooldown >0) return;
        
        _totalGrenades -=1;
        this.Update_UI();
        _cooldown = 2f;
       
        var pos = transform.position;

        GameObject grenade = Instantiate(_grenadePrefab, pos + transform.forward * 2  , transform.rotation);

        Rigidbody rb = grenade.GetComponent<Rigidbody>();
        rb.AddForce(Camera.main.transform.forward * _throwForce , ForceMode.VelocityChange);
    }
      public void RefillAll(){
       _totalGrenades = _maxGrenades;
        this.Update_UI();
    }
}
