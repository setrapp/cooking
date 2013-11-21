using UnityEngine;
using System.Collections;

public class vaseFlower2: MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	if ((GameObject.Find("vaseCollider").GetComponent<VaseScript>().vaseflow3)){
			renderer.enabled = true;
	}
}
}
