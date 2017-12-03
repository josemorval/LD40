using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ManagerSueño : MonoBehaviour {
	public bool durmiendo = false;
	public float incrementoSueño;
	public float sueño = 0;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void FixedUpdate () {
		if (!durmiendo){
			sueño += incrementoSueño;
			if (sueño>100) sueño = 100;
			print(sueño);
		}else{
			sueño -= incrementoSueño*5;
			if (sueño <= 0) sueño = 0;
		}
	}
}
