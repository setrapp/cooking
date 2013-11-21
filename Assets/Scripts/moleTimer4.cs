using UnityEngine;
using System.Collections;

public class moleTimer4 : MonoBehaviour {

	public float myTimer = 1.0F;
	// Use this for initialization
	void Start () {
		gameObject.transform.localPosition = new Vector3(-0.1582375F,-0.3110987F,-0.04788971F);
	
	}
	
	// Update is called once per frame
	void Update () {
		
 
		if(myTimer > 0){
		  myTimer -= Time.deltaTime;
		 }
		 if(myTimer <= 0){
		  gameObject.transform.localPosition = new Vector3(-0.1582375F,0.3110987F,-0.04788971F);
		 }
 
	}
	
	void OnTriggerEnter(Collider collisionObject){
		//print ("HIT");
		if(collisionObject.gameObject.tag == "Mallet"){
			gameObject.transform.localPosition = new Vector3(-0.1582375F,-0.3110987F,-0.04788971F);
			myTimer = Random.Range(1,5);
		}
	}
}
