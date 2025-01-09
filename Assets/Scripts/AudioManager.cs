using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Audio;
using System;

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

    /*void Start()
    {
        if (SceneManager.GetActiveScene().buildIndex != 0)
            Play("Music");
        else
            Play("MenuMusic");
    }*/

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

}
