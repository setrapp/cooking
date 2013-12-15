using System;
using UnityEngine;

class Scroller: MonoBehaviour {

	public float ScrollSpeed;
	public float MaxDistance;

	public Camera ScrollCamera;
	public GameObject Player;

	public static Rect CameraRect;

	float _currentDistance;

	public void Start() {
		_currentDistance = 0.0f;
	}

	public void Update() {
		if(_currentDistance <= this.MaxDistance) {
			float delta = ScrollSpeed * Time.fixedDeltaTime;

			Vector3 pos = ScrollCamera.transform.position;
			ScrollCamera.transform.position = new Vector3(pos.x, pos.y + delta, pos.z);

			pos = Player.transform.position;
			Player.transform.position = new Vector3(pos.x, pos.y + delta, pos.z);
	
			_currentDistance += delta;
		}

		UpdateCameraRect(Camera.main);
	}

	public static void UpdateCameraRect(Camera camera) {
		Vector3 pos = camera.transform.position;
		float   orthoSize = camera.orthographicSize;
		CameraRect = new Rect(pos.x - orthoSize,
		                	  pos.y - orthoSize,
		                      orthoSize * 2,
		                      orthoSize * 2);
	}

};
