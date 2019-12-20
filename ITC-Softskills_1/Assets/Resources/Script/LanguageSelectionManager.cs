using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;
using System.IO;

public class LanguageSelectionManager : MonoBehaviour
{
    private static LanguageSelectionManager instance;
    public static LanguageSelectionManager Instance
    {
        get
        {
            if (instance == null)
                instance = FindObjectOfType<LanguageSelectionManager>();
            return instance;
        }
    }
    public GameObject Languagebutton;
    private GameObject default_LangButton;
    List<GameObject> buttons = new List<GameObject>();
	public static bool isWrongModule;



    void Awake ()
    {
        //ContentProvider.instance.SetKey("language", "english");
        PlayerPrefs.SetString("currentLanguage", "en-US");
    }
    void Start()
    {
		
        Languagebutton.name = LanguageHandler.instance.Languages[0].DisplayName;
        Languagebutton.GetComponentInChildren<Text>().text = LanguageHandler.instance.Languages[0].DisplayName;
        buttons.Add(Languagebutton);

        for (int i = 1; i < LanguageHandler.instance.Languages.Count; i++)
        {
            GameObject temp = Instantiate(Languagebutton, Languagebutton.transform.parent) as GameObject;
            temp.name = LanguageHandler.instance.Languages[i].DisplayName;
            temp.GetComponentInChildren<Text>().text = LanguageHandler.instance.Languages[i].DisplayName;
            buttons.Add(temp);
        }

        buttons[LanguageHandler.instance.CurrentLanguageIndex].GetComponentInChildren<Text>().color = new Color32(105, 223, 0, 255);
        buttons[LanguageHandler.instance.CurrentLanguageIndex].GetComponent<EventTrigger>().enabled = false;
        default_LangButton = buttons[0] ;    
    }

    GameObject ClickedButton ;

	public void OnLanguageSelect (GameObject Button)
	{
		ClickedButton = Button;
		Debug.Log ("last saved language1 " + PlayerPrefs.GetString ("currentLanguage"));
		if (PlayerPrefs.GetString ("currentLanguage") == "en-US") {
            Debug.Log("");
			if (!isWrongModule) {
				//LoginManager.instance.EnableMainMenu ();
			//	LoginManager.instance.SameLanguageSelected ();
				Debug.Log ("enable main menu");
			} else {
				//LoginManager.instance.ShowWrongModulePanel ();
				Debug.Log ("show wrong panel");
			}
			Debug.Log ("current language " + LanguageHandler.instance.Languages [ClickedButton.transform.GetSiblingIndex ()].LanguageID);
			#if UNITY_ANDROID && !UNITY_EDITOR
			//ContentProvider.instance.SetKey ("currentLanguage",PlayerPrefs.GetString("currentLanguage"));
			#endif
		} else {
			OnClickLanguageButton (Button);
		}
	}


    public void OnClickLanguageButton(GameObject Button)
    {
		if (!isWrongModule) {
		//	LoginManager.instance.EnableMainMenu ();
			Debug.Log ("enable main menu");
		} else {
		//	LoginManager.instance.ShowWrongModulePanel ();
			Debug.Log ("show wrong panel");
		}

		Debug.Log ("Is wrong module - " + isWrongModule);

		if (MapManager.instance != null)
			MapManager.instance.ResetMap ();

        ClickedButton = Button;

		PlayerPrefs.SetString("currentLanguage", LanguageHandler.instance.Languages[ClickedButton.transform.GetSiblingIndex()].LanguageID);
		#if UNITY_ANDROID && !UNITY_EDITOR
		ContentProvider.instance.SetKey ("currentLanguage",PlayerPrefs.GetString("currentLanguage"));
		#endif

        // try to fetch the file from path
		if ( AssetBundleManager.Instance.IsFileExist() || (PlayerPrefs.GetString("currentLanguage") == LanguageHandler.defaultLanguage)|| 
			(PlayerPrefs.GetString("currentLanguage") == "hi-IN"))//||(PlayerPrefs.GetString("currentLanguage") == "zh-CN")|| (PlayerPrefs.GetString("currentLanguage") == "ar-AR") || (PlayerPrefs.GetString("currentLanguage") == "en-GB"))  
		{
            StartCoroutine("DelayChangeButtonFunctions");
            return;
        }
        // if the file is not on the path give inst to download the bundle
      //  Debug.Log("NotGotTheBundleInClickingChooseButton");
        InAbsenseOfAssetBundle.Instance.EnableAskForDownload();



		//--------------Three
		if (ContentProvider.instance.GetValue ("current_module") != Application.identifier.ToString ()) 
		{
		/*	if (LoginManager.instance != null)
				LoginManager.instance.ShowWrongModulePanel ();*/
		}


    }

	public void OnSelectLanguage(string id)
	{

		if (!isWrongModule) {
		//	LoginManager.instance.EnableMainMenu ();
			Debug.Log ("enable main menu");
		} else {
		//	LoginManager.instance.ShowWrongModulePanel ();
			Debug.Log ("show wrong panel");
		}

		if (MapManager.instance != null)
			MapManager.instance.ResetMap ();

		Debug.Log ("Is wrong module - " + isWrongModule);

		PlayerPrefs.SetString ("currentLanguage", id);

		#if UNITY_ANDROID && !UNITY_EDITOR
		ContentProvider.instance.SetKey ("currentLanguage",PlayerPrefs.GetString("currentLanguage"));
		#endif


		// try to fetch the file from path
		if (AssetBundleManager.Instance.IsFileExist () || (PlayerPrefs.GetString ("currentLanguage") == LanguageHandler.defaultLanguage) ||
			(PlayerPrefs.GetString ("currentLanguage") == "hi-IN")) {//||(PlayerPrefs.GetString("currentLanguage") == "zh-CN")|| (PlayerPrefs.GetString("currentLanguage") == "ar-AR") || (PlayerPrefs.GetString("currentLanguage") == "en-GB"))  
			StartCoroutine ("DelayChangeButtonFunctions");
			return;
		}
		// if the file is not on the path give inst to download the bundle
		//  Debug.Log("NotGotTheBundleInClickingChooseButton");
		InAbsenseOfAssetBundle.Instance.EnableAskForDownload ();

		//--------------Three
		if (ContentProvider.instance.GetValue ("current_module") != Application.identifier.ToString ()) 
		{
		/*	if (LoginManager.instance != null)
				LoginManager.instance.ShowWrongModulePanel ();*/
		}



	}

	// Call in choose default language method
    public void SetDefaultSettings()
    {
        ClickedButton = default_LangButton;
        StartCoroutine("DelayChangeButtonFunctions");
    }
    public IEnumerator DelayChangeButtonFunctions() 
	{
        makeDefaultcolor();
        if (ClickedButton != null)
        {
            ClickedButton.transform.GetChild(0).GetComponentInChildren<Text>().color = new Color32(105, 223, 0, 255);
            ClickedButton.GetComponent<EventTrigger>().enabled = false;    
        }

        LanguageHandler.instance.setCurrentLanguage();
        LanguageHandler.instance.changeLanguageUpdate();
        if ( ReverseMenu.Instance != null)
            ReverseMenu.Instance.Reverse();
		if (CloseButtonSwapping.Instance != null)
			CloseButtonSwapping.Instance.Swap_Btns();
        yield return null;
    }
    void makeDefaultcolor()
    {
        if (buttons.Count <= 0)
            return;
        for (int i = 0; i < buttons.Count; i++)
        {
            buttons[i].GetComponentInChildren<Text>().color = new Color32(233, 233, 233, 255);
            buttons[i].GetComponent<EventTrigger>().enabled = true;
        }
    }

    public void OnOffTable(GameObject Table)
    {
        Table.SetActive(!Table.activeInHierarchy);
		//SoundManager.Instance.PlayClickSound();
    }

    public void TableDisable(GameObject obj)
    {
        obj.SetActive(false);
		//SoundManager.Instance.PlayClickSound();
    }
}
