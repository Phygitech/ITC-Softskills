using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;
using LitJson;

public class ForgetPasswordManager : MonoBehaviour
{

	public string otpSendUrl = "http://192.168.1.210/ITC-Development/deepak.gupta/public/api/forgot-password";
	public InputField userName;


	// Use this for initialization
	void Start ()
	{
		//StartCoroutine (HitApiSendOTP ());
	}
	
	// Update is called once per frame
	void Update ()
	{
		
	}


	bool CheckValidation ()
	{
		var username = userName.text.ToString ();

		if (username.Trim ().Length < 1)
			return false;

		return true;
	}


	IEnumerator HitApiSendOTP ()
	{
//		if (!CheckValidation ()) {
//			Debug.Log ("VAlidation Failed");
//			yield break;
//		}

		WWWForm form = new WWWForm ();
		ParameterModule param = new ParameterModule ();

		Dictionary<string, string> headers = form.headers;

		string jsonData = JsonUtility.ToJson (param);
		byte[] postData = System.Text.Encoding.ASCII.GetBytes (jsonData);

		headers ["Authorization"] = "Basic " + System.Convert.ToBase64String (System.Text.Encoding.ASCII.GetBytes ("unity:@piun!ty"));


		if (headers.ContainsKey ("Content-Type")) {
			headers ["Content-Type"] = "application/json";
		} else {
			headers.Add ("Content-Type", "application/json");
		}


		using (WWW www = new WWW (otpSendUrl, postData, headers)) {
			yield return www;

			yield return new WaitUntil (() => www.isDone);

			if (www.error == null) {
				OnOTPSendSeccess (JsonMapper.ToObject (www.text));
				//print (www.text);
			} else {
				OnOTPSendFailed (www.error);
				//print (www.error);
			}
		}			
	}



	void OnOTPSendSeccess (JsonData jsonData)
	{
		if (jsonData ["status"].ToString () == "200") {
			print ("USer id : " + jsonData ["detail"] ["data"] ["USER_ID"].ToString ().Trim ('"'));
			print ("OTP : " + jsonData ["detail"] ["data"] ["OTP"].ToString ().Trim ('"'));
		}
	}


	void OnOTPSendFailed (string message)
	{
		Debug.Log ("OTP not send");
	}


	[Serializable]
	public class ParameterModule
	{
		public string username = "2YIMRGWMG9";
		public string param = "generateotp";
	}


}
