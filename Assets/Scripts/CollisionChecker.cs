using UnityEngine;
using System.Collections;

public class CollisionChecker : MonoBehaviour
{
	private bool isColliding = false;
	public string targetTag = "PlayerTrigger";
	
	public bool	Colliding {
		get { return isColliding; }
	}
	private bool isTriggering= false;
	public bool	Triggering {
		get { return isTriggering; }
	}
	
	void OnCollisionEnter(Collision collision) {
		if (collision.collider.gameObject.tag.Equals(targetTag)) {
			isColliding = true;
		}
	}
	
	void OnCollisionStay(Collision collision) {
		if (collision.collider.gameObject.tag.Equals(targetTag)) {
			isColliding = true;
		}
	}
	
	void OnCollisionExit(Collision collision) {
		if (collision.collider.gameObject.tag.Equals(targetTag)) {
			isColliding = false;
		}
	}
	
	void OnTriggerEnter(Collider other) {
		if (other.gameObject.tag.Equals(targetTag)) {
			isTriggering = true;
		}
	}
	
	void OnTriggerStay(Collider other) {
		if (other.gameObject.tag.Equals(targetTag)) {
			isTriggering = true;
		}
	}
	
	void OnTriggerExit(Collider other) {
		if (other.gameObject.tag.Equals(targetTag)) {
			isTriggering = false;
		}
	}
}

