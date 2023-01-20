using UnityEngine;

namespace OurGame.Units
{
    public abstract class Unit : MonoBehaviour
    {
        [SerializeField] private string m_unitName; //TODO: USE SCRIPTABLE OBJECTS
        [SerializeField] private float m_unitDamage;

    }
}

