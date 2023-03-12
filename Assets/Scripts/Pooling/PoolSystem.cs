using System.Collections.Generic;
using UnityEngine;

namespace Pooling
{
    [System.Serializable]
    public class PoolSystem
    {
        private const int MAX_SPAWN_COUNT = 3;
        private GameObject[] objectPrefabs;
        private Queue<GameObject> m_spawnQueue = new Queue<GameObject>();
        private int spawnCount = 0;

        private GameObject[] m_UnitsArray = new GameObject[MAX_SPAWN_COUNT];

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
                PopulateArray(obj);
                spawnCount++;

                if (!obj.TryGetComponent(out IAvailable _unit)) { return false; } //safety check and assinging the unit

                _unit?.SetAvailability(false); //the unit is now marked as Unavailable to pull from pool again, it's on the battefield
                return true;
            }
            else
            {
                obj = m_spawnQueue.Peek();

                //I pass the first elemet in-line
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
            GameObject availabeElement = null;

            //find the available element
            foreach (var ele in m_UnitsArray)
            {
                if (ele.TryGetComponent(out IAvailable _unit) && _unit.GetAvailability())
                {
                    availabeElement = ele;
                    break;
                }
            }

            //in case failure, leave
            if (availabeElement == null) { return false; }

            while(obj != availabeElement)
            {
                GameObject firstEle = m_spawnQueue.Dequeue();
                m_spawnQueue.Enqueue(firstEle);
                obj = m_spawnQueue.Peek();
            }

            return true;
        }

        private void PopulateArray(GameObject newObj)
        {
            m_UnitsArray[spawnCount] = newObj;
        }
    }
}
