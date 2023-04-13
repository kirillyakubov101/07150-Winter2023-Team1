using UnityEngine;

namespace OurGame
{
    public class InGamMenu : MonoBehaviour
    {
        [SerializeField] GameObject m_tempParentForDebug;

        private int m_shouldPause = -1;
        private SceneHandler m_sceneHandler;

        private void Awake()
        {
            m_sceneHandler = FindObjectOfType<SceneHandler>();

            if(m_sceneHandler == null)
            {
                m_sceneHandler = m_tempParentForDebug.AddComponent<SceneHandler>();
                //for testing
            }
        }

        private void Start()
        {
            Time.timeScale = 1f;
            m_shouldPause = -1;
        }
        public void TogglePause()
        {
            Time.timeScale += m_shouldPause;
            m_shouldPause *= -1;
        }

        public void RestartLevel()
        {
            m_sceneHandler.Restart();
        }

        public void QuitGame()
        {
            m_sceneHandler.QuitGame();
        }


       

    }
}
