#if UNITY_EDITOR
using UnityEditor;
using UnityEditor.Callbacks;
using UnityEditor.Build;
using System.IO;
using UnityEngine.VR;
using UnityEditorInternal.VR;

class PreBuild : IPreprocessBuild
{
    public int callbackOrder { get { return 0; } }

	const string assetPathOculus="Assets/AdvancementIntegration/Wrapper/DevelopmentGearVR.xml";

	const string assetPathOculusRelease="Assets/AdvancementIntegration/Wrapper/ReleaseGearVR.xml";

	const string assetPathgoogleVr="Assets/AdvancementIntegration/Wrapper/DevelopmentAndroidManifest.xml";

	const string assetPathgoogleVrRelease="Assets/AdvancementIntegration/Wrapper/ReleaseAndroidManifest.xml";

	const string assetPathManifest="Assets/Plugins/Android/AndroidManifest.xml";

	static string[] _multiPlatformDaydream = {"daydream","cardboard"};

    //Gets called before build is executed]
    public void OnPreprocessBuild(BuildTarget target, string path)
    {
        CheckDevelopment();
    }

	void CheckDevelopment()
    {
		AssetDatabase.DeleteAsset("Assets/Plugins/Android/AndroidManifest.xml");
		if (EditorUserBuildSettings.development) {
			if (UnityEngine.XR.XRSettings.supportedDevices [0] == "Oculus") {
				AssetDatabase.CopyAsset (assetPathOculus, assetPathManifest);
			}
			else
				AssetDatabase.CopyAsset (assetPathgoogleVr, assetPathManifest);
		} else {
			if (UnityEngine.XR.XRSettings.supportedDevices [0] == "Oculus")
				AssetDatabase.CopyAsset (assetPathOculusRelease, assetPathManifest);
			else {
				PlayerSettings.use32BitDisplayBuffer = false;
				VREditor.SetVREnabledOnTargetGroup(BuildTargetGroup.Android, true);
				VREditor.SetVREnabledDevicesOnTargetGroup(BuildTargetGroup.Android, _multiPlatformDaydream);
				PlayerSettings.Android.minSdkVersion = AndroidSdkVersions.AndroidApiLevel19;

				AssetDatabase.CopyAsset (assetPathgoogleVrRelease, assetPathManifest);
			}

		}

 
    }
}
#endif			