using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class L4_AnimActivities : MonoBehaviour {

    public GameObject vr_Player;
    public GameObject Fade;
    public List<GameObject> vrPos;
	// Use this for initialization
	void Start () {
        
	}
	
	
    public void FadeInOut(string name)
    {
        GameObject pos = vrPos.Find(x => x.tag == name);
        print(name);

        Fade.SetActive(true);
//		Fade.GetComponent<Animation>()["fade_effect"].speed = .5f;
        StartCoroutine(FadeIn(pos));

    }

    IEnumerator FadeIn(GameObject vr_pos)
    {
        
        yield return new WaitForSeconds(2f);
        vr_Player.transform.position = vr_pos.transform.position;
		yield return new WaitForSeconds(2f);
		Fade.SetActive(false);
        //Fade.GetComponent<Animation>()["fade_effect"].speed = .25f;
       // yield return new WaitForSeconds(4f);
       //Fade.GetComponent<Animation>()["fade_effect"].speed = .5f;
//        yield return new WaitForSeconds(1f);
       // Fade.SetActive(false);

    }










}
