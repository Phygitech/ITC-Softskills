using UnityEngine;
using System.Collections;

public class SoundManager : MonoBehaviour
{
    public static SoundManager instance;

    public AudioClip ClickSound;
    public AudioClip ScrollSound;
    public AudioClip RightSound;
    public AudioClip WrongSound;
    public AudioClip MessageSound;
	public AudioClip ClockTikSound;
	public AudioClip Score10_Sound;
	public AudioClip Score5_Sound;
	public AudioClip Score0_Sound;
    AudioSource asc;

    void Start()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
        asc = gameObject.GetComponent<AudioSource>();
    }

    public void PlayClickSound()
    {
        SoundManager.instance.asc.clip = ClickSound;
        SoundManager.instance.asc.PlayOneShot(ClickSound);
    }

    public void PlayScrollSound()
    {
        SoundManager.instance.asc.clip = ScrollSound;
        SoundManager.instance.asc.PlayOneShot(ScrollSound);
    }

    public void PlayRightSound()
    {
        SoundManager.instance.asc.clip = RightSound;
        SoundManager.instance.asc.PlayOneShot(RightSound);
    }

    public void PlayWrongSound()
    {
        SoundManager.instance.asc.clip = WrongSound;
        SoundManager.instance.asc.PlayOneShot(WrongSound);
    }

    public void PlayMessageSound()
    {
        SoundManager.instance.asc.clip = MessageSound;
        SoundManager.instance.asc.PlayOneShot(MessageSound);
    }

    public void PlayAudioClip(AudioClip _clip)
    {
        SoundManager.instance.asc.clip = _clip;
        SoundManager.instance.asc.PlayOneShot(_clip);
    }

	public void PlayTikTikSound(){
		SoundManager.instance.asc.clip = ClockTikSound;
		SoundManager.instance.asc.PlayOneShot (ClockTikSound);
	}

	public void SoundForSroring10(){
		SoundManager.instance.asc.clip = Score10_Sound;
		SoundManager.instance.asc.PlayOneShot (Score10_Sound);
	}
	public void SoundForSroring5(){
		SoundManager.instance.asc.clip = Score5_Sound;
		SoundManager.instance.asc.PlayOneShot (Score5_Sound);
	}
	public void SoundForSroring0(){
		SoundManager.instance.asc.clip = Score0_Sound;
		SoundManager.instance.asc.PlayOneShot (Score0_Sound);
	}
}