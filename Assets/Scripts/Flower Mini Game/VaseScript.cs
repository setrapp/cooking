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
		
		
	GUIManager.message = "Drag the lucky bamboo into the vase to make an arrangement.";	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
	
	void OnTriggerEnter (Collider col) {
	
		if(col.gameObject.name == "flowerOne"){
			print ("flower1");
			vaseflow1 = true;
			
			GUIManager.message = "Lovely!";	
			
		}
		
		if(col.gameObject.name == "flowerTwo"){
			print ("flower2");
			vaseflow2 = true;
			
			GUIManager.message = "So Chic!";	
			
		}
		
		if(col.gameObject.name == "flowerThree"){
			print ("flower3");
			vaseflow3 = true;
			
			GUIManager.message = "Martha Stewart would be proud!";	
		}
	}
}
