using UnityEngine;

namespace OurGame.VFX
{
    public class VFX_Factory : MonoBehaviour
    {
        [SerializeField] private ParticleSystem m_mageImpactEffect;

        private static VFX_Factory instance;
        private void Awake()
        {
            if (instance == null) { instance = this; }
            else { Destroy(gameObject); }
        }

        public static VFX_Factory Instance
        {
            get
            {
                if (instance == null) { Debug.LogWarning("no instance of Factory in the scene"); return null; }
                return instance;
            }
            private set { }

        }

        public ParticleSystem CreateMageImpactEffect(Transform parent)
        {
            ParticleSystem inst = Instantiate(m_mageImpactEffect, parent);

            return inst;

        }
    }
}
