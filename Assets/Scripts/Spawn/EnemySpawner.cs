using System.Collections;
using UnityEngine;

using Pooling;

namespace OurGame.Spawn
{
    public class EnemySpawner : MonoBehaviour
    {
        [SerializeField] private PoolSystem _poolSystem;
        [SerializeField] private GameObject[] _enemiesPrefabs;
        [SerializeField] private Transform[] _Lanes;
        [SerializeField] private float spawnCd;

        private static EnemySpawner instance;

        private void Awake()
        {
            if(instance == null)
            {
                instance = this;
            }
            else
            {
                Destroy(this);
            }
        }

        private void Start()
        {
            _poolSystem = new PoolSystem(_enemiesPrefabs);

            StartCoroutine(SpawnProcess());
        }

        private IEnumerator SpawnProcess()
        {
            yield return null;

            while(true)
            {
                var newInst = _poolSystem.CreateObject();

                newInst.SetActive(true);
                int randomLaneIndex = Random.Range(0, _Lanes.Length);
                newInst.transform.parent = _Lanes[randomLaneIndex];

                yield return null;
                newInst.transform.position = _Lanes[randomLaneIndex].position;
                newInst.transform.rotation = _Lanes[randomLaneIndex].rotation;


                yield return new WaitForSeconds(spawnCd);
            }
        }

        public static EnemySpawner Instance { get { return instance; } private set { } }
    }
}
