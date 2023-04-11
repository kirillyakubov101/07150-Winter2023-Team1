using UnityEngine;
using TMPro;

namespace OurGame.UI
{
    public class TextFactory : MonoBehaviour
    {
        [SerializeField] private TMP_Text m_damageTextPrefab;
        [SerializeField] private TMP_Text m_evadeTextPrefab;

        private const string EvadeTEXT = "MISS!";

        private static TextFactory instance;
        private void Awake()
        {
            if(instance == null) { instance = this; }
            else { Destroy(gameObject); }
        }

        public void CreateDamageText(float damageValue,Transform parent)
        {
            TMP_Text textInst = Instantiate(m_damageTextPrefab, parent);
            textInst.text = damageValue.ToString();
        }

        public void CreateEvadeText(Transform parent)
        {
            TMP_Text textInst = Instantiate(m_evadeTextPrefab, parent);
            textInst.text = EvadeTEXT;
        }

        public static TextFactory Instance
        {
            get {
                    if (instance == null) { Debug.LogWarning("no instance of Factory in the scene"); return null; }
                    return instance;
                }
            private set {}
                
        }
    }
}
