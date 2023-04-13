using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Audio;

[RequireComponent(typeof(Slider))]

public class SetVolume : MonoBehaviour
{
    [SerializeField] private AudioMixer audioM = null;
    [SerializeField] private string nameParam = null;
    private Slider slider;

    // Start is called before the first frame update
    void Start()
    {
        slider = GetComponent<Slider>();
        float v = PlayerPrefs.GetFloat(nameParam, 0.3f); //+0db (full volume)
        slider.value = v;
        audioM.SetFloat(nameParam, Mathf.Log10(v) * 30); //Linear volume rolloff
    }

    public void SetVol(float vol)
    {
        audioM.SetFloat(nameParam, vol);
        audioM.SetFloat(nameParam, Mathf.Log10(vol) * 30); //Linear volume 
    }
}
