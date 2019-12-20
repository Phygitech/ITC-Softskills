using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using LitJson;

public class Hit : MonoBehaviour {

    private const string baseUrl = "http://192.168.1.210/ITC-Development/deepak.gupta/public";
    private const string userLogin = "/api/userlogin";
    private const string updateUser = "/api/update-user";
    private const string userMarkets = "/api/user-markets";
    private const string forgotPassword = "/api/forgot-password";
    private const string characterMasterList = "/api/character-master";
    private const string validateTraineeActivity = "/api/validate-trainee-activity";
    private const string saveTraineeActivity = "/api/trainee-activity";
    private const string getTraineeActivityModuleWise = "/api/trainee-activity";
    private const string leaderBoardForTrainee = "/api/leader-board";
    private const string API_KEY = "";

    private string jsonString;
    private JsonData itemData;

	// Use this for initialization
	void Start () {
        jsonString = File.ReadAllText(Application.dataPath + "/Resources/items.json");
        itemData = JsonMapper.ToObject(jsonString);
        Debug.Log("hit test" + jsonString);
        Debug.Log(itemData["Weapons"][0]["name"]);
        Debug.Log(GetItem ("right", "Weapons")["power"]);
	}
	
    JsonData GetItem (string name, string type)
    {
        for (int i = 0; i < itemData [type].Count; i++)
        {
            if (itemData[type][i]["name"].ToString() == name)
                return itemData[type][i];
        }

        return null;
    }

    public void Request ()
    {
        WWWForm form = new WWWForm();
        Dictionary<string, string> headers = form.headers;
        headers[""] = API_KEY;

        form.AddField("username", "Mike");
        byte[] rawFormData = form.data;

        WWW request = new WWW(baseUrl, null, headers);
        StartCoroutine(OnResponse (request));
    }

    private IEnumerator OnResponse (WWW req)
    {
        yield return req;
        Debug.Log("Response " + req.text);
    }
}
