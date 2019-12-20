using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class AnimationSlider : MonoBehaviour
{
    public Animation _anim;
    AnimationState animState;
    public string animationName = "Take 001";
    public AudioSource animationAudio;

    public Slider slider;
    public GameObject PlayPauseButton;

    public Sprite playSprite;
    public Sprite pauseSprite;

    public Text durationText;

    public float animationDuration;
    public  int animationDurationInSeconds;
    public float animationDurationInMinutes;

    public bool isPointerDown;

    bool isPlaying ;

    void Start()
    {
        isPlaying = true;
        animState = _anim[animationName];
        slider.maxValue = animState.length;

		animationAudio.clip = Resources.Load<AudioClip> ("VoiceOvers/" + LanguageHandler.instance.Languages[LanguageHandler.instance.CurrentLanguageIndex].LanguageID+ "/" + "Animation_VO");
		animationAudio.Play ();
        animationDuration = animState.clip.length;

        while (animationDuration >= 60)
        {
            animationDurationInMinutes += 1;
            animationDuration -= 60;
        }

        animationDurationInSeconds = (int)animationDuration;
    }

    void Update()
    {
        slider.value = animState.time;



        durationText.text = ((int)(animState.time) / 60).ToString() + ":" + (((int)animState .time) % 60).ToString("D2") + "/" + animationDurationInMinutes + ":" + animationDurationInSeconds;

        if (!_anim.isPlaying)
        {
           // HANDLE AFTER ANIMATION PHASE HERE
        }
    }

    public void SliderDown()
    {
        isPointerDown = true;
        animState.speed = 0;
        animationAudio.Pause();
    }

    public void SliderUp()
    {
        isPointerDown = false;
        if (isPlaying)
        {
            animState.speed = 1;
            animationAudio.UnPause();

        }

        animationAudio.time = Mathf.Clamp(slider.value,0f, animationAudio.clip.length);
    }

    public void OnSliderChange()
    {
        animState.time = slider.value;
        //animationAudio.time = slider.value;
    }

    public void EnableSliderImage(Image imagetoBeEnabled)
    {
        imagetoBeEnabled.enabled = true;
    }

    public void DisableSliderImage(Image imagetoBeDisabled)
    {
        if (!isPointerDown)
            imagetoBeDisabled.enabled = false;
    }

    public void PlayPauseToggle()
    {
        if (isPlaying)
        {
            SoundManager.instance.PlayClickSound();
            animState.speed = 0f;
            animationAudio.Pause();
            PlayPauseButton.GetComponent<Image>().sprite = playSprite;
            isPlaying = false;
        }
        else
        {
            SoundManager.instance.PlayClickSound();
            animState.speed = 1f;
            animationAudio.Play();
            PlayPauseButton.GetComponent<Image>().sprite = pauseSprite;
            isPlaying = true;
        }
    }

    public void Go2Scene(int BuildIndex )
    {
        SoundManager.instance.PlayClickSound();
        LoadingScene.LoadingSceneIndex = BuildIndex;
    }
}
