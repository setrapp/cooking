using UnityEngine;
using System.Collections;

public class VaseScript : MonoBehaviour {
	
	public bool vaseflow1;
	public bool vaseflow2;
	public bool vaseflow3;

	// Use this for initialization
	void Start () {
	vaseflow1 = false;
	vaseflow2 = false;
	vaseflow3 = false;
	}
	
	// Update is called once per frame
	void Update () {
	
	}
	
	void OnTriggerEnter (Collider col) {
	
		if(col.gameObject.name == "flowerOne"){
			print ("flower1");
			vaseflow1 = true;
			
		}
	}
}
