using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class SoundManager : MonoBehaviour
{
    public static SoundManager SM;

    private void Awake()
    {
        if (SM != null)
        {
            GameObject.Destroy(SM);
        }
        else
        {
            SM = this;
        }
        DontDestroyOnLoad(this);

        SoundManager.Initialize();
    }

    public enum SoundType //If a specific sound needs to referenced it can be through these types
    {
        SoldierAttack,
        ArcherAttack,
        MageAttack,
        UnitHurt,
        CannonAttack,
        TowerDeath
    }

    //Delegates for playing sound Methods
    public delegate void PlayOneShotSoundDelegate(SoundType sound);
    public static PlayOneShotSoundDelegate playOneShotSound;
    public delegate void PlaySoundDelegate(SoundType sound, Vector3 position);
    public static PlaySoundDelegate playSound;

    //Dictionary to create a timer for current and future sounds
    private static Dictionary<SoundType, float> soundTimerDictionary; //To access and time sound clips to either prevent overlap or stop/play them from different points.

    //Static GameObjects for the one shot sounds to not create duplicate GameObjects
    private static GameObject oneShotGameObject;
    private static AudioSource oneShotAudioSource;

    //Audio Mixer Groups
    [SerializeField] private AudioMixerGroup sfxMixerGroup;

    public static void Initialize()
    {
        //Initializing the timer Dictionary
        soundTimerDictionary = new Dictionary<SoundType, float>();
    }

    private void Start()
    {
        playOneShotSound = PlayOneShotSound;
        playSound = PlaySound;
    }

    //Class for each audio clip that will be added to the list of AudioClips
    [System.Serializable]
    private class SoundAudioClip
    {
        public SoundType soundType;
        public AudioClip audioClip;
    }

    [SerializeField]
    private List<SoundAudioClip> audioClips;

    private static bool CanPlaySound(SoundType sound)
    {
        switch (sound)
        {
            default:
                return true;

            case SoundType.SoldierAttack:
                if (soundTimerDictionary.ContainsKey(sound))
                {
                    float lastTimePlayed = soundTimerDictionary[sound];
                    float attackTimerMax = 0.8f; // 0.8 seconds
                    if (lastTimePlayed + attackTimerMax < Time.time)
                    {
                        soundTimerDictionary[sound] = Time.time;
                        return true;
                    }
                    else
                        return false;
                }
                else
                    return true;

                //Add a case if needed to prevent the overlap of certain sounds
        }
    }

    //Play sounds in 3D or that are away from the player position with linear rolloff
    public static void PlaySound(SoundType sound, Vector3 position)
    {
        if (CanPlaySound(sound))
        {
            GameObject soundGameObject = new GameObject("Sound");
            soundGameObject.transform.position = position;
            AudioSource audioSource = soundGameObject.AddComponent<AudioSource>();

            audioSource.outputAudioMixerGroup = SM.sfxMixerGroup;
            audioSource.clip = GetAudioClip(sound);
            audioSource.maxDistance = 100f;
            audioSource.spatialBlend = 1f;
            audioSource.rolloffMode = AudioRolloffMode.Linear;
            audioSource.dopplerLevel = 0f;
            audioSource.Play();

            Destroy(soundGameObject, audioSource.clip.length); //Will destroy GameObject after clip is played
        }
    }

    //Plays a ONE SHOT audio clip that scales volume with volume scale only
    public static void PlayOneShotSound(SoundType sound)
    {
        if (CanPlaySound(sound))
        {
            if (oneShotGameObject == null)
            {
                oneShotGameObject = new GameObject("One Shot Sound");
                oneShotAudioSource = oneShotGameObject.AddComponent<AudioSource>();
                oneShotAudioSource.outputAudioMixerGroup = SM.sfxMixerGroup;
            }

            oneShotAudioSource.clip = GetAudioClip(sound);
            oneShotAudioSource.Play();
        }
    }

    //To search for the audio clip in the list of AudioClips
    private static AudioClip GetAudioClip(SoundType sound)
    {
        foreach (SoundAudioClip soundAudioClip in SM.audioClips)
        {
            switch (sound)
            {
                case SoundType.SoldierAttack:

                    if (Random.value < .5f)
                        return SM.audioClips[0].audioClip;
                    else
                        return SM.audioClips[1].audioClip;

                case SoundType.UnitHurt:

                    return SM.audioClips[Random.Range(3, 5)].audioClip;

                case SoundType.CannonAttack:

                    if (Random.value < .5f)
                        return SM.audioClips[6].audioClip;
                    else
                        return SM.audioClips[7].audioClip;

                default: return soundAudioClip.audioClip;
            }
        }
        Debug.LogError("This sound " + sound + " was not found!");
        return null;
    }
}
