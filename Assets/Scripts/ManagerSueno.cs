using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ManagerSueno : MonoBehaviour
{
	//public bool durmiendo = false;
	public GameObject pestañaArriba;
	public GameObject pestañaAbajo;
	float pestañaASize, pestañaBSize;
	float posicionA, posicionB;

	public float incrementoSueño;
	public float rateRecuperacionSueño;
	public float sueno = 0;
	float tiempoPunish = 0;
	float modificadorCancion = 1;
	//public FadeOutBlackPlane fade;

	public float ratePunishLvl1, ratePunishLvl2, duracionPunishLvl1, duracionPunishLvl2;

	[SerializeField]
	private SoundManagerScript _managerSonido;


	//Fade
	public bool fadedOut = false;
	public bool fadingIn = false;
	public float fadeRate;
	public bool durmiendo = false;
	Material rend;
	public GameObject planoNegro;


	//void StartManager()
	public void StartFake ()
	{
		pestañaASize = pestañaArriba.transform.localScale.x;
		pestañaBSize = pestañaAbajo.transform.localScale.x;
		posicionA = pestañaArriba.transform.position.y;
		posicionB = pestañaAbajo.transform.position.y;

		//Fade
		//FadeOut ();
		rend = planoNegro.GetComponent<SpriteRenderer> ().material;
	}

	//public void UpdateManager();
	// Update is called once per frame
	public void FixedUpdateFake ()
	{
		if (!durmiendo) {
			sueno += incrementoSueño * modificadorCancion;
			if (sueno > 100)
				sueno = 100;
			print (sueno);
		} else {
			if (tiempoPunish > 0) sueno -= rateRecuperacionSueño/2 - incrementoSueño/2;
			else sueno -= rateRecuperacionSueño - incrementoSueño;

			if (sueno <= 0)
				sueno = 0;
			print (sueno);
		}


		//PENALIZACION SUEÑO VERSIÓN 1
		/*
		if (tiempoPunish<=0){
			if (sueno >= 33 && sueno <=65){
				float rand = Random.Range(0f,1f);
				if (rand <= ratePunishLvl1) tiempoPunish = duracionPunishLvl1 + Random.Range(0f,1f)/5 - 0.1f;
			}else if (sueno>65){
				float rand = Random.Range(0,1);
				if (rand <= ratePunishLvl2) tiempoPunish = duracionPunishLvl2+  Random.Range(0f, 4f) - 2f;
			}
		}
		*/

		if (tiempoPunish<=0){
			if (sueno >= 33){
				float rand = Random.Range(0f,1f);
				if (rand <= ratePunishLvl1 + sueno/12000) tiempoPunish = duracionPunishLvl1 - Random.Range(0f,1f)/5 + sueno/100;
			}if (sueno>66){
				float rand = Random.Range(0f,1f);
				if (rand <= ratePunishLvl2 + sueno/12000) tiempoPunish = duracionPunishLvl2 +  Random.Range(0f,2f) - 1f;
			}
		}

		if ((Input.GetKey (KeyCode.LeftShift)  || Input.GetKey (KeyCode.C))){
			_managerSonido.VolumeControl("Switch", 2f);
			_managerSonido.PlaySound("Switch", false);
			_managerSonido.PlayRandomSound("Canciones", true);


		}



		if ((Input.GetKey (KeyCode.Space) || Input.GetKey (KeyCode.Mouse0) || Input.GetKey (KeyCode.Mouse1) || tiempoPunish > 0)) {
			//fade negro
			rend.color = new Color (rend.color.r, rend.color.g, rend.color.b, rend.color.a + fadeRate / 7);
			if (tiempoPunish>0) tiempoPunish -= Time.fixedDeltaTime;  
			//pestañas
			if (pestañaArriba.transform.localPosition.y > 17.5f)
				pestañaArriba.transform.position = new Vector3 (pestañaArriba.transform.position.x, pestañaArriba.transform.position.y - fadeRate, pestañaArriba.transform.position.z);       
			if (pestañaAbajo.transform.localPosition.y < -17.5f)
				pestañaAbajo.transform.position = new Vector3 (pestañaAbajo.transform.position.x, pestañaAbajo.transform.position.y + fadeRate, pestañaAbajo.transform.position.z);     
		} else {
			//fade negro
			rend.color = new Color (rend.color.r, rend.color.g, rend.color.b, rend.color.a - fadeRate / 7);
			//pestañas
			if (pestañaArriba.transform.localPosition.y < 23)
				pestañaArriba.transform.position = new Vector3 (pestañaArriba.transform.position.x, pestañaArriba.transform.position.y + fadeRate, pestañaArriba.transform.position.z);       
			if (pestañaAbajo.transform.localPosition.y > -23)
				pestañaAbajo.transform.position = new Vector3 (pestañaAbajo.transform.position.x, pestañaAbajo.transform.position.y - fadeRate, pestañaAbajo.transform.position.z);     
		}
		if (rend.color.a > 0.99f)
			rend.color = new Color (rend.color.r, rend.color.g, rend.color.b, 1);
		if (rend.color.a < 0.01f)
			rend.color = new Color (rend.color.r, rend.color.g, rend.color.b, 0);
		if (rend.color.a > 0.5f)
			durmiendo = true;
		else
			durmiendo = false;

	}




	public void FadeOut ()
	{
		StartCoroutine (FadeOut (this.gameObject, fadeRate));
	}

	public void FadeIn ()
	{
		StartCoroutine (FadeIn (this.gameObject, fadeRate));
	}

	IEnumerator FadeOut (GameObject plane, float fadeRate)
	{
		//fading = true;
		//plane.GetComponent<MeshRenderer>().enabled = true;
		//yield return new WaitForSeconds(2);
		Material rend = plane.GetComponent<SpriteRenderer>().material;
		for (float i = rend.color.a; i > 0; i -= fadeRate / 10) {
			rend.color = new Color (rend.color.r, rend.color.g, rend.color.b, i);
			//  mainAudio.GetComponent<AudioSource>().volume = 0.5f + i/2;
			yield return null;
		}
		rend.color = new Color (rend.color.r, rend.color.g, rend.color.b, 0);
		//yield return new WaitForSeconds(2);
		fadedOut = true; 
		//fading = false;
	}

	IEnumerator FadeIn (GameObject plane, float fadeRate)
	{
		//fading = true;
		//plane.GetComponent<MeshRenderer>().enabled = true;
		//yield return new WaitForSeconds(2);
		Material rend = plane.GetComponent<SpriteRenderer> ().material;
		for (float i = 0; i < 1; i += fadeRate / 10) {
			rend.color = new Color (rend.color.r, rend.color.g, rend.color.b, i);
			//  mainAudio.GetComponent<AudioSource>().volume = 0.5f + i/2;
			yield return null;
		}
		rend.color = new Color (rend.color.r, rend.color.g, rend.color.b, 0);
		//yield return new WaitForSeconds(2);
		fadedOut = false; 
		fadingIn = false;
	}
}
