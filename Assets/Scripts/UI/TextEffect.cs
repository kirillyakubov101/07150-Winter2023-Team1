using System.Collections;
using UnityEngine;

namespace OurGame.UI
{
    public class TextEffect : MonoBehaviour
    {
        [SerializeField] private CanvasGroup m_canvasGroup;
        [SerializeField] private float m_selfDestroyTime = 1.5f;
        [SerializeField] private float m_translateSpeed = 6f;

        private void Start()
        {
            DoAction();
        }

        //the text animation appear, float up and disappear
        public void DoAction()
        {
            StartCoroutine(MoveAndFade());
        }

        //TODO:: remove this, this is a test | use animation withing the text obj
        private IEnumerator MoveAndFade()
        {
            float time = 0f;

            while (time <= m_selfDestroyTime)
            {
                time += Time.deltaTime;
                m_canvasGroup.alpha -= Time.deltaTime * 0.8f;


                transform.Translate(Vector3.up * Time.deltaTime * m_translateSpeed);
                yield return null;
            }

            Destroy(gameObject);

        }
    }
}
