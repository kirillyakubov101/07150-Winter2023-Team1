using UnityEngine;

namespace OurGame.Props
{
    public class Windmill : MonoBehaviour
    {
        [SerializeField] private float m_rotationSpeed = 20f;
        [SerializeField] private Transform m_Fan;

        private void Update()
        {
            m_Fan.Rotate(Vector3.forward, m_rotationSpeed * Time.deltaTime);
        }
    }
}
