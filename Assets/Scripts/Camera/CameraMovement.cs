using UnityEngine;
using OurGame.Input;

namespace OurGame.Camera
{
    public class CameraMovement : MonoBehaviour
    {
        [SerializeField] private float m_cameraAcceleration = 0.5f;
        [SerializeField] private float Min_Z = 50f;
        [SerializeField] private float Max_Z = 1000f;
        [SerializeField] private float m_maxSpeed = 100f;
        [SerializeField] private float m_defaultSpeed = 10f;

        private InputControls m_input; 
        private Vector3 m_clamppedPos;
        private float m_velocity;

        private void Start()
        {
            m_input = InputControls.Instance;

            ResetVelocity();
        }

        private void LateUpdate()
        {
            if(m_input.CameraMoveDir.magnitude != 0)
            {
                Move();
                ClampPos();
            }
            else
            {
                ResetVelocity();
            }
        }

        private void ResetVelocity()
        {
            m_velocity = m_defaultSpeed;
        }

        private void Move()
        {
            m_velocity += Time.deltaTime * m_cameraAcceleration;
            transform.Translate(m_input.CameraMoveDir * m_velocity * Time.deltaTime);
           
            m_velocity = Mathf.Clamp(m_velocity, 2f, m_maxSpeed);
        }

        private void ClampPos()
        {
            m_clamppedPos = transform.position;
            m_clamppedPos.z = Mathf.Clamp(m_clamppedPos.z, Min_Z, Max_Z);
            transform.position = m_clamppedPos;
        }
    }

}

