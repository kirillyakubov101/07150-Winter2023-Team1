using System.Collections.Generic;
using UnityEngine;

namespace Pooling
{
    [System.Serializable]
    public class PoolSystem
    {
        private GameObject[] objectPrefabs;
        private const int MAX_SPAWN_COUNT = 25;

        private Queue<GameObject> _spawnQueue = new Queue<GameObject>();

        private int spawnCount = 0;
        public PoolSystem(GameObject[] objs)
        {
            objectPrefabs = objs;
        }

        public GameObject CreateObject()
        {
            GameObject obj = null;

            if(spawnCount >= MAX_SPAWN_COUNT)
            {
                //from pool Queue
                obj = _spawnQueue.Dequeue();
                
            }

            else
            {
                spawnCount++;
                obj = GameObject.Instantiate(GetRandomEnemyPrefab(), null); 
            }

            _spawnQueue.Enqueue(obj);

            obj.SetActive(false);
            return obj;
        }


        private GameObject GetRandomEnemyPrefab()
        {
            if(objectPrefabs == null || objectPrefabs.Length == 0) { return null; }

            int index = Random.Range(0, objectPrefabs.Length);
            return objectPrefabs[index];
        }
    }
}
