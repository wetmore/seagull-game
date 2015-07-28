using UnityEngine;
using System.Collections;

public class Player : MonoBehaviour {

	void Start () {
		activeState = flightState.HOVER;
	}

	// FixedUpdate is called once per physics frame
	void FixedUpdate () {

		// TODO(dannenberg): move all key press gathering to one place
		UpdateTiltAndRotate ();
		SeekFlightTarget ();
		//Flap ();

	}

	public Transform flightTarget;

	void SeekFlightTarget () {
		Vector2 target = new Vector2 (flightTarget.position.x, flightTarget.position.y);
		            
		transform.position = Vector2.Lerp (transform.position, target, 0.04f);
		           
	}

	private int flapTimer = 0;
	
	public int hoverCooldown = 60;
	public int flapCooldown = 30;
	
	public float hoverForce = 170;
	public float flapForce = 30;

	private enum flightState { HOVER, RISE, DIVE }; 
	private flightState activeState = flightState.HOVER;

	public float moveSpeed = 10;

	void Flap () {

		if (Input.GetKeyDown (KeyCode.UpArrow)) {
			flapTimer = 0;
		}

		if (Input.GetKey (KeyCode.UpArrow)) {
			activeState = flightState.RISE;
		} else {
			activeState = flightState.HOVER;
		}

		if (flapTimer > 0) {
			flapTimer--;
		}

		// constant flapping
		if (flapTimer == 0) {
			switch (activeState) {
			case flightState.HOVER:
				if (Input.GetKey (KeyCode.LeftArrow)) {
					GetComponent<Rigidbody2D> ().transform.Translate(-moveSpeed, 0, 0, Space.World);
				} else if (Input.GetKey (KeyCode.RightArrow)) {
					GetComponent<Rigidbody2D> ().transform.Translate(moveSpeed, 0, 0, Space.World);
				} else if (Input.GetKey (KeyCode.DownArrow)) {
					GetComponent<Rigidbody2D> ().transform.Translate(0, -moveSpeed/2, 0, Space.World);
				} 

				break;
			case flightState.RISE:
				GetComponent<Rigidbody2D> ().AddForce (transform.up * flapForce);
				flapTimer = flapCooldown;
				break;
			}

			print ("FLAP");
		}

	}

	public float rotationSpeed = 5;
	private float tilt = 0; // tilt angle in degrees
	public float maxTilt = 45;

	void UpdateTiltAndRotate () {
		if (Mathf.Abs (tilt) < 0.001)
			tilt = 0;
		
		if (Input.GetKey (KeyCode.LeftArrow)) {
			tilt += rotationSpeed;
		} else if (Input.GetKey (KeyCode.RightArrow)) {
			tilt -= rotationSpeed;
		} else {
			tilt *= 0.9f;
		}
		
		if (Mathf.Abs(tilt) > maxTilt) {
			tilt = Mathf.Sign (tilt) * maxTilt;
		}

		transform.Rotate(0, 0, tilt - transform.localEulerAngles.z);
	}
}


