using System;
using UnityEngine;

class MovableObject: MonoBehaviour {

	public string MovePathName;
	public float  MoveTime = 5;
	public float  Delay = 0f;

	public void Start() {

	}

	public void StartMove() {
		iTween.MoveTo(this.gameObject, iTween.Hash("path", iTweenPath.GetPath(MovePathName),
		                                           "time", MoveTime,
		                                           "orienttopath", true,
		                                           "looktime", 1f,
		                                           "looptype", iTween.LoopType.pingPong,
		                                           "easetype", iTween.EaseType.linear,
		                                           "delay", Delay));
	}

	public void OnTriggerEnter(Collider collider) {
		Vector3 position = this.gameObject.transform.position;
		iTween.MoveTo(this.gameObject, iTween.Hash("position", new Vector3(position.x, position.y - 5f, position.z),
		                                           "time", 10f));
	}

};
