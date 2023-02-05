using UnityEngine;
using System.Collections;
using OurGame.Units;

namespace OurGame.Spawn
{
    public class FriendlySpawner : MonoBehaviour
    {
        [SerializeField] private Transform[] m_lanes = new Transform[3];

        [SerializeField] private Unit m_Kngiht;

        private Transform m_activeLane = null;
        private bool canSpawn = true;
        private float spawnCoolDown = 1.2f;

        private void Start()
        {
            m_activeLane = m_lanes[0];
        }

        public void SetActiveLane(int index)
        {
            m_activeLane = m_lanes[index];
        }

        public void SpawnKnight()
        {
            if (!canSpawn) { return; }
            canSpawn = false;
            Instantiate(m_Kngiht, m_activeLane);
            StartCoroutine(SpawnProccess());
        }

        private IEnumerator SpawnProccess()
        {
            yield return new WaitForSeconds(spawnCoolDown);
            canSpawn = true;
        }
    }

}
