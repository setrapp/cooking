using System;
using UnityEngine;

class SatelliteShooter: MonoBehaviour {

	public void Start() {
		Vector3 pos = this.gameObject.transform.position;
		iTween.MoveTo(this.gameObject, iTween.Hash("position", new Vector3(pos.x + 5,
		                                                                   pos.y + 72,
		                                                                   pos.z),
		                                           "time", 20f));
	}

};
