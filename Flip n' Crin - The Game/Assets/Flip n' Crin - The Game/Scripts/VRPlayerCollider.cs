using UnityEngine;

namespace Scripts
{
    public class VRPlayerCollider : MonoBehaviour
    {
        public Transform CameraEye;
        private CapsuleCollider _collider;
        
        private void Update()
        {
            _collider = GetComponent<CapsuleCollider>();
            _collider.height = CameraEye.localPosition.y + 0.2f;
            _collider.center = new Vector3(CameraEye.localPosition.x, 0.1f + CameraEye.localPosition.y/2, CameraEye.localPosition.z);
        }
    }
}
