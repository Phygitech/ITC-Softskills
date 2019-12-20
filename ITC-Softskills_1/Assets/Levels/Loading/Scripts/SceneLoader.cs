using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SceneLoader : MonoBehaviour
{
    public static SceneLoader instance;

    void Start()
    {
        instance = this;
    }

    public void LoadScene(int BuildIndex)
    {
        SoundManager.instance.PlayClickSound();
        LoadingScene.LoadingSceneIndex = BuildIndex;
    }

    public void Quit()
    {
        SoundManager.instance.PlayClickSound();
        #if !UNITY_EDITOR
        System.Diagnostics.Process.GetCurrentProcess().Kill();
        #endif
        Debug.Log("Quit");
        Application.Quit();
    }
}
