using UnityEditor;
using UnityEditorInternal;
using UnityEditorInternal.VR;
using UnityEngine;
using UnityEngine.VR;

public class TimeScaleSelectorEditor : Editor
{
    static string[] _cardBoard = {"Time Scale"};


	[MenuItem("Time Scale/1")]
    static void _SetTimeScale_1()
    {
		Time.timeScale = 1;
    }

	[MenuItem("Time Scale/3")]
	static void _SetTimeScale_3()
    { 
		Time.timeScale = 3;
    }

	[MenuItem("Time Scale/5")]
	static void _SetTimeScale_5()
    {
		Time.timeScale = 5;
    }

	[MenuItem("Time Scale/10")]
	static void _SetTimeScale_10()
	{
		Time.timeScale = 10;
	}
}
