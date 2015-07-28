using UnityEngine;
using System.Collections;

public class MainCamera : MonoBehaviour {

	private int counter = 0;
	public Transform target;
	public float defaultZoomLevel = 50;

	void Awake () {
		QualitySettings.vSyncCount = 0;
		Application.targetFrameRate = 60;
	}

	void LateUpdate () {
		transform.position = new Vector3(target.position.x,
			                             target.position.y,
			                             transform.position.z);
		//counter++;
		//Camera.main.orthographicSize = defaultZoomLevel + counter;   
	}
}
