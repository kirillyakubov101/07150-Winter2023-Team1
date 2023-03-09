using UnityEngine;

namespace OurGame.Spawn
{
    public class Lane : MonoBehaviour
    {
        [SerializeField] private MeshRenderer m_renderer;
        [Tooltip("If you will change the time of the lane showcase, change it to all 3 lanes!")]
        [SerializeField] float hideLaneDelay = 2f;


        public void DisplayLane()
        {
            if (m_renderer.enabled) { return; }
            ShowLaneProcess();
            Invoke(nameof(HideLane), hideLaneDelay);
        }

        private void ShowLaneProcess()
        {
            m_renderer.enabled = true;
        }

        private void HideLane()
        {
            m_renderer.enabled = false;
        }

     

    } 

}
