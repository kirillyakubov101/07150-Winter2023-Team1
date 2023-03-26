using UnityEngine;
using System.Collections;
using OurGame.Units;
using System;

namespace OurGame.Spawn
{
    public class FriendlySpawner : MonoBehaviour
    {
        [SerializeField] private Lane[] m_lanes = new Lane[3];

        [SerializeField] private Unit m_Knight;
        [SerializeField] private Unit m_Archer;
        [SerializeField] private Unit m_Mage;

        private Lane m_activeLane = null;
        private bool canSpawn = true;
        private Coroutine m_Coroutine;

        public static event Action OnCooldownStart;

        public const float c_spawnCoolDown = 1.5f;

        private void Start()
        {
            m_activeLane = m_lanes[0];
        }

        public void SetActiveLane(int index)
        {
            m_activeLane = m_lanes[index];
            m_activeLane.DisplayLane();
        }

        public void SpawnKnight()
        {
            if (!canSpawn || m_Coroutine != null) { return; }
            canSpawn = false;
            Instantiate(m_Knight, m_activeLane.transform);
            m_Coroutine = StartCoroutine(SpawnProccess());
            
        }

        public void SpawnArcher()
        {
            if (!canSpawn || m_Coroutine !=null) { return; }
            canSpawn = false;
            Instantiate(m_Archer, m_activeLane.transform);
            m_Coroutine = StartCoroutine(SpawnProccess());
        }

        public void SpawnMage()
        {
            if (!canSpawn || m_Coroutine != null) { return; }
            canSpawn = false;
            Instantiate(m_Mage, m_activeLane.transform);
            m_Coroutine = StartCoroutine(SpawnProccess());
        }

        private IEnumerator SpawnProccess()
        {
            OnCooldownStart?.Invoke(); //notify images on the recruit UI to show cooldowns
            yield return new WaitForSeconds(c_spawnCoolDown);

            canSpawn = true;
            m_Coroutine = null;
        }
    }

}
