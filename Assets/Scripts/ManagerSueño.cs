using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ManagerSueño : MonoBehaviour {
	//public bool durmiendo = false;
	public GameObject pestañaArriba;
	public GameObject pestañaAbajo;
	float pestañaASize, pestañaBSize;
	float posicionA, posicionB;

	public float incrementoSueño;
	public float rateRecuperacionSueño;
	public float sueño = 0;
	//public FadeOutBlackPlane fade;


	//Fade
	public bool fadedOut = false;
	public bool fadingIn = false;
	public float fadeRate;
	public bool durmiendo = false;
	Material rend;
	public GameObject planoNegro;


	// Use this for initialization
	void Start () {
		pestañaASize = pestañaArriba.transform.localScale.x;
		pestañaBSize = pestañaAbajo.transform.localScale.x;
		posicionA = pestañaArriba.transform.position.y;
		posicionB = pestañaAbajo.transform.position.y;

		//Fade
		FadeOut();
		rend = planoNegro.GetComponent<SpriteRenderer>().material;
	}
	
	// Update is called once per frame
	void FixedUpdate () {
		if (!durmiendo){
			sueño += incrementoSueño;
			if (sueño>100) sueño = 100;
			print(sueño);
		}else{
			sueño -= incrementoSueño*rateRecuperacionSueño;
			if (sueño <= 0) sueño = 0;
			print(sueño);
		}

		if ((Input.GetKey(KeyCode.Space) || Input.GetKey(KeyCode.Mouse0) || Input.GetKey(KeyCode.Mouse1))){
			//fade negro
            rend.color = new Color(rend.color.r, rend.color.g, rend.color.b, rend.color.a + fadeRate/7);  
             //pestañas
			if (pestañaArriba.transform.localPosition.y > 17.5f) pestañaArriba.transform.position = new Vector3(pestañaArriba.transform.position.x, pestañaArriba.transform.position.y - fadeRate, pestañaArriba.transform.position.z);       
			if (pestañaAbajo.transform.localPosition.y < -17.5f) pestañaAbajo.transform.position = new Vector3(pestañaAbajo.transform.position.x, pestañaAbajo.transform.position.y + fadeRate, pestañaAbajo.transform.position.z);     
		}else{
			//fade negro
			rend.color = new Color(rend.color.r, rend.color.g, rend.color.b, rend.color.a - fadeRate/7);
			//pestañas
			if (pestañaArriba.transform.localPosition.y < 23) pestañaArriba.transform.position = new Vector3(pestañaArriba.transform.position.x, pestañaArriba.transform.position.y + fadeRate, pestañaArriba.transform.position.z);       
			if (pestañaAbajo.transform.localPosition.y > -23) pestañaAbajo.transform.position = new Vector3(pestañaAbajo.transform.position.x, pestañaAbajo.transform.position.y - fadeRate, pestañaAbajo.transform.position.z);     
		}
		if (rend.color.a > 0.99f)  rend.color = new Color(rend.color.r, rend.color.g, rend.color.b, 1);
		if (rend.color.a < 0.01f)  rend.color = new Color(rend.color.r, rend.color.g, rend.color.b, 0);
		if (rend.color.a > 0.5f) durmiendo = true;
		else durmiendo = false;

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
