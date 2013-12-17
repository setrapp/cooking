using UnityEngine;
using System.Collections;

public class vaseFlower1 : MonoBehaviour {

	public bool isplaced;
	
	// Use this for initialization
	void Start () {
		isplaced = false;
		

	}
	
	// Update is called once per frame
	void Update () {
	
	if ((GameObject.Find("vaseCollider").GetComponent<VaseScript>().vaseflow2)){
			renderer.enabled = true;
			isplaced = true;
	}
}
}
