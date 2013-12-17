using UnityEngine;
using System.Collections;

public class HideInRelativistic : MonoBehaviour {
	public bool gotHidden = false;
	public GameObject toHide = null;
	private MovementScripts movement = null;
	
	// Use this for initialization
	void Start () {
		GameObject player = GameObject.FindGameObjectWithTag("Player");
		if (player != null) {
			movement = player.GetComponent<MovementScripts>();	
		}
	}
	
	// Update is called once per frame
	void Update () {
		if (movement.IsRelativistic) {
			toHide.SetActive(false);
		} else {
			toHide.SetActive(true);
		}
	}
}
