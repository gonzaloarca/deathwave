using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateAndHover : MonoBehaviour
{
    

    private float _xPos = 0;
    private float _yPos = 0;
    private float _zPos = 0;
    private Vector3 _startingPos;

    
    [SerializeField] public float _rotationSpeed = 0.01f;
    [SerializeField] private float _xv , _yv  , _zv;
    [SerializeField] private float _xa , _ya , _za;
    [SerializeField] private float _rx , _ry , _rz;
    void Start(){
        _startingPos = transform.position;        
    }
       // Update is called once per frame
    void Update()
    {
        _xPos = Mathf.Sin((Time.time * _xv *Mathf.PI)) * _xa;
        _yPos = Mathf.Sin((Time.time * _yv *Mathf.PI)) * _ya;
        _zPos = Mathf.Sin((Time.time * _zv *Mathf.PI)) * _za;
        
        transform.Rotate(_rx * _rotationSpeed , _ry * _rotationSpeed , _rz * _rotationSpeed , Space.Self);
        transform.position = _startingPos + new Vector3(_xPos , _yPos , _zPos);   

    }
    
}
