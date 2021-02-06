using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MixerController : MonoBehaviour
{

    public static MixerController instance;

    // Make sure there is only 1 GameManager
    void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else if (instance != this)
        {
            Destroy(gameObject);
        }
        DontDestroyOnLoad(transform);
    }

    [SerializeField]
    private float defaultMasterVolume = 1.0f;
    [SerializeField]
    private float defaultMusicVolume = 1.0f;
    [SerializeField]
    private float defaultSFXVolume = 1.0f;

    string masterBusString = "Bus:/";
    string musicBusString = "Bus:/Music";
    string sfxBusString = "Bus:/SFX";


    // Start is called before the first frame update
    void Start()
    {
        FMOD.Studio.Bus masterBus;
        masterBus = FMODUnity.RuntimeManager.GetBus(masterBusString);
        masterBus.setVolume(defaultMasterVolume);

        FMOD.Studio.Bus musicBus;
        musicBus = FMODUnity.RuntimeManager.GetBus(musicBusString);
        musicBus.setVolume(defaultMusicVolume);

        FMOD.Studio.Bus sfxBus;
        sfxBus = FMODUnity.RuntimeManager.GetBus(sfxBusString);
        sfxBus.setVolume(defaultSFXVolume);
    }

    // Update is called once per frame
    void Update()
    {

    }

    void SetMasterVolume(float volume)
    {
        FMOD.Studio.Bus masterBus;
        masterBus = FMODUnity.RuntimeManager.GetBus(masterBusString);
        masterBus.setVolume(volume);
    }
    float GetMasterVolume()
    {
        FMOD.Studio.Bus masterBus;
        masterBus = FMODUnity.RuntimeManager.GetBus(masterBusString);
        float volume;
        masterBus.getVolume(out volume);
        return volume;
    }

    void SetMusicVolume(float volume)
    {
        FMOD.Studio.Bus musicBus;
        musicBus = FMODUnity.RuntimeManager.GetBus(musicBusString);
        musicBus.setVolume(volume);
    }
    float GetMusicVolume()
    {
        FMOD.Studio.Bus musicBus;
        musicBus = FMODUnity.RuntimeManager.GetBus(musicBusString);
        float volume;
        musicBus.getVolume(out volume);
        return volume;
    }

    void SetSFXVolume(float volume)
    {
        FMOD.Studio.Bus sfxBus;
        sfxBus = FMODUnity.RuntimeManager.GetBus(sfxBusString);
        sfxBus.setVolume(volume);
    }
    float GetSFXBus()
    {
        FMOD.Studio.Bus sfxBus;
        sfxBus = FMODUnity.RuntimeManager.GetBus(sfxBusString);
        float volume;
        sfxBus.getVolume(out volume);
        return volume;
    }
}
