using UnityEngine;
using TMPro;
using OurGame.Spawn;

namespace OurGame.UI
{
    public class RecruitUI : MonoBehaviour
    {
        [SerializeField] private TMP_Dropdown m_laneChoicesDropDown;
        [SerializeField] private FriendlySpawner m_friendlySpawner;

        private void Start()
        {
            m_laneChoicesDropDown.onValueChanged.AddListener(m_friendlySpawner.SetActiveLane);
        }
    }
}

