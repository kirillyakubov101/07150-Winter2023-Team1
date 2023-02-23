using System.Collections.Generic;
using UnityEngine;

namespace Pooling
{
    [System.Serializable]
    public class PoolSystem
    {
        private const int MAX_SPAWN_COUNT = 10;
        private GameObject[] objectPrefabs;
        private Queue<GameObject> m_spawnQueue = new Queue<GameObject>();
        private int spawnCount = 0;

        public PoolSystem(GameObject[] objs)
        {
            objectPrefabs = objs;
        }

        public bool CreateObject(out GameObject obj)
        {
            obj = null;

            //if the you have not reached the max spawn count
            //create the obj with "NEW"
            if (spawnCount < MAX_SPAWN_COUNT)
            {
                obj = CreateNewObjectDirectly();
                spawnCount++;

                if (!obj.TryGetComponent(out IAvailable _unit)) { return false; } //safety check and assinging the unit

                _unit?.SetAvailability(false); //the unit is now marked as Unavailable to pull from pool again, it's on the battefield
                return true;
            }
            else
            {
                obj = m_spawnQueue.Peek();

                if (TryGettingAvailableObject(obj))
                {
                    //return the available object
                    obj = m_spawnQueue.Dequeue();
                    m_spawnQueue.Enqueue(obj);

                    //safety check and assinging the unit
                    if (!obj.TryGetComponent(out IAvailable _unit)) { return false; } 

                    //the unit is now marked as Unavailable to pull from pool again, it's on the battefield
                    _unit.SetAvailability(false);

                    return true;
                }
            }

            return false;
        }

        private GameObject CreateNewObjectDirectly()
        {
            GameObject obj = GameObject.Instantiate(GetRandomEnemyPrefab(), null);
            m_spawnQueue.Enqueue(obj);
            return obj;
        }

        private GameObject GetRandomEnemyPrefab()
        {
            if(objectPrefabs == null || objectPrefabs.Length == 0) { return null; }

            int index = Random.Range(0, objectPrefabs.Length);
            return objectPrefabs[index];
        }

        private bool TryGettingAvailableObject(GameObject obj)
        {
            int count = 0;

            while(count < MAX_SPAWN_COUNT)
            {
                if (obj.TryGetComponent(out IAvailable _unit) && _unit.GetAvailability()) { return true; }

                //advance to the next queued obj if the previous one failed the condition
                obj = m_spawnQueue.Dequeue();
                m_spawnQueue.Enqueue(obj);

                obj = m_spawnQueue.Peek(); //assign the next one

                count++;
            }

            //searched too much for an avaialable unit
            if(count >= MAX_SPAWN_COUNT) { return false; }
          
           

            //in case it's not a IAvailable | should not get here
            Debug.LogWarning("Should not get here");
            return false;
        }
    }
}
