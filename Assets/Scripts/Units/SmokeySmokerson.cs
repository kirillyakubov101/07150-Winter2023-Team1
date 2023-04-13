using OurGame.UI;
using UnityEngine;

namespace OurGame.Units
{
    public class SmokeySmokerson : EnemyUnit
    {
        [SerializeField] private float m_missChance = 0.6f;

        public override void TakeDamage(float damage)
        {
            //getting hit
            if(IsDamaged())
            {
                base.TakeDamage(damage); 
            }
            //evading
            else
            {
                TextFactory.Instance.CreateEvadeText(m_canvasSpace);
            }
        }

        private bool IsDamaged()
        {
            float random = Random.Range(0f, 1f);

            return (random >= m_missChance);
        }
    }
}
