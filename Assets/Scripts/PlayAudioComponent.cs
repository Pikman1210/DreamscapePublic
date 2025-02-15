using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayAudioComponent : MonoBehaviour
{
    public void PlayAudioManager(string name)
    {
        AudioManager.Instance.Play(name);
    }

    public void StopAudioManager(string name)
    {
        AudioManager.Instance.Stop(name);
    }

    public void StopAllAudioManager()
    {
        AudioManager.Instance.StopAllAudio();
    }
}
