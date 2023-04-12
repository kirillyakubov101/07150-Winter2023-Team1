using UnityEngine;
using TMPro;
using OurGame.Spawn;

namespace OurGame.UI
{
    public class RecruitUI : MonoBehaviour
    {
        private static RecruitUI instance;

        [SerializeField] private TMP_Dropdown m_laneChoicesDropDown;
        [SerializeField] private FriendlySpawner m_friendlySpawner;

        public static RecruitUI Instance {
            get
            {
                if (instance == null)
                {
                    instance = FindObjectOfType<RecruitUI>();
                }
                return instance;
            }
        }

        private void Start()
        {
            m_laneChoicesDropDown.onValueChanged.AddListener(m_friendlySpawner.SetActiveLane);
        }

        public void ChangeLane(int laneIndex)
        {
            if (m_laneChoicesDropDown.value != laneIndex)
                m_laneChoicesDropDown.value = laneIndex;
        }
    }
}

