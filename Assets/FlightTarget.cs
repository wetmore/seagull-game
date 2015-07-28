using UnityEngine;
using System.Collections;

public class FlightTarget : MonoBehaviour {

	void Start () {
		transform.position = player.position;
	}
	
	public Transform player;

	void MoveToPlayer(float f) {
		transform.position = Vector2.MoveTowards(transform.position, player.position, f);
	}

	private int counter = 0;
	public int timeoutLength = 30;

	public float moveSpeed = 5;
	// Update is called once per frame
	void FixedUpdate () {
		if (counter > 0) {
			counter--;
		}

		if (Input.GetKey (KeyCode.LeftArrow)) {
			GetComponent<Rigidbody2D> ().transform.Translate(-moveSpeed, 0, 0);
			counter = timeoutLength;
		}
		if (Input.GetKey (KeyCode.RightArrow)) {
			GetComponent<Rigidbody2D> ().transform.Translate(moveSpeed, 0, 0);
			counter = timeoutLength;

		}
		if (Input.GetKey (KeyCode.DownArrow)) {
			GetComponent<Rigidbody2D> ().transform.Translate(0, -moveSpeed/2, 0);
			counter = timeoutLength;

		}
		if (Input.GetKey (KeyCode.UpArrow)) {
			GetComponent<Rigidbody2D> ().transform.Translate(0, moveSpeed/2, 0);
			counter = timeoutLength;

		}

		Leash ();

		if (counter == 0) {
			MoveToPlayer(0.1f);
		}
	}

	public float leashDistance = 10;

	void Leash() {
		Vector2 delta = player.position - transform.position;

		print (delta);

		if (delta.magnitude > leashDistance) {
			transform.position = (Vector2) player.position + delta.normalized * -leashDistance;
		}
	}
}
