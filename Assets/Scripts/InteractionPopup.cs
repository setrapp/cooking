using UnityEngine;
using System.Collections;

public class InteractionPopup : MonoBehaviour
{
	private GameObject player;	
	private bool popupOn = false;
	public GameObject popup = null;
	
	void Start () {
		player = GameObject.FindGameObjectWithTag("Playermesh");
	}
	
	void Update () {
		popupOn = false;
		if ((player.transform.position - transform.position).sqrMagnitude < Mathf.Pow(((SphereCollider)collider).radius,2)) {
			popupOn = true;	
			Vector3 newForward = (Camera.main.transform.position - popup.transform.position) * - 1;
			newForward.y = transform.forward.y;
			popup.transform.forward = newForward;
		}
		popup.SetActive(popupOn);
	}
}

