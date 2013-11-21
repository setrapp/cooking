using UnityEngine;
using System.Collections;

public class ColliderCheck: MonoBehaviour {
	public GameObject mover = null;
	void OnTriggerEnter(Collider collider) {
		//TODO make work????
		Debug.Log("hi");
		mover.SendMessage("HandleCollision");
	}
}
