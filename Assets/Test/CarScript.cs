using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarScript : MonoBehaviour
{

	public float vel;
	public float rotvel;
	public float otherrotvel;
	public Transform camtransform;
	public Rigidbody2D rigidbody;

	// Use this for initialization
	void Start ()
	{
		rigidbody = GetComponent<Rigidbody2D> ();
	}
	
	// Update is called once per frame
	void Update ()
	{
		if (Input.GetKey (KeyCode.UpArrow)) {
			rigidbody.AddForce (-transform.right * vel * Time.deltaTime);
		} else if (Input.GetKey (KeyCode.DownArrow)) {
			rigidbody.AddForce (transform.right * vel * Time.deltaTime);
		}

		Vector2 v = new Vector2 (transform.up.x, transform.up.y);
		if (Input.GetKey (KeyCode.LeftArrow)) {
			rigidbody.velocity = rigidbody.velocity.magnitude * RotateVector (rigidbody.velocity.normalized, -rotvel * Time.deltaTime);
			transform.Rotate (new Vector3 (0f, 0f, otherrotvel * Time.deltaTime));
		} else if (Input.GetKey (KeyCode.RightArrow)) {
			rigidbody.velocity = rigidbody.velocity.magnitude * RotateVector (rigidbody.velocity.normalized, rotvel * Time.deltaTime);
			transform.Rotate (new Vector3 (0f, 0f, -otherrotvel * Time.deltaTime));


		}
			
		camtransform.position = transform.position - new Vector3 (0f, 0f, 10f);
	}

	public Vector2 RotateVector (Vector2 v, float angle)
	{
		Vector2 vv = Vector2.zero;
		vv.x = Mathf.Cos (angle) * v.x + Mathf.Sin (angle) * v.y;
		vv.y = -Mathf.Sin (angle) * v.x + Mathf.Cos (angle) * v.y;

		return vv;
	}
}
