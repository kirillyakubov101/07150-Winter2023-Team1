using UnityEngine;

using MainCamera = UnityEngine.Camera;

namespace OurGame.UI
{
    public class LookAtCamera : MonoBehaviour
    {
        [SerializeField] private GameObject m_canvas;

        private MainCamera mainCam;

        private void Start()
        {
            mainCam = MainCamera.main;
        }
        private void LateUpdate()
        {
            m_canvas.transform.forward = mainCam.transform.forward;
        }
    }
}
