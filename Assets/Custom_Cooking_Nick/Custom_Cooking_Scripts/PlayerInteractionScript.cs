using UnityEngine;
using System.Collections;

public class InteractionScript : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetKey (KeyCode.E))
			print ("E key is held down");
	}
}
