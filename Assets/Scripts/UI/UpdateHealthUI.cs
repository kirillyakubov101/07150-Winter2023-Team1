using OurGame.Units;
using UnityEngine;
using UnityEngine.UI;

namespace OurGame
{
    public class UpdateHealthUI : MonoBehaviour
    {
        [SerializeField] private Image HpBarImage;
        [SerializeField] private Unit m_Unit;
        [SerializeField] private Canvas m_Canvas;

        private void OnEnable()
        {
            m_Unit.OnTakeDamage += UpdateHealth;
        }

        private void OnDestroy()
        {
            m_Unit.OnTakeDamage -= UpdateHealth;
        }

        private void UpdateHealth(float amount)
        {
            HpBarImage.fillAmount = amount;
            m_Canvas.gameObject.SetActive(true);
        }

    }
}
