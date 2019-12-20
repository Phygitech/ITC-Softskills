using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.UI;

public class NewBehaviourScript : MonoBehaviour {

    AndroidJavaObject jo;
	public Text inputField;
	public Text text;

	public Text saveText;
	public Text getText;

    void Awake()
    {
		Debug.Log ("C100 before call");
        Init();


    }

    void Init()
    {
        jo = new AndroidJavaObject("com.example.dbprovider.KVDBHandler");
        jo.Call("Init");
		Debug.Log ("C100  after call");
    }


    void SetKey(string Key,string Value)
    {
        jo.Call("SetKey", Key, Value);
    }


    bool HasKey(string Key)
    {
        return jo.Call<bool>("HasKey", Key);
    }


    string GetValue(string Key)
    {
        return jo.Call<string>("GetValue", Key);
    }


    void DeleteKey(string Key)
    {
        jo.Call("DeleteKey", Key);
    }


    void DeleteAllKey()
    {
        jo.Call("DeleteAllKey");
    }



    private void Start()
	{/*
		SetKey ("kk", "jj");
        Debug.Log("C101 " +HasKey("kk") +GetValue("kk"));
        DeleteKey("kk");
        Debug.Log("C103 " + GetValue("kk"));
        */
    }

	public void OnSaveData ()
	{
		string data = inputField.text.ToString ();
		SetKey ("test", data);
		Debug.Log ("Set value " + data);
		saveText.text = data;
	}

	public void OnGetData ()
	{
		getText.text = GetValue ("test").ToString ();
		text.text = GetValue ("test") + "";
		Debug.Log ("Get value " + GetValue ("test"));

	}
}
