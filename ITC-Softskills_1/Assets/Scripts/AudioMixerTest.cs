using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class AudioMixerTest : MonoBehaviour
{
    public AudioMixer am;

    public AudioMixerGroup bg_music, microphone_audio;

    void Awake()
    {

        if (Camera.main.GetComponent<AudioSource>() == null)
            return;

        Camera.main.GetComponent<AudioSource>().outputAudioMixerGroup = bg_music;
        Camera.main.GetComponent<AudioSource>().priority = 255;

        if (Camera.main.transform.parent.gameObject.AddComponent<AudioSource>() == null)
        {
            Camera.main.transform.parent.gameObject.AddComponent<AudioSource>().outputAudioMixerGroup = microphone_audio;
            Camera.main.transform.parent.gameObject.AddComponent<AudioSource>().priority = 0;
        }
        else
        {
            Camera.main.transform.parent.gameObject.GetComponent<AudioSource>().outputAudioMixerGroup = microphone_audio;
            Camera.main.transform.parent.gameObject.GetComponent<AudioSource>().priority = 0;
        }
    }
}
