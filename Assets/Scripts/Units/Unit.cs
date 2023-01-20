using UnityEngine;

namespace OurGame.Units
{
    public abstract class Unit : MonoBehaviour
    {
        [SerializeField] private string m_unitName; //TODO: USE SCRIPTABLE OBJECTS
        [SerializeField] private float m_unitDamage;
        [SerializeField] private float m_moveSpeed = 10f;

        public float MoveSpeed { get => m_moveSpeed; }
    }
}

