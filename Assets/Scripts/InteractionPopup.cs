using UnityEngine;
using System.Collections;

public class InteractionPopup : MonoBehaviour
{
	public string name;
	private GameObject player;	
	private bool popupOn = false;
	public GameObject popup = null;
	public CollisionChecker collisionChecker = null;
	
	void Start () {
		player = GameObject.FindGameObjectWithTag("Playermesh");
		if (collisionChecker == null) {
			collisionChecker = GetComponent<CollisionChecker>();	
		}
	}
	
	void Update () {
		popupOn = false;
		if (collisionChecker != null && (collisionChecker.Triggering || collisionChecker.Colliding)) {
			popupOn = true;	
			if (Camera.main != null) {
				Vector3 newForward = (Camera.main.transform.position - popup.transform.position) * - 1;
				newForward.y = transform.forward.y;
				popup.transform.forward = newForward;
			}
		}
		popup.SetActive(popupOn);
	}
	
	public void ForceOffAndDisable() {
		popup.SetActive(false);
		enabled = false;
	}
}

