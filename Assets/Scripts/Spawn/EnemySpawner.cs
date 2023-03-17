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
        [SerializeField] private int maxSpawnUnits;

        private void Start()
        {
            _poolSystem = new PoolSystem(_enemiesPrefabs, maxSpawnUnits);

            StartCoroutine(SpawnProcess());
        }

        private IEnumerator SpawnProcess()
        {
            yield return null;

            WaitForSeconds TimeToWait = new WaitForSeconds(spawnCd);

            while (true)
            {
                //if the object can be created or pulled from pool
                if(_poolSystem.CreateObject(out GameObject newInst))
                {
                    int randomLaneIndex = Random.Range(0, _Lanes.Length);
                    newInst.transform.parent = _Lanes[randomLaneIndex];

                    newInst.transform.position = _Lanes[randomLaneIndex].position;
                    newInst.transform.rotation = _Lanes[randomLaneIndex].rotation;



                    yield return null;
                    newInst.SetActive(true);
                }
             
                
                yield return TimeToWait;
            }
        }
    }
}
