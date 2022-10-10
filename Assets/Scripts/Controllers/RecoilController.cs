using UnityEngine;

namespace Controllers
{
    public class RecoilController : MonoBehaviour
    {
        private Vector3 _currentRotation;
        private Vector3 _targetRotation;

        [SerializeField] private float snappiness;
        [SerializeField] private float returnSpeed;
        
        public void Update()
        {
            _targetRotation = Vector3.Lerp(_targetRotation, Vector3.zero, returnSpeed * Time.deltaTime);
            _currentRotation = Vector3.Slerp(_currentRotation, _targetRotation, snappiness * Time.deltaTime);
            transform.localRotation = Quaternion.Euler(_currentRotation);
        }
        
        public void Fire(GunRecoil gunRecoil)
        {
            _targetRotation += new Vector3(gunRecoil.x, Random.Range(-gunRecoil.y, gunRecoil.y), Random.Range(-gunRecoil.z, gunRecoil.z));
        }
    }
}