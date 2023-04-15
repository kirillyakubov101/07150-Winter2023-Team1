using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace OurGame
{
    public class Base : MonoBehaviour
    {
       
        private void OnTriggerEnter(Collider other)
        {
            Debug.Log(other.gameObject.layer.ToString());
            if (other.gameObject.CompareTag("Enemy"))
            {
                Debug.Log("lose");
                GameManager.Instance.Lose();
            }
            if (other.gameObject.CompareTag("Ally"))
                GameManager.Instance.Win();
        }

        // Start is called before the first frame update
        void Start()
        {

        }

        // Update is called once per frame
        void Update()
        {

        }
    }
}
