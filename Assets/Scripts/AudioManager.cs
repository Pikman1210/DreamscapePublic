using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Audio;
using System;
using QFSW.QC;

// To refernce the AudioManager, use AudioManager.Instance.publicScriptName   VERY IMPORTANT
public class AudioManager : MonoBehaviour {

    public Sound[] sounds;

    private static AudioManager _instance;

    public static AudioManager Instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = FindObjectOfType<AudioManager>();
                if (_instance == null)
                {
                    GameObject singleton = new GameObject(typeof(AudioManager).ToString());
                    _instance = singleton.AddComponent<AudioManager>();
                    DontDestroyOnLoad(singleton);
                }
            }
            return _instance;
        }
    }

    void Awake ()
    {
        if (_instance == null)
        {
            _instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else if (_instance != this)
        {
            Destroy(gameObject);
        }

        foreach (Sound s in sounds)
        {
            s.source = gameObject.AddComponent<AudioSource>();
            s.source.clip = s.clip;
            s.source.outputAudioMixerGroup = s.group;

            s.source.volume = s.volume;
            s.source.pitch = s.pitch;
            s.source.loop = s.loop;
        }
    }

    void OnEnable()
    {
        //Tell our 'OnLevelFinishedLoading' function to start listening for a scene change as soon as this script is enabled.
        SceneManager.sceneLoaded += SceneSpecificMusic;
    }

    void OnDisable()
    {
        //Tell our 'OnLevelFinishedLoading' function to stop listening for a scene change as soon as this script is disabled. Remember to always have an unsubscription for every delegate you subscribe to!
        SceneManager.sceneLoaded -= SceneSpecificMusic;
    }

    private void SceneSpecificMusic(Scene scene, LoadSceneMode mode)
    {
        StopAllAudio();
        switch (SceneManager.GetActiveScene().buildIndex)
        {
            case 0:
                Play("MenuTheme");
                break;
            case 2:
                Play("LevelSelectTheme");
                break;
            case 4:
                Play("BasilicaTheme");
                break;
        }
    }

    [Command("play-audio")]
    [CommandDescription("Play audio clip by name")]
    public void Play (string name)
    {
        Sound s = Array.Find(sounds, sound => sound.name == name);
        if (s == null)
        {
            Debug.LogWarning("Sound: " + name + " missing!");
            return;
        }
        s.source.Play();
    }

    [Command("stop-audio")]
    [CommandDescription("Stop audio clip by name")]
    public void Stop (string name)
    {
        Sound s = Array.Find(sounds, sound => sound.name == name);
        if (s == null)
        {
            Debug.LogWarning("Sound: " + name + " missing!");
            return;
        }
        s.source.Stop();
    }

    [Command("stop-all-audio")]
    [CommandDescription("Stop all audio playing")]
    public void StopAllAudio()
    {
        foreach (Sound s in sounds)
        {
            s.source.Stop();
        }
    }

}
