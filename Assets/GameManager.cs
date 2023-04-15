using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace OurGame
{
    public class GameManager : MonoBehaviour
    {
        [SerializeField] private GameObject winMenu;
        [SerializeField] private GameObject loseMenu;

        private static GameManager instance;
        // Start is called before the first frame update
        public static GameManager Instance { 
            get {
                if (instance == null)
                    instance = FindObjectOfType<GameManager>();
                return instance;
            }
        }

        public void Win()
        {
            gameObject.GetComponent<InGamMenu>().TogglePause();
            winMenu.SetActive(true);
        }

        public void Lose()
        {
            gameObject.GetComponent<InGamMenu>().TogglePause();
            loseMenu.SetActive(true);
        }

        void Start()
        {
            
        }

        // Update is called once per frame
        void Update()
        {
        
        }
    }
}
