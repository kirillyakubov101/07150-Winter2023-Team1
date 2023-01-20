using UnityEngine;
using OurGame.Input;

namespace OurGame.Camera
{
    public class CameraMovement : MonoBehaviour
    {
        [SerializeField] private float m_cameraMoveSpeed = 2f;

        private InputControls m_input;

        private void Start()
        {
            m_input = InputControls.Instance;
        }

        private void LateUpdate()
        {
            transform.Translate(m_input.CameraMoveDir * m_cameraMoveSpeed * Time.deltaTime);
        }
    }

}

