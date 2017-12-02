using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoadVelScript : MonoBehaviour
{

	public Material mat;

	void Start ()
	{
		mat = GetComponent<MeshRenderer> ().sharedMaterial;
	}

	void Update ()
	{
		mat.mainTextureOffset += new Vector2 (0f, Time.deltaTime);
	}
}
