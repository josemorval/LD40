using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarScript : MonoBehaviour
{

	public float vel;
	public float rotvel;
	public Transform camtransform;

	// Use this for initialization
	void Start ()
	{
		
	}
	
	// Update is called once per frame
	void Update ()
	{
		if (Input.GetKey (KeyCode.UpArrow)) {
			transform.position -= vel * transform.right * Time.deltaTime;
		}

		if (Input.GetKey (KeyCode.LeftArrow)) {
			transform.Rotate (new Vector3 (0f, 0f, rotvel * Time.deltaTime));
		} else if (Input.GetKey (KeyCode.RightArrow)) {
			transform.Rotate (new Vector3 (0f, 0f, -rotvel * Time.deltaTime));
		}


		camtransform.position = transform.position - new Vector3 (0f, 0f, 10f);
	}
}
