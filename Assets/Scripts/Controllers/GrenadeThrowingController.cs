using System.Collections;
using System.Collections.Generic;
using Managers;
using UnityEngine;
using TMPro;
public class GrenadeThrowingController : MonoBehaviour
{
    [SerializeField] protected GameObject _grenadePrefab;
    [SerializeField] protected float _throwForce = 40f;
    [SerializeField] protected KeyCode _throwKey = KeyCode.G;
    [SerializeField] protected TextMeshProUGUI count;

    [SerializeField] protected int _maxGrenades = 4;
    [SerializeField] protected int _totalGrenades = 4;
    [SerializeField] protected float _cooldown = 2f;

    protected void Start(){
        EventsManager.Instance.OnRoundChange += OnRoundChange;
    }
    void Update()
    {
        _cooldown -= Time.deltaTime;
        if (Input.GetKeyDown(_throwKey)) ThrowGrenade();
    }

    public bool IsFull(){
        return _maxGrenades == _totalGrenades;
    }
    protected void Update_UI(){
        count.text = $"{_totalGrenades}";
    }
    protected void ThrowGrenade(){
    
        
        if(_totalGrenades <= 0 || _cooldown >0) return;
            _totalGrenades -=1;
        
        Update_UI();
        _cooldown = 2f;
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

    public void refill(int number){
        if(_totalGrenades + number >= _maxGrenades ) _totalGrenades = _maxGrenades;
        else _totalGrenades += number;
        Update_UI();
    }
    public void RefillAll(){
       _totalGrenades = _maxGrenades;
        this.Update_UI();
    }
    protected void OnRoundChange(int round){
        if(round > 10)
            refill(4);
        else
            refill(2);
    }
}
