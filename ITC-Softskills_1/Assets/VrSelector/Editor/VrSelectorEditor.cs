using UnityEditor;
using UnityEngine.SceneManagement;
using UnityEditor.SceneManagement;
using UnityEditorInternal;
using UnityEditorInternal.VR;
using UnityEngine;
using UnityEngine.VR;
using System.Collections;

public class VrSelectorEditor : EditorWindow
{
    static string[] _Oculus = {"Oculus"};
	static string[] _multiPlatformCardboard = {"cardboard","daydream"};
	static string[] _multiPlatformDaydream = {"daydream","cardboard"};
	static public bool isCardBoard, isDayDream, isGearVr;

	static GameObject obj;
	static Canvas[] _canvas;
	static object[] allobjects;
	static Canvas _camcanvas;

	const string MENU_NAME1 = "SelectPlatform/GearVR";
	const string MENU_NAME2 = "SelectPlatform/MultiplePlatform/DayDream";
	const string MENU_NAME3 = "SelectPlatform/MultiplePlatform/CardBoard";

	static bool enabled_1;
	static bool enabled_2;
	static bool enabled_3;

	static string Datapath = Application.dataPath ;
	static int Count = "Assets".Length; // Assets Folder Count
	static string OculusAndroidPath = "/OculusVR/OVR/Plugins/1.14.1/Android" ;
	static string OculusExtraPath =  "/OculusVR/OVR/Plugins/1.14.1/Extra";

	static string SetupMsg = "Setup is completed For ";
	static string AlreadySetupMsg = "Already Setup For ";

	static VrSelectorEditor()
	{
		VrSelectorEditor.enabled_1 = EditorPrefs.GetBool (VrSelectorEditor.MENU_NAME1, false);
		VrSelectorEditor.enabled_2 = EditorPrefs.GetBool (VrSelectorEditor.MENU_NAME2, false);
		VrSelectorEditor.enabled_3 = EditorPrefs.GetBool (VrSelectorEditor.MENU_NAME3, false);
	}

	[MenuItem(VrSelectorEditor.MENU_NAME1)]
	static void _SetBuildOculus()
	{	
		PlayerPrefs.SetString ("platform","Oculus");

		VrSelectorEditor.enabled_1 = true;
		VrSelectorEditor.enabled_2 = false;
		VrSelectorEditor.enabled_3 = false;
		
		Menu.SetChecked(VrSelectorEditor.MENU_NAME1, VrSelectorEditor.enabled_1);
		Menu.SetChecked(VrSelectorEditor.MENU_NAME2, VrSelectorEditor.enabled_2);
		Menu.SetChecked(VrSelectorEditor.MENU_NAME3, VrSelectorEditor.enabled_3); 

		Setup.Enable_Oculus ();

		string firstvar = PlayerSettings.applicationIdentifier.Substring (0, 3);
		string secondvar = PlayerSettings.applicationIdentifier.Substring (4, 7);
		string _id = "";

		if (PlayerSettings.applicationIdentifier.Length == 20) {
			_id = PlayerSettings.applicationIdentifier.Substring (12, 8);
		}
		if (PlayerSettings.applicationIdentifier.Length == 23) {
			_id = PlayerSettings.applicationIdentifier.Substring (15, 8);
		}

		if (firstvar == "com" && secondvar == "test" && _id!="") 
		{
			PlayerSettings.applicationIdentifier = "com.test.gv." + _id;
		}
		else {
			Debug.Log ("Please check your Package Name");
		}

        PlayerSettings.use32BitDisplayBuffer = true;
        VREditor.SetVREnabledOnTargetGroup(BuildTargetGroup.Android, true);
		VREditor.SetVREnabledDevicesOnTargetGroup(BuildTargetGroup.Android, _Oculus);
        PlayerSettings.Android.minSdkVersion = AndroidSdkVersions.AndroidApiLevel19;

		try
		{
			EditorUtility.DisplayDialog ("Status", SetupMsg +"GearVR", "OK");
		}
		catch
		{
			EditorUtility.DisplayDialog ("Status", AlreadySetupMsg + "GearVR", "OK");
		}
    }


	[MenuItem(VrSelectorEditor.MENU_NAME2)]
	static void _SetBuildDayDream()
    {
		PlayerPrefs.SetString ("platform","daydream");

		VrSelectorEditor.enabled_1 = false;
		VrSelectorEditor.enabled_2 = true;
		VrSelectorEditor.enabled_3 = false;

		Menu.SetChecked(VrSelectorEditor.MENU_NAME1, VrSelectorEditor.enabled_1);
		Menu.SetChecked(VrSelectorEditor.MENU_NAME2, VrSelectorEditor.enabled_2);
		Menu.SetChecked(VrSelectorEditor.MENU_NAME3, VrSelectorEditor.enabled_3);

		Setup.Enable_DayDream_Cardboard ();

		string firstvar = PlayerSettings.applicationIdentifier.Substring (0, 3);
		string secondvar = PlayerSettings.applicationIdentifier.Substring (4, 7);
		string _id = "";

		if (PlayerSettings.applicationIdentifier.Length == 20) {
			_id = PlayerSettings.applicationIdentifier.Substring (12, 8);
		}
		if (PlayerSettings.applicationIdentifier.Length == 23) {
			_id = PlayerSettings.applicationIdentifier.Substring (15, 8);
		}

		if (firstvar == "com" && secondvar == "test" && _id!="") 
		{
			PlayerSettings.applicationIdentifier = "com.test.cd." + _id;
		}
		else {
			Debug.Log ("Please check your Package Name");
		}

        PlayerSettings.use32BitDisplayBuffer = false;
        VREditor.SetVREnabledOnTargetGroup(BuildTargetGroup.Android, true);
		VREditor.SetVREnabledDevicesOnTargetGroup(BuildTargetGroup.Android, _multiPlatformDaydream);
        PlayerSettings.Android.minSdkVersion = AndroidSdkVersions.AndroidApiLevel23;

		try
		{
			EditorUtility.DisplayDialog ("Status", SetupMsg+"DayDream", "OK");
		}
		catch
		{
			EditorUtility.DisplayDialog ("Status", AlreadySetupMsg+"DayDream", "OK");
		}
    }

	[MenuItem(VrSelectorEditor.MENU_NAME3)]
	static void _SetBuildCardBoard()
    {
		PlayerPrefs.SetString ("platform","cardboard");

		VrSelectorEditor.enabled_1 = false;
		VrSelectorEditor.enabled_2 = false;
		VrSelectorEditor.enabled_3 = true;

		Menu.SetChecked(VrSelectorEditor.MENU_NAME1, VrSelectorEditor.enabled_1);
		Menu.SetChecked(VrSelectorEditor.MENU_NAME2, VrSelectorEditor.enabled_2);
		Menu.SetChecked(VrSelectorEditor.MENU_NAME3, VrSelectorEditor.enabled_3);

		Setup.Enable_DayDream_Cardboard ();

		string firstvar = PlayerSettings.applicationIdentifier.Substring (0, 3);
		string secondvar = PlayerSettings.applicationIdentifier.Substring (4, 7);
		string _id = "";

		if (PlayerSettings.applicationIdentifier.Length == 20) {
			_id = PlayerSettings.applicationIdentifier.Substring (12, 8);
		}
		if (PlayerSettings.applicationIdentifier.Length == 23) {
			_id = PlayerSettings.applicationIdentifier.Substring (15, 8);
		}

		if (firstvar == "com" && secondvar == "test" && _id!="") 
		{
			PlayerSettings.applicationIdentifier = "com.test.cd." + _id;
		}
		else {
			Debug.Log ("Please check your Package Name");
		}

        PlayerSettings.use32BitDisplayBuffer = false;
        VREditor.SetVREnabledOnTargetGroup(BuildTargetGroup.Android, true);
		VREditor.SetVREnabledDevicesOnTargetGroup(BuildTargetGroup.Android, _multiPlatformCardboard);
        PlayerSettings.Android.minSdkVersion = AndroidSdkVersions.AndroidApiLevel19;

		try
		{
			EditorUtility.DisplayDialog ("Status", SetupMsg+"CardBoard", "OK");
		}
		catch
		{
			EditorUtility.DisplayDialog ("Status", AlreadySetupMsg+"CardBoard", "OK");
		}
    }
}
