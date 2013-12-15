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
		}
		popup.SetActive(popupOn);
		Vector3 lookAtPos = new Vector3(player.transform.position.x, popup.transform.position.y, player.transform.position.z);
		popup.transform.LookAt(player.transform);
	}
}

