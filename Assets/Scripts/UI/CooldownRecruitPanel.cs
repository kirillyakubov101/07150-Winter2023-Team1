using UnityEngine;
using UnityEngine.UI;
using OurGame.Spawn;
using System.Collections;

namespace OurGame.UI
{
    public class CooldownRecruitPanel : MonoBehaviour
    {
        [SerializeField] private Image[] m_CooldownImages;

        private void OnEnable()
        {
            FriendlySpawner.OnCooldownStart += CoolDownEffect;
        }

        private void OnDestroy()
        {
            FriendlySpawner.OnCooldownStart -= CoolDownEffect;
        }

        private void CoolDownEffect()
        {
            foreach(Image ele in m_CooldownImages)
            {
                ele.fillAmount = 1;
            }

            StartCoroutine(CoolDownVisualProcess());
        }

        private IEnumerator CoolDownVisualProcess()
        {
            float timer = 0f;
            
            while (timer < FriendlySpawner.c_spawnCoolDown)
            {
                timer += Time.deltaTime;

                foreach (Image ele in m_CooldownImages)
                {
                    ele.fillAmount -= 1f / FriendlySpawner.c_spawnCoolDown * Time.deltaTime;
                }

                yield return null;
            }
        }
    }
}
