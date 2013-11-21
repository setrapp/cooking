using UnityEngine;
using System.Collections;

public class doorsound : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		
		if((GameObject.Find("Button").GetComponent<Decontamination>().opened)){
			gameObject.audio.Play();
		}
	
	}
}
