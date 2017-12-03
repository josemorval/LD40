using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FadeOutBlackPlane : MonoBehaviour {

	public bool fadedOut = false;
	public bool fadingIn = false;
	public float fadeRate;
	Material rend;
	// Use this for initialization
	void Start () {
		FadeOut();
		rend = this.gameObject.GetComponent<SpriteRenderer>().material;
	}
	
	// Update is called once per frame
	void Update () {
		if ((Input.GetKey(KeyCode.Space) || Input.GetKey(KeyCode.Mouse0) || Input.GetKey(KeyCode.Mouse1))){
			//if (!fadingIn){
			//	fadingIn = true;			
				//FadeIn();
			//}

             rend.color = new Color(rend.color.r, rend.color.g, rend.color.b, rend.color.a + fadeRate/10);
           //  mainAudio.GetComponent<AudioSource>().volume = 0.5f + i/2;
            
        

		}else{
			rend.color = new Color(rend.color.r, rend.color.g, rend.color.b, rend.color.a - fadeRate/10);
			//fading = false;
			//print ("asf");
			//FadeOut();
		}
		if (rend.color.a > 0.99f)  rend.color = new Color(rend.color.r, rend.color.g, rend.color.b, 1);
		if (rend.color.a < 0.01f)  rend.color = new Color(rend.color.r, rend.color.g, rend.color.b, 0);
	}

	public void FadeOut(){
		StartCoroutine(FadeOut(this.gameObject, fadeRate));
	}

	public void FadeIn(){
		StartCoroutine(FadeIn(this.gameObject, fadeRate));
	}

	IEnumerator FadeOut(GameObject plane, float fadeRate) {
		//fading = true;
		//plane.GetComponent<MeshRenderer>().enabled = true;
		//yield return new WaitForSeconds(2);
		Material rend = plane.GetComponent<SpriteRenderer>().material;
        for (float i = rend.color.a; i>0; i-=fadeRate/10) {
             rend.color = new Color(rend.color.r, rend.color.g, rend.color.b, i);
           //  mainAudio.GetComponent<AudioSource>().volume = 0.5f + i/2;
             yield return null;
        }
		rend.color = new Color(rend.color.r, rend.color.g, rend.color.b, 0);
        //yield return new WaitForSeconds(2);
        fadedOut = true; 
		//fading = false;
    }

	IEnumerator FadeIn(GameObject plane, float fadeRate) {
		//fading = true;
		//plane.GetComponent<MeshRenderer>().enabled = true;
		//yield return new WaitForSeconds(2);
		Material rend = plane.GetComponent<SpriteRenderer>().material;
		for (float i = 0; i<1; i+=fadeRate/10) {
             rend.color = new Color(rend.color.r, rend.color.g, rend.color.b, i);
           //  mainAudio.GetComponent<AudioSource>().volume = 0.5f + i/2;
             yield return null;
        }
		rend.color = new Color(rend.color.r, rend.color.g, rend.color.b, 0);
        //yield return new WaitForSeconds(2);
        fadedOut = false; 
        fadingIn = false;
    }
}
