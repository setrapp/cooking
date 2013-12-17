using UnityEngine;
using System.Collections;

public class rotaterWeight : MonoBehaviour {
	bool hammerDown = false;
	float holdTimer = 0;
	Quaternion startRotation;
	
	// Use this for initialization
	void Start () {
		startRotation = transform.rotation;
	}
	
	// Update is called once per frame
	void Update () {
		if(Input.GetMouseButtonDown(0))
		{
			gameObject.transform.Rotate(90,0,0);
			holdTimer = Time.time + 0.2f;
			hammerDown = true;
		}else if((Input.GetMouseButtonUp(0) || holdTimer < Time.time) && hammerDown){
			gameObject.transform.Rotate(-90,0,0);
			hammerDown = false;
		}
	
	}
}
