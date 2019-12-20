﻿using UnityEngine;
using System.Collections;
using SmartLocalization;
using UnityEngine.UI;
using ArabicSupport;
using System.Collections.Generic;
using UnityEngine.SceneManagement;

[System.Serializable]
public class LanguageData
{
    public enum LanguageTextAlignment{LeftToRight, RightToLeft};

    //Display Name Shows it's native name;
    //LanguageId shows Culture code
    public string DisplayName,LanguageID;
    public LanguageTextAlignment _Alignment;
    // Make this constructor to add langs description by  Editor AdvancementIntegrationEditor Line 108
    public LanguageData(string DisplayName, string LanguageID, LanguageTextAlignment _Alignment)
    {
        this.DisplayName = DisplayName;
        this.LanguageID = LanguageID;
        this._Alignment = _Alignment;
    }
}

public class LanguageHandler : MonoBehaviour
{
    private static LanguageHandler Instance = null;
    public static LanguageHandler instance 
    {
        get
        {
            if (Instance == null)
                Instance = FindObjectOfType<LanguageHandler>();
            return Instance;
        }
       
    }

    public delegate void LanguageChanged();
    public static event LanguageChanged LanguageChangeEventFire;

    [Header("This will set tool language editing bar ")]
    public List<LanguageData> Languages;
    [HideInInspector]
    public int CurrentLanguageIndex;
    
    public const int _DefaultLanguageIndex = 1;

    public bool IsLeftToRight
    {
        get
        { 
            return (LanguageHandler.instance.Languages[LanguageHandler.instance.CurrentLanguageIndex]._Alignment == LanguageData.LanguageTextAlignment.LeftToRight);
        }
    }

    public bool IsRightToLeft
    {
        get
        { 
            return (LanguageHandler.instance.Languages[LanguageHandler.instance.CurrentLanguageIndex]._Alignment == LanguageData.LanguageTextAlignment.RightToLeft);
        }
    }

    #region SetDefaultLang
    //if you change default language en-us to something different 
    // you need to change degault lang in LanguageHandlerEditor
    //Line no 42
	public const string defaultLanguage = "en-US";
    #endregion
    internal LoadingScene loadingScene;
    void Awake() 
    {
        if (SceneManager.GetActiveScene().name == "LoadingScene")
        {
            loadingScene = FindObjectOfType<LoadingScene>();
            if(loadingScene == null)
            {
                Debug.LogError("LoadingCanvasIsMissingInLoadingScene");
            }
            else
                loadingScene.gameObject.SetActive(false);
        }
            
        // below condition is if your playing your app first time
        if ( !PlayerPrefs.HasKey("currentLanguage") )
            PlayerPrefs.SetString("currentLanguage","en-US");
		//Step1

//        if (PlayerPrefs.GetString("currentLanguage") == defaultLanguage)
//        {
            setCurrentLanguage();
            changeLanguageUpdate();
            if (loadingScene != null)
            {
                loadingScene.gameObject.SetActive(true);
            }

 //       }
		// below condition will see asset bundle is downloaded or not for other lang
//        else
//        {
//            CheckAssetBundleForCurrentLanguage();
//        }
            
    }

    public void CheckAssetBundleForCurrentLanguage() 
    {
        //Asset bundle is on the path
        if (AssetBundleManager.Instance.IsFileExist())
        {
            setCurrentLanguage();
            changeLanguageUpdate();
            if (loadingScene != null)
            {
                loadingScene.gameObject.SetActive(true);
            }
        }
        //Asset bundle is not on the path
        else 
        {
            InAbsenseOfAssetBundle.Instance.EnableAskForDownload();
        }
    }

    internal void setCurrentLanguage()
    {    
        if (!PlayerPrefs.HasKey("currentLanguage"))
        {
			PlayerPrefs.SetString("currentLanguage", defaultLanguage);
        }
        else
        {
            for (int i = 0; i < Languages.Count; i++)
            {
                if (PlayerPrefs.GetString("currentLanguage").Equals(Languages[i].LanguageID))
                {
                    CurrentLanguageIndex = i;
                    return;
                }
            }
        }
    }

    internal void changeLanguageUpdate()
    {
        LanguageManager.Instance.ChangeLanguage(Languages[CurrentLanguageIndex].LanguageID);
        if (LanguageChangeEventFire != null)
            LanguageChangeEventFire();
    }   

    public void OnLanguageChangeListener(Text TextBox, string CurrKey)
    {
        if (LanguageHandler.instance.Languages[LanguageHandler.instance.CurrentLanguageIndex]._Alignment==LanguageData.LanguageTextAlignment.RightToLeft)
        {
            switch (TextBox.alignment)
            {
                case TextAnchor.UpperLeft:
                    TextBox.alignment = TextAnchor.UpperRight;
                    break;
                case TextAnchor.MiddleLeft:
                    TextBox.alignment = TextAnchor.MiddleRight;
                    break;
                case TextAnchor.LowerLeft:
                    TextBox.alignment = TextAnchor.LowerRight;
                    break;
            }
            TextBox.text = ArabicFixer.Fix("" + LanguageManager.Instance.GetTextValue(CurrKey));          
        }
        else
        {
            switch (TextBox.alignment)
            {
                case TextAnchor.UpperRight:
                    TextBox.alignment = TextAnchor.UpperLeft;
                    break;
                case TextAnchor.MiddleRight:
                    TextBox.alignment = TextAnchor.MiddleLeft;
                    break;
                case TextAnchor.LowerRight:
                    TextBox.alignment = TextAnchor.LowerLeft;
                    break;
            }
            TextBox.text = "" + LanguageManager.Instance.GetTextValue(CurrKey);
        }
    }

    public void OnLanguageChangeListener(TextMesh TextBox, string CurrKey)
    {
        if (LanguageHandler.instance.Languages[LanguageHandler.instance.CurrentLanguageIndex]._Alignment==LanguageData.LanguageTextAlignment.RightToLeft)
        {
            TextBox.alignment = TextAlignment.Right;

            TextBox.text = ArabicFixer.Fix("" + LanguageManager.Instance.GetTextValue(CurrKey));
        }
        else
        {         
            TextBox.alignment = TextAlignment.Left;
         
            TextBox.text = "" + LanguageManager.Instance.GetTextValue(CurrKey);
        }
    }

    public void PlayVoiceOver(string key)
    {
        if (GlobalAudioSrc.Instance.audioSrc.isPlaying && GlobalAudioSrc.Instance.audioSrc.clip.name == key)
            return;
        
        GlobalAudioSrc.Instance.audioSrc.clip = null;
       

		AudioClip _Clip;
		_Clip = Resources.Load < AudioClip >("VoiceOvers/" + LanguageHandler.instance.Languages[LanguageHandler.instance.CurrentLanguageIndex].LanguageID + "/" + key);

//		if ( PlayerPrefs.GetString("currentLanguage") == defaultLanguage) 
//		{
//
//		}
//		else 
//		{ 
//			 _Clip = AssetBundleManager.Instance.GetAssetBundle(PlayerPrefs.GetString("currentLanguage")).LoadAsset<AudioClip>(key);
//
//			if (_Clip == null)
//			{
//				return;
//			}
//		}

        GlobalAudioSrc.Instance.audioSrc.clip = _Clip;
        GlobalAudioSrc.Instance.audioSrc.PlayOneShot(_Clip);
    }

    public void StopVoiceOver()
    {
        if (GlobalAudioSrc.Instance == null)
            return;
        
        GlobalAudioSrc.Instance.audioSrc.Stop();
    }

    public void PlayBackMenuVoiceOver(string key)
    {
        if (GlobalAudioSrc.Instance.SecondAudioSrc.isPlaying && GlobalAudioSrc.Instance.SecondAudioSrc.clip.name == key)
            return;
        GlobalAudioSrc.Instance.SecondAudioSrc.clip = null;

    //    AudioClip _Clip = Resources.Load<AudioClip>("VoiceOvers/" + LanguageHandler.instance.Languages[LanguageHandler.instance.CurrentLanguageIndex].LanguageID + "/" + key);
		AudioClip _Clip;
		_Clip = Resources.Load < AudioClip >("VoiceOvers/" + LanguageHandler.instance.Languages[LanguageHandler.instance.CurrentLanguageIndex].LanguageID + "/" + key);

//		if ( PlayerPrefs.GetString("currentLanguage") == defaultLanguage) 
//		{
//			//Debug.Log("Audio file path of Rest"+PlayerPrefs.GetString("currentLanguage"));
//
//		}
//		else 
//		{ 
//			_Clip = AssetBundleManager.Instance.GetAssetBundle(PlayerPrefs.GetString("currentLanguage")).LoadAsset<AudioClip>(key);
//
//			if (_Clip == null)
//			{
//				return;
//			}
//		}
        GlobalAudioSrc.Instance.SecondAudioSrc.clip = _Clip;
        GlobalAudioSrc.Instance.SecondAudioSrc.PlayOneShot(_Clip);
    }

    public void StopBackMenuVoiceOver()
    {
        if (GlobalAudioSrc.Instance == null)
            return;

        GlobalAudioSrc.Instance.SecondAudioSrc.Stop();
    }

    public bool CheckIfLanguageExist(string _currentLanguage)
    {
        List < SmartCultureInfo > languages = SmartLocalization.LanguageManager.Instance.GetSupportedLanguages();

        for (int i = 0; i < languages.Count; i++)
        {
            if (languages[i].languageCode == _currentLanguage)
            {
                return true;
            }
        }
        return false;
    }
}
