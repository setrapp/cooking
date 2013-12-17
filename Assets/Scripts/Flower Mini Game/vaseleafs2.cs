using UnityEngine;
using System.Collections;

public class vaseleafs2 : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {

		if(transform.parent.gameObject.GetComponent<vaseFlower2>().isplaced)
		{
			gameObject.renderer.enabled = true;
		}
	
	}
}
