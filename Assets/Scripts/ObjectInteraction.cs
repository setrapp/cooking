using UnityEngine;
using System.Collections;

public class ObjectInteraction : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		if(GameObject.FindGameObjectWithTag("Playermesh").transform.position.x <= this.transform.position.x + 5 && 
			GameObject.FindGameObjectWithTag("Playermesh").transform.position.z <= this.transform.position.z + 5)
		{
			if (Input.GetKey (KeyCode.E)){
				if(GameObject.FindGameObjectWithTag("FloatingBox").renderer.enabled == false){
					GameObject.FindGameObjectWithTag("FloatingBox").renderer.enabled = true;
				}else{
					GameObject.FindGameObjectWithTag("FloatingBox").renderer.enabled = false;
				}
			}
		}
			
	}
}
