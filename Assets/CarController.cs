using UnityEngine;
 using System.Collections;
 
 public class CarController : MonoBehaviour {
 
     public float acceleration;
     public float steering;
     public float maxSpeed;
     public float rozamiento;
     private Rigidbody2D rb;
	 public Transform camtransform;
	 float currentSteering;
 
     void Start () {
         rb = GetComponent<Rigidbody2D>();
         currentSteering = steering;
     }
 
     void FixedUpdate () {
     	 currentSteering = steering;
         float h = -Input.GetAxis("Horizontal");
         float v = Input.GetAxis("Vertical");

         if (v < 0) v/= 2; //MARCHA ATRÁS MÁS LENTA!


         //RETOQUE
         if (v==0) currentSteering = steering * 1.25f;
		 if (v<0) currentSteering = steering * 1.5f;

        // if (v == 0 && rb.velocity.magnitude > 0) v -= rozamiento;   INTENTO DE ROZAMIENTO FALLIDO XD
 
         Vector2 speed = transform.up * (v * acceleration);
//         print(rb.velocity.magnitude);
         currentSteering = currentSteering-rb.velocity.magnitude/10;
         if (rb.velocity.magnitude < maxSpeed) rb.AddForce(speed);  //AQUI VA LA MAXSPEED

 		 //if (rb.velocity. > maxSpeed) rb.velocity = maxSpeed;

         float direction = Vector2.Dot(rb.velocity, rb.GetRelativeVector(Vector2.up));
         if(direction >= 0.0f) {
             rb.rotation += h * currentSteering * (rb.velocity.magnitude / 5.0f);
             //rb.AddTorque((h * steering) * (rb.velocity.magnitude / 10.0f));
         } else {
             rb.rotation -= h * currentSteering * (rb.velocity.magnitude / 5.0f);
             //rb.AddTorque((-h * steering) * (rb.velocity.magnitude / 10.0f));
         }
 
         Vector2 forward = new Vector2(0.0f, 0.5f);
         float steeringRightAngle;
         if(rb.angularVelocity > 0) {
             steeringRightAngle = -90;
         } else {
             steeringRightAngle = 90;
         }
 
         Vector2 rightAngleFromForward = Quaternion.AngleAxis(steeringRightAngle, Vector3.forward) * forward;
         Debug.DrawLine((Vector3)rb.position, (Vector3)rb.GetRelativePoint(rightAngleFromForward), Color.green);
 
         float driftForce = Vector2.Dot(rb.velocity, rb.GetRelativeVector(rightAngleFromForward.normalized));
 
         Vector2 relativeForce = (rightAngleFromForward.normalized * -1.0f) * (driftForce * 10.0f);
 
 
         Debug.DrawLine((Vector3)rb.position, (Vector3)rb.GetRelativePoint(relativeForce), Color.red);
 
         rb.AddForce(rb.GetRelativeVector(relativeForce));
		 camtransform.position = transform.position - new Vector3 (0f, 0f, 10f);
     }
 }