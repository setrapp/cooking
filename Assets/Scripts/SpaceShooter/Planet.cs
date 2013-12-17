using System;
using UnityEngine;

class Planet: BaseSpaceObject {

	public void Start() {
	}

	public void OnTriggerEnter2D(Collider2D col) {
		GameObject obj = col.gameObject;
		Bullet bullet = obj.GetComponent<Bullet>();
		if(bullet != null) {
			bullet.Died = true;
		}
	}
};
