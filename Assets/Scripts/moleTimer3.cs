using UnityEngine;
using System.Collections;

public class moleTimer3 : MonoBehaviour {
	
	public float myTimer = 1.0F;
	// Use this for initialization
	void Start () {
		gameObject.transform.localPosition = new Vector3(-0.02577019F,-0.3169861F,0.1735435F);
	
	}
	
	// Update is called once per frame
	void Update () {
		
 
		if(myTimer > 0){
		  myTimer -= Time.deltaTime;
		 }
		 if(myTimer <= 0){
		  gameObject.transform.localPosition = new Vector3(-0.02577019F,0.3110987F,0.1735435F);
		 }
 
	}
	
	void OnTriggerEnter(Collider collisionObject){
		//print ("HIT");
		if(collisionObject.gameObject.tag == "Mallet"){
			gameObject.transform.localPosition = new Vector3(-0.02577019F,-0.3169861F,0.1735435F);
			myTimer = Random.Range(1,5);
		}
	}
}
