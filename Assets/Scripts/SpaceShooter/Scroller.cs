using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

class Scroller: MonoBehaviour {

	public float ScrollSpeed;
	public float MaxDistance;

	public Camera ScrollCamera;
	public GameObject Player;
	public HUDLayer HudLayer;

	public List<GameObject> OtherScrollObjects = new List<GameObject>();

	public static Rect CameraRect;
	public static bool Paused;
	public static Scroller Instance;

	float _currentDistance;

	public void Start() {
		_currentDistance = 0.0f;
		Paused = false;

		Instance = this;

		if(HudLayer != null) {
			HudLayer.ShowScreenText("Move: Arrows\nShoot: Z\nHyper: C", 1.0f, 1.0f, 1.0f, TextAnchor.UpperLeft);
		}
	}

	public void LateUpdate() {
		if(Paused)
			return;

		if(_currentDistance <= this.MaxDistance) {
			float delta = ScrollSpeed * Time.deltaTime;

			Vector3 pos = ScrollCamera.transform.position;
			ScrollCamera.transform.position = new Vector3(pos.x, pos.y + delta, pos.z);

			pos = Player.transform.position;
			Player.transform.position = new Vector3(pos.x, pos.y + delta, pos.z);

			foreach(GameObject obj in this.OtherScrollObjects) {
				pos = obj.transform.position;
				obj.transform.position = new Vector3(pos.x, pos.y + delta, pos.z);
			}
	
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
