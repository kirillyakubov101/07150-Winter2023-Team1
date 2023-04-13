using OurGame.Units;
using UnityEngine;

namespace OurGame.Projectiles
{
    public class Projectile : MonoBehaviour
    {
        [SerializeField] private float m_moveSpeed = 20f;
        [SerializeField] private float m_rotationSpeed = 100f;

        private float m_projectileDamage;
        private bool m_hasHit = false;

        private void Start()
        {
            Destroy(gameObject,10f);
        }

        public void InitStats(float damage)
        {
            m_projectileDamage = damage;
        }

        private void Update()
        {
            transform.Translate(transform.forward * Time.deltaTime * m_moveSpeed);
            transform.Rotate(Vector3.forward * Time.deltaTime * m_rotationSpeed);
        }

        private void OnTriggerEnter(Collider other)
        {
            if (m_hasHit) { return; }
            if(other.TryGetComponent(out Unit unit))
            {
                unit.TakeDamage(m_projectileDamage);
                m_hasHit = true;
                Destroy(gameObject);
            }
        }
    }
}
