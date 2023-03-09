using OurGame.Units;
using UnityEngine;
using UnityEngine.UI;

namespace OurGame.UI
{
    public class UpdateHealthUI : MonoBehaviour
    {
        [SerializeField] private Image HpBarImage;
        [SerializeField] private Unit m_Unit;
        [SerializeField] private GameObject m_Hp_UI_Container;

        private void OnEnable()
        {
            m_Unit.OnTakeDamage += UpdateHealth;
            HpBarImage.fillAmount = 1;
        }

        private void OnDestroy()
        {
            m_Unit.OnTakeDamage -= UpdateHealth;
        }

        private void UpdateHealth(float amount)
        {
            m_Hp_UI_Container.SetActive(true);
            HpBarImage.fillAmount = amount; 
        }

    }
}
