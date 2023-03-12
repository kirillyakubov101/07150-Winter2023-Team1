using System.Collections.Generic;
using TMPro;
using UnityEngine;


namespace Options
{
    public class GraphicsOptions : MonoBehaviour
    {
        [SerializeField] private TMP_Dropdown m_resolutionDropdown;

        private Resolution[] m_resolutions;
        private void Start()
        {
            m_resolutions = Screen.resolutions; //cache availabe resolutions

            m_resolutionDropdown.ClearOptions(); //clear default

            List<string> resolutionsNames = new List<string>();

            int currentResolutionIndex = 0;
            int count = 0;

            foreach (Resolution ele in m_resolutions)
            {

                string option = ele.width + " x " + ele.height + " @ " + ele.refreshRate + "hz"; ;
                resolutionsNames.Add(option);

                if(ele.width == Screen.currentResolution.width && ele.height == Screen.currentResolution.height && ele.refreshRate == Screen.currentResolution.refreshRate)
                {
                    currentResolutionIndex = count;
                }

                count++;
            }

            m_resolutionDropdown.AddOptions(resolutionsNames);
            m_resolutionDropdown.value = currentResolutionIndex;
            m_resolutionDropdown.RefreshShownValue();
        }

        public void SetQuality(int value)
        {
            QualitySettings.SetQualityLevel(value);
        }

        public void SetResolution(int resIndex)
        {
            Resolution resolution = m_resolutions[resIndex];
            Screen.SetResolution(resolution.width, resolution.height, Screen.fullScreen);
        }

    }
}
