using UnityEngine;
using System.Collections;

public class vaseleafs : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {

		if(transform.parent.gameObject.GetComponent<vaseFlower>().isplaced)
		{
			gameObject.renderer.enabled = true;
		}

	
	}
}
