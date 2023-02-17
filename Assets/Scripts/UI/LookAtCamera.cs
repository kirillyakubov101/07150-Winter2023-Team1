using UnityEngine;

using MainCamera = UnityEngine.Camera;

namespace OurGame.UI
{
    public class LookAtCamera : MonoBehaviour
    {
        private MainCamera mainCam;

        private void Start()
        {
            mainCam = MainCamera.main;
        }
        private void LateUpdate()
        {
            transform.forward = mainCam.transform.forward;
        }
    }
}
