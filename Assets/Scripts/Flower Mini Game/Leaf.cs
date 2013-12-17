using UnityEngine;
using System.Collections;

public class Leaf : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {

		if(transform.parent.gameObject.GetComponent<Flowerscript>().placed)
		{
			gameObject.renderer.enabled = false;
		}
	
	}
}
