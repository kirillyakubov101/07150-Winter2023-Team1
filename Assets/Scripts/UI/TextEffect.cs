using System.Collections;
using UnityEngine;

namespace OurGame.UI
{
    public class TextEffect : MonoBehaviour
    {
        [SerializeField] private CanvasGroup m_canvasGroup;
        [SerializeField] private float m_selfDestroyTime = 1.5f;
        [SerializeField] private float m_translateSpeed = 6f;

        private float m_randomOffsetX = 0f;
        private const float c_MIN_OFFSET_X = -2f;
        private const float c_MAX_OFFSET_X = 2f;
        private Vector3 m_directionFloat;
        private float m_time = 0f;
        

        private void Start()
        {
            DoAction();
        }

        /// <summary>
        /// The text animation appear, float up and disappear
        /// </summary>
        public void DoAction()
        {
            StartCoroutine(MoveAndFade());
        }

        /// <summary>
        /// Text effect moves up with a random offset and fades away
        /// </summary>
        /// <returns></returns>
        private IEnumerator MoveAndFade()
        {
            m_time = 0f;
            m_randomOffsetX = Random.Range(c_MIN_OFFSET_X, c_MAX_OFFSET_X);
            m_directionFloat = Vector3.up;
            m_directionFloat.x = m_randomOffsetX;

            while (m_time <= m_selfDestroyTime)
            {
                m_time += Time.deltaTime;
                m_canvasGroup.alpha -= Time.deltaTime * 0.8f;


                transform.Translate(m_directionFloat * Time.deltaTime * m_translateSpeed);
                transform.localScale = Vector3.Lerp(transform.localScale, Vector3.zero,Time.deltaTime * 1.2f);
                yield return null;
            }

            Destroy(gameObject);

        }
    }
}
