using UnityEngine;
using TMPro;
using System.Collections;

namespace OurGame.UI
{
    public class TextFactory : MonoBehaviour
    {
        [SerializeField] private TMP_Text m_damageTextPrefab;


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
