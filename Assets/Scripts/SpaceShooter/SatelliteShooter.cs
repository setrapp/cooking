using System;
using UnityEngine;

class SatelliteShooter: MonoBehaviour {

	public void Start() {
		Vector3 pos = this.gameObject.transform.position;
		iTween.MoveTo(this.gameObject, iTween.Hash("position", new Vector3(pos.x,
		                                                                   pos.y + 80,
		                                                                   pos.z),
		                                           "time", 30f));
	}

};
