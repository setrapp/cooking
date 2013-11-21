using UnityEngine;
using System.Collections;

public class moleTimer1 : MonoBehaviour {

	public float myTimer = 1.0F;
	// Use this for initialization
	void Start () {
		gameObject.transform.localPosition = new Vector3(0.09500027F,-0.3454213F,-0.2200003F);
	
	}
	
	// Update is called once per frame
	void Update () {
		
 
		if(myTimer > 0){
		  myTimer -= Time.deltaTime;
		 }
		 if(myTimer <= 0){
		  gameObject.transform.localPosition = new Vector3(0.09500027F,0.3110982F,-0.2200003F);
		 }
 
	}
	
	void OnTriggerEnter(Collider collisionObject){
		//print ("HIT");
		if(collisionObject.gameObject.tag == "Mallet"){
			gameObject.transform.localPosition = new Vector3(0.09500027F,-0.3454213F,-0.2200003F);
			myTimer = Random.Range(1,5);
		}
	}
}
