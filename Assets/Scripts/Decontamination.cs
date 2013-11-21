using UnityEngine;
using System.Collections;

public class Decontamination : MonoBehaviour {
	
	
	public bool opened;
	public bool alreadyplayed;
	public bool triggered;
	
	// Use this for initialization
	void Start () {
		
		opened = false;
		alreadyplayed = false;
		triggered = false;
	
	}
	
	// Update is called once per frame
	void Update () {
		
		if(Input.GetKey(KeyCode.B) && (triggered == true)) {
			
			
			
			if(alreadyplayed == false) {
				gameObject.audio.Play();
				alreadyplayed = true;
			}
			
			renderer.material.color = Color.green;
			
			StartCoroutine(doorwait());
			
		}
	
	}
	
	void OnTriggerEnter() {
		
			print("triggered");
		
			GUIManager.message = "Press Button 'B' for Decontamination";
			
			triggered = true;
			
		}
	
	IEnumerator doorwait (){
		
		yield return new WaitForSeconds(7.0f);
		print ("opened");
		opened = true;
	}
}
