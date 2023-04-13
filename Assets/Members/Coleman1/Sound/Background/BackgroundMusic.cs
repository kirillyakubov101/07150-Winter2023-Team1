using System.Collections;
using System.Collections.Generic;
using System.Drawing.Text;
using System.Xml.Serialization;
using UnityEditor.SearchService;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.SceneManagement;

namespace OurGame
{
    public class BackgroundMusic : MonoBehaviour
    {
        #region Singletone Reference to the Music Object

        private static BackgroundMusic instance = null;

        public static BackgroundMusic Instance
        {
            get { return instance; }
        }

        private void Awake()
        {
            if (instance != null && instance != this)
            {
                Destroy(this.gameObject);
                return;
            }
            else
                instance = this;

            DontDestroyOnLoad(this.gameObject);
        }

        #endregion

        private AudioSource track1, track2; //Need two audio sources to fade in and fade out of them
        private bool isPlayingTrack1;

        [SerializeField] private AudioClip defaultTrack;

        [SerializeField] private AudioMixerGroup musicGroup;

        private int sceneIndex;

        private void Start()
        {
            track1 = gameObject.AddComponent<AudioSource>();
            track2 = gameObject.AddComponent<AudioSource>();

            track1.outputAudioMixerGroup = musicGroup;
            track2.outputAudioMixerGroup = musicGroup;

            isPlayingTrack1 = true;

            SwapTrack(defaultTrack);
        }

        public void SwapTrack(AudioClip audioClip)
        {
            StopAllCoroutines();

            StartCoroutine(FadeTrack(audioClip));

            isPlayingTrack1 = !isPlayingTrack1;
        }

        private IEnumerator FadeTrack(AudioClip audioClip)
        {
            float timeToFade = 0.25f;
            float timeElapsed = 0;

            if (isPlayingTrack1)
            {
                track2.clip = audioClip;
                track2.Play();

                while(timeElapsed < timeToFade)
                {
                    track2.volume = Mathf.Lerp(0, 1, timeElapsed / timeToFade);
                    track1.volume = Mathf.Lerp(1, 0, timeElapsed / timeToFade);
                    timeElapsed += Time.deltaTime;
                    yield return null;
                }

                track1.Stop();
            }
            else
            {
                track1.clip = audioClip;
                track1.Play(); 
                
                while (timeElapsed < timeToFade)
                {
                    track1.volume = Mathf.Lerp(0, 1, timeElapsed / timeToFade);
                    track2.volume = Mathf.Lerp(1, 0, timeElapsed / timeToFade);
                    timeElapsed += Time.deltaTime;
                    yield return null;
                }

                track2.Stop();
            }
        }
    }
}
