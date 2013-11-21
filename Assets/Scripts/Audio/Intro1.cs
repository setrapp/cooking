using UnityEngine;
using System.Collections;

public class Intro1 : MonoBehaviour {

	// Use this for initialization
	public bool alreadyplayed;
	
	void Start () {
	alreadyplayed = false;
	}
	
	// Update is called once per frame
	void Update () {
	
	}
	
	void OnTriggerEnter (){
	if(alreadyplayed == false){
		gameObject.audio.Play();
		alreadyplayed = true;	
		}
	}
}
