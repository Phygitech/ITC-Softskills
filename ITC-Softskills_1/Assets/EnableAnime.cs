using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnableAnime : MonoBehaviour {

	// Use this for initialization


    public GameObject Anim1,Anim2;

	public void EnableAnim () {
		
        //Anim1.SetActive(false);
        Anim2.SetActive(true);

	}
}
