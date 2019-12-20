using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LoadingScene : MonoBehaviour
{
    static int loadingSceneIndex;
    static bool FisrtTime;

    public static int LoadingSceneIndex
    {
        set
        {
            loadingSceneIndex = value;
            SceneManager.LoadScene("LoadingScene");
        }
    }

    //public Text ProgressText;
    public Image LoadingBar;

    AsyncOperation _AO;

    // Use this for initialization


    void Start()
    {
		
        if (!FisrtTime)
        {
            if (!PlayerPrefs.HasKey("PlayLevel"))
                PlayerPrefs.SetString("PlayLevel", "MainMenu");
		
            #region ToGetVersion	
            if (PlayerPrefs.GetString("PlayLevel").Contains("_"))
            {
                string[] version;
                version = PlayerPrefs.GetString("PlayLevel").Split('_');
                try
                {
                    PlayerPrefs.SetString("PlayLevel", version[0]);
                    PlayerPrefs.SetString("PlayVersion", version[1]);	
                }
                catch
                {
                    PlayerPrefs.SetString("PlayLevel", version[0]);
                    PlayerPrefs.SetString("PlayVersion", "0");	
                }
            }
            #endregion
            //	Debug.Log ("TestingSetSceneWith"+PlayerPrefs.GetString("SetScene"));
            _AO = SceneManager.LoadSceneAsync(PlayerPrefs.GetString("PlayLevel"));

            if (_AO == null)
                _AO = SceneManager.LoadSceneAsync("MainMenu");

            FisrtTime = true;
        }
        else
        {
            _AO = SceneManager.LoadSceneAsync(loadingSceneIndex);
        }

        _AO.allowSceneActivation = false;
        StartCoroutine(StartLoadingBar());
    }

    IEnumerator StartLoadingBar()
    {
        while (_AO.progress < 0.9f)
        {
            LoadingBar.fillAmount = Mathf.Lerp(LoadingBar.fillAmount, _AO.progress, 0.1f);
            yield return new WaitForSeconds(0.1f);
        }

        while (LoadingBar.fillAmount < 1)
        {
            yield return new WaitForSeconds(0.1f);
            LoadingBar.fillAmount += 0.1f;
        }

        LoadingBar.fillAmount = 1;
        _AO.allowSceneActivation = true;
    }
}
